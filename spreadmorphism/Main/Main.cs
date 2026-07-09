using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node2D
{

    [Export]
    PackedScene cellScene;

    List<Cell> cells = new List<Cell>();

    public override void _Ready()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Cell cell = cellScene.Instantiate<Cell>();
                cell.Position = new Vector2(100, j * 60);
                AddChild(cell);
                cells.Add(cell);
            }
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.ButtonIndex == MouseButton.Left && eventMouseButton.Pressed)
        {
            // クリック時
            foreach (Cell cell in cells)
            {
                if (cell.IsClicked(eventMouseButton.Position))
                {
                    cell.SetSelected(true);
                }
                else
                {
                    cell.SetSelected(false);
                }
            }
        }
    }
}