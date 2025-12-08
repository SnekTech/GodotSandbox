using GodotGadgets.TooltipSystem;

namespace Sandbox.TooltipSystem;

[GlobalClass]
public sealed partial class ControlTooltipTrigger : Node
{
    [Export]
    TooltipLayer _tooltipLayer = null!;

    Control _parent = null!;
    TooltipTriggerBehavior _tooltipTriggerBehavior = null!;

    public TooltipContent Content
    {
        set => _tooltipTriggerBehavior.Content = value;
    }

    public override void _EnterTree()
    {
        _parent = GetParent<Control>();
        _tooltipTriggerBehavior = TooltipTriggerBehavior.FromControl(_parent, _tooltipLayer);

        _parent.MouseEntered += _tooltipTriggerBehavior.OnMouseEntered;
        _parent.MouseExited += _tooltipTriggerBehavior.OnMouseExited;
    }

    public override void _ExitTree()
    {
        _parent.MouseEntered -= _tooltipTriggerBehavior.OnMouseEntered;
        _parent.MouseExited -= _tooltipTriggerBehavior.OnMouseExited;
    }
}