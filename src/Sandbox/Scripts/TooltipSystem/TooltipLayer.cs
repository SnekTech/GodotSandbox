using GodotGadgets.TooltipSystem;

namespace Sandbox.TooltipSystem;

[SceneTree]
public partial class TooltipLayer : CanvasLayer, ITooltipDisplay
{
    public override void _Ready()
    {
        Tooltip.Hide();
    }

    public void ShowTooltip(TooltipContent content, Rect2 targetGlobalRect)
    {
        Tooltip.ShowAt(content, targetGlobalRect);
    }

    public void HideTooltip()
    {
        Tooltip.FadeOut();
    }
}