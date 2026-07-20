using Godot;
using System;
using System.Collections.Generic;

public partial class ObjectViewBase : Node2D
{
    protected List<Cell> cells = new List<Cell>();
    public List<Cell> GetCells() => cells;

}
