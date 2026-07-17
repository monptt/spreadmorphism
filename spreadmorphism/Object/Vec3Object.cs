using Godot;
using System;
using System.Collections.Generic;

public partial class Vec3Object : ObjectBase
{
    [Export]
    Cell cell_x;

    [Export]
    Cell cell_y;

    [Export]
    Cell cell_z;

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
            ElementBase x = cell_x.Formula.Evaluate();
            ElementBase y = cell_y.Formula.Evaluate();
            ElementBase z = cell_z.Formula.Evaluate();
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
        NumberElement x = new NumberElement(cell_x.Value);
        NumberElement y = new NumberElement(cell_y.Value);
        NumberElement z = new NumberElement(cell_z.Value);
        return new Vec3Element(x, y, z);
    }

    public override void _Ready()
    {
        SetElement(new Vec3Element(new NumberElement(0), new NumberElement(0), new NumberElement(0)));
    }

    protected override void Init()
    {
        cell_x.SetFormula("0");
        cell_y.SetFormula("0");
        cell_z.SetFormula("0");
    }

    public override List<Cell> GetCells()
    {
        return new List<Cell> { cell_x, cell_y, cell_z };
    }

    void SetElement(Vec3Element element)
    {
        this.element = element;
        cell_x.SetValue(element.X.Value);
        cell_y.SetValue(element.Y.Value);
        cell_z.SetValue(element.Z.Value);
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
