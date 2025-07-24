namespace Sandbox.Combo;

[SceneTree]
public partial class ComboDemo : Control
{
    private int _level;
    private bool _isCountingCombo;

    public override void _EnterTree()
    {
        ComboTimer.Timeout += OnComboTimerTimeout;
    }

    public override void _ExitTree()
    {
        ComboTimer.Timeout -= OnComboTimerTimeout;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionReleased(InputActions.LeftClick))
        {
            _level++;
            ComboLevelLabel.Text = GetComboLevelText(_level);
            ComboProgressBar.Value = 100;
            StartCounting();
        }
    }

    public override void _Process(double delta)
    {
        ComboProgressBar.Value = Mathf.Lerp(ComboProgressBar.Value, ComboTimer.TimeLeft / ComboTimer.WaitTime * 100, 1);
    }

    private void StartCounting(float interval = 2)
    {
        ComboTimer.Start(interval);
    }

    private void OnComboTimerTimeout()
    {
        if (_level <= 0) return;

        _level--;
        ComboLevelLabel.Text = GetComboLevelText(_level);
        if (_level == 0)
        {
            ComboTimer.Stop();
        }
    }

    private static string GetComboLevelText(int level) =>
        level switch
        {
            0 => string.Empty,
            1 => "Good",
            2 => "Great",
            3 => "Excellent",
            _ => "Default",
        };
}