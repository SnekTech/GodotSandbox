using GodotGadgets.Extensions;
using GTweens.Easings;
using GTweensGodot.Extensions;

namespace Sandbox.Tutorial;

[SceneTree]
public partial class FocusMaskLayer : CanvasLayer
{
    private const int DefaultPadding = 10;

    private FocusMaskShader _focusMaskShader = null!;

    public override void _Ready()
    {
        _focusMaskShader = new FocusMaskShader(FocusMaskSprite.GetMaterialAs<ShaderMaterial>());

        var resolution = FocusMaskSprite.GetViewportRect().Size;
        FocusMaskSprite.GetTextureAs<GradientTexture2D>().SetSize(resolution);

        _focusMaskShader.Resolution.Value = resolution;
    }

    public async Task FocusAsync(Rect2 focusRect, CancellationToken token)
    {
        _focusMaskShader.FocusRect.Value = focusRect.Grow(DefaultPadding);
        Show();

        await _focusMaskShader.Progress.Tween(1, 1f)
            .SetEasing(Easing.OutCubic)
            .PlayAsync(token);
    }

    public async Task HideAsync(CancellationToken token)
    {
        await _focusMaskShader.Progress.Tween(0, 0.5f)
            .SetEasing(Easing.InOutCubic)
            .PlayAsync(token);
        Hide();
    }
}