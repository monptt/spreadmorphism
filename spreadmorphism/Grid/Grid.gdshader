shader_type canvas_item;

uniform vec2 cameraPosition;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	// Called for every pixel the material is visible on.
	vec4 bgColor = vec4(vec3(0.4), 1.0);
	vec4 gridColor = vec4(vec3(0.2), 1.0);

	vec2 p = FRAGCOORD.xy + cameraPosition;

	float lineWidth = 1.0;
	float gridHeight = 30.0;
	float gridWidth = 100.0;

	bool isGrid = mod(p.x, gridWidth) < lineWidth || mod(p.y, gridHeight) < lineWidth;

	COLOR = isGrid ? gridColor : bgColor;
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
