using Godot;
using System;

public partial class Grid : Node2D
{
    [Export]
    ColorRect colorRect;

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
