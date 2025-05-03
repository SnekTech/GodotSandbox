using Godot;
using GodotUtilities;

namespace Sandbox.Jigsaw;

[Scene]
public partial class JigsawBoard : Node2D
{
    private const string BackgroundImagePath = "res://Art/assets_to_dowmload/flower_12_8.jpg";
    private const float GhostBoardTransparency = 0.1f;

    [Node]
    private Sprite2D board = null!;

    [Node]
    private Sprite2D border = null!;

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
        InitBorderBackground(boardImage, Vector2I.One * 10, Colors.White);
        LoadBoardTexture(boardImage);

        ShowGhostBoard();
    }

    private void LoadBoardTexture(Image boardImage)
    {
        var boardTexture = new ImageTexture();
        boardTexture.SetImage(boardImage);
        board.Texture = boardTexture;
    }

    private void InitBorderBackground(Image contentImage, Vector2I size, Color color)
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