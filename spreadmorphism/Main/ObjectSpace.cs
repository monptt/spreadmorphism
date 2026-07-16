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

    [Export]
    PackedScene stringObjScene;

    static ObjectSpace instance = null;
    public static ObjectSpace Instance => instance;

    /// <summary>
    /// 生成されたオブジェクトのリスト
    /// </summary>
    List<ObjectBase> objects = new List<ObjectBase>();
    public List<ObjectBase> Objects => objects;



    public override void _Ready()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            GD.PrintErr("ObjectSpace already exists");
        }
    }

    public override void _Process(double delta)
    {
        // すべてのオブジェクト(セル)の値を更新する
        //@todo: 必要なときだけにする
        UpdateAllObjects();
    }

    /// <summary>
    /// すべてのセルの値を更新する
    /// </summary>
    public void UpdateAllObjects()
    {
        foreach (ObjectBase obj in objects)
        {
            obj.ForceUpdate();
        }
    }

    public void CreateObject(ObjectType type, GridPos pos)
    {
        ObjectBase obj = null;
        switch (type)
        {
            case ObjectType.Number:
                obj = numberObjScene.Instantiate<NumberObject>();
                break;
            case ObjectType.Vec2:
                obj = vec2ObjScene.Instantiate<Vec2Object>();
                break;
            case ObjectType.Vec3:
                obj = vec3ObjScene.Instantiate<Vec3Object>();
                break;
            case ObjectType.String:
                obj = stringObjScene.Instantiate<StringObject>();
                break;
            default:
                break;
        }

        if (obj != null)
        {
            obj.Position = new Vector2(pos.X * Grid.GRID_WIDTH, pos.Y * Grid.GRID_HEIGHT);
            AddChild(obj);
            objects.Add(obj);
        }
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

    /// <summary>
    /// 指定されたグリッド座標にあるオブジェクトを取得する
    /// </summary>
    /// <param name="pos">グリッド座標</param>
    /// <returns>オブジェクト</returns>
    public ObjectBase GetObject(GridPos pos)
    {
        foreach (ObjectBase obj in objects)
        {
            if (obj.IsContainPos(pos))
            {
                return obj;
            }
        }
        return null;
    }
}
