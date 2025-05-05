using System;
using System.Collections.Generic;
using Godot;

namespace Sandbox.Jigsaw;

public class Tile
{
    public const int CurveCount = 4;
    public const int Size = 100;

    public Tile((int x, int y) coordinate, Image boardImage)
    {
        Coordinate = coordinate;
        var curveUp = new TileCurve { Direction = TileCurve.CurveDirection.Up };
        var curveRight = new TileCurve { Direction = TileCurve.CurveDirection.Right };
        var curveDown = new TileCurve { Direction = TileCurve.CurveDirection.Down };
        var curveLeft = new TileCurve { Direction = TileCurve.CurveDirection.Left };
        _curves = [curveUp, curveRight, curveDown, curveLeft];

        var (boardImageWidth, boardImageHeight) = boardImage.GetSize();
        _boardImage = Image.CreateEmpty(boardImageWidth, boardImageHeight, false, Image.Format.Rgba8);
        _boardImage.CopyFrom(boardImage);
        _boardImage.Convert(Image.Format.Rgba8);

        _image = Image.CreateEmpty(Size, Size, false, Image.Format.Rgba8);
    }

    private readonly Image _boardImage;
    private readonly Image _image;
    private readonly List<TileCurve> _curves;

    public (int x, int y) Coordinate { get; }
    public Vector2I PositionInBoard => new(Coordinate.x * Size, Coordinate.y * Size);

    public void DrawCurves(List<Line2D> lines)
    {
        if (lines.Count != CurveCount)
            throw new ArgumentException(
                $"lines should have exact {CurveCount} lines, buw now we have {lines.Count} lines");

        for (var i = 0; i < lines.Count; i++)
        {
            var curve = _curves[i];
            curve.Draw(lines[i], Size);
        }
    }

    public void RandomizeCurveShapes()
    {
        foreach (var tileCurve in _curves)
        {
            tileCurve.Shape = GetRandomCurveShape();
        }
    }

    private List<Vector2> GetCurvePoints()
    {
        var points = new List<Vector2>();
        foreach (var tileCurve in _curves)
        {
            points.AddRange(tileCurve.GetPoints(Size));
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
        var color = _boardImage.GetPixel(positionX + x, positionY + y);
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