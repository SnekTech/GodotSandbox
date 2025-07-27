namespace Sandbox.Combo;

[SceneTree]
public partial class ComboDemo : Control
{
    public override void _Ready()
    {
        var comboDisplay = new BasicComboDisplay(ComboLevelLabel, ComboProgressBar);
        _.ComboComponent.ComboDisplay = comboDisplay;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionReleased(InputActions.LeftClick))
        {
            _.ComboComponent.IncreaseComboLevel();
        }
    }
}