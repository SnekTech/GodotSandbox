namespace Sandbox.Tutorial;

[SceneTree]
public partial class FocusMaskLayer : CanvasLayer
{
    private const int DefaultPadding = 10;
    private ShaderMaterial _focusMaskShader = null!;
    private Rect2 _focusRect;
    public Rect2 FocusRect
    {
        get => _focusRect;
        set
        {
            _focusRect = value;
            _focusMaskShader.SetShaderParameter(Uniforms.FocusRect, _focusRect.Grow(DefaultPadding));
        }
    }

    public override void _Ready()
    {
        _focusMaskShader = (ShaderMaterial)FocusMaskSprite.Material;

        var resolution = FocusMaskSprite.GetViewportRect().Size;
        FocusMaskSprite.Scale = resolution;

        _focusMaskShader.SetShaderParameter(Uniforms.Resolution, resolution);
    }

    private static class Uniforms
    {
        public static readonly StringName FocusRect = "u_focusRect";
        public static readonly StringName Resolution = "u_resolution";
    }
}