using GodotGadgets.ShaderStuff;
using GTweens.Extensions;
using GTweens.Tweens;

namespace Sandbox.Tutorial;

public class FocusMaskShader(ShaderMaterial shaderMaterial)
{
    public Uniform<Rect2> FromRect { get; } = new(shaderMaterial, Uniforms.FromRect);
    public Uniform<Rect2> FocusRect { get; } = new(shaderMaterial, Uniforms.FocusRect);
    public Uniform<Vector2> Resolution { get; } = new(shaderMaterial, Uniforms.Resolution);
    public Uniform<float> Progress { get; } = new(shaderMaterial, Uniforms.Progress);

    private static class Uniforms
    {
        public static readonly StringName FromRect = "u_fromRect";
        public static readonly StringName FocusRect = "u_focusRect";
        public static readonly StringName Resolution = "u_resolution";
        public static readonly StringName Progress = "u_progress";
    }
}

public static class UniformExtensions
{
    public static GTween Tween(this Uniform<float> floatUniform, float to, float duration) =>
        GTweenExtensions.Tween(() => floatUniform.Value, current => floatUniform.Value = current, to, duration);
}