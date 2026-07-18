using Godot;
using System;
using System.Collections.Generic;

public partial class Vec3Object : ObjectBase
{
    public override ObjectType Type => ObjectType.Vec3;

    Vec3Element element = new Vec3Element(new NumberElement(0), new NumberElement(0), new NumberElement(0));

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
            if (x is NumberElement numX && y is NumberElement numY && z is NumberElement numZ)
            {
                SetElement(new Vec3Element(numX, numY, numZ));
            }
            else
            {
                SetElement(new Vec3Element(new NumberElement(0), new NumberElement(0), new NumberElement(0)));
            }
        }
    }
    public override ElementBase GetElement()
    {
        NumberElement x = new NumberElement(ObjectView.GetCells()[0].Value);
        NumberElement y = new NumberElement(ObjectView.GetCells()[1].Value);
        NumberElement z = new NumberElement(ObjectView.GetCells()[2].Value);
        return new Vec3Element(x, y, z);
    }

    protected override void InitView()
    {

        SetElement(new Vec3Element(new NumberElement(0), new NumberElement(0), new NumberElement(0)));
        ObjectView.GetCells()[0].SetFormula("0");
        ObjectView.GetCells()[1].SetFormula("0");
        ObjectView.GetCells()[2].SetFormula("0");
    }

    void SetElement(Vec3Element element)
    {
        this.element = element;
        ObjectView.GetCells()[0].SetValue(element.X.Value);
        ObjectView.GetCells()[1].SetValue(element.Y.Value);
        ObjectView.GetCells()[2].SetValue(element.Z.Value);
    }

    protected override bool EvaluateFormula(Formula formula)
    {
        ElementBase element = formula.Evaluate();
        if (element is Vec3Element vec3Element)
        {
            SetElement(vec3Element);
            return true;
        }
        else
        {
            return false;
        }
    }
}
