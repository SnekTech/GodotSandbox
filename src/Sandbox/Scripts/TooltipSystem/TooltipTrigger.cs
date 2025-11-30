namespace Sandbox.TooltipSystem;

public abstract partial class TooltipTrigger : Node
{
    public TooltipContent Content { get; set; } = TooltipContent.New("test title",
        "test test content testent ");

    public abstract Rect2 ParentRect { get; }

    protected void OnMouseEntered()
    {
        TooltipLayer.ShowTooltip(Content, GetTooltipPosition(ParentRect));
    }

    protected void OnMouseExited()
    {
        TooltipLayer.HideTooltip();
    }

    private static Vector2 GetTooltipPosition(Rect2 parentRect)
    {
        const int tooltipMargin = 10;
        var tooltipX = parentRect.Position.X + parentRect.Size.X + tooltipMargin;
        return parentRect.Position with { X = tooltipX };
    }
}