using Godot;
using System;
using System.Collections.Generic;

public partial class Vec2Object : ObjectBase
{
    public override ObjectType Type => ObjectType.Vec2;

    Vec2Element element = new Vec2Element(new IntegerElement(0), new IntegerElement(0));

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
            if (x is IntegerElement numX && y is IntegerElement numY)
            {
                SetElement(new Vec2Element(numX, numY));
            }
            else
            {
                SetElement(new Vec2Element(new IntegerElement(0), new IntegerElement(0)));
            }
        }
    }

    public override ElementBase GetElement()
    {
        IntegerElement x = ObjectView.GetCells()[0].Element as IntegerElement;
        IntegerElement y = ObjectView.GetCells()[1].Element as IntegerElement;
        return new Vec2Element(x, y);
    }

    void SetElement(Vec2Element element)
    {
        this.element = element;
        ObjectView.GetCells()[0].SetElement(element.X);
        ObjectView.GetCells()[1].SetElement(element.Y);
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
