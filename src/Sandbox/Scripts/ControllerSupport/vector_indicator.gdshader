shader_type canvas_item;

uniform vec2 u_vector_input;
uniform float u_line_width: hint_range(0, 1) = 0.5;
uniform vec3 u_line_color: source_color;

const float MaxLineWidth = 0.1;

float Clamp01(float value)
{
    return clamp(value, 0.0, 1.0);
}

float DrawLineSegment(vec2 uv, vec2 point_a, vec2 point_b)
{
    vec2 ab = point_b - point_a;
    vec2 ap = uv - point_a;
    float ab_sq = dot( ab, ab );
    
    float t = 0.0; // 避免除以 0
    if ( ab_sq > 0.0 )
    {
        t = dot( ap, ab ) / ab_sq;
        t = Clamp01( t );
    }
    
    vec2 projection = point_a + ab * t;
    float dist = distance( uv, projection );
    
    return step( dist, u_line_width * MaxLineWidth );
}

void fragment()
{
    vec2 uv = UV * 2. - 1.;
    float line_segment_mask = DrawLineSegment( uv, vec2( 0.0, 0.0 ), u_vector_input );
    COLOR.rgb = mix( COLOR.rgb, u_line_color, line_segment_mask );
}