using GodotGadgets.ShaderStuff;

namespace Sandbox.ShaderPlayground;

public class UltimateShader(ShaderMaterial shaderMaterial)
{
    public Uniform<Vector2> Offset { get; } = new(shaderMaterial, Uniforms.Offset);
    public Uniform<float> Radius { get; } = new(shaderMaterial, Uniforms.Radius);

    static class Uniforms
    {
        public static readonly StringName Offset = "offset";
        public static readonly StringName Radius = "radius";
    }
}