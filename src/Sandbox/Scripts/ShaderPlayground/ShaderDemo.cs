using GodotGadgets.Extensions;

namespace Sandbox.ShaderPlayground;

[SceneTree]
public partial class ShaderDemo : Node2D
{
    private Vector2 _picSize;
    private ShaderBookShader _shader = null!;

    public override void _Ready()
    {
        _shader = new ShaderBookShader(Pic.GetMaterialAs<ShaderMaterial>());
        
        _picSize = Pic.Texture.GetSize() * Pic.Scale;
        _shader.Resolution.Value = _picSize;
    }

    public override void _Process(double delta)
    {
        _shader.Mouse.Value = GetMousePositionInPic();
    }

    private Vector2 GetMousePositionInPic()
    {
        var (relativeX, relativeY) = GetGlobalMousePosition() - _Pic.GlobalPosition;
        var (width, height) = _picSize;

        relativeX = Mathf.Clamp(relativeX, 0, width) / width;
        relativeY = Mathf.Clamp(relativeY, 0, height) / height;

        return new Vector2(relativeX, relativeY);
    }
}