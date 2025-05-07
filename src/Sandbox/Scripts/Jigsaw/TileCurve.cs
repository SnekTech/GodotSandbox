using System.Collections.Generic;
using Godot;

namespace Sandbox.Jigsaw;

public class TileCurve
{
    public CurveDirection Direction { get; init; }
    public CurveShape Shape { get; set; }

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

    private static void DiagonalSymmetry(List<Vector2> points)
    {
        for (var i = 0; i < points.Count; i++)
        {
            var (x, y) = points[i];
            points[i] = new Vector2(y, x);
        }
    }

    public List<Vector2> GetPoints((int width, int height) tileSize)
    {
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
            var (startX, startY) = (0, 0);
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
            var (startX, startY) = (width, 0);
            if (Shape == CurveShape.Positive)
            {
                InvertY(points);
                DiagonalSymmetry(points);
                TranslatePoints(points, new Vector2(startX, startY));
            }
            else if (Shape == CurveShape.Negative)
            {
                DiagonalSymmetry(points);
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
            var (startX, startY) = (0, 0 + height);
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
            var (startX, startY) = (0, 0);
            if (Shape == CurveShape.Positive)
            {
                DiagonalSymmetry(points);
                TranslatePoints(points, new Vector2(startX, startY));
            }
            else if (Shape == CurveShape.Negative)
            {
                InvertY(points);
                DiagonalSymmetry(points);
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

    public void Draw(Line2D line2D, (int width, int height) tileSize, float width = 1f)
    {
        line2D.ClearPoints();
        line2D.DefaultColor = DefaultColors[Shape];
        line2D.Width = width;
        line2D.Points = GetPoints(tileSize).ToArray();
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