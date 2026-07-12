using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node2D
{
    [Export]
    LineEdit lineEdit;

    [Export]
    Camera2D mainCamera;

    [Export]
    ObjectSpace objectSpace;

    [Export]
    Grid grid;

    List<Cell> cells = new List<Cell>();

    Cell selectedCell = null;

    // Input関連
    Vector2 lastMousePosition = Vector2.Zero;
    bool isMouseOn = false;

    public override void _Ready()
    {
        // セルを配置
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Cell cell = objectSpace.CreateCell(i, j);
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
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.ButtonIndex == MouseButton.Left)
        {
            if (eventMouseButton.Pressed)
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

                isMouseOn = true;
            }
            else
            {
                isMouseOn = false;
            }
        }
        else if (@event is InputEventMouseMotion eventMouseMotion)
        {
            Vector2 delta = eventMouseMotion.Position - lastMousePosition;
            lastMousePosition = eventMouseMotion.Position;

            if (isMouseOn)
            {
                // ドラッグ時
                // カメラを移動させる
                mainCamera.Position -= delta;
                grid.SetCameraPosition(mainCamera.Position);
            }
        }
    }
}