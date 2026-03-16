struct Rect {
    vec2 Position;
    vec2 Size;
};

float Clamp01(float value)
{
    return clamp(value, 0.0, 1.0);
}

Rect Vec4ToRect(in vec4 u_rect)
{
    return Rect(u_rect.xy, u_rect.zw);
}

vec4 RectToVec4(in Rect rect)
{
    return vec4(rect.Position, rect.Size);
}

Rect GetNormalized(Rect rect, vec2 resolution)
{
    float x = rect.Position.x / resolution.x;
    float y = rect.Position.y / resolution.y;
    float width = rect.Size.x / resolution.x;
    float height = rect.Size.y / resolution.y;
    x = Clamp01(x);
    y = Clamp01(y);
    width = Clamp01(width);
    height = Clamp01(height);
    return Rect(vec2(x, y), vec2(width, height));
}

Rect MixRect(in Rect a, in Rect b, float progress)
{
    vec4 mixed = mix(RectToVec4(a), RectToVec4(b), progress);
    return Vec4ToRect(mixed);
}

float Square(float x0, float y0, float w, float h, vec2 st)
{
    float x = step( x0, st.x ) * step( st.x, x0 + w );
    float y = step( y0, st.y ) * step( st.y, y0 + h );

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
