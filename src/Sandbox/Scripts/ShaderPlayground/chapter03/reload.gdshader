shader_type canvas_item;

uniform float u_reload: hint_range(0, 1);
uniform vec4 u_color: source_color;
uniform float u_ripple_progress: hint_range(0, 1);
uniform float u_alpha: hint_range(0, 1) = 0.7;

void fragment()
{
    vec2 uv = UV * 2. - 1.;
    vec4 originalColor = COLOR;
    
    // ripple
    float ripple_mask = step( length( uv ), u_ripple_progress * 1.5 );
    
    // reload
    float dial = PI + atan( uv.y, uv.x ) - PI / 2.;
    dial = mod( dial + TAU, TAU );
    float dial_mask = step( u_reload * TAU, dial );
    float main_mask = ripple_mask * dial_mask;
    
    COLOR = mix(originalColor, u_color, main_mask * u_alpha);
}