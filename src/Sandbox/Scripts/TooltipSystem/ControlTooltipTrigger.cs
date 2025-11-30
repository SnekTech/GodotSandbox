namespace Sandbox.TooltipSystem;

[GlobalClass]
public sealed partial class ControlTooltipTrigger : TooltipTrigger
{
    private Control _parent = null!;
    
    public override Rect2 ParentRect => _parent.GetGlobalRect();

    public override void _EnterTree()
    {
        _parent = GetParent<Control>();
        _parent.MouseEntered += OnMouseEntered;
        _parent.MouseExited += OnMouseExited;
    }

    public override void _ExitTree()
    {
        _parent.MouseEntered -= OnMouseEntered;
        _parent.MouseExited -= OnMouseExited;
    }
}