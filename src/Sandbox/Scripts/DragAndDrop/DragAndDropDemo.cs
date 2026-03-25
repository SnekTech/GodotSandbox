using GodotGadgets.Extensions;
using GodotGadgets.Tasks;

namespace Sandbox.DragAndDrop;

[SceneTree]
public partial class DragAndDropDemo : Node2D
{
    DragStateMachine _stateMachine = null!;

    public override void _Ready()
    {
        var context = new DragStateContext(DragTarget, DragTarget.GetFirstChildOfType<Area2D>()!);
        _stateMachine = new DragStateMachine(context);
        _stateMachine.SetInitStateAsync(new Idle(_stateMachine), this.GetCancellationTokenOnTreeExit()).Fire();
    }

    public override void _Input(InputEvent inputEvent)
    {
        _stateMachine.HandleInputAsync(DragInput.FromInputEvent(inputEvent, GetGlobalMousePosition),
            this.GetCancellationTokenOnTreeExit()).Fire();
    }
}