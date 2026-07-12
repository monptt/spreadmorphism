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
        cell.Position = new Vector2(x * 120, y * 42);
        AddChild(cell);
        return cell;
    }
}
