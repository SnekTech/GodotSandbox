shader_type canvas_item;
render_mode unshaded;

uniform vec4 u_fromRect;
uniform vec4 u_focusRect;
uniform vec2 u_resolution;
uniform vec4 u_maskColor: source_color;
uniform float u_progress: hint_range(0, 1);

vec4 GetNormalized(vec4 rect, vec2 resolution)
{
    float x = rect.x / resolution.x;
    float y = rect.y / resolution.y;
    float width = rect.z / resolution.x;
    float height = rect.w / resolution.y;
    x = clamp(x, 0.0, 1.0);
    y = clamp(y, 0.0, 1.0);
    width = clamp(width, 0.0, 1.0);
    height = clamp(height, 0.0, 1.0);
    return vec4(x, y, width, height);
}


float Square(float x0, float y0, float w, float h, vec2 st)
{
    float x = step(x0, st.x) * step(1.0 - x0 - w, 1.0 - st.x);
    float y = step(y0, st.y) * step(1.0 - y0 - h, 1.0 - st.y);

    return x * y;
}

float Square(vec4 rect, vec2 st)
{
    return Square(rect.x, rect.y, rect.z, rect.w, st);
}

void fragment() {
    vec4 normalizedFromRect = GetNormalized(u_fromRect, u_resolution);
    vec4 normalizedFocusRect = GetNormalized(u_focusRect, u_resolution);
    vec4 actualRect = mix(normalizedFromRect , normalizedFocusRect, u_progress);
    float focusAreaAlpha = 1.0 - Square(actualRect, UV);
    COLOR = vec4(u_maskColor.rgb, focusAreaAlpha * u_maskColor.a);
}
