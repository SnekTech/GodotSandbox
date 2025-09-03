using GodotGadgets.Extensions;

namespace Sandbox.Tutorial;

[SceneTree]
public partial class FocusMaskDemo : Control
{
    public override void _Ready()
    {
        var focusRect = _.Grid.GetRect();
        FocusMask.FocusAsync(focusRect, CancellationToken.None).Fire();

        FocusedButton.Pressed += () => "clicked".DumpGd();
        MaskedButton.Pressed += () => "should not appear".DumpGd();
    }
}