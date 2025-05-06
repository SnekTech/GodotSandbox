using System;
using Godot;
using GodotUtilities;

namespace Sandbox.Jigsaw;

[Scene]
public partial class JigsawBoard : Node2D
{
    private const string BackgroundImagePath = "res://Art/assets_to_dowmload/flower_12_8.jpg";
    private const float GhostBoardTransparency = 0.1f;

    [Export]
    private PackedScene jigsawTileScene = null!;

    [Node]
    private Sprite2D board = null!;

    [Node]
    private Node2D tilesContainer = null!;

    [Node]
    private Sprite2D border = null!;

    private Tile[,]? _tiles;

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public override void _Ready()
    {
        var boardImage = GD.Load<Image>(BackgroundImagePath);
        InitBorderSprite(boardImage, Vector2I.One * 10, Colors.White);
        LoadBoardTexture(boardImage);
        InitTiles(boardImage);

        ShowGhostBoard();
    }

    private void InitTiles(Image boardImage)
    {
        var (boardWidth, boardHeight) = boardImage.GetSize();
        if (boardHeight % Tile.Size != 0 || boardWidth % Tile.Size != 0)
        {
            throw new ArgumentException($"board image size must be multiple of {Tile.Size}");
        }

        var tileRowCount = boardHeight / Tile.Size;
        var tileColumnCount = boardWidth / Tile.Size;
        _tiles = new Tile[tileRowCount, tileColumnCount];

        for (var i = 0; i < tileRowCount; i++)
        {
            for (var j = 0; j < tileColumnCount; j++)
            {
                var tile = new Tile((i, j), boardImage);
                _tiles[i, j] = tile;
            }
        }

        RandomizeTileShape(_tiles);

        for (var i = 0; i < tileRowCount; i++)
        {
            for (var j = 0; j < tileColumnCount; j++)
            {
                var jigsawTile = jigsawTileScene.Instantiate<JigsawTile>();
                tilesContainer.AddChild(jigsawTile);
                jigsawTile.Init(_tiles[i, j]);
            }
        }

        return;

        void RandomizeTileShape(Tile[,] tiles)
        {
            var rowCount = tiles.GetLength(0);
            var columnCount = tiles.GetLength(1);

            // randomize the starting tile
            var topLeftTile = tiles[0, 0];
            topLeftTile.RandomizeCurveShapes();

            // randomize top row
            for (var j = 1; j < columnCount; j++)
            {
                var currentTile = tiles[0, j];
                var leftTile = tiles[0, j - 1];
                currentTile.RandomizeCurveShapes();

                var leftTileRightShape = leftTile.GetCurveShape(TileCurve.CurveDirection.Right);
                var currentLeftShape = leftTileRightShape == TileCurve.CurveShape.Positive
                    ? TileCurve.CurveShape.Negative
                    : TileCurve.CurveShape.Positive;
                currentTile.SetCurveShape(TileCurve.CurveDirection.Left, currentLeftShape);
            }

            // randomize left column
            for (var i = 1; i < rowCount; i++)
            {
                var currentTile = tiles[i, 0];
                var upTile = tiles[i - 1, 0];
                currentTile.RandomizeCurveShapes();

                var upTileDownShape = upTile.GetCurveShape(TileCurve.CurveDirection.Down);
                var currentUpShape = upTileDownShape == TileCurve.CurveShape.Positive
                    ? TileCurve.CurveShape.Negative
                    : TileCurve.CurveShape.Positive;
                currentTile.SetCurveShape(TileCurve.CurveDirection.Up, currentUpShape);
            }

            // randomize the rest
            for (var i = 1; i < rowCount; i++)
            {
                for (var j = 1; j < columnCount; j++)
                {
                    var currentTile = tiles[i, j];
                    var leftTile = tiles[i, j - 1];
                    var upTile = tiles[i - 1, j];
                    currentTile.RandomizeCurveShapes();

                    var leftTileRightShape = leftTile.GetCurveShape(TileCurve.CurveDirection.Right);
                    var currentLeftShape = leftTileRightShape == TileCurve.CurveShape.Positive
                        ? TileCurve.CurveShape.Negative
                        : TileCurve.CurveShape.Positive;
                    currentTile.SetCurveShape(TileCurve.CurveDirection.Left, currentLeftShape);

                    var upTileDownShape = upTile.GetCurveShape(TileCurve.CurveDirection.Down);
                    var currentUpShape = upTileDownShape == TileCurve.CurveShape.Positive
                        ? TileCurve.CurveShape.Negative
                        : TileCurve.CurveShape.Positive;
                    currentTile.SetCurveShape(TileCurve.CurveDirection.Up, currentUpShape);
                }
            }

            // finally, reset tile curves on 4 sides to straight
            // top & bottom row
            for (var j = 0; j < columnCount; j++)
            {
                tiles[0, j].SetCurveShape(TileCurve.CurveDirection.Up, TileCurve.CurveShape.None);
                tiles[rowCount - 1, j].SetCurveShape(TileCurve.CurveDirection.Down, TileCurve.CurveShape.None);
            }

            // left & right column
            for (var i = 0; i < rowCount; i++)
            {
                tiles[i, 0].SetCurveShape(TileCurve.CurveDirection.Left, TileCurve.CurveShape.None);
                tiles[i, columnCount - 1].SetCurveShape(TileCurve.CurveDirection.Right, TileCurve.CurveShape.None);
            }
        }
    }

    private void LoadBoardTexture(Image boardImage)
    {
        var boardTexture = new ImageTexture();
        boardTexture.SetImage(boardImage);
        board.Texture = boardTexture;
    }

    private void InitBorderSprite(Image contentImage, Vector2I size, Color color)
    {
        var (contentWidth, contentHeight) = contentImage.GetSize();
        var (borderWidth, borderHeight) = size;
        var backgroundBorderImage =
            Image.CreateEmpty(contentWidth + borderWidth * 2, contentHeight + borderHeight * 2, false,
                Image.Format.Rgba8);
        backgroundBorderImage.Fill(color);

        for (var x = borderWidth; x < borderWidth + contentWidth; x++)
        {
            for (var y = borderHeight; y < borderHeight + contentHeight; y++)
            {
                backgroundBorderImage.SetPixel(x, y, Colors.Transparent);
            }
        }

        var backgroundBorderTexture = new ImageTexture();
        backgroundBorderTexture.SetImage(backgroundBorderImage);
        border.Texture = backgroundBorderTexture;
        border.Offset = new Vector2(-borderWidth, -borderHeight);
    }

    private void ShowGhostBoard()
    {
        SetBoardAlpha(GhostBoardTransparency);
    }

    private void SetBoardAlpha(float alpha) => board.Modulate = border.Modulate with { A = alpha };
}