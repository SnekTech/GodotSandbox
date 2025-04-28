using Godot;
using GodotUtilities;

namespace Sandbox.Jigsaw;

[Scene]
public partial class ControlPoint : Node2D
{
    [Node]
    private Area2D area2D = null!;

    private Vector2 _offset = Vector2.Zero;
    private bool _isDragging;

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public override void _Ready()
    {
        area2D.InputEvent += OnArea2DInputEvent;
    }

    public override void _Process(double delta)
    {
        if (_isDragging)
            GlobalPosition = GetGlobalMousePosition() - _offset;
    }

    private void OnArea2DInputEvent(Node viewport, InputEvent @event, long shapeIdx)
    {
        if (@event is InputEventMouse eventMouse)
        {
            if (eventMouse is InputEventMouseButton eventMouseButton)
            {
                if (eventMouseButton.IsPressed())
                {
                    _isDragging = true;
                    _offset = GetLocalMousePosition();
                }
                else if (eventMouseButton.IsReleased())
                {
                    _isDragging = false;
                    _offset = Vector2.Zero;
                }
            }
        }
    }
}