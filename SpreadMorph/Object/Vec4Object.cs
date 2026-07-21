using Godot;
using System;
using System.Collections.Generic;

public partial class Vec4Object : ObjectBase
{
    public override ObjectType Type => ObjectType.Vec4;

    Vec4Element element = new Vec4Element(new IntegerElement(0), new IntegerElement(0), new IntegerElement(0), new IntegerElement(0));

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
            ElementBase z = ObjectView.GetCells()[2].Formula.Evaluate();
            ElementBase w = ObjectView.GetCells()[3].Formula.Evaluate();
            if (x is IntegerElement numX && y is IntegerElement numY && z is IntegerElement numZ && w is IntegerElement numW)
            {
                SetElement(new Vec4Element(numX, numY, numZ, numW));
            }
            else
            {
                SetElement(new Vec4Element(new IntegerElement(0), new IntegerElement(0), new IntegerElement(0), new IntegerElement(0)));
            }
        }
    }
    public override ElementBase GetElement()
    {
        IntegerElement x = ObjectView.GetCells()[0].Element as IntegerElement;
        IntegerElement y = ObjectView.GetCells()[1].Element as IntegerElement;
        IntegerElement z = ObjectView.GetCells()[2].Element as IntegerElement;
        IntegerElement w = ObjectView.GetCells()[3].Element as IntegerElement;
        return new Vec4Element(x, y, z, w);
    }

    protected override void InitView()
    {

        SetElement(new Vec4Element(new IntegerElement(0), new IntegerElement(0), new IntegerElement(0), new IntegerElement(0)));
        ObjectView.GetCells()[0].SetFormula("0");
        ObjectView.GetCells()[1].SetFormula("0");
        ObjectView.GetCells()[2].SetFormula("0");
        ObjectView.GetCells()[3].SetFormula("0");
    }

    void SetElement(Vec4Element element)
    {
        this.element = element;
        ObjectView.GetCells()[0].SetElement(element.X);
        ObjectView.GetCells()[1].SetElement(element.Y);
        ObjectView.GetCells()[2].SetElement(element.Z);
        ObjectView.GetCells()[3].SetElement(element.W);
    }

    protected override bool EvaluateFormula(Formula formula)
    {
        ElementBase element = formula.Evaluate();
        if (element is Vec4Element vec4Element)
        {
            SetElement(vec4Element);
            return true;
        }
        else
        {
            return false;
        }
    }
}
