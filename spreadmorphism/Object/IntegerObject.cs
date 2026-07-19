using Godot;
using System;
using System.Collections.Generic;

public partial class IntegerObject : ObjectBase
{
    public override ObjectType Type => ObjectType.Number;

    IntegerElement element = new IntegerElement(0);

    Cell Cell => ObjectView.GetCells()[0];


    protected override void InitView()
    {
        this.SetIsOneObject(true);
        SetElement(new IntegerElement(0));
        Cell.SetFormula("0");
        this.SetFormula("0");
    }

    public override void UpdateObject()
    {
        if (IsOneObject)
        {
            ElementBase element = this.Formula.Evaluate();

            if (element is IntegerElement numberElement)
            {
                SetElement(numberElement);
            }
            else
            {
                SetElement(new IntegerElement(0));
            }
        }
        else
        {
            ElementBase element = Cell.Formula.Evaluate();

            if (element is IntegerElement numberElement)
            {
                SetElement(numberElement);
            }
            else
            {
                SetElement(new IntegerElement(0));
            }
        }
    }

    protected override bool EvaluateFormula(Formula formula)
    {
        ElementBase result = formula.Evaluate();

        if (result is IntegerElement numberElement)
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

    void SetElement(IntegerElement element)
    {
        this.element = element;
        Cell.SetValue(element.Value);
    }
}
