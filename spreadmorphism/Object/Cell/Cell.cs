using Godot;
using System;

public enum CellStatus
{
    Default,    // デフォルト
    Selected,   // 選択されている
    Dependent,  // 他のセルの値に依存している
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

    bool isSelected = false;

    /// <summary>
    /// 個別に編集可能か
    /// </summary>
    public bool IsEditable => status != CellStatus.Dependent;

    public override void _Ready()
    {
        valueLabel.Text = value.ToString();
    }

    /// <summary>
    /// 値を更新する（他のセルが変更されたときとか）
    /// </summary>
    public void UpdateValue()
    {
        int value = ParseValue(valueStr);
        this.SetValue(value);
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
    }

    public void SetValue(int value)
    {
        this.value = value;
        valueLabel.Text = $"{value}";
    }

    public void SetValueStr(string valueStr)
    {
        this.valueStr = valueStr;
        int value = ParseValue(valueStr);
        this.SetValue(value);

        // 他のセルの値も更新する
        ObjectSpace.Instance.UpdateAllObjects();
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

    int ParseValue(string valueStr)
    {
        if (valueStr.Length == 0)
        {
            return 0;
        }

        if (valueStr[0] == '=')
        {
            SetStatus(CellStatus.Dependent);

            //@todo: 数式を解析する
            // とりあえず全セルのSUMとする
            int sum = 0;
            foreach (ObjectBase obj in ObjectSpace.Instance.Objects)
            {
                foreach (Cell cell in obj.GetCells())
                {
                    if (cell == this)
                    {
                        continue;
                    }
                    sum += cell.Value;
                }
            }
            return sum;
        }
        else
        {
            SetStatus(isSelected ? CellStatus.Selected : CellStatus.Default);
        }

        // 整数として解析する
        int value = 0;
        if (int.TryParse(valueStr, out value))
        {
            return value;
        }
        else
        {
            return 0;
        }
    }
}
