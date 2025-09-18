shader_type canvas_item;
render_mode unshaded;

#include "res://ShaderLib/rect_lib.gdshaderinc"
#include "res://ShaderLib/circle_lib.gdshaderinc"

uniform vec2 u_mouse;
uniform vec2 u_resolution;
uniform vec2 u_global_position;

const vec3 pointColor = vec3(0.58, 1.0, 0.352);
const vec3 colorA = vec3(0.149,0.141,0.912);
const vec3 colorB = vec3(1.000,0.833,0.224);

vec2 FlipY(vec2 uv)
{
    return vec2(uv.x, 1.0f - uv.y);
}

vec2 FlipFragCoord(vec2 fragCoord)
{
    return vec2(fragCoord.x, u_resolution.y - fragCoord.y);
}

float Plot(vec2 st)
{
    return smoothstep(0.02, 0.0, abs(st.y - st.x));
}

float Plot(vec2 st, float pct)
{
    return smoothstep(pct-0.02, pct, st.y) -
            smoothstep(pct, pct+0.02, st.y);
}

void fragment()
{
    vec2 mouse = FlipY(u_mouse);
    vec2 st = FlipY(UV);
    float pct = 0.0;
    vec3 color = vec3(0.0);

    Circle circle = Circle(vec2(0.4, 0.4), 0.3);
    pct = DrawCircleFrame(circle, st);

    color = vec3(pct);

    COLOR = vec4(color, 1.0);
}
