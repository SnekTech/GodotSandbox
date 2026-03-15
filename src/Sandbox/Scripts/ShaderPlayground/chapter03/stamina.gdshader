shader_type canvas_item;

uniform float u_progress: hint_range(0, 1);
uniform float u_radius: hint_range(0, 1) = 0.5;
uniform float u_width: hint_range(0, 1) = 0.5;
uniform vec3 u_bg_color: source_color;
uniform vec3 u_color: source_color;
uniform vec3 u_increase_color: source_color;
uniform float u_increase_progress: hint_range(0, 1);

float Circle(vec2 uv, float radius)
{
    return step( length( uv ), radius );
}

void fragment()
{
    vec2 uv = UV * 2. - 1.;
    vec4 originalColor = COLOR;
    
    // ring
    float innerCircle = Circle( uv, u_radius - u_width * 0.25 );
    float outerCircle = Circle( uv, u_radius );
    float ringMask = outerCircle * ( 1. - innerCircle );
    
    // progress
    float dial = atan( uv.y, -uv.x ) + PI / 2.;
    dial = mod( dial + TAU, TAU );
    float dial_mask = step( (1. - u_progress) * TAU, dial );
    
    float increase_mask = step( ( 1. - ( u_increase_progress ) ) * TAU, dial ) * ( 1. - dial_mask );
    COLOR = vec4( u_bg_color, ringMask );
    
    COLOR.rgb = mix( COLOR.rgb, u_increase_color, increase_mask );
    COLOR.rgb = mix( COLOR.rgb, u_color, dial_mask );
}