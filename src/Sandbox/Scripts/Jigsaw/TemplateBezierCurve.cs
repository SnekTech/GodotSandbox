using System.Collections.Generic;
using System.Linq;
using Godot;
using GodotUtilities;

namespace Sandbox.Jigsaw;

[Scene]
public partial class TemplateBezierCurve : Node2D
{
    [Export]
    private PackedScene controlPointScene = null!;

    [Node]
    private Line2D controlPointLines = null!;

    [Node]
    private Line2D curve = null!;

    [Node]
    private Node2D controlPointContainer = null!;

    public static readonly List<Vector2> TemplateControlPoints =
    [
        new(0, 0),
        new(35, 15),
        new(47, 13),
        new(45, 5),
        new(48, 0),
        new(25, -5),
        new(15, -18),
        new(36, -20),
        new(64, -20),
        new(85, -18),
        new(75, -5),
        new(52, 0),
        new(55, 5),
        new(53, 13),
        new(65, 15),
        new(100, 0)
    ];

    private List<Vector2> ControlPoints
    {
        get
        {
            var points = new List<Vector2>();
            foreach (var child in controlPointContainer.GetChildren())
            {
                if (child is ControlPoint controlPoint)
                {
                    points.Add(controlPoint.GlobalPosition);
                }
            }

            return points;
        }
    }

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public override void _Ready()
    {
        foreach (var templatePosition in TemplateControlPoints)
        {
            SpawnControlPointAt(controlPointContainer.GlobalPosition + templatePosition);
        }
    }

    public override void _Process(double delta)
    {
        DrawControlLines();
        DrawBezierCurve();
    }

    private void DrawControlLines()
    {
        controlPointLines.ClearPoints();
        foreach (var controlPoint in ControlPoints)
        {
            controlPointLines.AddPoint(controlPoint - controlPointLines.GlobalPosition);
        }
    }

    private void DrawBezierCurve()
    {
        curve.ClearPoints();
        var curvePoints = BezierCurve.PointList2(controlPointLines.Points.ToList());
        foreach (var point in curvePoints)
        {
            curve.AddPoint(point);
        }
    }

    private void SpawnControlPointAt(Vector2 globalPosition)
    {
        var newControlPoint = controlPointScene.Instantiate<ControlPoint>();
        newControlPoint.GlobalPosition = globalPosition;
        controlPointContainer.AddChild(newControlPoint);
    }
}