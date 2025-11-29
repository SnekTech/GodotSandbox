namespace Sandbox.TooltipSystem;

public abstract partial class TooltipTrigger : Node
{
    public TooltipContent Content { get; set; } = TooltipContent.New("test title",
        "test test content testent ");

    protected void OnMouseEntered()
    {
        TooltipLayer.ShowTooltip(Content);
    }

    protected void OnMouseExited()
    {
        TooltipLayer.HideTooltip();
    }
}