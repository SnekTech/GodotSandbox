using GodotGadgets.FSM;

namespace Sandbox.ControllerSupport.FSM;

abstract class ItemSelectionState(ItemSelectionStateMachine stateMachine) : IState
{
    protected ItemSelectionStateMachine StateMachine { get; } = stateMachine;

    internal virtual Task HandleInputAsync(InputEvent inputEvent, CancellationToken ct = default) => Task.CompletedTask;

    public virtual Task OnEnterAsync(CancellationToken ct) => Task.CompletedTask;

    public virtual Task OnExitAsync(CancellationToken ct) => Task.CompletedTask;

    protected Task ChangeStateAsync(ItemSelectionState newState, CancellationToken ct = default) =>
        StateMachine.ChangeStateAsync(newState, ct);
}

class ItemSelectionStateMachine : StateMachineV2<ItemSelectionState>
{
    internal float Threshold => 0.8f;

    internal Task HandleInputAsync(InputEvent inputEvent, CancellationToken ct = default) =>
        CurrentState?.HandleInputAsync(inputEvent, ct) ?? Task.CompletedTask;
}