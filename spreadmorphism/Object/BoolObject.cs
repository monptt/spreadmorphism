using Godot;
using System;
using System.Collections.Generic;

public class BoolObject : ObjectBase
{
    public override ObjectType Type => ObjectType.Bool;

    BoolElement element = new BoolElement(false);

    Cell Cell => ObjectView.GetCells()[0];

    protected override void InitView()
    {
        SetIsOneObject(true);
        SetElement(new BoolElement(false));
        Cell.SetFormula("false");
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
            ElementBase element = Cell.Formula.Evaluate();
            if (element is BoolElement boolElement)
            {
                SetElement(boolElement);
            }
            else
            {
                SetElement(new BoolElement(false));
            }
        }
    }

    public override ElementBase GetElement()
    {
        return element;
    }

    protected override bool EvaluateFormula(Formula formula)
    {
        ElementBase result = formula.Evaluate();
        if (result is BoolElement boolElement)
        {
            SetElement(boolElement);
            return true;
        }
        else
        {
            SetElement(new BoolElement(false));
            return false;
        }
    }

    void SetElement(BoolElement element)
    {
        this.element = element;
        Cell.SetElement(element);
    }
}
