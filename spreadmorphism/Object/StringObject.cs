using Godot;
using System;
using System.Collections.Generic;

public partial class StringObject : ObjectBase
{
    public override ObjectType Type => ObjectType.String;

    StringElement element = null;

    protected override void InitView()
    {
        SetIsOneObject(true);
        SetElement(new StringElement(""));
        ObjectView.GetCells()[0].SetFormula("");
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
            ElementBase element = ObjectView.GetCells()[0].Formula.Evaluate();
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
        ObjectView.GetCells()[0].SetElement(element);
    }
}
