using GodotGadgets.Extensions;
using GodotGadgets.ShaderStuff;
using GTweens.Builders;
using GTweensGodot.Extensions;

namespace Sandbox.ShaderPlayground;

[SceneTree]
public partial class ShaderDemo : Node2D
{
    Vector2 _canvasSize;
    UltimateShader _ultimateShader = null!;

    public override void _Ready()
    {
        _ultimateShader = new UltimateShader(Canvas.GetMaterialAs<ShaderMaterial>());
        
        PlayTestAnimation();
    }

    void PlayTestAnimation()
    {
        _ultimateShader.Radius.Value = 0.5f;
        GTweenSequenceBuilder.New()
            .Append(_ultimateShader.Radius.Tween(0.1f, 1))
            .Append(_ultimateShader.Radius.Tween(0.5f,1))
            .Append(_ultimateShader.Offset.Tween(new Vector2(50, 0), 1))
            .Append(_ultimateShader.Offset.Tween(new Vector2(0, 0), 1))
            .Build()
            .SetMaxLoops()
            .Play();
    }

    Vector2 GetMousePositionInPic()
    {
        var (relativeX, relativeY) = GetGlobalMousePosition() - Canvas.GlobalPosition;
        var (width, height) = _canvasSize;

        relativeX = Mathf.Clamp(relativeX, 0, width) / width;
        relativeY = Mathf.Clamp(relativeY, 0, height) / height;

        return new Vector2(relativeX, relativeY);
    }
}