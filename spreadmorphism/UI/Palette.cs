using Godot;
using System;

public partial class Palette : Node2D
{
    [Export]
    Button numButton;

    [Export]
    Button vec2Button;

    [Export]
    Button vec3Button;

    [Export]
    ColorRect colorRect;

    [Signal]
    public delegate void AddObjectEventHandler(ObjectType type);

    public override void _Ready()
    {
        numButton.Pressed += () =>
        {
            EmitSignal(SignalName.AddObject, (int)ObjectType.Number);
        };

        vec2Button.Pressed += () =>
        {
            EmitSignal(SignalName.AddObject, (int)ObjectType.Vec2);
        };

        vec3Button.Pressed += () =>
        {
            EmitSignal(SignalName.AddObject, (int)ObjectType.Vec3);
        };
    }

    public bool IsInPaletteArea(Vector2 position)
    {
        return colorRect.GetGlobalRect().HasPoint(position);
    }
}
