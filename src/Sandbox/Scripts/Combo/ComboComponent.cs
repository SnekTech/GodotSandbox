namespace Sandbox.Combo;

[SceneTree]
public partial class ComboComponent : Node
{
    private int _level;
    private const int MaxComboLevel = 4;
    private const float MaxComboInterval = 2.0f;

    public IComboDisplay? ComboDisplay { get; set; }

    private int Level
    {
        get => _level;
        set
        {
            _level = Mathf.Clamp(value, 0, MaxComboLevel);
            ComboDisplay?.UpdateLevelText(GetComboLevelText(_level));
        }
    }

    #region lifecycle

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

    public override void _Process(double delta)
    {
        if (ComboTimer.IsStopped()) return;

        ComboDisplay?.UpdateProgress(ComboTimer.TimeLeft / ComboTimer.WaitTime);
    }

    #endregion

    public void Reset()
    {
        Level = 0;
        ComboDisplay?.UpdateProgress(0);
        ComboTimer.Stop();
    }

    public void IncreaseComboLevel()
    {
        Level++;
        ComboDisplay?.UpdateProgress(1);
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