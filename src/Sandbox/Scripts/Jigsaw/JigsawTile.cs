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
    private Node2D spriteContainer = null!;

    private Tile _tile = null!;

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
        CreateTileSprite();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey inputEventKey)
        {
            if (inputEventKey.Keycode == Key.F)
            {
                _tile.RandomizeCurveShapes();
                _tile.DrawCurves();
            }
        }
    }

    private void InitTile()
    {
        var lines = new List<Line2D>();
        for (var i = 0; i < Tile.CurveCount; i++)
        {
            var curveLine = new Line2D();
            lines.Add(curveLine);
            curveLineContainer.AddChild(curveLine);
        }

        var tileImage = GD.Load<Image>(SunflowerPath);
        _tile = new Tile(lines, tileImage);
        _tile.RandomizeCurveShapes();
        _tile.DrawCurves();
    }

    private void CreateTileSprite()
    {
        var texture = _tile.GetJigsawTexture();
        var sprite = new Sprite2D();
        sprite.Centered = false;
        sprite.Texture = texture;
        spriteContainer.AddChild(sprite);
    }
}