using GodotGadgets.Extensions;
using GTweens.Easings;
using GTweensGodot.Extensions;

namespace Sandbox.Tutorial;

[SceneTree]
public partial class FocusMask : Sprite2D
{
    private const int DefaultPadding = 10;

    private FocusMaskShader _focusMaskShader = null!;
    private Rect2 _inputValidArea;

    public override void _Ready()
    {
        _focusMaskShader = new FocusMaskShader(this.GetMaterialAs<ShaderMaterial>());

        var resolution = GetViewportRect().Size;
        this.GetTextureAs<GradientTexture2D>().SetSize(resolution);

        _focusMaskShader.Resolution.Value = resolution;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton inputEventMouseButton)
        {
            if (!_inputValidArea.HasPoint(inputEventMouseButton.Position))
            {
                GetViewport().SetInputAsHandled();
            }
        }
    }

    public async Task FocusAsync(Rect2 focusRect, CancellationToken token)
    {
        var focusRectWithPadding = focusRect.Grow(DefaultPadding);
        _inputValidArea = focusRectWithPadding;
        _focusMaskShader.FocusRect.Value = focusRectWithPadding;
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