using Godot;
using GodotUtilities;
using Sandbox.DragAndDrop.StateMachine;
using Sandbox.DragAndDrop.StateMachine.States;

namespace Sandbox.DragAndDrop;

public partial class Draggable : Node2D
{
    [Export] private Area2D dragArea = null!;

    private DraggableStateMachine _stateMachine = null!;
    
    private Vector2? _dragOffset;

    public override void _Ready()
    {
        _stateMachine = new DraggableStateMachine(this);
        _stateMachine.SetInitState<Idle>();
        dragArea.InputEvent += OnDragAreaInputEvent;
    }

    public void CaptureDragStartPosition()
    {
        _dragOffset = GetGlobalMousePosition() - GlobalPosition;
    }

    public void DragToMouse(Vector2 mousePosition)
    {
        if (!_dragOffset.HasValue) return;
        
        GlobalPosition = mousePosition - _dragOffset.Value;
    }
    
    private void OnDragAreaInputEvent(Node viewport, InputEvent @event, long idx)
    {
        if (@event is not InputEventMouse eventMouse) return;

        _stateMachine.OnMouseEvent(eventMouse);
    }
}