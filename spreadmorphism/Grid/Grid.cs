using Godot;
using System;

public partial class Grid : Node2D
{
    [Export]
    ColorRect colorRect;

    public const float GRID_WIDTH = 100.0f;
    public const float GRID_HEIGHT = 30.0f;

    public void SetCameraPosition(Vector2 position)
    {
        ShaderMaterial shaderMaterial = colorRect.Material as ShaderMaterial;
        shaderMaterial.SetShaderParameter("cameraPosition", position);
    }

    public void SetCameraZoom(float zoom)
    {
        ShaderMaterial shaderMaterial = colorRect.Material as ShaderMaterial;
        shaderMaterial.SetShaderParameter("cameraZoom", zoom);
    }
}
