namespace Sandbox.TooltipSystem;

[SceneTree]
public partial class TooltipLayer : CanvasLayer
{
    private static TooltipLayer Instance { get; set; } = null!;

    public override void _Ready()
    {
        Instance = this;

        HideTooltip();
    }

    public static void ShowTooltip()
    {
        Instance.Tooltip.Show();
    }

    public static void HideTooltip()
    {
        Instance.Tooltip.Hide();
    }
}