namespace Sandbox.Combo;

[SceneTree]
public partial class ComboDemo : Control
{
    private int _level;
    private const int MaxComboLevel = 4;
    private const float MaxComboInterval = 2.0f;

    private int Level
    {
        get => _level;
        set
        {
            _level = Mathf.Clamp(value, 0, MaxComboLevel);
            ComboLevelLabel.Text = GetComboLevelText(_level);
        }
    }

    public override void _EnterTree()
    {
        ComboTimer.Timeout += OnComboTimerTimeout;
    }

    public override void _ExitTree()
    {
        ComboTimer.Timeout -= OnComboTimerTimeout;
    }

    public override void _Ready()
    {
        Reset();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionReleased(InputActions.LeftClick))
        {
            IncreaseComboLevel();
        }
    }

    public override void _Process(double delta)
    {
        if (ComboTimer.IsStopped()) return;

        ComboProgressBar.Value = ComboTimer.TimeLeft / ComboTimer.WaitTime * 100;
    }

    private void Reset()
    {
        Level = 0;
        ComboProgressBar.Value = 0;
        ComboTimer.Stop();
    }

    public void IncreaseComboLevel()
    {
        Level++;
        ComboProgressBar.Value = 100;
        ComboTimer.Start(MaxComboInterval);
    }

    private void DecreaseComboLevel()
    {
        if (Level <= 0) return;

        Level--;

        if (Level > 1) return;

        Reset();
    }

    private void OnComboTimerTimeout()
    {
        DecreaseComboLevel();
    }

    private static string GetComboLevelText(int level) =>
        level switch
        {
            <= 1 => string.Empty,
            2 => "Good",
            3 => "Great",
            MaxComboLevel => "Excellent",
            _ => "Overflow!",
        };
}