namespace Sandbox.Tutorial;

public class FocusMaskShader(ShaderMaterial shaderMaterial)
{
    public Uniform<Rect2> FocusRect { get; } = new(shaderMaterial, Uniforms.FocusRect);
    public Uniform<Vector2> Resolution { get; } = new(shaderMaterial, Uniforms.Resolution);
    public Uniform<float> Progress { get; } = new(shaderMaterial, Uniforms.Progress);

    private static class Uniforms
    {
        public static readonly StringName FocusRect = "u_focusRect";
        public static readonly StringName Resolution = "u_resolution";
        public static readonly StringName Progress = "u_progress";
    }
}