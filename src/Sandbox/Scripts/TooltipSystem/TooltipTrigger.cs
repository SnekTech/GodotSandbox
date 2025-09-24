namespace Sandbox.TooltipSystem;

public abstract partial class TooltipTrigger : Node
{
    protected static void OnMouseEntered()
    {
        TooltipLayer.ShowTooltip();
    }

    protected static void OnMouseExited()
    {
        TooltipLayer.HideTooltip();
    }
}