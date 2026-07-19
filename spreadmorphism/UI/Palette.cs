using Godot;
using System;

public partial class Palette : Node2D
{
    // Object生成ボタン
    [Export]
    Button numButton;

    [Export]
    Button vec2Button;

    [Export]
    Button vec3Button;

    [Export]
    Button stringButton;

    [Export]
    Button complexButton;

    [Export]
    Button boolButton;


    // その他
    [Export]
    ColorRect colorRect;

    [Signal]
    public delegate void AddObjectEventHandler(ObjectType type);

    public override void _Ready()
    {
        numButton.Pressed += () =>
        {
            EmitSignal(SignalName.AddObject, (int)ObjectType.Integer);
        };

        vec2Button.Pressed += () =>
        {
            EmitSignal(SignalName.AddObject, (int)ObjectType.Vec2);
        };

        vec3Button.Pressed += () =>
        {
            EmitSignal(SignalName.AddObject, (int)ObjectType.Vec3);
        };

        stringButton.Pressed += () =>
        {
            EmitSignal(SignalName.AddObject, (int)ObjectType.String);
        };

        complexButton.Pressed += () =>
        {
            EmitSignal(SignalName.AddObject, (int)ObjectType.Complex);
        };

        boolButton.Pressed += () =>
        {
            EmitSignal(SignalName.AddObject, (int)ObjectType.Bool);
        };
    }

    public bool IsInPaletteArea(Vector2 position)
    {
        return colorRect.GetGlobalRect().HasPoint(position);
    }
}
