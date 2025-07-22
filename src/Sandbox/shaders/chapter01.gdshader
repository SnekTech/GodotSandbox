shader_type canvas_item;
render_mode unshaded;

uniform vec2 u_mouse;
uniform vec2 u_resolution;
uniform vec2 u_global_position;

float plot(vec2 st, float y)
{
//     return smoothstep(percent - 0.02, percent, st.y)
//         - smoothstep(percent, percent + 0.02, st.y);
    return smoothstep(0.02, 0.0, abs(st.y - y));
}

float transformX(float x0)
{
    float x = x0;
    return x;
}

float transformY(float y0)
{
    float y = -y0 + 1.0 - 0.5;
    return y;
}

float fx(float x)
{
    return sin(x * PI);
}

void fragment()
{
    const vec3 pointColor = vec3(0.58, 1.0, 0.352);
    
    float x = transformX(UV.x);
    
    float y = fx(x);
    vec3 color = vec3(y);
    
    y = transformY(UV.y);
    
    float percent = plot(UV, y);
    color = mix(color, pointColor, percent);
    
    COLOR = vec4(color, 1.0);
}