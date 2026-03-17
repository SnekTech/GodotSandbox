shader_type canvas_item;

uniform vec2 u_vector_input;
group_uniforms line;
uniform float u_line_width: hint_range(0, 1) = 0.5;
uniform vec3 u_line_color: source_color;
group_uniforms;

group_uniforms end_circle;
uniform float u_end_circle_radius: hint_range(0, 1);
uniform vec3 u_end_circle_color: source_color;
group_uniforms;

group_uniforms circle_frame;
uniform float u_circle_frame_thickness: hint_range(0, 1);
uniform float u_threshold1: hint_range(0, 1) = 0.7;
uniform vec3 u_circle_frame_color: source_color;
group_uniforms;

const float MaxLineWidth = 0.1;
const float MaxEndCircleRadius = 0.2;

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

float DrawCircle(vec2 uv, vec2 center, float radius)
{
    float d = distance( uv, center );
    return step( d, radius );
}

float DrawCircleFrame(vec2 uv, vec2 center, float radius, float thickness)
{
    const float MaxThickness = 0.1;
    float inner = DrawCircle( uv, center, radius );
    float outer = DrawCircle( uv, center, radius + thickness * MaxThickness );
    return Clamp01( outer * (1.0 - inner) );
}

void fragment()
{
    vec2 uv = UV * 2. - 1.;
    vec2 point_a = vec2( 0.0, 0.0 );
    vec2 point_b = u_vector_input;
    float lineSegmentMask = DrawLineSegment( uv, point_a, point_b );
    float endCircleMask = DrawCircle( uv, point_b, u_end_circle_radius * MaxEndCircleRadius );
    float circleFrameMask = DrawCircleFrame( uv, point_a, u_threshold1, u_circle_frame_thickness );
    
    COLOR.rgb = mix( COLOR.rgb, u_circle_frame_color, circleFrameMask );
    COLOR.rgb = mix( COLOR.rgb, u_line_color, lineSegmentMask );
    COLOR.rgb = mix( COLOR.rgb, u_end_circle_color, endCircleMask );
}