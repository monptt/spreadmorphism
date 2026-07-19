using System.Collections.Generic;

/// <summary>
/// 引数の合計を計算する
/// </summary>
/// <param name="args">引数リスト</param>
/// <returns>合計値</returns>
public class FuncMultiply : FormulaFuncBase
{
    public static ElementBase Multiply(List<ElementBase> args)
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
            NumberElement product = new NumberElement(1);
            foreach (ElementBase arg in args)
            {
                if (arg is NumberElement numberElement)
                {
                    product = NumberElement.Multiply(product, numberElement);
                }
            }
            return product;
        }
        return null;
    }
}