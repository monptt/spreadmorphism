using Godot;
using System;

public partial class Palette : Node2D
{
    // Object生成ボタン
    [Export]
    Button integerButton;

    [Export]
    Button rationalButton;

    [Export]
    Button vec2Button;

    [Export]
    Button vec3Button;

    [Export]
    Button vec4Button;

    [Export]
    Button stringButton;

    [Export]
    Button complexButton;

    [Export]
    Button boolButton;

    [Export]
    Button polynomialButton;

    [Export]
    Button functionButton;

    // その他
    [Export]
    ColorRect colorRect;

    [Signal]
    public delegate void AddObjectEventHandler(ObjectType type);

    public override void _Ready()
    {
        integerButton.Pressed += () =>
        {
            EmitSignal(SignalName.AddObject, (int)ObjectType.Integer);
        };

        rationalButton.Pressed += () =>
        {
            EmitSignal(SignalName.AddObject, (int)ObjectType.Rational);
        };

        vec2Button.Pressed += () =>
        {
            EmitSignal(SignalName.AddObject, (int)ObjectType.Vec2);
        };

        vec3Button.Pressed += () =>
        {
            EmitSignal(SignalName.AddObject, (int)ObjectType.Vec3);
        };

        vec4Button.Pressed += () =>
        {
            EmitSignal(SignalName.AddObject, (int)ObjectType.Vec4);
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

        polynomialButton.Pressed += () =>
        {
            EmitSignal(SignalName.AddObject, (int)ObjectType.Polynomial);
        };

        functionButton.Pressed += () =>
        {
            EmitSignal(SignalName.AddObject, (int)ObjectType.Function);
        };
    }

    public bool IsInPaletteArea(Vector2 position)
    {
        return colorRect.GetGlobalRect().HasPoint(position);
    }
}
