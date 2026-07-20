using Godot;
using System;
using System.Collections.Generic;

public class RationalObject : ObjectBase
{
    public override ObjectType Type => ObjectType.Rational;

    RationalElement element = new RationalElement(new IntegerElement(0), new IntegerElement(1));

    Cell Cell => ObjectView.GetCells()[0];

    protected override void InitView()
    {
        SetIsOneObject(true);
        SetElement(new RationalElement(new IntegerElement(0), new IntegerElement(1)));
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
            if (element is RationalElement rationalElement)
            {
                SetElement(rationalElement);
            }
            else
            {
                SetElement(new RationalElement(new IntegerElement(0), new IntegerElement(1)));
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
        if (result is RationalElement rationalElement)
        {
            SetElement(rationalElement);
            return true;
        }
        else
        {
            SetElement(new RationalElement(new IntegerElement(0), new IntegerElement(1)));
            return false;
        }
    }

    void SetElement(RationalElement element)
    {
        this.element = element;
        Cell.SetElement(element);
    }
}
