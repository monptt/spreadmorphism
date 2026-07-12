using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// セルなどのオブジェクトを配置する空間ノード
/// </summary>
public partial class ObjectSpace : Node2D
{
    [Export]
    PackedScene cellScene;


    List<Cell> cells = new List<Cell>();

    public Cell CreateCell(int x, int y)
    {
        Cell cell = cellScene.Instantiate<Cell>();
        cell.Position = new Vector2(x * Grid.GRID_WIDTH, y * Grid.GRID_HEIGHT);
        AddChild(cell);
        cells.Add(cell);
        return cell;
    }

    public Cell OnCick(Vector2 position)
    {
        foreach (Cell cell in cells)
        {
            if (cell.IsClicked(position))
            {
                return cell;
            }
        }
        return null;
    }
}
