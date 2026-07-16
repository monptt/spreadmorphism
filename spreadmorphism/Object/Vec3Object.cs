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

    Vec3Element element = null;

    public override ElementBase GetElement()
    {
        return element;
    }

    public override void _Ready()
    {
        SetElement(new Vec3Element(new NumberElement(0), new NumberElement(0), new NumberElement(0)));
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
        //@todo: 実装
        return true;
    }
}
