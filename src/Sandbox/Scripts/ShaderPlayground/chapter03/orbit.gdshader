shader_type canvas_item;

group_uniforms earth;
uniform float u_earth_radius: hint_range(0.005, 0.2) = 0.1;
uniform vec3 u_earth_color: source_color;
group_uniforms;

group_uniforms moon;
uniform float u_moon_distance: hint_range(0, 0.5) = 0.4;
uniform float u_moon_radius: hint_range(0.005, 0.2) = 0.1;
uniform vec3 u_moon_color: source_color;
group_uniforms;

group_uniforms tiny_moon;
uniform float u_tiny_moon_distance: hint_range(0, 0.5) = 0.4;
uniform float u_tiny_moon_radius: hint_range(0.005, 0.2) = 0.1;
uniform vec3 u_tiny_moon_color: source_color;
group_uniforms;

void fragment()
{
    vec2 uv = UV * 2. - 1.;
    
    // earth
    float earth_mask = step( length(uv), u_earth_radius );
    COLOR.a = earth_mask;
    COLOR.rgb = mix( COLOR.rgb, u_earth_color, earth_mask );
    
    // moon
    float angle = TIME;
    float moon_x = u_moon_distance * cos(angle);
    float moon_y = u_moon_distance * sin(angle);
    vec2 moon_center = vec2( moon_x, moon_y );
    float moon_mask = step( distance( uv, moon_center ),  u_moon_radius);
    COLOR.a += moon_mask;
    COLOR.rgb = mix( COLOR.rgb, u_moon_color, moon_mask );
    
    // tiny moon
    float tiny_moon_angle = angle * 2. + PI / 4.;
    float tiny_moon_x = moon_center.x + u_tiny_moon_distance * cos(tiny_moon_angle);
    float tiny_moon_y = moon_center.y + u_tiny_moon_distance * sin(tiny_moon_angle);
    vec2 tiny_moon_center = vec2( tiny_moon_x, tiny_moon_y );
    float tiny_moon_mask = step( distance( uv, tiny_moon_center ), u_tiny_moon_radius );
    COLOR.a += tiny_moon_mask;
    COLOR.rgb = mix( COLOR.rgb, u_tiny_moon_color, tiny_moon_mask );
}