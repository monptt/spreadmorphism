using System.Collections.Generic;

/// <summary>
/// 最大公約数
/// </summary>
/// <param name="a"></param>
/// <param name="b"></param>
/// <returns>最大公約数</returns>
public class FuncGCD : FormulaFuncBase
{
    /// <summary>
    /// 互除法により最大公約数を求める
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static IntegerElement GCD(IntegerElement a, IntegerElement b)
    {
        if (a.Value < b.Value)
        {
            IntegerElement temp = a;
            a = b;
            b = temp;
        }
        while (b.Value != 0)
        {
            IntegerElement temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
}
