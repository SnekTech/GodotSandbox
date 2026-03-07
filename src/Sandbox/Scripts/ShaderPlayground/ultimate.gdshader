shader_type canvas_item;

uniform vec2 offset;
uniform float radius;
uniform float diagonal_progress : hint_range(0.0, 1.0);

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

float Diagonal(vec2 uv, float progress)
{
    return step(progress, (uv.x + uv.y) * 0.5);
}

void fragment()
{
    float circleMask = CircleMask(UV, radius);
    
    COLOR.a = Diagonal(UV, diagonal_progress);
}