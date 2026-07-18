using Godot;
using System;
using System.Collections.Generic;

public partial class Vec2Object : ObjectBase
{
    public override ObjectType Type => ObjectType.Vec2;

    Vec2Element element = new Vec2Element(new NumberElement(0), new NumberElement(0));

    protected override void InitView()
    {
        ObjectView.GetCells()[0].SetFormula("0");
        ObjectView.GetCells()[1].SetFormula("0");
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
            ElementBase x = ObjectView.GetCells()[0].Formula.Evaluate();
            ElementBase y = ObjectView.GetCells()[1].Formula.Evaluate();
            if (x is NumberElement numX && y is NumberElement numY)
            {
                SetElement(new Vec2Element(numX, numY));
            }
            else
            {
                SetElement(new Vec2Element(new NumberElement(0), new NumberElement(0)));
            }
        }
    }

    public override ElementBase GetElement()
    {
        NumberElement x = new NumberElement(ObjectView.GetCells()[0].Value);
        NumberElement y = new NumberElement(ObjectView.GetCells()[1].Value);
        return new Vec2Element(x, y);
    }

    void SetElement(Vec2Element element)
    {
        this.element = element;
        ObjectView.GetCells()[0].SetValue(element.X.Value);
        ObjectView.GetCells()[1].SetValue(element.Y.Value);
    }

    protected override bool EvaluateFormula(Formula formula)
    {
        ElementBase result = formula.Evaluate();
        if (result is Vec2Element vec2Element)
        {
            SetElement(vec2Element);
            return true;
        }
        else
        {
            return false;
        }
    }
}
