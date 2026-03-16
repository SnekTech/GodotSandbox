using GodotGadgets.Tasks;
using GodotGadgets.TooltipSystem;
using GodotGadgets.TweenStuff;
using GTweens.Tweens;
using GTweensGodot.Extensions;

namespace Sandbox.TooltipSystem;

[SceneTree]
public partial class Tooltip : Control
{
    const float FadeDuration = 0.3f;
    readonly CancellableTweenHolder _tweenHolder = new();

    public override void _ExitTree()
    {
        _tweenHolder.Dispose();
    }
    
    internal void ShowAt(TooltipContent content, Rect2 targetGlobalRect)
    {
        Header.Text = content.Title;
        Content.Text = content.Content;
        Show();

        Callable.From(UpdateTooltipPosition).CallDeferred();

        var showTween = this.TweenModulateAlpha(1, FadeDuration);
        _tweenHolder.CancelPreviousAndPlayAsync(showTween, this.GetCancellationTokenOnTreeExit()).Fire();
        return;

        void UpdateTooltipPosition()
        {
            // the new tooltip panel container size can only
            // be obtained after the new tooltip appearing,
            // so the call to update the tooltip position should be deferred
            GlobalPosition =
                GetValidGlobalPosition(targetGlobalRect, _.PanelContainer.Get().Size, GetViewportRect().Size);
        }
    }

    internal void FadeOut()
    {
        var hideTween = this.TweenModulateAlpha(0, FadeDuration)
            .OnComplete(Hide);
        _tweenHolder.CancelPreviousAndPlayAsync(hideTween, this.GetCancellationTokenOnTreeExit()).Fire();
    }

    static Vector2 GetValidGlobalPosition(Rect2 targetGlobalRect, Vector2 tooltipSize, Vector2 viewportSize)
    {
        const int tooltipMarginX = 10;
        var targetPosition = targetGlobalRect.Position;
        var targetSize = targetGlobalRect.Size;

        var tooltipX = IsOverflowHorizontally()
            ? targetPosition.X - tooltipMarginX - tooltipSize.X
            : targetPosition.X + targetSize.X + tooltipMarginX;

        var tooltipY = IsOverflowVertically()
            ? targetPosition.Y - (tooltipSize.Y - targetSize.Y)
            : targetPosition.Y;

        return new Vector2(tooltipX, tooltipY);

        bool IsOverflowHorizontally() =>
            targetPosition.X + targetPosition.X + tooltipMarginX + tooltipSize.X > viewportSize.X;

        bool IsOverflowVertically() => targetPosition.Y + tooltipSize.Y > viewportSize.Y;
    }
}