using Godot;
using System;

public partial class CellMatrixObjectView : ObjectViewBase
{
    [Export]
    Label label;

    [Export]
    Node2D cellNode;

    [Export]
    PackedScene cellScene;

    public override void _Ready()
    {
    }

    public void Init(int x, int y, string name)
    {
        // 一旦クリア
        foreach (var child in cellNode.GetChildren())
        {
            child.QueueFree();
        }

        // セルノードを作成
        cellNode = cellScene.Instantiate<Node2D>();
        AddChild(cellNode);
        cellNode.Position = new Vector2(x * Grid.GRID_WIDTH, y * Grid.GRID_HEIGHT);

        // 名前を設定
        SetName(name);
    }


    void SetName(string name)
    {
        label.Text = name;
    }
}
