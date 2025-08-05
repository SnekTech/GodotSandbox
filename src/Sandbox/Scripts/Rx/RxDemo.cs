using GodotGadgets.Extensions;
using R3;

namespace Sandbox.Rx;

[SceneTree]
public partial class RxDemo : Node2D
{
    private IDisposable? _frameCounterSubscription;

    public override void _Ready()
    {
        _frameCounterSubscription = Observable.EveryUpdate()
            .ThrottleLastFrame(10)
            .Subscribe(x => { $"Observable.EveryUpdate: {GodotFrameProvider.Process.GetFrameCount()}".DumpGd(); });
    }

    public override void _ExitTree()
    {
        _frameCounterSubscription?.Dispose();
    }
}