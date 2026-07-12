using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// セルなどのオブジェクトを配置する空間ノード
/// </summary>
public partial class ObjectSpace : Node2D
{
    [Export]
    PackedScene numberObjScene;


    List<NumberObject> cells = new List<NumberObject>();

    public NumberObject CreateCell(int x, int y)
    {
        NumberObject cell = numberObjScene.Instantiate<NumberObject>();
        cell.Position = new Vector2(x * Grid.GRID_WIDTH, y * Grid.GRID_HEIGHT);
        AddChild(cell);
        cells.Add(cell);
        return cell;
    }

    public NumberObject OnCick(Vector2 position)
    {
        // foreach (NumberObject cell in cells)
        // {
        //     if (cell.IsClicked(position))
        //     {
        //         return cell;
        //     }
        // }
        return null;
    }
}
