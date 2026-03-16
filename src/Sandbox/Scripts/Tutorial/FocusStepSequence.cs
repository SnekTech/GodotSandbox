namespace Sandbox.Tutorial;

public class FocusStepSequence(List<FocusStep> steps)
{
    int _index;

    public FocusStep CurrentStep => steps[_index];

    public void Forward() => _index = Mathf.Min(_index + 1, steps.Count - 1);
    public void Cycle() => _index = (_index + 1) % steps.Count;

    public void Reset() => _index = 0;
}

public readonly record struct FocusStep(Rect2 RectToFocus);