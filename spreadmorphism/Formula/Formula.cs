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
                List<List<FormulaToken>> argTokens = SplitArgsByComma(tokens.Skip(2).Take(tokens.Count - 3).ToList());

                List<ElementBase> argElements = new List<ElementBase>();
                foreach (List<FormulaToken> argToken in argTokens)
                {
                    ElementBase argElement = Evaluate(argToken);
                    argElements.Add(argElement);
                    GD.Print(argElement.GetType());
                }
                return Sum(argElements);
            }
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

    /// <summary>
    /// 引数の合計を計算する
    /// </summary>
    /// <param name="args">引数リスト</param>
    /// <returns>合計値</returns>
    ElementBase Sum(List<ElementBase> args)
    {
        if (args.Count == 0)
        {
            return null;
        }

        if (args.Count == 1)
        {
            return args[0];
        }

        if (args[0] is NumberElement)
        {
            NumberElement sum = new NumberElement(0);
            foreach (ElementBase arg in args)
            {
                if (arg is NumberElement numberElement)
                {
                    sum = NumberElement.Sum(sum, numberElement);
                }
            }
            return sum;
        }
        else if (args[0] is Vec2Element)
        {
            Vec2Element sum = new Vec2Element(new NumberElement(0), new NumberElement(0));
            foreach (ElementBase arg in args)
            {
                if (arg is Vec2Element vec2Element)
                {
                    sum = Vec2Element.Sum(sum, vec2Element);
                }
            }
            return sum;
        }
        else if (args[0] is Vec3Element)
        {
            Vec3Element sum = new Vec3Element(new NumberElement(0), new NumberElement(0), new NumberElement(0));
            foreach (ElementBase arg in args)
            {
                if (arg is Vec3Element vec3Element)
                {
                    sum = Vec3Element.Sum(sum, vec3Element);
                }
            }
            return sum;
        }
        return null;
    }
}