using GodotGadgets.Extensions;

namespace Sandbox.TooltipSystem;

[SceneTree]
public partial class TooltipLayer : CanvasLayer
{
    private static TooltipLayer Instance { get; set; } = null!;
    private static CancellationTokenSource _currentActionCancellationSource = new();

    public override void _Ready()
    {
        Instance = this;

        Instance.Tooltip.Hide();
    }

    public static void ShowTooltip(TooltipContent content, Rect2 targetGlobalRect)
    {
        _currentActionCancellationSource.CancelAndDispose();
        _currentActionCancellationSource = new CancellationTokenSource();

        Instance.Tooltip.ShowAsync(content, targetGlobalRect, _currentActionCancellationSource.Token).Fire();
    }

    public static void HideTooltip()
    {
        _currentActionCancellationSource.CancelAndDispose();
        _currentActionCancellationSource = new CancellationTokenSource();

        Instance.Tooltip.HideAsync(_currentActionCancellationSource.Token).Fire();
    }
}