using Godot;
using System;
using System.Collections.Generic;

public class PolynomialObject : ObjectBase
{
    public override ObjectType Type => ObjectType.Polynomial;

    PolynomialElement element = new PolynomialElement();

    Cell Cell => ObjectView.GetCells()[0];

    protected override void InitView()
    {
        SetIsOneObject(true);
        SetElement(new PolynomialElement());
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
            if (element is PolynomialElement polynomialElement)
            {
                SetElement(polynomialElement);
            }
            else
            {
                SetElement(new PolynomialElement());
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
        if (result is PolynomialElement polynomialElement)
        {
            SetElement(polynomialElement);
            return true;
        }
        else
        {
            SetElement(new PolynomialElement());
            return false;
        }
    }

    void SetElement(PolynomialElement element)
    {
        this.element = element;
        Cell.SetElement(element);
    }
}
