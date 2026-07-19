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
                else if (c == 'i')
                {
                    if (tempToken != "")
                    {
                        tokens.Add(new FormulaToken(tempToken));
                        tempToken = "";
                    }
                    tokens.Add(new FormulaToken("i"));
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
        // 無効な数式
        if (tokens.Count == 0)
        {
            return null;
        }

        // 数式の先頭が "=" の場合は、数式を評価する
        //@todo: 左辺に変数をおいて代入したい
        if (tokens.First().TokenStr == "=")
        {
            return Evaluate(tokens.Skip(1).ToList());
        }

        // 数値のみになったら値を評価
        if (tokens.Count == 1)
        {
            // 虚数単位
            if (tokens.First().TokenStr == "i")
            {
                return new ComplexElement(new IntegerElement(0), new IntegerElement(1));
            }

            // bool
            if (tokens.First().TokenStr.ToLower() == "true")
            {
                return new BoolElement(true);
            }
            if (tokens.First().TokenStr.ToLower() == "false")
            {
                return new BoolElement(false);
            }

            // 数値のみの想定
            bool parseResult = int.TryParse(tokens.First().TokenStr, out int value);
            if (!parseResult)
            {
                return null;
            }
            return new IntegerElement(value);
        }

        // 純虚数
        if (tokens.Count == 2 && tokens.Last().TokenStr == "i")
        {
            ElementBase im = Evaluate(tokens.Skip(0).Take(1).ToList());
            if (im is IntegerElement imElement)
            {
                return new ComplexElement(new IntegerElement(0), imElement);
            }
        }

        /*** ここから演算子の優先順に（外側になるものから）評価していく ***/
        // "+", "-"
        {
            int depth = 0;
            for (int i = tokens.Count - 1; i >= 0; i--)
            {
                if (tokens[i].TokenStr == "(" || tokens[i].TokenStr == "[")
                {
                    depth--;
                }
                if (tokens[i].TokenStr == ")" || tokens[i].TokenStr == "]")
                {
                    depth++;
                }

                if (depth == 0 && (tokens[i].TokenStr == "+" || tokens[i].TokenStr == "-"))
                {
                    ElementBase left = Evaluate(tokens.Take(i).ToList());
                    ElementBase right = Evaluate(tokens.Skip(i + 1).ToList());

                    if (right == null)
                    {
                        return null;
                    }

                    if (tokens[i].TokenStr == "+")
                    {
                        if (left == null)
                        {
                            return right;
                        }
                        return FuncSum.Sum(new List<ElementBase> { left, right });
                    }

                    if (tokens[i].TokenStr == "-")
                    {
                        if (left == null)
                        {
                            return FuncNegate.Negate(right);
                        }
                        return FuncSum.Sum(new List<ElementBase> { left, FuncNegate.Negate(right) });
                    }
                }

            }
        }

        // "*", "/"
        {
            int depth = 0;
            for (int i = tokens.Count - 1; i >= 0; i--)
            {
                if (tokens[i].TokenStr == "(" || tokens[i].TokenStr == "[")
                {
                    depth--;
                }
                if (tokens[i].TokenStr == ")" || tokens[i].TokenStr == "]")
                {
                    depth++;
                }

                if (depth == 0 && (tokens[i].TokenStr == "*" || tokens[i].TokenStr == "/"))
                {
                    ElementBase left = Evaluate(tokens.Take(i).ToList());
                    ElementBase right = Evaluate(tokens.Skip(i + 1).ToList());
                    if (left == null || right == null)
                    {
                        return null;
                    }

                    if (tokens[i].TokenStr == "*")
                    {
                        return FuncMultiply.Multiply(new List<ElementBase> { left, right });
                    }
                    if (tokens[i].TokenStr == "/")
                    {
                        return FuncDivide.Divide(left, right);
                    }
                }
            }
        }

        // 関数系
        if (funcNames.Contains(tokens.First().TokenStr.ToUpper()))
        {
            // 一旦引数をリスト化
            List<ElementBase> argElements = new List<ElementBase>();
            if (tokens.Count > 2)
            {
                if (tokens[1].TokenStr == "(" && tokens.Last().TokenStr == ")")
                {
                    List<List<FormulaToken>> argTokens = SplitArgsByComma(tokens.Skip(2).Take(tokens.Count - 3).ToList());

                    foreach (List<FormulaToken> argToken in argTokens)
                    {
                        ElementBase argElement = Evaluate(argToken);
                        argElements.Add(argElement);
                    }
                }
            }

            string funcName = tokens.First().TokenStr.ToUpper();
            if (funcName == "SUM")
            {
                return FuncSum.Sum(argElements);
            }
            return null;
        }

        // () で囲まれてるだけのものは中身を評価
        if (tokens.First().TokenStr == "(" && tokens.Last().TokenStr == ")")
        {
            return Evaluate(tokens.Skip(1).Take(tokens.Count - 2).ToList());
        }

        // [x, y] 形式は該当の座標にあるオブジェクトを取得
        if (tokens.First().TokenStr == "[" && tokens.Last().TokenStr == "]")
        {
            int separatorIndex = -1;    // "," の位置
            for (int i = 1; i < tokens.Count - 1; i++)
            {
                int depth = 0;
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
                    separatorIndex = i;
                    break;
                }
            }
            if (separatorIndex == -1)
            {
                return null;
            }

            // [x, y] 形式を想定
            ElementBase xElement = Evaluate(tokens.Skip(1).Take(separatorIndex - 1).ToList());
            ElementBase yElement = Evaluate(tokens.Skip(separatorIndex + 1).Take(tokens.Count - separatorIndex - 2).ToList());

            if (xElement is IntegerElement x && yElement is IntegerElement y)
            {
                GridPos pos = new GridPos(x.Value, y.Value);
                ObjectBase obj = ObjectSpace.Instance.GetObject(pos);
                if (obj != null)
                {
                    return obj.GetElement();
                }
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
