using Godot;
using System;

public partial class Palette : Node2D
{
    [Export]
    Button numButton;

    [Signal]
    public delegate void AddObjectEventHandler(ObjectType type);

    public override void _Ready()
    {
        numButton.Pressed += () =>
        {
            EmitSignal(SignalName.AddObject, (int)ObjectType.Number);
        };
    }
}
