using GodotGadgets.Extensions;

namespace Sandbox.ShaderPlayground;

[SceneTree]
public partial class ShaderDemo : Node2D
{
    private Vector2 _canvasSize;
    private ShaderBookShader _shader = null!;

    public override void _Ready()
    {
        _shader = new ShaderBookShader(Canvas.GetMaterialAs<ShaderMaterial>());
        
        _canvasSize = Canvas.Texture.GetSize() * Canvas.Scale;
        _shader.Resolution.Value = _canvasSize;
    }

    public override void _Process(double delta)
    {
        _shader.Mouse.Value = GetMousePositionInPic();
    }

    private Vector2 GetMousePositionInPic()
    {
        var (relativeX, relativeY) = GetGlobalMousePosition() - Canvas.GlobalPosition;
        var (width, height) = _canvasSize;

        relativeX = Mathf.Clamp(relativeX, 0, width) / width;
        relativeY = Mathf.Clamp(relativeY, 0, height) / height;

        return new Vector2(relativeX, relativeY);
    }
}