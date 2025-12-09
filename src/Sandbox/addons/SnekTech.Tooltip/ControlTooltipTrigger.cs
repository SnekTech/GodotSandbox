namespace SnekTech.Tooltip;

[GlobalClass]
public sealed partial class ControlTooltipTrigger : TooltipTriggerNode<Control>
{
    protected override void OnEnterTree()
    {
        Parent.MouseEntered += TooltipTriggerBehavior.OnMouseEntered;
        Parent.MouseExited += TooltipTriggerBehavior.OnMouseExited;
    }

    protected override void OnExitTree()
    {
        Parent.MouseEntered -= TooltipTriggerBehavior.OnMouseEntered;
        Parent.MouseExited -= TooltipTriggerBehavior.OnMouseExited;
    }
}