using Godot;
using System;
using System.Collections.Generic;

public partial class NumberObject : ObjectBase
{
    [Export]
    Cell cell;

    public override ObjectType Type => ObjectType.Number;

    NumberElement element = null;

    protected override void EvaluateFormula(Formula formula)
    {
        ElementBase element = formula.Evaluate(formula.Tokenize(formula.FormulaStr));
        GD.Print(formula.FormulaStr);
        if (element is NumberElement numberElement)
        {
            GD.Print($"numberElement: {numberElement.Value}");
            SetElement(numberElement);
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
