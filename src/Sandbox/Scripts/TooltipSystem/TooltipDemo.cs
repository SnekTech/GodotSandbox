using GodotGadgets.TooltipSystem;

namespace Sandbox.TooltipSystem;

[SceneTree]
public partial class TooltipDemo : Control
{
    public override void _Ready()
    {
        Area2DTooltipTrigger.Content = TooltipContent.New("Area2D",
            "tthe area2dthe area2dthe area2dthe area2dthe area2dthe area2dthe area2dthe area2dthe area2dhe area2d");

        ControlTooltipTrigger.Content = TooltipContent.New("Control",
            "thhe controlhe controlhe controlhe controlhe controlhe controlhe controlhe controlhe controlhe controlhe controlhe controle control");
    }
}