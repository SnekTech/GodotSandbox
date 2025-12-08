using GodotGadgets.TooltipSystem;

namespace Sandbox.TooltipSystem;

[GlobalClass]
public sealed partial class Area2DTooltipTrigger : Node
{
    [Export]
    TooltipLayer _tooltipLayer = null!;

    Area2D _parent = null!;
    TooltipTriggerBehavior _tooltipTriggerBehavior = null!;

    public TooltipContent Content
    {
        set => _tooltipTriggerBehavior.Content = value;
    }

    public override void _EnterTree()
    {
        _parent = GetParent<Area2D>();
        _tooltipTriggerBehavior = TooltipTriggerBehavior.FromArea2D(_parent, _tooltipLayer);

        _parent.MouseEntered += _tooltipTriggerBehavior.OnMouseEntered;
        _parent.MouseExited += _tooltipTriggerBehavior.OnMouseExited;
    }

    public override void _ExitTree()
    {
        _parent.MouseEntered -= _tooltipTriggerBehavior.OnMouseEntered;
        _parent.MouseExited -= _tooltipTriggerBehavior.OnMouseExited;
    }
}