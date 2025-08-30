using GTweens.Extensions;
using GTweens.Tweens;

namespace Sandbox.Tutorial;

public class Uniform<[MustBeVariant] T>(ShaderMaterial shaderMaterial, StringName name) where T : struct
{
    private T _value;

    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            shaderMaterial.SetShaderParameter(name, Variant.From(value));
        }
    }
}

public static class UniformExtensions
{
    public static GTween Tween(this Uniform<float> floatUniform, float to, float duration) =>
        GTweenExtensions.Tween(() => floatUniform.Value, current => floatUniform.Value = current, to, duration);
}