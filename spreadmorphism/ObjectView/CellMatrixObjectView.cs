using Godot;
using System;
using System.Collections.Generic;

public partial class CellMatrixObjectView : ObjectViewBase
{
    [Export]
    Label label;

    [Export]
    Node2D cellNode;

    [Export]
    PackedScene cellScene;

    public void Init(int xNum, int yNum, GridPos pos, string name)
    {
        // 一旦クリア
        foreach (var child in cellNode.GetChildren())
        {
            cellNode.RemoveChild(child);
            child.QueueFree();
        }

        // セルを作成
        for (int y = 0; y < yNum; y++)
        {
            for (int x = 0; x < xNum; x++)
            {
                Cell cell = cellScene.Instantiate<Cell>();
                cell.Position = new Vector2(x * Grid.GRID_WIDTH, y * Grid.GRID_HEIGHT);
                cellNode.AddChild(cell);
                this.cells.Add(cell);
            }
        }

        // 名前を設定
        SetName(name);
    }


    void SetName(string name)
    {
        label.Text = name;
    }
}
