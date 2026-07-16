using Godot;
using System;
using System.Collections.Generic;

public partial class NumberObject : ObjectBase
{
    [Export]
    Cell cell;

    public override ObjectType Type => ObjectType.Number;

    NumberElement element = new NumberElement(0);

    protected override bool EvaluateFormula(Formula formula)
    {
        ElementBase result = formula.Evaluate(formula.Tokenize(formula.FormulaStr));

        if (result is NumberElement numberElement)
        {
            SetElement(numberElement);
            return true;
        }
        else
        {
            return false;
        }
    }

    public override ElementBase GetElement()
    {
        return element;
    }

    public override List<Cell> GetCells()
    {
        return new List<Cell> { cell };
    }

    public int Value => cell.Value;

    public override void _Ready()
    {
        this.SetIsOneObject(true);
        SetElement(new NumberElement(0));
        cell.SetFormula("0");
        this.SetFormula("0");
    }

    void SetElement(NumberElement element)
    {
        this.element = element;
        cell.SetValue(element.Value);
    }

    public Cell GetCell()
    {
        return cell;
    }
}
