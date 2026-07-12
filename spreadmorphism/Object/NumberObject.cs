using Godot;
using System;
using System.Collections.Generic;

public partial class NumberObject : ObjectBase
{
    [Export]
    Cell cell;

    public override ObjectType Type => ObjectType.Number;

    public override List<Cell> GetCells()
    {
        return new List<Cell> { cell };
    }

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
