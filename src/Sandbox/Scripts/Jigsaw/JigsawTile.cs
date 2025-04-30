using System.Collections.Generic;
using Godot;
using GodotUtilities;

namespace Sandbox.Jigsaw;

[Scene]
public partial class JigsawTile : Node2D
{
    [Node]
    private Node2D curveLineContainer = null!;
    
    private Tile _tile = null!;

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }
    
    public override void _Ready()
    {
        var lines = new List<Line2D>();
        for (var i = 0; i < Tile.CurveCount; i++)
        {
            var curveLine = new Line2D();
            lines.Add(curveLine);
            curveLineContainer.AddChild(curveLine);
        }

        _tile = new Tile(lines);
        _tile.RandomizeCurveShapes();
        _tile.DrawCurves();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey inputEventKey)
        {
            if (inputEventKey.Keycode == Key.F)
            {
                _tile.RandomizeCurveShapes();
                _tile.DrawCurves();
            }
        }
    }
}