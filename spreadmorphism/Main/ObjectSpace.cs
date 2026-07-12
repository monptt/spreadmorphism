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


    /// <summary>
    /// 生成されたオブジェクトのリスト
    /// </summary>
    List<ObjectBase> objects = new List<ObjectBase>();

    public NumberObject CreateNumberObject(int x, int y)
    {
        NumberObject cell = numberObjScene.Instantiate<NumberObject>();
        cell.Position = new Vector2(x * Grid.GRID_WIDTH, y * Grid.GRID_HEIGHT);
        AddChild(cell);
        numberObjects.Add(cell);
        objects.Add(cell);
        return cell;
    }

    public Vec2Object CreateVec2Object(int x, int y)
    {
        Vec2Object obj = vec2ObjScene.Instantiate<Vec2Object>();
        obj.Position = new Vector2(x * Grid.GRID_WIDTH, y * Grid.GRID_HEIGHT);
        AddChild(obj);
        objects.Add(obj);
        return obj;
    }

    public Vec3Object CreateVec3Object(int x, int y)
    {
        Vec3Object obj = vec3ObjScene.Instantiate<Vec3Object>();
        obj.Position = new Vector2(x * Grid.GRID_WIDTH, y * Grid.GRID_HEIGHT);
        AddChild(obj);
        objects.Add(obj);
        return obj;
    }

    /// <summary>
    /// クリックされたセルを取得する
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Cell OnCick(Vector2 position)
    {
        foreach (ObjectBase obj in objects)
        {
            foreach (Cell cell in obj.GetCells())
            {
                if (cell.IsClicked(position))
                {
                    return cell;
                }
            }
        }
        return null;
    }

    public Cell GetCell(GridPos pos)
    {
        foreach (ObjectBase obj in objects)
        {
            foreach (Cell cell in obj.GetCells())
            {
                if (cell.GetGridPos() == pos)
                {
                    return cell;
                }
            }
        }
        return null;
    }
}
