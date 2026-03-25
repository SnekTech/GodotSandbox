namespace Sandbox.DragAndDrop;

abstract record DragInput;

sealed record DragStart(Vector2 StartGlobalPosition) : DragInput;

sealed record DragMove(Vector2 NextGlobalPosition) : DragInput;

sealed record DragRelease : DragInput
{
    internal static DragRelease Instance { get; } = new();
}

sealed record InvalidInput : DragInput;

static class DragInputFactory
{
    extension(DragInput)
    {
        internal static DragInput CreateDragStart(Vector2 startGlobalPosition) => new DragStart(startGlobalPosition);
        internal static DragInput CreateDragMove(Vector2 nextGlobalPosition) => new DragMove(nextGlobalPosition);
        internal static DragInput CreateDragRelease() => DragRelease.Instance;
        internal static DragInput CreateInvalid() => new InvalidInput();
    }
}

static class DragInputExtensions
{
    extension(DragInput)
    {
        internal static DragInput FromInputEvent(InputEvent inputEvent, Func<Vector2> mouseGlobalPositionGetter) =>
            inputEvent switch
            {
                InputEventMouseButton { ButtonIndex: MouseButton.Left, Pressed: true } => DragInput
                    .CreateDragStart(mouseGlobalPositionGetter()),
                InputEventMouseButton { ButtonIndex: MouseButton.Left, Pressed: false } =>
                    DragInput.CreateDragRelease(),
                InputEventMouseMotion => DragInput.CreateDragMove(mouseGlobalPositionGetter()),
                _ => DragInput.CreateInvalid(),
            };
    }
}