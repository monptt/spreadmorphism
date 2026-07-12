using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// セルなどのオブジェクトを配置する空間ノード
/// </summary>
public partial class ObjectSpace : Node2D
{
    [Export]
    PackedScene numberObjScene;

    [Export]
    PackedScene vec2ObjScene;

    [Export]
    PackedScene vec3ObjScene;

    List<NumberObject> numberObjects = new List<NumberObject>();

    public NumberObject CreateNumberObject(int x, int y)
    {
        NumberObject cell = numberObjScene.Instantiate<NumberObject>();
        cell.Position = new Vector2(x * Grid.GRID_WIDTH, y * Grid.GRID_HEIGHT);
        AddChild(cell);
        numberObjects.Add(cell);
        return cell;
    }

    public Vec2Object CreateVec2Object(int x, int y)
    {
        Vec2Object obj = vec2ObjScene.Instantiate<Vec2Object>();
        obj.Position = new Vector2(x * Grid.GRID_WIDTH, y * Grid.GRID_HEIGHT);
        AddChild(obj);
        return obj;
    }

    public Vec3Object CreateVec3Object(int x, int y)
    {
        Vec3Object obj = vec3ObjScene.Instantiate<Vec3Object>();
        obj.Position = new Vector2(x * Grid.GRID_WIDTH, y * Grid.GRID_HEIGHT);
        AddChild(obj);
        return obj;
    }

    public Cell OnCick(Vector2 position)
    {
        foreach (NumberObject obj in numberObjects)
        {
            if (obj.GetCell().IsClicked(position))
            {
                return obj.GetCell();
            }
        }
        return null;
    }
}
