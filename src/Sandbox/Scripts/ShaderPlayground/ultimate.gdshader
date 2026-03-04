shader_type canvas_item;

uniform vec2 offset;
uniform float radius;

void vertex()
{
    float offsetX = abs(sin(TIME)) * 50.0;
    VERTEX = vec2(VERTEX + offset);
}

float CircleMask(vec2 uv, float r)
{
    float dx = uv.x - 0.5;
    float dy = uv.y - 0.5;
    return step(dx * dx + dy * dy, r * r);
}

void fragment()
{
    float circleMask = CircleMask(UV, radius);
    
    COLOR = vec4(COLOR.rgb, circleMask);
}