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