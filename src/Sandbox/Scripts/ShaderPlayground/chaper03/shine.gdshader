shader_type canvas_item;

uniform float u_angle: hint_range(0, 180);
uniform float u_thickness: hint_range(0, 1);
uniform vec3 u_shine_color: source_color;
uniform vec2 u_offset;

void fragment()
{
    vec2 uv = UV * 2. - 1.;
    float slope = tan( radians(u_angle) );
    float line = uv.y - u_offset.y - slope * ( uv.x - u_offset.x );
    float distance_to_line = abs( line ) / sqrt( 1. + slope * slope );
    float shine_mask = step( distance_to_line, u_thickness );
    
    COLOR.rgb = mix( COLOR.rgb, u_shine_color, shine_mask );
}