shader_type canvas_item;

uniform int pixel_size: hint_range(1, 32);

void fragment()
{
    vec2 texture_size_px = 1. / TEXTURE_PIXEL_SIZE;
    vec2 grid_dimension = texture_size_px / float( pixel_size );
    vec2 pixelated_uv = floor( UV * grid_dimension + 0.5 ) / grid_dimension;
    COLOR = texture( TEXTURE, pixelated_uv );
}