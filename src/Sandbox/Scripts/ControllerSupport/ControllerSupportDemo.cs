using GodotGadgets.Extensions;
using GodotGadgets.ShaderStuff;

namespace Sandbox.ControllerSupport;

[SceneTree]
public partial class ControllerSupportDemo : Node2D
{
    Uniform<Vector2> vectorInput = null!;

    public override void _Ready()
    {
        vectorInput = new Uniform<Vector2>(JoyStickInputIndicator.GetMaterialAs<ShaderMaterial>(), "u_vector_input");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion)
        {
            var mousePositionNormalized = GetMousePositionInTexture(JoyStickInputIndicator);
            vectorInput.Value = mousePositionNormalized;
        }
    }

    static Vector2 GetSpriteSize(Sprite2D sprite2D) => sprite2D.Texture.GetSize() * sprite2D.Scale;

    Vector2 GetMousePositionInTexture(Sprite2D sprite)
    {
        var (relativeX, relativeY) = GetGlobalMousePosition() - sprite.GlobalPosition;
        var (width, height) = sprite.Texture.GetSize() * sprite.Scale;
        var normalizedX = (relativeX / width).Clamp01() * 2f - 1f;
        var normalizedY = (relativeY / height).Clamp01() * 2f - 1f;

        return new Vector2(normalizedX, normalizedY);
    }
}