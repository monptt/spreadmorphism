using Godot;
using System;
using System.Collections.Generic;

public partial class NumberObject : ObjectBase
{
    [Export]
    Cell cell;

    public override ObjectType Type => ObjectType.Number;

    string valueStr = "";
    public string ValueStr => valueStr;

    public override List<Cell> GetCells()
    {
        return new List<Cell> { cell };
    }

    public int Value => cell.Value;

    public override void _Ready()
    {
        SetValue(0);
    }

    void SetValue(int value)
    {
        cell.SetValue(value);
    }

    public Cell GetCell()
    {
        return cell;
    }
}
