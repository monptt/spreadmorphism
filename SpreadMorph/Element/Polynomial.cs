using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 多項式
/// </summary>
public class PolynomialElement : ElementBase
{
    /// <summary>
    /// 次数と係数のペア
    /// </summary>
    Dictionary<IntegerElement, IntegerElement> coefficients = new Dictionary<IntegerElement, IntegerElement>();

    public PolynomialElement()
    {
    }

    /// <summary>
    /// 次数と係数から単項式を作成
    /// </summary>
    /// <param name="degree">次数</param>
    /// <param name="coefficient">係数</param>
    public PolynomialElement(IntegerElement degree, IntegerElement coefficient)
    {
        coefficients.Add(degree, coefficient);
    }


    public override string ToString()
    {
        if (coefficients.Count == 0)
        {
            return "0";
        }

        string str = "";
        var sortedCoefficients = coefficients.OrderBy(pair => pair.Key).ToList();
        for (int i = 0; i < sortedCoefficients.Count; i++)
        {
            if (i != 0)
            {
                str += "+";
            }

            var pair = sortedCoefficients[i];
            IntegerElement degree = pair.Key;
            IntegerElement coefficient = pair.Value;

            string coefficientStr = ""; // 係数の部分
            string xStr = ""; // x^n の部分

            if (coefficient.Value == 1)
            {
                coefficientStr = "";
            }
            else
            {
                coefficientStr = coefficient.ToString();
            }

            if (degree.Value == 0)
            {
                xStr = "";
            }
            if (degree.Value == 1)
            {
                xStr = "x";
            }
            else
            {
                xStr = $"x^{degree.Value}";
            }

            str += $"{coefficientStr}{xStr}";
        }
        return str;
    }

    public static PolynomialElement Pow(PolynomialElement polynomialElement, IntegerElement exponent)
    {
        if (exponent.Value == 0)
        {
            return new PolynomialElement(new IntegerElement(0), new IntegerElement(1));
        }


        // x のみの場合
        if (polynomialElement.coefficients.Count == 1 && polynomialElement.coefficients.First().Key.Value == 1 && polynomialElement.coefficients.First().Value.Value == 1)
        {
            return new PolynomialElement(new IntegerElement(exponent.Value), new IntegerElement(1));
        }

        return null;    // 未実装
    }
}