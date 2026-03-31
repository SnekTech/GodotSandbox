using GodotGadgets.Tasks;
using Sandbox.ControllerSupport.FSM;

namespace Sandbox.ControllerSupport;

[SceneTree]
public partial class ControllerSupportDemo : Node2D
{
    readonly ItemSelectionStateMachine _itemSelectionStateMachine = new();

    public override void _Ready()
    {
        _itemSelectionStateMachine.InitAsync(this.GetCancellationTokenOnTreeExit()).Fire();
    }

    public override void _Input(InputEvent inputEvent)
    {
        _itemSelectionStateMachine.HandleInputAsync(DirectionSelectInput.FromInputEvent(inputEvent),
            this.GetCancellationTokenOnTreeExit()).Fire();

        _.UnitVectorIndicator.VectorToDisplay = inputEvent switch
        {
            InputEventMouse inputEventMouse => GetVectorInputFromMouse(inputEventMouse),
            InputEventJoypadMotion joypadMotion => joypadMotion.GetRightJoystickValue(),
            _ => Vector2.Zero,
        };
        return;

        Vector2 GetVectorInputFromMouse(InputEventMouse inputEventMouse)
        {
            var viewportCenter = GetViewportRect().Size / 2;
            var mouseVectorRaw = inputEventMouse.GlobalPosition - viewportCenter;
            var alpha = mouseVectorRaw.Length() / viewportCenter.Y;
            return Vector2.Zero.Lerp(mouseVectorRaw.Normalized(), alpha);
        }
    }

    Vector2 GetMousePositionInTexture(Sprite2D sprite)
    {
        var (relativeX, relativeY) = GetGlobalMousePosition() - sprite.GlobalPosition;
        var (width, height) = GetSpriteSize(sprite);
        var normalizedX = float.Clamp(relativeX / width, -1, 1);
        var normalizedY = float.Clamp(relativeY / height, -1, 1);

        return new Vector2(normalizedX, normalizedY);

        static Vector2 GetSpriteSize(Sprite2D sprite2D) => sprite2D.Texture.GetSize() * sprite2D.Scale;
    }
}