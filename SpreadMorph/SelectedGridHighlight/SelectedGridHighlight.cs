using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// 参照されたグリッドをハイライトする
/// </summary>
public partial class SelectedGridHighlight : Node2D
{
    [Export]
    PackedScene highlightScene;

    List<Node2D> highlightObjList = new List<Node2D>();

    public override void _Ready()
    {
        // 先に生成だけしておく
        int N = 10;
        for (int i = 0; i < N; i++)
        {
            Node2D highlightObj = highlightScene.Instantiate<Node2D>();
            highlightObjList.Add(highlightObj);
            AddChild(highlightObj);
            // 非表示
            highlightObj.Visible = false;
        }
    }

    /// <summary>
    /// 参照されたグリッドを更新
    /// </summary>
    /// <param name="gridPosList">参照されたグリッドのリスト</param>
    public void Update(List<GridPos> gridPosList)
    {

    }
}
