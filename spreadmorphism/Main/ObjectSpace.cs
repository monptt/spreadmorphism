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


    List<NumberObject> numberObjects = new List<NumberObject>();

    public NumberObject CreateNumberObject(int x, int y)
    {
        NumberObject cell = numberObjScene.Instantiate<NumberObject>();
        cell.Position = new Vector2(x * Grid.GRID_WIDTH, y * Grid.GRID_HEIGHT);
        AddChild(cell);
        numberObjects.Add(cell);
        return cell;
    }

    public Cell OnCick(Vector2 position)
    {
        foreach (NumberObject obj in numberObjects)
        {
            if (obj.GetCell().IsClicked(position))
            {
                return obj.GetCell();
            }
        }
        return null;
    }
}
