namespace Sandbox.Tutorial;

[SceneTree]
public partial class FocusMaskLayer : CanvasLayer
{
    private const int DefaultPadding = 10;

    private FocusMaskShader _focusMaskShader = null!;

    public Rect2 FocusRect
    {
        set => _focusMaskShader.FocusRect.Value = value.Grow(DefaultPadding);
    }

    public override void _Ready()
    {
        _focusMaskShader = new FocusMaskShader((ShaderMaterial)FocusMaskSprite.Material);

        var resolution = FocusMaskSprite.GetViewportRect().Size;
        FocusMaskSprite.Scale = resolution;

        _focusMaskShader.Resolution.Value = resolution;
    }
}