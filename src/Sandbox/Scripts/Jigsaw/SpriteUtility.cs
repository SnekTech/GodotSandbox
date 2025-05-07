
using Godot;

namespace Sandbox.Jigsaw;

public static class SpriteUtility
{
    public static Image GetPaddedImage(Image baseImage, (int x, int y) padding, Color borderColor)
    {
        var (baseWidth, baseHeight) = baseImage.GetSize();
        var (paddingX, paddingY) = padding;
        var width = baseWidth + paddingX * 2;
        var height = baseHeight + paddingY * 2;
        var paddedImage = Image.CreateEmpty(width, height, false, Image.Format.Rgba8);
        paddedImage.Fill(borderColor);

        for (var x = 0; x < baseWidth; x++)
        {
            for (var y = 0; y < baseHeight; y++)
            {
                var baseColor = baseImage.GetPixel(x, y);
                paddedImage.SetPixel(x+paddingX, y + paddingY, baseColor);
            }
        }

        return paddedImage;
    }
}