namespace Sandbox.TooltipSystem;

[GlobalClass]
public sealed partial class ControlTooltipTrigger : TooltipTrigger
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
}