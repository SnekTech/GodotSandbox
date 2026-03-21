namespace Sandbox.ControllerSupport.FSM;

sealed class ItemSelectionIdle(ItemSelectionStateMachine stateMachine) : ItemSelectionState(stateMachine)
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