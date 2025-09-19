shader_type canvas_item;
render_mode unshaded;

#include "res://ShaderLib/rect_lib.gdshaderinc"

uniform vec4 u_fromRect;
uniform vec4 u_focusRect;
uniform vec2 u_resolution;
uniform vec4 u_maskColor: source_color;
uniform float u_progress: hint_range(0, 1);

void fragment() {
    Rect fromRect = GetNormalized(Vec4ToRect(u_fromRect), u_resolution);
    Rect focusRect = GetNormalized(Vec4ToRect(u_focusRect), u_resolution);
    Rect actualRect = MixRect(fromRect , focusRect, u_progress);
    
    float focusAreaAlpha = 1.0 - DrawRect(actualRect, UV);
    
    COLOR = vec4(u_maskColor.rgb, focusAreaAlpha * u_maskColor.a);
}
