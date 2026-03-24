using GodotGadgets.Extensions;
using GodotGadgets.FSM;

namespace Sandbox.DragAndDrop;

abstract class DragState(DragStateMachine stateMachine) : IState
{
    protected DragStateMachine StateMachine { get; } = stateMachine;
    protected DragStateContext Context => StateMachine.Context;

    public virtual Task OnEnterAsync(CancellationToken ct) => Task.CompletedTask;

    public virtual Task OnExitAsync(CancellationToken ct) => Task.CompletedTask;

    internal virtual Task HandleMouseInputAsync(InputEventMouse inputEventMouse, CancellationToken ct = default) =>
        Task.CompletedTask;

    protected Task ChangeStateAsync(DragState newState, CancellationToken ct = default) =>
        StateMachine.ChangeStateAsync(newState, ct);
}

sealed class Idle(DragStateMachine stateMachine) : DragState(stateMachine)
{
    public override Task OnEnterAsync(CancellationToken ct)
    {
        "enter idle state".DumpGd();
        return base.OnEnterAsync(ct);
    }

    internal override async Task HandleMouseInputAsync(InputEventMouse inputEventMouse, CancellationToken ct = default)
    {
        if (inputEventMouse is InputEventMouseButton
            {
                ButtonIndex: MouseButton.Left, Pressed: true,
            })
        {
            var clickPosition = Context.Target.GetViewport().GetCamera2D().GetGlobalMousePosition();

            var clickInArea = Context.Area.ContainsPoint(clickPosition);
            if (clickInArea)
            {
                await ChangeStateAsync(new Selected(StateMachine), ct);
            }
        }
    }
}

sealed class Selected(DragStateMachine stateMachine) : DragState(stateMachine)
{
    public override Task OnEnterAsync(CancellationToken ct)
    {
        "enter selected state".DumpGd();
        return Task.CompletedTask;
    }

    internal override async Task HandleMouseInputAsync(InputEventMouse inputEventMouse, CancellationToken ct = default)
    {
        if (inputEventMouse is InputEventMouseMotion)
        {
            await ChangeStateAsync(new Dragging(StateMachine), ct);
            await StateMachine.HandleInputAsync(inputEventMouse, ct);
        }
    }
}

sealed class Dragging(DragStateMachine stateMachine) : DragState(stateMachine)
{
    internal override async Task HandleMouseInputAsync(InputEventMouse inputEventMouse, CancellationToken ct = default)
    {
        if (inputEventMouse is InputEventMouseMotion)
        {
            var mouseGlobalPosition = Context.Target.GetViewport().GetCamera2D().GetGlobalMousePosition();
            var newGlobalPosition = Context.Target.GlobalPosition.Lerp(mouseGlobalPosition, 0.5f);
            Context.Target.GlobalPosition = newGlobalPosition;
        }
        else if (inputEventMouse is InputEventMouseButton { Pressed: false })
        {
            await ChangeStateAsync(new Idle(StateMachine), ct);
        }
    }
}

static class Area2DExtensions
{
    extension(Area2D area2D)
    {
        internal bool ContainsPoint(Vector2 globalPosition)
        {
            var collisionShape2D = area2D.CollisionShape;
            if (collisionShape2D.Shape is not RectangleShape2D rectangleShape2D)
            {
                return false;
            }

            var localPosition = area2D.ToLocal(globalPosition);
            var halfSize = rectangleShape2D.Size / 2;
            var regionRect = new Rect2(-halfSize + collisionShape2D.Position, rectangleShape2D.Size);
            return regionRect.HasPoint(localPosition);
        }
    }
}