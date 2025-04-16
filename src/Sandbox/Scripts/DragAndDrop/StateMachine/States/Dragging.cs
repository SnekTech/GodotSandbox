using Godot;

namespace Sandbox.DragAndDrop.StateMachine.States;

public class Dragging(DraggableStateMachine draggableStateMachine) : DraggableState(draggableStateMachine)
{
    
    public override void OnEnter()
    {
        GD.Print("enter dragging");
        StateMachine.Draggable.CaptureDragStartPosition();
    }
    
    public override void OnMouseEvent(InputEventMouse eventMouse)
    {
        var isLeftMouseButtonReleased = eventMouse is InputEventMouseButton { ButtonIndex: MouseButton.Left, Pressed: false };
        
        if (isLeftMouseButtonReleased)
        {
            StateMachine.ChangeState<Idle>();
            return;
        }

        
        if (eventMouse is InputEventMouseMotion eventMouseMotion)
        {
            GD.Print(eventMouseMotion.Position);
            Draggable.DragToMouse(eventMouseMotion.Position);
        }
    }
}