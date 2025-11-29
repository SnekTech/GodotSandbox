using GodotGadgets.Extensions;

namespace Sandbox.TooltipSystem;

[SceneTree]
public partial class TooltipLayer : CanvasLayer
{
    private static TooltipLayer Instance { get; set; } = null!;
    private static CancellationTokenSource? _showCancellationSource;
    private static CancellationTokenSource? _hideCancellationSource;

    public override void _Ready()
    {
        Instance = this;

        Instance.Tooltip.Hide();
    }

    public static void ShowTooltip(TooltipContent content)
    {
        _hideCancellationSource?.CancelAndDispose();
        _hideCancellationSource = null;

        _showCancellationSource ??= new CancellationTokenSource();

        Instance.Tooltip.ShowAsync(content, _showCancellationSource.Token).Fire();
    }

    public static void HideTooltip()
    {
        _showCancellationSource?.CancelAndDispose();
        _showCancellationSource = null;

        _hideCancellationSource ??= new CancellationTokenSource();

        Instance.Tooltip.HideAsync(_hideCancellationSource.Token).Fire();
    }
}