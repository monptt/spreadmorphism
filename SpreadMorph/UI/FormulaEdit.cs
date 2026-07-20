using Godot;
using System;

public partial class FormulaEdit : Node2D
{
    [Export]
    LineEdit lineEdit;

    [Export]
    ColorRect colorRect;

    [Signal]
    public delegate void TextChangedEventHandler(string text);

    public override void _Ready()
    {
        lineEdit.TextChanged += (value) => EmitSignal(SignalName.TextChanged, value);
    }

    public bool IsInArea(Vector2 position)
    {
        return colorRect.GetGlobalRect().HasPoint(position);
    }

    public string GetText()
    {
        return lineEdit.Text;
    }

    public void SetText(string text)
    {
        lineEdit.Text = text;
    }
}
