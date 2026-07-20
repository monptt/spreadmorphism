using Godot;
using System;
using System.Collections.Generic;

public partial class ComplexObject : ObjectBase
{
    public override ObjectType Type => ObjectType.Complex;

    ComplexElement element = new ComplexElement(new IntegerElement(0), new IntegerElement(0));

    Cell Cell => ObjectView.GetCells()[0];


    protected override void InitView()
    {
        this.SetIsOneObject(true);
        SetElement(new ComplexElement(new IntegerElement(0), new IntegerElement(0)));
        Cell.SetFormula("0");
        this.SetFormula("0");
    }

    public override void UpdateObject()
    {
        if (IsOneObject)
        {
            ElementBase element = this.Formula.Evaluate();

            if (element is ComplexElement complexElement)
            {
                SetElement(complexElement);
            }
            else
            {
                SetElement(new ComplexElement(new IntegerElement(0), new IntegerElement(0)));
            }
        }
        else
        {
            ElementBase element = Cell.Formula.Evaluate();

            if (element is ComplexElement complexElement)
            {
                SetElement(complexElement);
            }
            else
            {
                SetElement(new ComplexElement(new IntegerElement(0), new IntegerElement(0)));
            }
        }
    }

    protected override bool EvaluateFormula(Formula formula)
    {
        ElementBase result = formula.Evaluate();

        if (result is ComplexElement complexElement)
        {
            SetElement(complexElement);
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

    void SetElement(ComplexElement element)
    {
        this.element = element;
        Cell.SetElement(element);
    }
}
