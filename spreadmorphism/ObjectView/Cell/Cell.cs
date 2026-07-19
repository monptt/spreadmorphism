using Godot;
using System;

public enum CellStatus
{
    Default,    // デフォルト
    Selected,   // 選択されている
    Dependent,  // 他のセルの値に依存している
}

/// <summary>
/// セル（数式入力と結果表示）
/// </summary>
public partial class Cell : Node2D
{
    [Export]
    Label valueLabel;

    [Export]
    ColorRect colorRect;

    int value = 0;
    public int Value => value;

    Formula formula = new Formula("");
    public Formula Formula => formula;

    CellStatus status = CellStatus.Default;
    public CellStatus Status => status;

    bool isSelected = false;

    bool isError = false;

    /// <summary>
    /// 個別に編集可能か
    /// </summary>
    public bool IsEditable => status != CellStatus.Dependent;

    public override void _Ready()
    {
        valueLabel.Text = value.ToString();
    }

    public override void _Process(double delta)
    {
        // 状態によって色を変える
        if (status == CellStatus.Dependent)
        {
            colorRect.Color = new Color(0.8f, 0.8f, 1.0f, 1.0f);
        }
        else if (status == CellStatus.Selected)
        {
            colorRect.Color = new Color(1, 0.9f, 0.8f, 1.0f);
        }
        else
        {
            colorRect.Color = new Color(1, 1, 1, 1.0f);
        }

        if (isError)
        {
            colorRect.Color = new Color(1, 0, 0, 0.8f);
        }
    }

    /// <summary>
    /// エラー状態にする
    /// </summary>
    /// <param name="isError"></param>
    public void SetIsError(bool isError)
    {
        this.isError = isError;
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

    public void SetValue(IntegerElement value)
    {
        this.value = value.Value;
        valueLabel.Text = $"{value.Value}";
    }

    public void SetValue(StringElement value)
    {
        valueLabel.Text = $"{value.Value}";
    }

    public void SetFormula(string formulaStr)
    {
        this.formula = new Formula(formulaStr);
    }

    public void SetSelected(bool selected)
    {
        if (status == CellStatus.Dependent)
        {
            return;
        }

        if (selected)
        {
            SetStatus(CellStatus.Selected);
            isSelected = true;
        }
        else
        {
            SetStatus(CellStatus.Default);
            isSelected = false;
        }
    }

    public bool IsClicked(Vector2 position)
    {
        return colorRect.GetGlobalRect().HasPoint(position);
    }
}
