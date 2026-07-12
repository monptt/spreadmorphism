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
    ColorRect selectedRect;

    [Export]
    Grid grid;

    // UI系
    [Export]
    LineEdit lineEdit;

    [Export]
    CoordView coordView;

    [Export]
    Palette palette;

    Cell selectedCell = null;
    Vector2I selectedCoord = new Vector2I(0, 0);

    // Input関連
    Vector2 lastMousePosition = Vector2.Zero;
    bool isMouseOn = false;

    Vector2 windowSize = new Vector2(1920, 1080);

    public override void _Ready()
    {
        // エディット時の操作
        lineEdit.TextChanged += (value) =>
        {
            if (selectedCell != null)
            {
                selectedCell.SetValue(int.Parse(value));
            }
        };

        palette.AddObject += (type) =>
        {
            GD.Print("addObject: ", type);
            objectSpace.CreateNumberObject(selectedCoord.X, selectedCoord.Y);
        };
    }

    public override void _Process(double delta)
    {
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
                    Vector2 objectSpacePos = GetObjectSpacePosition(eventMouseButton.Position);
                    Cell cell = objectSpace.OnCick(objectSpacePos);
                    if (cell != null)
                    {
                        selectedCell?.SetSelected(false);
                        cell.SetSelected(true);
                        lineEdit.Text = $"{cell.Value}";
                        selectedCell = cell;
                    }

                    // 選択したセルを表示
                    this.selectedCoord = GetCoord(eventMouseButton.Position);
                    Vector2 selectedRectPos = new Vector2(this.selectedCoord.X, this.selectedCoord.Y);
                    selectedRectPos.X *= Grid.GRID_WIDTH;
                    selectedRectPos.Y *= Grid.GRID_HEIGHT;
                    selectedRect.Position = selectedRectPos;


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
        Vector2 objectSpacePosition = GetObjectSpacePosition(position);
        int x = Mathf.FloorToInt(objectSpacePosition.X / Grid.GRID_WIDTH);
        int y = Mathf.FloorToInt(objectSpacePosition.Y / Grid.GRID_HEIGHT);
        return new Vector2I(x, y);
    }

    public Vector2 GetObjectSpacePosition(Vector2 position)
    {
        return position + mainCamera.Position - windowSize / 2;
    }
}