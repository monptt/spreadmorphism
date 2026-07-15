using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

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
        List<FormulaToken> tokens = Tokenize(formulaStr);
    }

    /// <summary>
    /// 数式をトークンに分割する
    /// </summary>
    /// <param name="formulaStr">数式文字列</param>
    /// <returns>トークンリスト</returns>
    public List<FormulaToken> Tokenize(string formulaStr)
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

        return tokens;
    }

    /// <summary>
    /// 数式を評価する
    /// </summary>
    /// <param name="tokens">トークンリスト</param>
    /// <returns>値</returns>
    public ElementBase Evaluate(List<FormulaToken> tokens)
    {
        if (tokens.Count == 0)
        {
            return null;
        }

        if (tokens[0].TokenStr == "=")
        {
            return Evaluate(tokens.Skip(1).ToList());
        }

        if (tokens.Count == 1)
        {
            // 数値のみの想定
            bool parseResult = int.TryParse(tokens[0].TokenStr, out int value);
            if (!parseResult)
            {
                return null;
            }
            return new NumberElement(value);
        }

        if (tokens[0].TokenStr == "[" && tokens.Count == 5 && tokens[2].TokenStr == "," && tokens[4].TokenStr == "]")
        {
            // [x, y] 形式を想定
            int x = 0;
            int y = 0;
            bool parseResult = int.TryParse(tokens[1].TokenStr, out x) && int.TryParse(tokens[3].TokenStr, out y);
            if (!parseResult)
            {
                return null;
            }
            GridPos pos = new GridPos(x, y);
            ObjectBase obj = ObjectSpace.Instance.GetObject(pos);
            if (obj == null)
            {
                return null;
            }
            return obj.GetElement();
        }

        // 関数系
        if (tokens[0].TokenStr == "SUM" && tokens.Count > 2)
        {
            if (tokens[1].TokenStr == "(" && tokens[tokens.Count - 1].TokenStr == ")")
            {
                // ","で分割して、左側と右側とを足す
                //@todo: 複数引数にも対応する
                List<FormulaToken> leftTokens = new List<FormulaToken>();
                List<FormulaToken> rightTokens = new List<FormulaToken>();
                bool isLeft = true;
                int depth = 0;
                for (int i = 2; i < tokens.Count - 1; i++)
                {
                    if (tokens[i].TokenStr == "(" || tokens[i].TokenStr == "[")
                    {
                        depth++;
                    }
                    if (tokens[i].TokenStr == ")" || tokens[i].TokenStr == "]")
                    {
                        depth--;
                    }

                    if (tokens[i].TokenStr == "," && depth == 0)
                    {
                        isLeft = !isLeft;
                        continue;
                    }
                    if (isLeft)
                    {
                        leftTokens.Add(tokens[i]);
                    }
                    else
                    {
                        rightTokens.Add(tokens[i]);
                    }
                }

                ElementBase leftElement = Evaluate(leftTokens);
                ElementBase rightElement = Evaluate(rightTokens);
                if (leftElement == null || rightElement == null)
                {
                    return null;
                }

                // 数値が得られた場合
                if (leftElement is NumberElement leftNumberElement && rightElement is NumberElement rightNumberElement)
                {
                    return new NumberElement(leftNumberElement.Value + rightNumberElement.Value);
                }
            }
        }

        return null;
    }
}
