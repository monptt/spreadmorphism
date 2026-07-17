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

    /// <summary>
    /// 関数名として認識されるトークンリスト
    /// </summary>
    List<string> funcNames = new List<string> { "SUM" };


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

    public ElementBase Evaluate()
    {
        return Evaluate(Tokenize(formulaStr));
    }

    /// <summary>
    /// 数式を評価する
    /// </summary>
    /// <param name="tokens">トークンリスト</param>
    /// <returns>値</returns>
    ElementBase Evaluate(List<FormulaToken> tokens)
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
        if (funcNames.Contains(tokens[0].TokenStr))
        {
            // 一旦引数をリスト化
            List<ElementBase> argElements = new List<ElementBase>();
            if (tokens.Count > 2)
            {
                if (tokens[1].TokenStr == "(" && tokens[tokens.Count - 1].TokenStr == ")")
                {
                    List<List<FormulaToken>> argTokens = SplitArgsByComma(tokens.Skip(2).Take(tokens.Count - 3).ToList());

                    foreach (List<FormulaToken> argToken in argTokens)
                    {
                        ElementBase argElement = Evaluate(argToken);
                        argElements.Add(argElement);
                    }
                }
            }

            string funcName = tokens[0].TokenStr;
            if (funcName == "SUM")
            {
                return FuncSum.Sum(argElements);
            }

            return null;
        }


        return null;
    }

    /// <summary>
    /// (xxx, yyy, zzz) 形式を ","で分割する
    /// </summary>
    /// <param name="tokens"></param>
    /// <returns></returns>
    List<List<FormulaToken>> SplitArgsByComma(List<FormulaToken> tokens)
    {
        List<List<FormulaToken>> result = new List<List<FormulaToken>>();
        List<FormulaToken> current = new List<FormulaToken>();

        int depth = 0;
        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i].TokenStr == "(" || tokens[i].TokenStr == "[")
            {
                depth++;
            }
            if (tokens[i].TokenStr == ")" || tokens[i].TokenStr == "]")
            {
                depth--;
            }

            if (depth == 0 && tokens[i].TokenStr == ",")
            {
                if (current.Count > 0)
                {
                    result.Add(current);
                    current = new List<FormulaToken>();
                }
            }
            else
            {
                current.Add(tokens[i]);
            }
        }

        if (current.Count > 0)
        {
            result.Add(current);
        }
        return result;
    }
}