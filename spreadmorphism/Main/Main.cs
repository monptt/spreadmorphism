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
    FormulaEdit formulaEdit;

    [Export]
    CoordView coordView;

    [Export]
    Palette palette;

    /// <summary>
    /// 選択されたオブジェクト（オブジェクトが選択された場合）
    /// </summary>
    ObjectBase selectedObject = null;
    /// <summary>
    /// 選択されたセル（セルが選択された場合）
    /// </summary>
    Cell selectedCell = null;
    /// <summary>
    /// 選択されたグリッド座標
    /// </summary>
    GridPos selectedGridPos = new GridPos(0, 0);

    // Input関連
    Vector2 lastMousePosition = Vector2.Zero;
    bool isMouseOn = false;
    ObjectBase draggedObject = null;

    Vector2 windowSize = new Vector2(1920, 1080);

    public override void _Ready()
    {
        // カメラ初期化
        SetCameraPosition(mainCamera.Position);
        SetCameraZoom(mainCamera.Zoom.X);

        // エディット時の操作
        formulaEdit.TextChanged += (value) =>
        {
            if (selectedCell != null)
            {
                selectedCell.SetFormula(value);
            }
            else if (selectedObject != null)
            {
                selectedObject.SetFormula(value);
            }
        };

        palette.AddObject += (type) =>
        {
            GD.Print("addObject: ", type);
            objectSpace.CreateObject(type, selectedGridPos);
            SelectGrid(selectedGridPos);
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

                    // フォーミュラエディットのクリックは除外
                    if (formulaEdit.IsInArea(eventMouseButton.Position))
                    {
                        return;
                    }

                    // 選択したセルを表示
                    GridPos gridPos = CalcGridPos(eventMouseButton.Position);
                    SelectGrid(gridPos);

                    // オブジェクトがあればドラッグ開始
                    ObjectBase obj = objectSpace.GetObject(CalcGridPos(eventMouseButton.Position));
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
                    GridPos gridPos = CalcGridPos(eventMouseButton.Position);
                    ObjectBase obj = objectSpace.GetObject(gridPos);
                    if (obj != null)
                    {
                        obj.SetIsOneObject(!obj.IsOneObject);
                    }
                }
            }
            else if (eventMouseButton.ButtonIndex == MouseButton.WheelUp)
            {
                SetCameraZoom(mainCamera.Zoom.X + 0.1f);
            }
            else if (eventMouseButton.ButtonIndex == MouseButton.WheelDown)
            {
                SetCameraZoom(mainCamera.Zoom.X - 0.1f);
            }
        }
        else if (@event is InputEventMouseMotion eventMouseMotion)
        {
            Vector2 delta = eventMouseMotion.Position - lastMousePosition;
            lastMousePosition = eventMouseMotion.Position;

            // セル座標を表示
            GridPos coord = CalcGridPos(eventMouseMotion.Position);
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
                    SetCameraPosition(mainCamera.Position - delta);
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
                    SelectGrid(selectedGridPos - new GridPos(1, 0));
                }
                else if (eventKey.Keycode == Key.Right)
                {
                    SelectGrid(selectedGridPos + new GridPos(1, 0));
                }
                else if (eventKey.Keycode == Key.Up)
                {
                    SelectGrid(selectedGridPos - new GridPos(0, 1));
                }
                else if (eventKey.Keycode == Key.Down)
                {
                    SelectGrid(selectedGridPos + new GridPos(0, 1));
                }
            }
        }
    }

    public GridPos CalcGridPos(Vector2 position)
    {
        Vector2 objectSpacePosition = GetObjectSpacePosition(position);
        int x = Mathf.FloorToInt(objectSpacePosition.X / Grid.GRID_WIDTH);
        int y = Mathf.FloorToInt(objectSpacePosition.Y / Grid.GRID_HEIGHT);
        return new GridPos(x, y);
    }

    public Vector2 GetObjectSpacePosition(Vector2 position)
    {
        return (position - windowSize / 2) / mainCamera.Zoom + mainCamera.Position;
    }

    void SelectGrid(GridPos pos)
    {
        GD.Print($"SelectGrid: ({pos.X}, {pos.Y})");

        // グリッド選択状態を更新
        this.selectedGridPos = pos;
        Vector2 selectedRectPos = new Vector2(pos.X, pos.Y);
        selectedRectPos.X *= Grid.GRID_WIDTH;
        selectedRectPos.Y *= Grid.GRID_HEIGHT;
        selectedRect.Position = selectedRectPos;

        // オブジェクトが全体選択になってたらオブジェクトを選択
        ObjectBase obj = objectSpace.GetObject(pos);
        if (obj != null && obj.IsOneObject)
        {
            GD.Print("SelectObject");
            formulaEdit.SetText(obj.Formula.FormulaStr);
            selectedObject = obj;
            selectedCell = null;
            return;
        }

        // セルがあれば選択
        Cell cell = objectSpace.GetCell(pos);
        if (cell != null)
        {
            GD.Print("SelectCell");
            selectedCell?.SetSelected(false);
            cell.SetSelected(true);
            formulaEdit.SetText(cell.Formula.FormulaStr);
            selectedCell = cell;
            selectedObject = null;
            return;
        }
    }

    void SetCameraPosition(Vector2 position)
    {
        mainCamera.Position = position;
        grid.SetCameraPosition(position);
    }

    void SetCameraZoom(float zoom)
    {
        mainCamera.Zoom = new Vector2(zoom, zoom);
        grid.SetCameraZoom(zoom);
    }
}