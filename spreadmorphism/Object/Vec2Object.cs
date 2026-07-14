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

    public override List<Cell> GetCells()
    {
        return new List<Cell> { cell_x, cell_y };
    }

    void SetValue(int x, int y)
    {
        cell_x.SetValue(x);
        cell_y.SetValue(y);
    }

    protected override void EvaluateFormula(Formula formula)
    {

        string formulaStr = formula.FormulaStr;
        if (formulaStr == "")
        {
            return;
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
                this.SetValue(targetObj.cell_x.Value, targetObj.cell_y.Value);
            }
        }
    }
}
