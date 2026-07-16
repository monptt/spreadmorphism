using Godot;
using System;
using System.Collections.Generic;

public enum ObjectType
{
    Number,
    Vec2,
    Vec3,
    String,
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
    /// 数式
    /// </summary>
    Formula formula = new Formula("");
    public Formula Formula => formula;

    bool isError = false;

    protected abstract bool EvaluateFormula(Formula formula);

    public abstract ElementBase GetElement();

    public void ForceUpdate()
    {
        if (IsOneObject)
        {
            bool result = EvaluateFormula(formula);
            SetIsError(!result);

            foreach (Cell cell in GetCells())
            {
                cell.SetIsError(false);
            }
        }
        else
        {
            foreach (Cell cell in GetCells())
            {
                cell.UpdateValue();
            }
        }
    }

    public void SetIsOneObject(bool isOneObject)
    {
        this.isOneObject = isOneObject;
        if (isOneObject)
        {
            foreach (Cell cell in GetCells())
            {
                cell.SetStatus(CellStatus.Dependent);

                cell.SetIsError(false);
            }
        }
        else
        {
            foreach (Cell cell in GetCells())
            {
                cell.SetStatus(CellStatus.Default);
            }

            this.SetIsError(false);
        }
    }

    public void SetIsError(bool isError)
    {
        this.isError = isError;
        if (isError)
        {
            this.Modulate = new Color(1, 0, 0, 0.8f);
        }
        else
        {
            this.Modulate = new Color(1, 1, 1, 1.0f);
        }
    }

    public void SetFormula(string formulaStr)
    {
        this.formula = new Formula(formulaStr);
        bool result = EvaluateFormula(formula);
        SetIsError(!result);
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
