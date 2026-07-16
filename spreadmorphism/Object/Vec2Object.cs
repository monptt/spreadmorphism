using Godot;
using System;
using System.Collections.Generic;

public partial class Vec2Object : ObjectBase
{
    [Export]
    Cell cell_x;

    [Export]
    Cell cell_y;

    public override ObjectType Type => ObjectType.Vec2;

    Vec2Element element = null;

    public override List<Cell> GetCells()
    {
        return new List<Cell> { cell_x, cell_y };
    }

    public override ElementBase GetElement()
    {
        return element;
    }

    void SetElement(Vec2Element element)
    {
        this.element = element;
        cell_x.SetValue(element.X.Value);
        cell_y.SetValue(element.Y.Value);
    }

    protected override bool EvaluateFormula(Formula formula)
    {

        string formulaStr = formula.FormulaStr;
        if (formulaStr == "")
        {
            return false;
        }

        // [x,y] みたいな形式を読みたい（仮）
        if (formulaStr[0] == '[' && formulaStr[formulaStr.Length - 1] == ']')
        {
            formulaStr = formulaStr.Substring(1, formulaStr.Length - 2);
        }

        string[] values = formulaStr.Split(',');
        if (values.Length == 2)
        {
            GridPos targetPos = new GridPos(int.Parse(values[0]), int.Parse(values[1]));

            ObjectBase obj = ObjectSpace.Instance.GetObject(targetPos);
            if (obj is Vec2Object targetObj)
            {
                this.SetElement(targetObj.element);
            }
            return true;
        }

        return false;
    }
}
