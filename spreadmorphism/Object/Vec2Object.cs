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

    Vec2Element element = new Vec2Element(new NumberElement(0), new NumberElement(0));

    public override void UpdateObject()
    {
        if (IsOneObject)
        {
            bool result = EvaluateFormula(this.Formula);
            this.SetIsError(!result);
        }
        else
        {
            ElementBase x = cell_x.Formula.Evaluate();
            ElementBase y = cell_y.Formula.Evaluate();
            if (x is NumberElement numX && y is NumberElement numY)
            {
                SetElement(new Vec2Element(numX, numY));
            }
            else
            {
                SetElement(new Vec2Element(new NumberElement(0), new NumberElement(0)));
            }
        }
    }

    public override List<Cell> GetCells()
    {
        return new List<Cell> { cell_x, cell_y };
    }

    public override ElementBase GetElement()
    {
        NumberElement x = new NumberElement(cell_x.Value);
        NumberElement y = new NumberElement(cell_y.Value);
        return new Vec2Element(x, y);
    }

    void SetElement(Vec2Element element)
    {
        this.element = element;
        cell_x.SetValue(element.X.Value);
        cell_y.SetValue(element.Y.Value);
    }

    protected override bool EvaluateFormula(Formula formula)
    {
        ElementBase result = formula.Evaluate();
        if (result is Vec2Element vec2Element)
        {
            SetElement(vec2Element);
            return true;
        }
        else
        {
            return false;
        }
    }
}
