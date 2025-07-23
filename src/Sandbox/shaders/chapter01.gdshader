shader_type canvas_item;
render_mode unshaded;

uniform vec2 u_mouse;
uniform vec2 u_resolution;
uniform vec2 u_global_position;

const vec3 pointColor = vec3(0.58, 1.0, 0.352);
    
float plot(vec2 st, float y)
{
//     return smoothstep(percent - 0.02, percent, st.y)
//         - smoothstep(percent, percent + 0.02, st.y);
    return smoothstep(0.02, 0.0, abs(st.y - y));
}

float transformX(float x0)
{
    return x0;
}

float transformY(float y0)
{
    return -y0 + 1.0;
}

float fx(float x)
{
    return x;
}

float Square(float x0, float y0, float w, float h, vec2 st)
{
    float x = step(x0, st.x) * step(1.0 - x0 - w, 1.0 - st.x);
    float y = step(y0, st.y) * step(1.0 - y0 - h, 1.0 - st.y);
    
    return x * y;
}

vec3 Square(vec2 position, float width, vec2 st)
{
    
    return vec3(Square(position.x, position.y, width, width, st));
}


void fragment()
{
    vec3 color = vec3(0.0);
    
    vec2 startPosition = vec2(0.1, 0.2);
//     color += Square(startPosition, 0.5, UV);
    color += Square(0.2, 0.3, 0.5, 0.5, UV);
    color *= (1.0 - Square(0.3, 0.4, 0.1, 0.1, UV));
    
    COLOR = vec4(color, 1.0);
}