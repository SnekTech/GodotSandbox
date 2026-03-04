using GodotGadgets.ShaderStuff;
using GTweens.Extensions;
using GTweens.Tweeners;
using GTweens.Tweens;
using GTweensGodot.Extensions;

namespace Sandbox.ShaderPlayground;

public static class UniformExtensions
{
    extension<[MustBeVariant] T>(Uniform<T> uniform) where T : struct
    {
        Tweener<T>.Getter Getter => () => uniform.Value;
        Tweener<T>.Setter Setter => v => uniform.Value = v;
    }

    extension(Uniform<float> uniform)
    {
        public GTween Tween(float to, float duration)
            => GTweenExtensions.Tween(uniform.Getter, uniform.Setter, to, duration);
    }

    extension(Uniform<Vector2> uniform)
    {
        public GTween Tween(Vector2 to, float duration) =>
            GTweenGodotExtensions.Tween(uniform.Getter, uniform.Setter, to, duration);
    }

    extension(Uniform<Color> uniform)
    {
        public GTween Tween(Color to, float duration) =>
            GTweenGodotExtensions.Tween(uniform.Getter, uniform.Setter, to, duration);
    }
}