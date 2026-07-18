using Godot;
using System;

/// <summary>
/// グリッド座標
/// </summary>
public class GridPos
{
    public int X { get; set; }
    public int Y { get; set; }

    public GridPos(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static GridPos operator +(GridPos a, GridPos b)
    {
        return new GridPos(a.X + b.X, a.Y + b.Y);
    }

    public static GridPos operator -(GridPos a, GridPos b)
    {
        return new GridPos(a.X - b.X, a.Y - b.Y);
    }

    public static bool operator ==(GridPos a, GridPos b)
    {
        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator !=(GridPos a, GridPos b)
    {
        return a.X != b.X || a.Y != b.Y;
    }
}

public partial class Grid : Node2D
{
    [Export]
    ColorRect colorRect;

    public const float GRID_WIDTH = 100.0f;
    public const float GRID_HEIGHT = 30.0f;

    Vector2 windowSize = new Vector2(1920, 1080);

    public override void _Ready()
    {
        ShaderMaterial shaderMaterial = colorRect.Material as ShaderMaterial;
        shaderMaterial.SetShaderParameter("windowSize", windowSize);
    }

    public void SetCameraPosition(Vector2 position)
    {
        ShaderMaterial shaderMaterial = colorRect.Material as ShaderMaterial;
        shaderMaterial.SetShaderParameter("cameraPosition", position); // カメラの描画範囲の左上にくるように位置を調整
    }

    public void SetCameraZoom(float zoom)
    {
        ShaderMaterial shaderMaterial = colorRect.Material as ShaderMaterial;
        shaderMaterial.SetShaderParameter("cameraZoom", zoom);
    }
}
