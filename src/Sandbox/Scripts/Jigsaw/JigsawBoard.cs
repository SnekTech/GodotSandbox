using Godot;
using GodotUtilities;

namespace Sandbox.Jigsaw;

[Scene]
public partial class JigsawBoard : Node2D
{
    private const string BackgroundImagePath = "res://Art/assets_to_dowmload/flower_12_8.jpg";

    [Node]
    private Sprite2D boardSprite = null!;

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public override void _Ready()
    {
        var texture = LoadBoardImageWithBorder(BackgroundImagePath, Vector2I.One * 10);
        boardSprite.Texture = texture;
    }


    private Texture2D LoadBoardImageWithBorder(string imagePath, Vector2I borderSize)
    {
        var originalImage = GD.Load<Image>(imagePath);
        var paddedImage = GetImageWithBorder(originalImage, borderSize);

        var tex = new ImageTexture();
        tex.SetImage(paddedImage);
        return tex;
    }

    private Image GetImageWithBorder(Image contentImage, Vector2I borderSize)
    {
        var (borderX, borderY) = borderSize;
        var (width, height) = contentImage.GetSize();
        var paddedWidth = width + borderX * 2;
        var paddedHeight = height + borderY * 2;
        var paddedImage = Image.CreateEmpty(paddedWidth, paddedHeight, false, Image.Format.Rgba8);

        paddedImage.Fill(Colors.White);

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var color = contentImage.GetPixel(x, y);
                paddedImage.SetPixel(x + borderX, y + borderY, color);
            }
        }

        return paddedImage;
    }
}