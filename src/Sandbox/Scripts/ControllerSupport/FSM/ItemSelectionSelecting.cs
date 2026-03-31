using GodotGadgets.Extensions;

namespace Sandbox.ControllerSupport.FSM;

sealed class ItemSelectionSelecting(ItemSelectionStateMachine stateMachine) : ItemSelectionState(stateMachine)
{
    CompassDirection _selectedDirection = CompassDirection.None;

    public override Task OnExitAsync(CancellationToken ct)
    {
        // todo: hide the selection items, send out the selection
        $"input back in deadzone, so the direction [{_selectedDirection}] has been selected".DumpGd();
        return Task.CompletedTask;
    }

    internal override Task HandleInputAsync(DirectionSelectInput input, CancellationToken ct = default)
    {
        return input switch
        {
            Move move when move.AxisValue.IsLongerThan(StateMachine.Threshold) => UpdateDirectionTask(move),
            Move => HandleMoveBelowThresholdTask(),
            Cancel => ChangeStateAsync(new ItemSelectionIdle(StateMachine), ct),
            _ => Task.CompletedTask,
        };

        Task UpdateDirectionTask(Move move)
        {
            $"direction {_selectedDirection} selected".DumpGd();
            _selectedDirection = CompassDirection.FromVector2(move.AxisValue);
            return Task.CompletedTask;
        }

        Task HandleMoveBelowThresholdTask() => ChangeStateAsync(new ItemSelectionIdle(StateMachine), ct);
    }
}