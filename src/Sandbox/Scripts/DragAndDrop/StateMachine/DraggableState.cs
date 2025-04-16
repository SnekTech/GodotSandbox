using Godot;
using Sandbox.FSM;

namespace Sandbox.DragAndDrop.StateMachine;

public abstract class DraggableState(DraggableStateMachine draggableStateMachine) : IState
{
    protected readonly DraggableStateMachine StateMachine = draggableStateMachine;
    protected Draggable Draggable => StateMachine.Draggable;
    
    public virtual void OnEnter()
    {
    }

    public virtual void OnExit()
    {
    }

    public abstract void OnMouseEvent(InputEventMouse eventMouse);
}