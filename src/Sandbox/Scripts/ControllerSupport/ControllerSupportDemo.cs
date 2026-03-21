using GodotGadgets.Extensions;
using GodotGadgets.ShaderStuff;
using GodotGadgets.Tasks;
using Sandbox.ControllerSupport.FSM;

namespace Sandbox.ControllerSupport;

[SceneTree]
public partial class ControllerSupportDemo : Node2D
{
    Uniform<Vector2> vectorInput = null!;

    readonly ItemSelectionStateMachine _itemSelectionStateMachine = new();

    public override void _Ready()
    {
        vectorInput = new Uniform<Vector2>(JoyStickInputIndicator.GetMaterialAs<ShaderMaterial>(), "u_vector_input");
        _itemSelectionStateMachine.SetInitStateAsync(new ItemSelectionIdle(_itemSelectionStateMachine),
            this.GetCancellationTokenOnTreeExit()).Fire();
    }

    public override void _Input(InputEvent @event)
    {
        _itemSelectionStateMachine.HandleInputAsync(@event, this.GetCancellationTokenOnTreeExit()).Fire();

        if (@event is InputEventMouseMotion)
        {
            var mousePositionNormalized = GetMousePositionInTexture(JoyStickInputIndicator);
            vectorInput.Value = mousePositionNormalized;
        }
        else if (@event is InputEventJoypadMotion joypadMotion)
        {
            vectorInput.Value = joypadMotion.GetRightJoystickValue();
        }
    }

    static Vector2 GetSpriteSize(Sprite2D sprite2D) => sprite2D.Texture.GetSize() * sprite2D.Scale;

    Vector2 GetMousePositionInTexture(Sprite2D sprite)
    {
        var (relativeX, relativeY) = GetGlobalMousePosition() - sprite.GlobalPosition;
        var (width, height) = GetSpriteSize(sprite);
        var normalizedX = (relativeX / width).Clamp01() * 2f - 1f;
        var normalizedY = (relativeY / height).Clamp01() * 2f - 1f;

        return new Vector2(normalizedX, normalizedY);
    }
}