using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node2D
{
    [Export]
    Camera2D mainCamera;

    [Export]
    ObjectSpace objectSpace;


    [Export]
    Grid grid;

    // UI系
    [Export]
    LineEdit lineEdit;

    [Export]
    CoordView coordView;

    List<Cell> cells = new List<Cell>();

    Cell selectedCell = null;

    // Input関連
    Vector2 lastMousePosition = Vector2.Zero;
    bool isMouseOn = false;

    Vector2 windowSize = new Vector2(1920, 1080);

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
        if (@event is InputEventMouseButton eventMouseButton)
        {
            if (eventMouseButton.ButtonIndex == MouseButton.Left)
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
            else if (eventMouseButton.ButtonIndex == MouseButton.Right)
            {
                if (eventMouseButton.Pressed)
                {
                    // 右クリック時
                }
            }
            else if (eventMouseButton.ButtonIndex == MouseButton.WheelUp)
            {
                mainCamera.Zoom += new Vector2(0.1f, 0.1f);
                grid.SetCameraZoom(mainCamera.Zoom.X);
            }
            else if (eventMouseButton.ButtonIndex == MouseButton.WheelDown)
            {
                mainCamera.Zoom -= new Vector2(0.1f, 0.1f);
                grid.SetCameraZoom(mainCamera.Zoom.X);
            }
        }
        else if (@event is InputEventMouseMotion eventMouseMotion)
        {
            Vector2 delta = eventMouseMotion.Position - lastMousePosition;
            lastMousePosition = eventMouseMotion.Position;

            // セル座標を表示
            Vector2I coord = GetCoord(eventMouseMotion.Position);
            coordView.SetCoord(coord.X, coord.Y);

            if (isMouseOn)
            {
                // ドラッグ時
                // カメラを移動させる
                mainCamera.Position -= delta;
                grid.SetCameraPosition(mainCamera.Position - windowSize / 2);
            }
        }
    }

    public Vector2I GetCoord(Vector2 position)
    {
        return new Vector2I((int)(position.X / Grid.GRID_WIDTH), (int)(position.Y / Grid.GRID_HEIGHT));
    }
}