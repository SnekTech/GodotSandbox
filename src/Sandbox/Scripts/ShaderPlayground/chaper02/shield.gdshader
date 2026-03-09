shader_type canvas_item;

const vec2 center = vec2(0.5);

uniform float u_radius: hint_range(0., 1.) = 0.5;
uniform float u_width: hint_range(0., 1.) = 0.5;
uniform vec2 u_highlight_center = vec2(0.25, 0.15);
uniform float u_highlight_alpha: hint_range(0., 1.) = 0.5;

uniform vec3 u_main_color: source_color;
uniform vec3 u_highlight_color: source_color;
uniform float u_glow: hint_range(0., 1.) = 0.5;
uniform sampler2D u_noise: repeat_enable;

void vertex()
{
    VERTEX += normalize(VERTEX) * 50. * sin(TIME) * 0.1;
}

void fragment()
{
    // main shape
    float circleMain = step( distance( UV, center ), u_radius );
    float circleInner = smoothstep( u_width * 0.5, 1., distance( UV, center ) );
    float mainShape = circleMain * circleInner;
    
    // highlight shape
    float highlightShape = pow( 1. - distance( UV, u_highlight_center ), 3. ) * u_highlight_alpha * mainShape;
    
    // colors
    vec3 color = u_main_color * mainShape + u_highlight_color * highlightShape;
    color *= vec3(1. + u_glow * 5.);
    
    // texture
    float texture_alpha = texture( u_noise, UV + vec2( TIME * 0.1, 0. ) ).r * 3.;
    
    COLOR.a = ( highlightShape + mainShape ) * texture_alpha;
    COLOR.rgb = color;
}