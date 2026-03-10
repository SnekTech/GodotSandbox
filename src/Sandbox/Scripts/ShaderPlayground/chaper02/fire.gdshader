shader_type canvas_item;

uniform float u_width: hint_range(0, 1);
uniform float u_size: hint_range(0, 1) = 0.75;
uniform float u_blur: hint_range(0, 1) = 0.1;
uniform sampler2D u_fire_noise: repeat_enable;
uniform float u_flicker: hint_range(0, 1);
uniform vec3 u_main_color: source_color;
uniform vec3 u_mid_color: source_color;
uniform vec3 u_inner_color: source_color;

vec2 pixelate(vec2 uv)
{
    vec2 grid = vec2(16.0);
    return floor( uv * grid ) / grid;
}

void fragment()
{
//     vec2 pixelated_uv = pixelate(UV);
    
    // shape
    vec2 uv = UV * 2. - 1.;
    float ellipse = length( vec2( uv.x * ( 2. - u_width ), uv.y ) );
    float falloff = smoothstep( -0.6, 0.5, uv.y );
    float shape = ( 1. - smoothstep( u_size - u_blur, u_size, ellipse ) ) * falloff;
    
    // texture
    vec2 moving_uv = vec2( UV.x, UV.y + TIME * u_flicker );
    float fire_noise = texture(u_fire_noise, moving_uv).r;
    
    // fire shape
    float fire_main_shape = step( 0.3, shape * 0.3 + fire_noise * 0.3 );
    float fire_mid_shape = step( 0.4, shape * 0.3 + fire_noise * 0.4 );
    float fire_inner_shape = step( 0.5, shape * 0.3 + fire_noise * 0.5 );
    COLOR.a = fire_main_shape;
    
    float outer_mask = fire_main_shape - fire_mid_shape;
    float mid_mask = fire_mid_shape - fire_inner_shape;
    
    
    COLOR.rgb = outer_mask * u_main_color + mid_mask * u_mid_color + fire_inner_shape * u_inner_color;
}