using Godot;
using System;

/// <summary>
/// セルなどのオブジェクトを配置する空間ノード
/// </summary>
public partial class ObjectSpace : Node2D
{
    [Export]
    PackedScene cellScene;

    public Cell CreateCell(int x, int y)
    {
        Cell cell = cellScene.Instantiate<Cell>();
        cell.Position = new Vector2(x * Grid.GRID_WIDTH, y * Grid.GRID_HEIGHT);
        AddChild(cell);
        return cell;
    }
}
