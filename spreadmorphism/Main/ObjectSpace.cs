using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// セルなどのオブジェクトを配置する空間ノード
/// </summary>
public partial class ObjectSpace : Node2D
{
    [Export]
    PackedScene cellMatrixObjScene;

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
        Vector2I matrixSize = new Vector2I(1, 1);
        string name = "";
        switch (type)
        {
            case ObjectType.Number:
                obj = new NumberObject();
                matrixSize = new Vector2I(1, 1);
                name = "Num";
                break;
            case ObjectType.Vec2:
                obj = new Vec2Object();
                matrixSize = new Vector2I(1, 2);
                name = "Vec2";
                break;
            case ObjectType.Vec3:
                obj = new Vec3Object();
                matrixSize = new Vector2I(1, 3);
                name = "Vec3";
                break;
            case ObjectType.String:
                obj = new StringObject();
                matrixSize = new Vector2I(1, 1);
                name = "String";
                break;
            default:
                break;
        }

        if (obj != null)
        {
            objects.Add(obj);

            // オブジェクトビューを作成
            CellMatrixObjectView objView = cellMatrixObjScene.Instantiate<CellMatrixObjectView>();
            objView.Init(matrixSize.X, matrixSize.Y, pos, name);
            AddChild(objView);

            // オブジェクトに紐づけ
            obj.SetUpObjectView(objView);
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
            foreach (Cell cell in obj.ObjectView.GetCells())
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
            foreach (Cell cell in obj.ObjectView.GetCells())
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
