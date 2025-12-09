using GodotGadgets.TooltipSystem;

namespace Sandbox.TooltipSystem;

public abstract partial class TooltipTriggerNode<T> : Node where T : Node
{
    protected T Parent = null!;
    protected TooltipTriggerBehavior TooltipTriggerBehavior = null!;

    public void SetTooltipContent(TooltipContent content) => TooltipTriggerBehavior.Content = content;
    public void SetTooltipDisplay(ITooltipDisplay display) => TooltipTriggerBehavior.TooltipDisplay = display;

    protected abstract void OnEnterTree();
    protected abstract void OnExitTree();

    public sealed override void _EnterTree()
    {
        Parent = GetParent<T>();
        TooltipTriggerBehavior = TooltipTriggerBehavior.New(Parent);

        OnEnterTree();
    }

    public sealed override void _ExitTree()
    {
        OnExitTree();
    }
}