shader_type canvas_item;

uniform vec4 u_line_color: source_color;
uniform float u_line_thickness: hint_range(0., 1.) = 0.5;

const float MAX_LINE_THICKNESS = 2.;

void fragment()
{
    vec4 color = texture(TEXTURE, UV);
    vec2 offset = TEXTURE_PIXEL_SIZE * u_line_thickness * MAX_LINE_THICKNESS;
    float outline = color.a;
    
    vec2 offsetsInAllDirections[8] = 
    {
        vec2( offset.x, 0 ),
        vec2( -offset.x, 0 ),
        vec2( 0, offset.y ),
        vec2( 0, -offset.y ),
        vec2( offset.x, offset.y ),
        vec2( -offset.x, offset.y ),
        vec2( offset.x, -offset.y ),
        vec2( -offset.x, -offset.y )
    };
    
    for (int i = 0; i < 8; i++)
    {
        outline += texture( TEXTURE, UV + offsetsInAllDirections[i] ).a;
    }

    outline = min(outline, 1.);
    
    COLOR = mix(color, u_line_color, outline - color.a);
}