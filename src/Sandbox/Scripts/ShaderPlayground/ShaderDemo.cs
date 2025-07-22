using GodotGadgets.Extensions;

namespace Sandbox.ShaderPlayground;

[SceneTree]
public partial class ShaderDemo : Node2D
{
    private static readonly StringName UniformMouse = "u_mouse";
    private static readonly StringName UniformResolution = "u_resolution";
    
    private Vector2 _picSize;
    private ShaderMaterial _shader = null!;

    public override void _Ready()
    {
        _picSize = Pic.Texture.GetSize() * Pic.Scale;
        Scale.DumpGd();
        _shader = (ShaderMaterial)Pic.Material;
        _shader.SetShaderParameter(UniformResolution, _picSize);
    }

    public override void _Process(double delta)
    {
        _shader.SetShaderParameter(UniformMouse, GetMousePositionInPic());
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