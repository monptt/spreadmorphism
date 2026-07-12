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
    GridPos selectedCoord = new GridPos(0, 0);

    // Input関連
    Vector2 lastMousePosition = Vector2.Zero;
    bool isMouseOn = false;
    ObjectBase draggedObject = null;

    Vector2 windowSize = new Vector2(1920, 1080);

    public override void _Ready()
    {
        // エディット時の操作
        lineEdit.TextChanged += (value) =>
        {
            if (selectedCell != null)
            {
                selectedCell.SetValueStr(value);
            }
        };

        palette.AddObject += (type) =>
        {
            GD.Print("addObject: ", type);
            switch (type)
            {
                case ObjectType.Number:
                    objectSpace.CreateNumberObject(selectedCoord.X, selectedCoord.Y);
                    break;
                case ObjectType.Vec2:
                    objectSpace.CreateVec2Object(selectedCoord.X, selectedCoord.Y);
                    break;
                case ObjectType.Vec3:
                    objectSpace.CreateVec3Object(selectedCoord.X, selectedCoord.Y);
                    break;
                default:
                    break;
            }
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
                    isMouseOn = true;

                    // パレットのクリックは除外
                    if (palette.IsInPaletteArea(eventMouseButton.Position))
                    {
                        return;
                    }

                    // 選択したセルを表示
                    GridPos coord = GetCoord(eventMouseButton.Position);
                    SelectCell(coord);

                    // オブジェクトがあればドラッグ開始
                    ObjectBase obj = objectSpace.GetObject(GetCoord(eventMouseButton.Position));
                    if (obj != null)
                    {
                        draggedObject = obj;
                    }
                }
                else
                {
                    isMouseOn = false;
                    draggedObject = null;
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
            GridPos coord = GetCoord(eventMouseMotion.Position);
            coordView.SetCoord(coord.X, coord.Y);

            if (isMouseOn)
            {
                // ドラッグ時
                if (draggedObject != null)
                {
                    // ドラッグ中ならオブジェクトを移動させる
                    draggedObject.Position = new Vector2(coord.X * Grid.GRID_WIDTH, coord.Y * Grid.GRID_HEIGHT);
                }
                else
                {
                    // カメラを移動させる
                    mainCamera.Position -= delta;
                    grid.SetCameraPosition(mainCamera.Position - windowSize / 2);
                }
            }
        }
        else if (@event is InputEventKey eventKey)
        {
            // キー入力
            if (eventKey.Pressed)
            {
                if (eventKey.Keycode == Key.Left)
                {
                    SelectCell(selectedCoord - new GridPos(1, 0));
                }
                else if (eventKey.Keycode == Key.Right)
                {
                    SelectCell(selectedCoord + new GridPos(1, 0));
                }
                else if (eventKey.Keycode == Key.Up)
                {
                    SelectCell(selectedCoord - new GridPos(0, 1));
                }
                else if (eventKey.Keycode == Key.Down)
                {
                    SelectCell(selectedCoord + new GridPos(0, 1));
                }
            }
        }
    }

    public GridPos GetCoord(Vector2 position)
    {
        Vector2 objectSpacePosition = GetObjectSpacePosition(position);
        int x = Mathf.FloorToInt(objectSpacePosition.X / Grid.GRID_WIDTH);
        int y = Mathf.FloorToInt(objectSpacePosition.Y / Grid.GRID_HEIGHT);
        return new GridPos(x, y);
    }

    public Vector2 GetObjectSpacePosition(Vector2 position)
    {
        return position + mainCamera.Position - windowSize / 2;
    }

    void SelectCell(GridPos pos)
    {
        GD.Print($"SelectCell: ({pos.X}, {pos.Y})");

        // セルがあれば選択
        Cell cell = objectSpace.GetCell(pos);
        if (cell != null)
        {
            selectedCell?.SetSelected(false);
            cell.SetSelected(true);
            lineEdit.Text = cell.ValueStr;
            selectedCell = cell;
        }

        this.selectedCoord = pos;
        Vector2 selectedRectPos = new Vector2(pos.X, pos.Y);
        selectedRectPos.X *= Grid.GRID_WIDTH;
        selectedRectPos.Y *= Grid.GRID_HEIGHT;
        selectedRect.Position = selectedRectPos;
    }
}