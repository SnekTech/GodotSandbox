using GodotGadgets.Extensions;

namespace Sandbox.ControllerSupport.FSM;

sealed class ItemSelectionSelecting(ItemSelectionStateMachine stateMachine) : ItemSelectionState(stateMachine)
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