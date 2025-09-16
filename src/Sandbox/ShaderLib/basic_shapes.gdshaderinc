struct Rect {
    vec2 Position;
    vec2 Size;
};

Rect GetNormalized(Rect rect, vec2 resolution)
{
    float x = rect.Position.x / resolution.x;
    float y = rect.Position.y / resolution.y;
    float width = rect.Size.x / resolution.x;
    float height = rect.Size.y / resolution.y;
    x = clamp(x, 0.0, 1.0);
    y = clamp(y, 0.0, 1.0);
    width = clamp(width, 0.0, 1.0);
    height = clamp(height, 0.0, 1.0);
    return Rect(vec2(x, y), vec2(width, height));
}

float Square(float x0, float y0, float w, float h, vec2 st)
{
    float x = step(x0, st.x) * step(1.0 - x0 - w, 1.0 - st.x);
    float y = step(y0, st.y) * step(1.0 - y0 - h, 1.0 - st.y);

    return x * y;
}

float DrawRect(Rect rect, vec2 st)
{
    vec2 position = rect.Position;
    vec2 size = rect.Size;
    
    return Square(position.x, position.y, size.x, size.y, st);
}

float DrawRectFrame(Rect rect, vec2 st)
{
    float r = 0.01;
    vec2 position = rect.Position;
    vec2 size = rect.Size;
    float inOuterRect = Square(position.x, position.y, size.x, size.y, st);
    float inInnerRect = Square(position.x + r, position.y + r, size.x - r * 2.0, size.y - r * 2.0, st);

    return inOuterRect * (1.0 - inInnerRect);
}
