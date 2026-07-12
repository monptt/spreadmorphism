using Godot;
using System;
using System.Collections.Generic;

public partial class Vec2Object : ObjectBase
{
    [Export]
    Cell cell_x;

    [Export]
    Cell cell_y;

    public override ObjectType Type => ObjectType.Vec2;

    public override List<Cell> GetCells()
    {
        return new List<Cell> { cell_x, cell_y };
    }
}
