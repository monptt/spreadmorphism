using Godot;
using System;

public partial class CoordView : Node2D
{
    [Export]
    Label label;

    public void SetCoord(int x, int y)
    {
        label.Text = $"({x}, {y})";
    }
}
