using GodotGadgets.Extensions;
using GodotGadgets.ShaderStuff;

namespace Sandbox.ControllerSupport;

[SceneTree]
public partial class UnitVectorIndicator : Sprite2D
{
    /// <summary>
    /// input vector will be clamped between [-1, 1]
    /// </summary>
    public Vector2 VectorToDisplay
    {
        get;
        set
        {
            field = value;
            _vectorInput.Value = value.Clamp(-1, 1);
        }
    }

    Uniform<Vector2> _vectorInput = null!;

    public override void _Ready()
    {
        _vectorInput = new Uniform<Vector2>(this.GetMaterialAs<ShaderMaterial>(), "u_vector_input");
    }
}