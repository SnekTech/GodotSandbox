namespace Sandbox.TooltipSystem;

public abstract partial class TooltipTrigger : Node
{
    public TooltipContent Content { get; set; } = TooltipContent.New("test title",
        "test test content testent ");

    public abstract Rect2 TargetGlobalRect { get; }

    protected void OnMouseEntered()
    {
        TooltipLayer.ShowTooltip(Content, TargetGlobalRect);
    }

    protected void OnMouseExited()
    {
        TooltipLayer.HideTooltip();
    }
}