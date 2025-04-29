using System.Collections.Generic;
using Godot;

namespace Sandbox.Jigsaw;

public class Tile
{
    public Vector2I Offset { get; set; } = new(20, 20);
    public Vector2 Size { get; set; } = new(100, 100);

    public List<TileCurve> Curves =
    [
        new(TileCurve.Direction.Up, TileCurve.Shape.Positive),
        new(TileCurve.Direction.Right, TileCurve.Shape.Negative),
        new(TileCurve.Direction.Down, TileCurve.Shape.None),
        new(TileCurve.Direction.Left, TileCurve.Shape.Positive),
        
    ];

    public List<Line2D> DrawCurves(Color color, float width = 1f)
    {
        var curveLines = new List<Line2D>();
        foreach (var curve in Curves)
        {
            var line = new Line2D();
            line.DefaultColor = color;
            line.Width = width;
            line.Points = curve.GetPoints(Offset, Size).ToArray();
            curveLines.Add(line);
        }

        return curveLines;
    }
}

public class TileCurve(TileCurve.Direction direction, TileCurve.Shape shape)
{
    public static readonly List<Vector2> BezCurve = BezierCurve.PointList2(TemplateBezierCurve.TemplateControlPoints);

    private static void TranslatePoints(List<Vector2> points, Vector2 offset)
    {
        for (var i = 0; i < points.Count; i++)
        {
            points[i] += offset;
        }
    }

    private static void InvertY(List<Vector2> points)
    {
        for (var i = 0; i < points.Count; i++)
        {
            var (x, y) = points[i];
            points[i] = new Vector2(x, -y);
        }
    }

    private static void SwapXY(List<Vector2> points)
    {
        for (var i = 0; i < points.Count; i++)
        {
            var (x, y) = points[i];
            points[i] = new Vector2(y, x);
        }
    }

    public List<Vector2> GetPoints(Vector2 offset, Vector2 tileSize)
    {
        var (paddingX, paddingY) = offset;
        var (width, height) = tileSize;

        var points = new List<Vector2>(BezCurve);

        switch (direction)
        {
            case Direction.Up:
                HandleUp();
                break;
            case Direction.Right:
                HandleRight();
                break;
            case Direction.Down:
                HandleDown();
                break;
            case Direction.Left:
                HandleLeft();
                break;
        }

        return points;

        void HandleUp()
        {
            var (startX, startY) = (paddingX, paddingY);
            if (shape == Shape.Positive)
            {
                TranslatePoints(points, new Vector2(startX, startY));
            }
            else if (shape == Shape.Negative)
            {
                InvertY(points);
                TranslatePoints(points, new Vector2(startX, startY));
            }
            else
            {
                points.Clear();
                for (var i = 0; i < width; i++)
                {
                    points.Add(new Vector2(startX + i, startY));
                }
            }
        }

        void HandleRight()
        {
            var (startX, startY) = (paddingX + width, paddingY);
            if (shape == Shape.Positive)
            {
                InvertY(points);
                SwapXY(points);
                TranslatePoints(points, new Vector2(startX, startY));
            }
            else if (shape == Shape.Negative)
            {
                SwapXY(points);
                TranslatePoints(points, new Vector2(startX, startY));
            }
            else
            {
                points.Clear();
                for (var i = 0; i < height; i++)
                {
                    points.Add(new Vector2(startX, startY + i));
                }
            }
        }

        void HandleDown()
        {
            var (startX, startY) = (paddingX, paddingY + height);
            if (shape == Shape.Positive)
            {
                InvertY(points);
                TranslatePoints(points, new Vector2(startX, startY));
            }
            else if (shape == Shape.Negative)
            {
                TranslatePoints(points, new Vector2(startX, startY));
            }
            else
            {
                points.Clear();
                for (var i = 0; i < width; i++)
                {
                    points.Add(new Vector2(startX + i, startY));
                }
            }
        }

        void HandleLeft()
        {
            var (startX, startY) = (paddingX, paddingY);
            if (shape == Shape.Positive)
            {
                SwapXY(points);
                TranslatePoints(points, new Vector2(startX, startY));
            }
            else if (shape == Shape.Negative)
            {
                InvertY(points);
                SwapXY(points);
                TranslatePoints(points, new Vector2(startX, startY));
            }
            else
            {
                points.Clear();
                for (var i = 0; i < height; i++)
                {
                    points.Add(new Vector2(startX, startY + i));
                }
            }
        }
    }

    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public enum Shape
    {
        Positive,
        Negative,
        None
    }
}