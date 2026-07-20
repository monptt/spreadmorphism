using System.Collections.Generic;
using Godot;
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
        {
            if (dividend is IntegerElement a && divisor is IntegerElement b)
            {
                return new RationalElement(a, b);
            }
        }

        {
            if (dividend is RationalElement a && divisor is IntegerElement b)
            {
                return a * (b.Inverse() as RationalElement);
            }
        }


        {
            if (dividend is IntegerElement a && divisor is RationalElement b)
            {
                return a * (b.Inverse() as RationalElement);
            }
        }

        {
            if (dividend is RationalElement a && divisor is RationalElement b)
            {
                return a * (b.Inverse() as RationalElement);
            }
        }

        return null;
    }
}