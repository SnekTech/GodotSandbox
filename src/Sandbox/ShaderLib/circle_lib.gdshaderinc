struct Circle {
    vec2 Position;
    float Radius;
};

Circle GetNormalized(Circle circle, vec2 resolution)
{
    float x = circle.Position.x / resolution.x;
    float y = circle.Position.y / resolution.y;
    float radius = circle.Radius / resolution.x;
    x = clamp(x, 0.0, 1.0);
    y = clamp(y, 0.0, 1.0);
    radius = clamp(radius, 0.0, 1.0);
    return Circle(vec2(x, y), radius);
}

float DrawCircle(Circle circle, in vec2 st)
{
    vec2 position = circle.Position;
    float r = circle.Radius;
    
    float dx = st.x - position.x;
    float dy = st.y - position.y;
    return step(dx * dx + dy * dy, r * r);
}

float DrawCircleFrame(Circle circle, in vec2 st)
{
    vec2 position = circle.Position;
    float radius = circle.Radius;
    float width = 0.005;
    
    float dx = st.x - position.x;
    float dy = st.y - position.y;
    float threshold = dx * dx + dy * dy;
    return smoothstep(threshold - width, threshold, radius * radius) -
            smoothstep(threshold, threshold + width, radius * radius);
}
