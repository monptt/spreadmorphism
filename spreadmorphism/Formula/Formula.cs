using Godot;
using System;
using System.Collections.Generic;

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

        // 解析
        Tokenize(formulaStr);
    }

    /// <summary>
    /// 数式をトークンに分割する
    /// </summary>
    /// <param name="formulaStr">数式文字列</param>
    /// <returns>トークンリスト</returns>
    List<FormulaToken> Tokenize(string formulaStr)
    {
        List<FormulaToken> tokens = new List<FormulaToken>();

        // 数式文字列をトークンに分割
        foreach (string str in formulaStr.Split(' ')) // 一旦スペースで分割
        {
            if (str == "")
            {
                continue;
            }

            string tempToken = "";
            foreach (char c in str)
            {
                if ("+-*/()[]{},<>=!".Contains(c))
                {
                    if (tempToken != "")
                    {
                        tokens.Add(new FormulaToken(tempToken));
                        tempToken = "";
                    }
                    tokens.Add(new FormulaToken(c.ToString()));
                }
                else
                {
                    tempToken += c;
                }
            }
            if (tempToken != "")
            {
                tokens.Add(new FormulaToken(tempToken));
            }
        }



        // 出力
        string output = "";
        foreach (FormulaToken token in tokens)
        {
            output += $"\"{token.TokenStr}\", ";
        }
        GD.Print(output);

        return tokens;
    }
}
