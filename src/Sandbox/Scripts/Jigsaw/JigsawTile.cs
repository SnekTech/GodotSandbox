using System.Collections.Generic;
using Godot;
using GodotUtilities;

namespace Sandbox.Jigsaw;

[Scene]
public partial class JigsawTile : Node2D
{
    private const string SunflowerPath = "res://Art/assets_to_dowmload/sunflower_140.jpg";

    [Node]
    private Node2D curveLineContainer = null!;

    [Node]
    private Sprite2D tileSprite = null!;

    private Tile _tile = null!;

    private readonly List<Line2D> _lines = [];

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public override void _Ready()
    {
        InitTile();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey inputEventKey)
        {
            if (inputEventKey.Keycode == Key.F)
            {
                _tile.RandomizeCurveShapes();
                UpdateTileSprite();
            }
        }
    }

    private void InitTile()
    {
        for (var i = 0; i < Tile.CurveCount; i++)
        {
            var curveLine = new Line2D();
            _lines.Add(curveLine);
            curveLineContainer.AddChild(curveLine);
        }

        var tileImage = GD.Load<Image>(SunflowerPath);
        _tile = new Tile(tileImage);
        _tile.RandomizeCurveShapes();
        UpdateTileSprite();
    }

    private void UpdateTileSprite()
    {
        _tile.DrawCurves(_lines);
        var texture = _tile.GetJigsawTexture();
        tileSprite.Texture = texture;
    }
}