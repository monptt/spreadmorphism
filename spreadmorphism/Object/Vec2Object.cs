using Godot;
using System;

public partial class Vec2Object : ObjectBase
{
    [Export]
    Cell cell_x;

    [Export]
    Cell cell_y;

    public override ObjectType Type => ObjectType.Vec2;
}
