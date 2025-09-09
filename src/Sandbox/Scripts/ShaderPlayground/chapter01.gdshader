shader_type canvas_item;
render_mode unshaded;

uniform vec2 u_mouse;
uniform vec2 u_resolution;
uniform vec2 u_global_position;

const vec3 pointColor = vec3(0.58, 1.0, 0.352);

void fragment()
{
    COLOR = vec4(1.0,0.0,1.0,1.0);
}