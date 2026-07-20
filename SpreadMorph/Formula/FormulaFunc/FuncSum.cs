using System.Collections.Generic;

/// <summary>
/// 引数の合計を計算する
/// </summary>
/// <param name="args">引数リスト</param>
/// <returns>合計値</returns>
public class FuncSum : FormulaFuncBase
{
    /// <summary>
    /// 2つの引数に対応する場合
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static ElementBase Sum(ElementBase a, ElementBase b)
    {
        {
            if (a is IntegerElement integerElementA && b is IntegerElement integerElementB)
            {
                return IntegerElement.Sum(integerElementA, integerElementB);
            }
            if (a is ComplexElement complexElementA && b is ComplexElement complexElementB)
            {
                return ComplexElement.Sum(complexElementA, complexElementB);
            }
        }

        // 多項式
        {
            if (a is PolynomialElement polynomialElementA && b is PolynomialElement polynomialElementB)
            {
                return PolynomialElement.Add(polynomialElementA, polynomialElementB);
            }
        }
        {
            if (a is PolynomialElement polynomialElementA && b is IntegerElement integerElementB)
            {
                return PolynomialElement.Add(polynomialElementA, new PolynomialElement(integerElementB));
            }
        }

        return null;
    }

    /// <summary>
    /// 3以上の引数に対応する場合
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static ElementBase Sum(List<ElementBase> args)
    {
        if (args.Count == 0)
        {
            return null;
        }

        if (args.Count == 1)
        {
            return args[0];
        }

        // 複素数が含まれてたら複素数として計算
        bool isComplex = false;
        foreach (ElementBase arg in args)
        {
            if (arg is ComplexElement)
            {
                isComplex = true;
                break;
            }
        }
        if (isComplex)
        {
            ComplexElement sum = new ComplexElement(new IntegerElement(0), new IntegerElement(0));
            foreach (ElementBase arg in args)
            {
                if (arg is ComplexElement complexElement)
                {
                    sum = ComplexElement.Sum(sum, complexElement);
                }
                else if (arg is IntegerElement integerElement)
                {
                    sum = ComplexElement.Sum(sum, integerElement);
                }
            }
            return sum;
        }

        if (args[0] is IntegerElement)
        {
            IntegerElement sum = new IntegerElement(0);
            foreach (ElementBase arg in args)
            {
                if (arg is IntegerElement numberElement)
                {
                    sum = IntegerElement.Sum(sum, numberElement);
                }
            }
            return sum;
        }
        else if (args[0] is Vec2Element)
        {
            Vec2Element sum = new Vec2Element(new IntegerElement(0), new IntegerElement(0));
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
            Vec3Element sum = new Vec3Element(new IntegerElement(0), new IntegerElement(0), new IntegerElement(0));
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