shader_type canvas_item;

uniform vec2 cameraPosition;	// ここではカメラの左上座標
uniform float cameraZoom = 1.0;

uniform vec2 windowSize;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	// Called for every pixel the material is visible on.
	vec4 bgColor = vec4(vec3(0.4), 1.0);
	vec4 gridColor = vec4(vec3(0.2), 1.0);

	// カメラがグリッドオブジェクトの中央に居る前提
	vec2 p = FRAGCOORD.xy; // ウィンドウ基準の座標
	// をオブジェクトスペースにおける座標に変換する
	p -= windowSize / 2.0;
	p /= cameraZoom;
	p += cameraPosition;

	float lineWidth = 1.0;
	float gridHeight = 30.0;
	float gridWidth = 100.0;

	bool isGrid = mod(p.x, gridWidth) > gridWidth - lineWidth || mod(p.y, gridHeight) > gridHeight - lineWidth;

	COLOR = isGrid ? gridColor : bgColor;
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
