using GodotGadgets.Extensions;

namespace Sandbox.Tutorial;

[SceneTree]
public partial class FocusMask : Control
{
    private static readonly StringName FocusRectUniform = "u_focusRect";
    private static readonly StringName ResolutionUniform = "u_resolution";

    private ShaderMaterial _focusMaskShader = null!;
    private Rect2 _focusArea;

    public override void _Ready()
    {
        _focusMaskShader = (ShaderMaterial)FocusMaskSprite.Material;
        var gridRect = _.Grid.GetRect();
        gridRect.DumpGd();

        _focusArea = gridRect.Grow(10);
        _focusArea.DumpGd();

        var resolution = GetViewportRect().Size;
        _focusMaskShader.SetShaderParameter(FocusRectUniform, _focusArea);
        _focusMaskShader.SetShaderParameter(ResolutionUniform, resolution);
    }
}