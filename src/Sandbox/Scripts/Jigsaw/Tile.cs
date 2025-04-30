using System.Collections.Generic;
using Godot;

namespace Sandbox.Jigsaw;

public class Tile
{
    public const int CurveCount = 4;

    public Tile(List<Line2D> lines)
    {
        var curveUp = new TileCurve { Direction = TileCurve.CurveDirection.Up, Line2D = lines[0] };
        var curveRight = new TileCurve { Direction = TileCurve.CurveDirection.Right, Line2D = lines[1] };
        var curveDown = new TileCurve { Direction = TileCurve.CurveDirection.Down, Line2D = lines[2] };
        var curveLeft = new TileCurve { Direction = TileCurve.CurveDirection.Left, Line2D = lines[3] };
        _curves = [curveUp, curveRight, curveDown, curveLeft];
    }

    public Vector2I Offset { get; set; } = new(20, 20);
    public Vector2I Size { get; set; } = new(100, 100);

    private readonly List<TileCurve> _curves;

    public void DrawCurves()
    {
        foreach (var tileCurve in _curves)
        {
            tileCurve.Draw(Offset, Size);
        }
    }

    public void RandomizeCurveShapes()
    {
        foreach (var tileCurve in _curves)
        {
            tileCurve.Shape = GetRandomCurveShape();
        }
    }

    private TileCurve.CurveShape GetRandomCurveShape()
    {
        var rand = GD.Randf();
        return rand > 0.5 ? TileCurve.CurveShape.Positive : TileCurve.CurveShape.Negative;
    }
}