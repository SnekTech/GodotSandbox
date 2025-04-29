using Godot;
using GodotUtilities;

namespace Sandbox.Jigsaw;

[Scene]
public partial class JigsawTile : Node2D
{
    [Node]
    private Node2D curveLineContainer = null!;
    private readonly Tile _tile = new();

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }
    
    public override void _Ready()
    {
        var curveLines = _tile.DrawCurves(Colors.Red);
        foreach (var curveLine in curveLines)
        {
            curveLineContainer.AddChild(curveLine);
        }
    }
}