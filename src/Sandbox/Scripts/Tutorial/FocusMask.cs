using GodotGadgets.Extensions;
using GTweens.Easings;
using GTweensGodot.Extensions;

namespace Sandbox.Tutorial;

[SceneTree]
public partial class FocusMask : Sprite2D
{
    private const int DefaultPadding = 20;

    private FocusMaskShader _focusMaskShader = null!;
    private Rect2? _currentFocusRect;

    public override void _Ready()
    {
        _focusMaskShader = new FocusMaskShader(this.GetMaterialAs<ShaderMaterial>());

        _currentFocusRect = GetViewportRect();
        var resolution = _currentFocusRect.Value.Size;
        this.GetTextureAs<GradientTexture2D>().SetSize(resolution);

        _focusMaskShader.Resolution.Value = resolution;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton inputEventMouseButton)
        {
            if (!_currentFocusRect.HasValue || _currentFocusRect.Value.HasPoint(inputEventMouseButton.Position))
                return;

            var message = $"""
                           mouse button event {inputEventMouseButton.ButtonIndex} at position {inputEventMouseButton.Position}
                           is outside of the focus rect, so it will be marked as handled;
                           """;
            message.DumpGd();
            GetViewport().SetInputAsHandled();
        }
    }

    public async Task FocusAsync(Rect2 focusRect, CancellationToken token)
    {
        var lastFocusRect = _currentFocusRect ?? GetViewportRect();
        _currentFocusRect = focusRect.Grow(DefaultPadding);

        _focusMaskShader.FromRect.Value = lastFocusRect;
        _focusMaskShader.FocusRect.Value = _currentFocusRect.Value;
        _focusMaskShader.Progress.Value = 0;
        Show();

        await _focusMaskShader.Progress.Tween(1, 1f)
            .SetEasing(Easing.OutCubic)
            .PlayAsync(token);
    }
}