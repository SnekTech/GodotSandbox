using GodotGadgets.Extensions;
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

class ItemSelectionIdle(ItemSelectionStateMachine stateMachine) : ItemSelectionState(stateMachine)
{
    internal override async Task HandleInputAsync(InputEvent inputEvent, CancellationToken ct = default)
    {
        if (inputEvent is InputEventJoypadMotion { IsRightJoystick: true } joypadMotion)
        {
            if (!joypadMotion.IsInDeadzone(StateMachine.Threshold))
            {
                await ChangeStateAsync(new ItemSelectionSelecting(StateMachine), ct);
            }
        }
    }
}

class ItemSelectionSelecting(ItemSelectionStateMachine stateMachine) : ItemSelectionState(stateMachine)
{
    CompassDirection _selectedDirection = CompassDirection.None;

    public override Task OnEnterAsync(CancellationToken ct)
    {
        // todo: show the selection items in 8 directions
        return Task.CompletedTask;
    }

    public override Task OnExitAsync(CancellationToken ct)
    {
        // todo: hide the selection items, send out the selection
        $"input back in deadzone, so the direction [{_selectedDirection}] has been selected".DumpGd();
        return Task.CompletedTask;
    }

    internal override async Task HandleInputAsync(InputEvent inputEvent, CancellationToken ct = default)
    {
        if (inputEvent is InputEventJoypadMotion { IsRightJoystick: true } joypadMotion)
        {
            if (!joypadMotion.IsInDeadzone(StateMachine.Threshold))
            {
                var rightJoystickValue = joypadMotion.GetRightJoystickValue();
                _selectedDirection = CompassDirection.FromVector2(rightJoystickValue);
            }
            else
            {
                await ChangeStateAsync(new ItemSelectionIdle(StateMachine), ct);
            }
        }
    }
}

class ItemSelectionStateMachine : StateMachineV2<ItemSelectionState>
{
    internal float Threshold => 0.8f;

    internal Task HandleInputAsync(InputEvent inputEvent, CancellationToken ct = default) =>
        CurrentState?.HandleInputAsync(inputEvent, ct) ?? Task.CompletedTask;
}