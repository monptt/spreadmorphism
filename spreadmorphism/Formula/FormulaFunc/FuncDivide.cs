using System.Collections.Generic;

/// <summary>
/// 割り算
/// </summary>
/// <param name="dividend">被除数</param>
/// <param name="divisor">除数</param>
/// <returns>商</returns>
public class FuncDivide : FormulaFuncBase
{
    public static ElementBase Divide(ElementBase dividend, ElementBase divisor)
    {
        if (dividend is NumberElement a && divisor is NumberElement b)
        {
            return new NumberElement(a.Value / b.Value);
        }
        return null;
    }
}