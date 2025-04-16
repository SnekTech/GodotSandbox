using Godot;

namespace Sandbox.DragAndDrop.StateMachine.States;

public class Idle(DraggableStateMachine draggableStateMachine) : DraggableState(draggableStateMachine)
{
    public override void OnEnter()
    {
        GD.Print("enter idle");
    }

    public override void OnMouseEvent(InputEventMouse eventMouse)
    {
        var isLeftMouseButtonPressed = eventMouse is InputEventMouseButton
        {
            ButtonIndex: MouseButton.Left, Pressed: true
        };

        if (isLeftMouseButtonPressed)
        {
            StateMachine.ChangeState<Dragging>();
        }
    }
}