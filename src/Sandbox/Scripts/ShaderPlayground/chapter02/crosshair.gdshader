shader_type canvas_item;

uniform float u_center_size: hint_range(0.0, 1.0) = 0.5;
uniform float u_outer_radius: hint_range(0.0, 1.0) = 0.7;
uniform float u_ring_width: hint_range(0.0, 1.0) = 0.3;
uniform float u_cutout_width: hint_range(0.0, 1.0) = 0.5;
uniform float u_crosshair_distance: hint_range(0., 1.) = 0.25;
uniform float u_crosshair_length: hint_range(0., 1.) = 0.5;
uniform float u_crosshair_width: hint_range(0., 1.) = 0.5;

float clamp01(float value)
{
    return clamp(value, 0., 1.);
}

float CreateCircle(vec2 uv, float radius)
{
    return step(distance(uv, vec2(0.5)), radius);
}

float CreateLines(vec2 uv)
{
    vec2 centerDistance = vec2(0.5) + u_crosshair_distance;
    float hWidth = step(distance(uv.x, centerDistance.x), u_crosshair_length * 0.25)
                + step(distance(1. - uv.x, centerDistance.x), u_crosshair_length * 0.25);
    float hHeight = step(distance(uv.y, 0.5), u_crosshair_width * 0.02);
    float vHeight = step(distance(uv.y, centerDistance.y), u_crosshair_length * 0.25)
                + step(distance(1. - uv.y, centerDistance.y), u_crosshair_length * 0.25);
    float vWidth = step(distance(uv.x, 0.5), u_crosshair_width * 0.02);

    
    return clamp01( hWidth * hHeight + vHeight * vWidth );
}

float CreateCrosshair(vec2 uv, float outerRadius, float width)
{
    float centerCircle = CreateCircle(uv, u_center_size * 0.1);

    float outerCircle = CreateCircle(uv, outerRadius);
    float innerCircle = CreateCircle(uv, outerRadius - width);
    float ring = outerCircle - innerCircle;
    
    float crossCutout = clamp01( 
                        step(distance(uv.x, 0.5), u_cutout_width * 0.1) + 
                        step(distance(uv.y, 0.5), u_cutout_width * 0.1) );
    
    return centerCircle + clamp01( ring - crossCutout ) + CreateLines(uv);
}

void fragment()
{
    COLOR.rbg = vec3(1.);
    
    COLOR.a = CreateCrosshair(UV, u_outer_radius * 0.5, u_ring_width * 0.1);
}