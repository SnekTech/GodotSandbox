using System.Collections.Generic;
using System.Linq;
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

    private List<Line2D> Lines => curveLineContainer.GetChildren<Line2D>().ToList();

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public void Init(Tile tile)
    {
        _tile = tile;
        UpdateTileSprite();
        tile.RandomizeCurveShapes();
    }

    private void UpdateTileSprite()
    {
        _tile.DrawCurves(Lines);
        var texture = _tile.GetJigsawTexture();
        tileSprite.Texture = texture;
    }
}