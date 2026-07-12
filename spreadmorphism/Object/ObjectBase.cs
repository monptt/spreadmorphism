using Godot;
using System;
using System.Collections.Generic;

public enum ObjectType
{
    Number = 0,
    Vec2 = 1,
    Vec3 = 2,
}

public abstract partial class ObjectBase : Node2D
{
    /// <summary>
    /// オブジェクトの種類（型）
    /// </summary>
    public abstract ObjectType Type { get; }

    public abstract List<Cell> GetCells();

    public GridPos GetGridPos()
    {
        int x = (int)(Position.X / Grid.GRID_WIDTH);
        int y = (int)(Position.Y / Grid.GRID_HEIGHT);
        return new GridPos(x, y);
    }
}
