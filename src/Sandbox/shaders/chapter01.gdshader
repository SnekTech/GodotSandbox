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

float circle(in vec2 _st, in float _radius){
    vec2 l = _st-vec2(0.5);
    return 1.-smoothstep(_radius-(_radius*0.01),
                         _radius+(_radius*0.01),
                         dot(l,l)*4.0);
}

mat2 rotate2d(float _angle){
//     return mat2(cos(_angle),-sin(_angle),
//                 sin(_angle),cos(_angle));

    return mat2( vec2( cos(_angle), sin(_angle) ), vec2( -sin(_angle), cos(_angle) ) );
}
mat2 scale(vec2 _scale){
//     return mat2(_scale.x,0.0,
//                 0.0,_scale.y);
    return mat2( vec2(_scale.x, 0.0), vec2(0.0, _scale.y) );
}

vec2 tile(vec2 st, float n)
{
    return st * n;
}

void fragment()
{
    vec3 color = vec3(0.0);
    
    float percent = 0.0;
    vec2 st = tile(UV, 3.0);
    st = fract(st);
    
    color = vec3(st, 0.0);
    color = vec3(circle(st, 0.5) );
    
    COLOR = vec4(color, 1.0);
}