using R3;

namespace Sandbox.Rx;

[SceneTree]
public partial class RxDemo : Node2D
{
    private IDisposable? _subscription;

    private readonly BehaviorSubject<int> countSubject = new(0);

    private Observable<int> CountObservable => countSubject;

    public override void _Ready()
    {
        AddButton.Pressed += () =>
        {
            if (countSubject.IsDisposed) return;

            countSubject.OnNext(countSubject.Value + 1);
        };

        _subscription = CountObservable.Subscribe(count => CountLabel.Text = count.ToString());
    }

    public override void _ExitTree()
    {
        _subscription?.Dispose();
    }
}