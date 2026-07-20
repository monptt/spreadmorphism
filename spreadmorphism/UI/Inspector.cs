using Godot;
using System;

public partial class Inspector : Node2D
{
    [Export]
    Label selectedPosLabel;

    [Export]
    Label selectedObjTypeLabel;

    [Export]
    Label valueLabel;


    public override void _Ready()
    {
    }

    public void UpdateInspector(GridPos pos, string objType = "null", string value = "null")
    {
        selectedPosLabel.Text = pos.ToString();
        selectedObjTypeLabel.Text = objType;
        valueLabel.Text = value;
    }
}
