shader_type canvas_item;

uniform sampler2D u_noise;
uniform float u_progress: hint_range(0, 1);
uniform float u_gradient_progress: hint_range(0, 1);
uniform vec3 u_dissolve_color: source_color;

void fragment()
{
    float noise_alpha = texture(u_noise, UV).r;
    COLOR.a = step( u_progress, noise_alpha );
    COLOR.a += 1. - smoothstep( 0., noise_alpha, u_gradient_progress );
    COLOR.rgb = step( u_progress, noise_alpha ) < 1.0 ? u_dissolve_color : COLOR.rgb;
    
    COLOR.a = min( COLOR.a, texture( TEXTURE, UV ).a );
}