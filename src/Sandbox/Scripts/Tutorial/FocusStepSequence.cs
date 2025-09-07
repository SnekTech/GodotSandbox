namespace Sandbox.Tutorial;

public class FocusStepSequence(List<FocusStep> steps)
{
    private int _index;

    public FocusStep CurrentStep => steps[_index];

    public void StepForward() => _index = Mathf.Min(_index + 1, steps.Count - 1);

    public void Reset() => _index = 0;
}

public readonly record struct FocusStep(Rect2 RectToFocus);