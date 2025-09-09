using GodotGadgets.ShaderStuff;

namespace Sandbox.ShaderPlayground;

public class ShaderBookShader(ShaderMaterial shaderMaterial)
{
    public Uniform<Vector2> Resolution { get; } = new(shaderMaterial, Uniforms.Resolution);
    public Uniform<Vector2> Mouse { get; } = new(shaderMaterial, Uniforms.Mouse);

    private static class Uniforms
    {
        public static readonly StringName Resolution = "u_resolution";
        public static readonly StringName Mouse = "u_mouse";
    }
}