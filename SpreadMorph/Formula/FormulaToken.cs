using Godot;
using System;

public class FormulaToken
{
    string tokenStr = "";
    public string TokenStr => tokenStr;

    public FormulaToken(string tokenStr)
    {
        this.tokenStr = tokenStr;
    }
}
