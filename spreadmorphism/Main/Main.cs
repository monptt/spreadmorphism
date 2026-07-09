using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node2D
{

    [Export]
    PackedScene cellScene;

    [Export]
    LineEdit lineEdit;

    List<Cell> cells = new List<Cell>();

    Cell selectedCell = null;

    public override void _Ready()
    {
        // セルを配置
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Cell cell = cellScene.Instantiate<Cell>();
                cell.Position = new Vector2(100 + i * 120, 100 + j * 42);
                AddChild(cell);
                cells.Add(cell);
            }
        }

        // エディット時の操作
        lineEdit.TextChanged += (value) =>
        {
            if (selectedCell != null)
            {
                selectedCell.SetValue(int.Parse(value));
            }
        };
    }

    public override void _Process(double delta)
    {
        // 雑に合計値を出すだけ
        int sum = 0;
        for (int i = 1; i < cells.Count; i++)
        {
            sum += cells[i].Value;
        }
        cells[0].SetValue(sum);
        GD.Print(cells[0].Value);
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
                    selectedCell?.SetSelected(false);
                    cell.SetSelected(true);
                    lineEdit.Text = $"{cell.Value}";
                    selectedCell = cell;
                }
            }
        }
    }
}