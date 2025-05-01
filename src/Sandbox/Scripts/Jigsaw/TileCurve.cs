using System.Collections.Generic;
using Godot;

namespace Sandbox.Jigsaw;

public class TileCurve
{
    public CurveDirection Direction { get; set; }
    public CurveShape Shape { get; set; }
    public required Line2D Line2D { get; init; }

    public static readonly List<Vector2> BezCurve = BezierCurve.PointList2(TemplateBezierCurve.TemplateControlPoints, 0.001f);

    private static readonly Dictionary<CurveShape, Color> DefaultColors = new()
    {
        [CurveShape.Positive] = Colors.Red,
        [CurveShape.Negative] = Colors.Blue,
        [CurveShape.None] = Colors.White
    };

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

    public List<Vector2> GetPoints(Vector2I offset, Vector2I tileSize)
    {
        var (paddingX, paddingY) = offset;
        var (width, height) = tileSize;

        var points = new List<Vector2>(BezCurve);

        switch (Direction)
        {
            case CurveDirection.Up:
                HandleUp();
                break;
            case CurveDirection.Right:
                HandleRight();
                break;
            case CurveDirection.Down:
                HandleDown();
                break;
            case CurveDirection.Left:
                HandleLeft();
                break;
        }

        return points;

        void HandleUp()
        {
            var (startX, startY) = (paddingX, paddingY);
            if (Shape == CurveShape.Positive)
            {
                TranslatePoints(points, new Vector2(startX, startY));
            }
            else if (Shape == CurveShape.Negative)
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
            if (Shape == CurveShape.Positive)
            {
                InvertY(points);
                SwapXY(points);
                TranslatePoints(points, new Vector2(startX, startY));
            }
            else if (Shape == CurveShape.Negative)
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
            if (Shape == CurveShape.Positive)
            {
                InvertY(points);
                TranslatePoints(points, new Vector2(startX, startY));
            }
            else if (Shape == CurveShape.Negative)
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
            if (Shape == CurveShape.Positive)
            {
                SwapXY(points);
                TranslatePoints(points, new Vector2(startX, startY));
            }
            else if (Shape == CurveShape.Negative)
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

    public void Draw(Vector2I offset, Vector2I tileSize, float width = 1f)
    {
        Line2D.ClearPoints();
        Line2D.DefaultColor = DefaultColors[Shape];
        Line2D.Width = width;
        Line2D.Points = GetPoints(offset, tileSize).ToArray();
    }

    public enum CurveDirection
    {
        Up,
        Right,
        Down,
        Left
    }

    public enum CurveShape
    {
        Positive,
        Negative,
        None
    }
}