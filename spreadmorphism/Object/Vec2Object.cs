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
        ElementBase element = formula.Evaluate(formula.Tokenize(formula.FormulaStr));
        if (element is Vec2Element vec2Element)
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
