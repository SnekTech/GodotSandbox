namespace Sandbox.TooltipSystem;

[GlobalClass]
public sealed partial class Area2DTooltipTrigger : TooltipTriggerNode<Area2D>
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