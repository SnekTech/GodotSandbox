namespace Sandbox.ControllerSupport.FSM;

sealed class ItemSelectionIdle(ItemSelectionStateMachine stateMachine) : ItemSelectionState(stateMachine)
{
    internal override Task HandleInputAsync(DirectionSelectInput input, CancellationToken ct = default)
    {
        return input switch
        {
            Start => ChangeStateAsync(new ReadyToSelect(StateMachine), ct),
            _ => Task.CompletedTask,
        };
    }
}

sealed class ReadyToSelect(ItemSelectionStateMachine stateMachine) : ItemSelectionState(stateMachine)
{
    public override Task OnEnterAsync(CancellationToken ct)
    {
        // todo: show the selection items in 8 directions
        return Task.CompletedTask;
    }

    internal override Task HandleInputAsync(DirectionSelectInput input, CancellationToken ct = default) =>
        input switch
        {
            Cancel => ChangeStateAsync(new ItemSelectionIdle(StateMachine), ct),
            Move move when move.AxisValue.IsLongerThan(StateMachine.Threshold) =>
                ChangeStateAsync(new ItemSelectionSelecting(StateMachine), ct),
            _ => Task.CompletedTask,
        };
}