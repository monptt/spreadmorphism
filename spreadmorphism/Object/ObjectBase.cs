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

    /// <summary>
    /// 全体を1つのオブジェクトとして扱うかどうか
    /// </summary>
    public bool IsOneObject => isOneObject;
    bool isOneObject = false;

    /// <summary>
    /// 値の文字列
    /// </summary>
    string valueStr = "";

    public string ValueStr => valueStr;

    protected abstract void ParseValueStr(string valueStr);

    public void SetIsOneObject(bool isOneObject)
    {
        this.isOneObject = isOneObject;
        if (isOneObject)
        {
            foreach (Cell cell in GetCells())
            {
                cell.SetStatus(CellStatus.Dependent);
            }
        }
        else
        {
            foreach (Cell cell in GetCells())
            {
                cell.SetStatus(CellStatus.Default);
            }
        }
    }

    public void SetValueStr(string valueStr)
    {
        this.valueStr = valueStr;
        ParseValueStr(valueStr);
    }

    public GridPos GetGridPos()
    {
        int x = (int)(Position.X / Grid.GRID_WIDTH);
        int y = (int)(Position.Y / Grid.GRID_HEIGHT);
        return new GridPos(x, y);
    }

    /// <summary>
    /// 指定の座標がオブジェクトに含まれるかどうか
    /// </summary>
    /// <param name="gridPos"></param>
    /// <returns></returns>
    public bool IsContainPos(GridPos gridPos)
    {
        foreach (Cell cell in GetCells())
        {
            if (cell.GetGridPos() == gridPos)
            {
                return true;
            }
        }
        return false;
    }
}
