using GodotGadgets.Extensions;

namespace Sandbox.Tutorial;

[SceneTree]
public partial class FocusMaskDemo : Control
{
    private FocusStepSequence _focusStepSequence = null!;
    private bool _isMovingFocus;

    public override void _Ready()
    {
        List<ColorRect> targets = [Grid1, Grid2, Grid3];
        var focusSteps = targets.Select(grid => new FocusStep(grid.GetRect())).ToList();
        _focusStepSequence = new FocusStepSequence(focusSteps);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed(InputActions.TestKeyM))
        {
            if (_isMovingFocus)
            {
                "current focus not complete, cant move to next".DumpGd();
                return;
            }

            MovingToNextFocus().Fire();
        }
    }

    private async Task MovingToNextFocus()
    {
        _isMovingFocus = true;
        await FocusMask.FocusAsync(_focusStepSequence.CurrentStep.RectToFocus, CancellationToken.None);
        _focusStepSequence.StepForward();
        _isMovingFocus = false;
    }
}