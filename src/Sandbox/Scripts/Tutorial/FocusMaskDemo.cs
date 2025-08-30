using GodotGadgets.Extensions;

namespace Sandbox.Tutorial;

[SceneTree]
public partial class FocusMaskDemo : Control
{
    public override void _Ready()
    {
        var focusRect = _.Grid.GetRect();
        _.FocusMaskLayer.FocusAsync(focusRect, CancellationToken.None).Fire();
    }
}