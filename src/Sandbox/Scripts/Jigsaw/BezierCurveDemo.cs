using System.Collections.Generic;
using System.Linq;
using Godot;
using GodotUtilities;

namespace Sandbox.Jigsaw;

[Scene]
public partial class BezierCurveDemo : Node
{
    [Node]
    private Line2D controlPointLines = null!;

    [Node]
    private Line2D curve = null!;

    [Node]
    private Node2D controlPointContainer = null!;

    private List<Vector2> ControlPoints
    {
        get
        {
            var points = new List<Vector2>();
            foreach (var child in controlPointContainer.GetChildren())
            {
                if (child is Node2D node2D)
                {
                    points.Add(node2D.GlobalPosition);
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
}