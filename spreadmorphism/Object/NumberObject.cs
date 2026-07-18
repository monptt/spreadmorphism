using Godot;
using System;
using System.Collections.Generic;

public partial class NumberObject : ObjectBase
{
    public override ObjectType Type => ObjectType.Number;

    NumberElement element = new NumberElement(0);

    protected override void Init()
    {
        this.SetIsOneObject(true);
        SetElement(new NumberElement(0));
        ObjectView.GetCells()[0].SetFormula("0");
        this.SetFormula("0");
    }

    public override void UpdateObject()
    {
        if (IsOneObject)
        {
            ElementBase element = this.Formula.Evaluate();

            if (element is NumberElement numberElement)
            {
                SetElement(numberElement);
            }
            else
            {
                SetElement(new NumberElement(0));
            }
        }
        else
        {
            ElementBase element = ObjectView.GetCells()[0].Formula.Evaluate();

            if (element is NumberElement numberElement)
            {
                SetElement(numberElement);
            }
            else
            {
                SetElement(new NumberElement(0));
            }
        }
    }

    protected override bool EvaluateFormula(Formula formula)
    {
        ElementBase result = formula.Evaluate();

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

    public int Value => ObjectView.GetCells()[0].Value;

    void SetElement(NumberElement element)
    {
        this.element = element;
        ObjectView.GetCells()[0].SetValue(element.Value);
    }

    public Cell GetCell()
    {
        return ObjectView.GetCells()[0];
    }
}
