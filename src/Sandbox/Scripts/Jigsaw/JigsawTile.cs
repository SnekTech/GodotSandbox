using System.Collections.Generic;
using Godot;
using GodotUtilities;

namespace Sandbox.Jigsaw;

[Scene]
public partial class JigsawTile : Node2D
{
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
        foreach (var line in curveLineContainer.GetChildren<Line2D>())
        {
            _lines.Add(line);
        }
    }

    public void Init(Tile tile)
    {
        _tile = tile;
        Position = tile.PositionInBoard;
        UpdateTileSprite();
        GD.Print($"jigsaw tile initialized at {tile.Coordinate}");
    }

    private void UpdateTileSprite()
    {
        _tile.DrawCurves(_lines);
        var texture = _tile.GetJigsawTexture();
        tileSprite.Texture = texture;
    }
}