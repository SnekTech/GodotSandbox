namespace Sandbox.TooltipSystem;

[GlobalClass]
public sealed partial class Area2DTooltipTrigger : TooltipTrigger
{
    public override void _EnterTree()
    {
        var parent = GetParent<Area2D>();
        parent.MouseEntered += OnMouseEntered;
        parent.MouseExited += OnMouseExited;
    }

    public override void _ExitTree()
    {
        var parent = GetParent<Area2D>();
        parent.MouseEntered -= OnMouseEntered;
        parent.MouseExited -= OnMouseExited;
    }
}