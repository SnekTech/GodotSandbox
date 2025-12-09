using GodotGadgets.TooltipSystem;

namespace Sandbox.TooltipSystem;

[SceneTree]
public partial class TooltipDemo : Control
{
    public override void _Ready()
    {
        Area2DTooltipTrigger.SetTooltipDisplay(_.TooltipLayer);
        ControlTooltipTrigger.SetTooltipDisplay(_.TooltipLayer);
        Area2DTooltipTrigger.SetTooltipContent(TooltipContent.New("Area2D",
            "tthe area2dthe area2dthe area2dthe area2dthe area2dthe area2dthe area2dthe area2dthe area2dhe area2d"));

        ControlTooltipTrigger.SetTooltipContent(TooltipContent.New("Control",
            "thhe controlhe controlhe controlhe controlhe controlhe controlhe controlhe controlhe controlhe controlhe controlhe controle control"));
    }
}