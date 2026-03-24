using GodotGadgets.FSM;

namespace Sandbox.DragAndDrop;


record DragStateContext(Node2D Target, Area2D Area);

class DragStateMachine(DragStateContext context) : StateMachineV2<DragState>
{
    internal DragStateContext Context { get; } = context;
    
    internal async Task HandleInputAsync(InputEvent inputEvent, CancellationToken ct = default)
    {
        if (inputEvent is not InputEventMouse inputEventMouse)
        {
            return;
        }

        if (CurrentState is not null)
        {
            await CurrentState.HandleMouseInputAsync(inputEventMouse, ct);
        }
    }
}