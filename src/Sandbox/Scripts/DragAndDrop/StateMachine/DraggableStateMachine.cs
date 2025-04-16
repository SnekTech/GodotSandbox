using Godot;
using Sandbox.DragAndDrop.StateMachine.States;
using Sandbox.FSM;

namespace Sandbox.DragAndDrop.StateMachine;

public class DraggableStateMachine(Draggable draggable) : StateMachine<DraggableState>
{
    public readonly Draggable Draggable = draggable;
    
    protected override void InstantiateStateInstances()
    {
        StateInstances[typeof(Idle)] = new Idle(this);
        StateInstances[typeof(Dragging)] = new Dragging(this);
    }

    public void OnMouseEvent(InputEventMouse eventMouse)
    {
        CurrentState?.OnMouseEvent(eventMouse);
    }
}