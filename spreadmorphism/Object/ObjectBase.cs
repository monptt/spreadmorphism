using Godot;
using System;
using System.Collections.Generic;

public enum ObjectType
{
    Integer,
    Complex,
    Vec2,
    Vec3,
    String,
}

public abstract partial class ObjectBase
{
    /// <summary>
    /// オブジェクトの種類（型）
    /// </summary>
    public abstract ObjectType Type { get; }



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

    protected abstract void InitView();

    protected abstract bool EvaluateFormula(Formula formula);

    public abstract ElementBase GetElement();

    public ObjectViewBase ObjectView => objectView;
    ObjectViewBase objectView = null;


    public void SetUpObjectView(ObjectViewBase objectView)
    {
        this.objectView = objectView;
        InitView();
    }

    public ObjectBase()
    {

    }


    /// <summary>
    /// オブジェクト更新処理
    /// </summary>
    public abstract void UpdateObject();

    public void ForceUpdate()
    {
        UpdateObject();
    }

    public void SetIsOneObject(bool isOneObject)
    {
        this.isOneObject = isOneObject;
        if (isOneObject)
        {
            foreach (Cell cell in ObjectView.GetCells())
            {
                cell.SetStatus(CellStatus.Dependent);

                cell.SetIsError(false);
            }
        }
        else
        {
            foreach (Cell cell in ObjectView.GetCells())
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
            this.ObjectView.Modulate = new Color(1, 0, 0, 0.8f);
        }
        else
        {
            this.ObjectView.Modulate = new Color(1, 1, 1, 1.0f);
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
        int x = (int)(ObjectView.Position.X / Grid.GRID_WIDTH);
        int y = (int)(ObjectView.Position.Y / Grid.GRID_HEIGHT);
        return new GridPos(x, y);
    }

    /// <summary>
    /// 指定の座標がオブジェクトに含まれるかどうか
    /// </summary>
    /// <param name="gridPos"></param>
    /// <returns></returns>
    public bool IsContainPos(GridPos gridPos)
    {
        foreach (Cell cell in ObjectView.GetCells())
        {
            if (cell.GetGridPos() == gridPos)
            {
                return true;
            }
        }
        return false;
    }
}
