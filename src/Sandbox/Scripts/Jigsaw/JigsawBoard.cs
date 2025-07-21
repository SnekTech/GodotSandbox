using System;
using System.Threading.Tasks;
using Godot;
using GodotGadgets.Extensions;
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
        var paddedBoardImage = SpriteUtility.GetPaddedImage(boardImage, Tile.Padding, Colors.Gray);
        LoadBoardTexture(paddedBoardImage);
        InitTilesAsync(paddedBoardImage).Fire();

        ShowGhostBoard();
    }

    private async Task InitTilesAsync(Image paddedBoardImage)
    {
        var (boardWidth, boardHeight) = paddedBoardImage.GetSize();
        var (tileWidth, tileHeight) = Tile.Size;
        var (paddingX, paddingY) = Tile.Padding;
        
        var (baseWidth, baseHeight) = (boardWidth - paddingX * 2, boardHeight - paddingY * 2);
        if (baseWidth % tileWidth != 0 || baseHeight % tileHeight != 0)
        {
            throw new ArgumentException($"board image size must be multiple of {Tile.Size}");
        }

        var tileRowCount = baseHeight / tileHeight;
        var tileColumnCount = baseWidth / tileWidth;
        _tiles = new Tile[tileRowCount, tileColumnCount];

        for (var i = 0; i < tileRowCount; i++)
        {
            for (var j = 0; j < tileColumnCount; j++)
            {
                var tile = new Tile((i, j), paddedBoardImage);
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
                await Task.Delay(1);
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

    private void ShowGhostBoard()
    {
        SetBoardAlpha(GhostBoardTransparency);
    }

    private void SetBoardAlpha(float alpha) => board.Modulate = board.Modulate with { A = alpha };
}