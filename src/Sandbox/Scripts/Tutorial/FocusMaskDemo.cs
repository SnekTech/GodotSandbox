namespace Sandbox.Tutorial;

[SceneTree]
public partial class FocusMaskDemo : Control
{
    public override void _Ready()
    {
        _.FocusMaskLayer.FocusRect = _.Grid.GetRect();
    }
}