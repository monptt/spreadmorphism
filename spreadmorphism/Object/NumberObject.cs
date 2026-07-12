using Godot;
using System;

public partial class NumberObject : ObjectBase
{
    [Export]
    Cell cell;

    public override ObjectType Type => ObjectType.Number;

    public int Value => cell.Value;

    public override void _Ready()
    {
        SetValue(0);
    }

    public void SetValue(int value)
    {
        cell.SetValue(value);
    }

    public Cell GetCell()
    {
        return cell;
    }
}
