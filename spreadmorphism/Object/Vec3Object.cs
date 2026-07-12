using Godot;
using System;

public partial class Vec3Object : ObjectBase
{
    [Export]
    Cell cell_x;

    [Export]
    Cell cell_y;

    [Export]
    Cell cell_z;

    public override ObjectType Type => ObjectType.Vec3;
}
