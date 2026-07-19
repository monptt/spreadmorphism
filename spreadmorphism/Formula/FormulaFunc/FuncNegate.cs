using System.Collections.Generic;

/// <summary>
/// 引数の合計を計算する
/// </summary>
/// <param name="args">引数リスト</param>
/// <returns>合計値</returns>
public class FuncNegate : FormulaFuncBase
{
    public static ElementBase Negate(ElementBase element)
    {
        if (element is NumberElement numberElement)
        {
            return new NumberElement(-numberElement.Value);
        }

        return null;
    }
}