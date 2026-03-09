shader_type canvas_item;

uniform vec2 u_offset;

void fragment()
{
    // scale & translate
//     vec2 scaled_uv = UV * 2. - u_offset;
//     vec4 scaled_texture_color = texture(TEXTURE, scaled_uv);
//     vec2 mask = step( vec2( 0. ), scaled_uv ) * step( scaled_uv, vec2( 1. ) );
//     scaled_texture_color.a *= mask.x * mask.y;
//     COLOR = scaled_texture_color;

    // centering
//     vec2 uv = UV * 2. - 1.;
// //     COLOR.a = step( length(uv), 0.5 );
//     COLOR.a = step( distance( abs(uv.x), 0.5 ), 0.1 );
//     COLOR.rgb = vec3( uv, 0 );

    // basic pixelate
//     vec2 grid = vec2( 4., 3. );
//     vec2 pixelated_uv = floor( UV * grid ) / grid;
//     COLOR.rgb = vec3( pixelated_uv, 0 );

    // checkerboard
    vec2 grid = floor( UV * 4. );
    float checker = mod( grid.x + grid.y, 2. );
    COLOR.a = checker;
    COLOR.rgb = vec3( grid, 0 );
}