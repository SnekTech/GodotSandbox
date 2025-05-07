using System;
using System.Collections.Generic;
using Godot;

namespace Sandbox.Jigsaw;

public class Tile
{
    public const int CurveCount = 4;
    public static readonly Vector2I Size = new(100, 100);
    public static readonly Vector2I Padding = new(20, 20);

    public Tile((int i, int j) coordinate, Image paddedBoardImage)
    {
        Coordinate = coordinate;
        var curveUp = new TileCurve { Direction = TileCurve.CurveDirection.Up };
        var curveRight = new TileCurve { Direction = TileCurve.CurveDirection.Right };
        var curveDown = new TileCurve { Direction = TileCurve.CurveDirection.Down };
        var curveLeft = new TileCurve { Direction = TileCurve.CurveDirection.Left };
        _curvesByDirection = new Dictionary<TileCurve.CurveDirection, TileCurve>
        {
            [TileCurve.CurveDirection.Up] = curveUp,
            [TileCurve.CurveDirection.Right] = curveRight,
            [TileCurve.CurveDirection.Down] = curveDown,
            [TileCurve.CurveDirection.Left] = curveLeft
        };

        var (boardImageWidth, boardImageHeight) = paddedBoardImage.GetSize();
        _paddedBoardImage = Image.CreateEmpty(boardImageWidth, boardImageHeight, false, Image.Format.Rgba8);
        _paddedBoardImage.CopyFrom(paddedBoardImage);
        _paddedBoardImage.Convert(Image.Format.Rgba8);

        var imageWidth = Padding.X * 2 + Size.X;
        var imageHeight = Padding.Y * 2 + Size.Y;
        _image = Image.CreateEmpty(imageWidth, imageHeight, false, Image.Format.Rgba8);
    }

    private readonly Image _paddedBoardImage;
    private readonly Image _image;
    private readonly Dictionary<TileCurve.CurveDirection, TileCurve> _curvesByDirection;

    private IEnumerable<TileCurve> Curves => _curvesByDirection.Values;

    public (int i, int j) Coordinate { get; }

    public Vector2I PositionInBoard =>
        new(Coordinate.j * Size.X, Coordinate.i * Size.Y);


    public void DrawCurves(List<Line2D> lines)
    {
        if (lines.Count != CurveCount)
            throw new ArgumentException(
                $"lines should have exact {CurveCount} lines, buw now we have {lines.Count} lines");

        var i = 0;
        foreach (var tileCurve in Curves)
        {
            tileCurve.Draw(lines[i], Size, Padding);
            i++;
        }
    }

    public void SetCurveShape(TileCurve.CurveDirection direction, TileCurve.CurveShape shape)
    {
        _curvesByDirection[direction].Shape = shape;
    }

    public TileCurve.CurveShape GetCurveShape(TileCurve.CurveDirection direction)
    {
        return _curvesByDirection[direction].Shape;
    }

    public void RandomizeCurveShapes()
    {
        foreach (var tileCurve in Curves)
        {
            tileCurve.Shape = GetRandomCurveShape();
        }
    }

    private List<Vector2> GetCurvePoints()
    {
        var points = new List<Vector2>();
        foreach (var tileCurve in Curves)
        {
            points.AddRange(tileCurve.GetPoints(Size, Padding));
        }

        return points;
    }

    public Texture2D GetJigsawTexture()
    {
        _image.Fill(Colors.Transparent);
        FloodFill();

        var imageTexture = new ImageTexture();
        imageTexture.SetImage(_image);

        return imageTexture;
    }

    private void FillPixel(int x, int y)
    {
        var (positionX, positionY) = PositionInBoard;
        var color = _paddedBoardImage.GetPixel(positionX + x, positionY + y);
        _image.SetPixel(x, y, color);
    }

    private void FloodFill()
    {
        var (sizeX, sizeY) = _image.GetSize();

        var stack = new Stack<Vector2I>();
        var visitedPixels = new HashSet<Vector2I>();
        var curvePoints = GetCurvePoints();
        foreach (var curvePoint in curvePoints)
        {
            visitedPixels.Add((Vector2I)curvePoint);
        }

        var startPixel = new Vector2I(sizeX / 2, sizeY / 2);
        stack.Push(startPixel);

        while (stack.Count > 0)
        {
            var pixel = stack.Pop();
            var (x, y) = pixel;

            FillPixel(x, y);
            visitedPixels.Add(pixel);

            var upPixel = new Vector2I(x, y - 1);
            var rightPixel = new Vector2I(x + 1, y);
            var downPixel = new Vector2I(x, y + 1);
            var leftPixel = new Vector2I(x - 1, y);

            if (upPixel.Y >= 0 && visitedPixels.Contains(upPixel) == false)
            {
                stack.Push(upPixel);
            }

            if (rightPixel.X < sizeX && visitedPixels.Contains(rightPixel) == false)
            {
                stack.Push(rightPixel);
            }

            if (downPixel.Y < sizeY && visitedPixels.Contains(downPixel) == false)
            {
                stack.Push(downPixel);
            }

            if (leftPixel.X >= 0 && visitedPixels.Contains(leftPixel) == false)
            {
                stack.Push(leftPixel);
            }
        }
    }

    private static TileCurve.CurveShape GetRandomCurveShape()
    {
        var rand = GD.Randf();
        return rand > 0.5 ? TileCurve.CurveShape.Positive : TileCurve.CurveShape.Negative;
    }
}