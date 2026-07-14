using Godot;
using System;

/// <summary>
/// 数式クラス
/// </summary>
public class Formula
{
    string formulaStr = "";
    public string FormulaStr => formulaStr;


    public Formula(string formulaStr)
    {
        this.formulaStr = formulaStr;
    }
}
