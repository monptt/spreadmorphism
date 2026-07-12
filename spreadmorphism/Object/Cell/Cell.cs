using Godot;
using System;

public enum CellStatus
{
    Default,
    Selected,
    NotEditable,
}

public partial class Cell : Node2D
{
    [Export]
    Label valueLabel;

    [Export]
    ColorRect colorRect;

    int value = 0;
    public int Value => value;

    string valueStr = "";
    public string ValueStr => valueStr;

    CellStatus status = CellStatus.Default;
    public CellStatus Status => status;

    /// <summary>
    /// 個別に編集可能か
    /// </summary>
    public bool IsEditable => status != CellStatus.NotEditable;

    public override void _Ready()
    {
        valueLabel.Text = value.ToString();
    }

    public GridPos GetGridPos()
    {
        int x = Mathf.FloorToInt(GlobalPosition.X / Grid.GRID_WIDTH);
        int y = Mathf.FloorToInt(GlobalPosition.Y / Grid.GRID_HEIGHT);
        return new GridPos(x, y);
    }

    public void SetStatus(CellStatus status)
    {
        this.status = status;
    }

    public void SetValue(int value)
    {
        this.value = value;
        valueLabel.Text = $"{value}";
    }

    public void SetValueStr(string valueStr)
    {
        this.valueStr = valueStr;
        this.SetValue(int.Parse(valueStr));
    }

    public void SetSelected(bool selected)
    {
        if (status == CellStatus.NotEditable)
        {
            return;
        }

        if (selected)
        {
            colorRect.Color = new Color(1, 0.9f, 0.8f, 1.0f);
            status = CellStatus.Selected;
        }
        else
        {
            colorRect.Color = new Color(1, 1, 1, 1.0f);
            status = CellStatus.Default;
        }
    }

    public bool IsClicked(Vector2 position)
    {
        return colorRect.GetGlobalRect().HasPoint(position);
    }
}
