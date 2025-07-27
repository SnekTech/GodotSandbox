namespace Sandbox.Combo;

public class BasicComboDisplay(Label levelLabel, ProgressBar progressBar): IComboDisplay
{
    public void UpdateLevelText(string text)
    {
        levelLabel.Text = text;
    }

    public void UpdateProgress(double progressNormalized)
    {
        progressBar.Value = progressNormalized * 100;
    }
}