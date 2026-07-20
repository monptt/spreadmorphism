using Godot;
using System;
using System.Collections.Generic;

public class FunctionObject : ObjectBase
{
    public override ObjectType Type => ObjectType.Function;

    FunctionElement element = new FunctionElement();

    Cell Cell => ObjectView.GetCells()[0];

    protected override void InitView()
    {
        SetIsOneObject(true);
        SetElement(new FunctionElement());
        this.SetFormula("0");
        Cell.SetFormula("0");
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
            if (element is FunctionElement functionElement)
            {
                SetElement(functionElement);
            }
            else
            {
                SetElement(new FunctionElement());
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
        if (result is FunctionElement functionElement)
        {
            SetElement(functionElement);
            return true;
        }
        else
        {
            SetElement(new FunctionElement());
            return false;
        }
    }

    void SetElement(FunctionElement element)
    {
        this.element = element;
        Cell.SetElement(element);
    }
}
