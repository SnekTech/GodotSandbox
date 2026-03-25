using GodotGadgets.FSM;

namespace Sandbox.DragAndDrop;

record DragStateContext(Node2D Target, Area2D Area);

class DragStateMachine(DragStateContext context) : StateMachineV2<DragState>
{
    internal DragStateContext Context { get; } = context;

    internal Task HandleInputAsync(DragInput dragInput, CancellationToken ct = default) =>
        CurrentState?.HandleDragInputAsync(dragInput, ct) ?? Task.CompletedTask;
}