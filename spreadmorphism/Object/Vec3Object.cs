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

    public override List<Cell> GetCells()
    {
        return new List<Cell> { cell_x, cell_y, cell_z };
    }

    protected override void ParseValueStr(string valueStr)
    {
        //@todo: 実装
    }
}
