namespace Sandbox.TooltipSystem;

[GlobalClass]
public partial class ControlTooltipTrigger : Node
{
    public override void _EnterTree()
    {
        var parent = GetParent<Control>();
        parent.MouseEntered += OnMouseEntered;
        parent.MouseExited += OnMouseExited;
    }

    public override void _ExitTree()
    {
        var parent = GetParent<Control>();
        parent.MouseEntered -= OnMouseEntered;
        parent.MouseExited -= OnMouseExited;
    }
    
    private static void OnMouseEntered()
    {
        TooltipLayer.ShowTooltip();
    }

    private static void OnMouseExited()
    {
        TooltipLayer.HideTooltip();
    }
}