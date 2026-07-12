using Godot;
using System;

public partial class Cell : Node2D
{
    [Export]
    Label valueLabel;

    [Export]
    ColorRect colorRect;

    int value = 0;
    public int Value => value;

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

    public void SetValue(int value)
    {
        this.value = value;
        valueLabel.Text = $"{value}";
    }

    public void SetSelected(bool selected)
    {
        if (selected)
        {
            colorRect.Color = new Color(1, 0.9f, 0.8f, 1.0f);
        }
        else
        {
            colorRect.Color = new Color(1, 1, 1, 1.0f);
        }
    }

    public bool IsClicked(Vector2 position)
    {
        return colorRect.GetGlobalRect().HasPoint(position);
    }
}
