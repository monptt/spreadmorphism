using Godot;
using System;

public partial class NumberObject : ObjectBase
{
    [Export]
    Label valueLabel;

    int value = 0;
    public int Value => value;

    public override void _Ready()
    {
        SetValue(value);
    }

    public void SetValue(int value)
    {
        this.value = value;
        valueLabel.Text = $"{value}";
    }
}
