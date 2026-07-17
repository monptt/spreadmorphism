using Godot;
using System;
using System.Collections.Generic;

public partial class StringObject : ObjectBase
{
    [Export]
    Cell cell;

    public override ObjectType Type => ObjectType.String;

    StringElement element = null;

    public override void _Ready()
    {
        SetIsOneObject(true);
        SetElement(new StringElement(""));
    }

    protected override void Init()
    {
        cell.SetFormula("");
    }

    public override void UpdateObject()
    {
        if (IsOneObject)
        {
            bool result = EvaluateFormula(this.Formula);
            this.SetIsError(!result);
        }
        else
        {
            ElementBase element = cell.Formula.Evaluate();
            if (element is StringElement stringElement)
            {
                SetElement(stringElement);
            }
            else
            {
                SetElement(new StringElement(""));
            }
        }
    }

    public override List<Cell> GetCells()
    {
        return new List<Cell> { cell };
    }

    public override ElementBase GetElement()
    {
        return element;
    }

    protected override bool EvaluateFormula(Formula formula)
    {
        StringElement element = new StringElement(formula.FormulaStr);
        SetElement(element);
        return true;
    }

    void SetElement(StringElement element)
    {
        this.element = element;
        cell.SetValue(element);
    }
}
