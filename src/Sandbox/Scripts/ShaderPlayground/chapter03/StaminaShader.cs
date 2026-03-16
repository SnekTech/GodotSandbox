using GodotGadgets.Extensions;
using GodotGadgets.ShaderStuff;
using GodotGadgets.Tasks;
using GodotGadgets.TweenStuff;
using GTweens.Builders;

namespace Sandbox.ShaderPlayground.chapter03;

sealed class Player
{
    internal event Action<PropertyChange<float>>? StaminaChanged;

    public float CurrentStamina
    {
        get;
        set
        {
            var previous = field;
            field = value.Clamp01();
            StaminaChanged?.Invoke(new PropertyChange<float>(previous, field));
        }
    }
}

record PropertyChange<T>(T Previous, T Now);

public class StaminaShader(ShaderMaterial shaderMaterial)
{
    const float ChangeDuration = 0.3f;

    public Uniform<float> Progress { get; } = new(shaderMaterial, Uniforms.Progress);
    public Uniform<float> ChangeProgress { get; } = new(shaderMaterial, Uniforms.ChangeProgress);
    public Uniform<float> Radius { get; } = new(shaderMaterial, Uniforms.Radius);
    public Uniform<float> Width { get; } = new(shaderMaterial, Uniforms.Width);
    public Uniform<Color> MainColor { get; } = new(shaderMaterial, Uniforms.MainColor);
    public Uniform<Color> BackgroundColor { get; } = new(shaderMaterial, Uniforms.BackgroundColor);
    public Uniform<Color> ChangeColor { get; } = new(shaderMaterial, Uniforms.ChangeColor);

    readonly CancellableTweenHolder _tweenHolder = new();

    public void ChangeToVisual(float previous, float now, CancellationToken ct = default)
    {
        var isIncreasing = now - previous > 0;
        var (changeColor, tween) = isIncreasing switch
        {
            true => (Pico8Palette.White,
                GTweenSequenceBuilder.New()
                    .Append(ChangeProgress.Tween(now, ChangeDuration))
                    .Append(Progress.Tween(now, ChangeDuration))
                    .Build()),
            false => (Pico8Palette.DarkPurple,
                GTweenSequenceBuilder.New()
                    .Append(Progress.Tween(now, ChangeDuration))
                    .Append(ChangeProgress.Tween(now, ChangeDuration))
                    .Build()),
        };
        ChangeColor.Value = changeColor;
        _tweenHolder.CancelPreviousAndPlayAsync(tween, ct).Fire();
    }

    static class Uniforms
    {
        public static readonly StringName Progress = "u_progress";
        public static readonly StringName MainColor = "u_color";
        public static readonly StringName BackgroundColor = "u_bg_color";
        public static readonly StringName ChangeColor = "u_increase_color";
        public static readonly StringName ChangeProgress = "u_increase_progress";
        public static readonly StringName Radius = "u_radius";
        public static readonly StringName Width = "u_width";
    }
}