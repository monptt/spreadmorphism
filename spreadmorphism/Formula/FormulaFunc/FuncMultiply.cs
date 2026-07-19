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
            ComplexElement product = new ComplexElement(new IntegerElement(1), new IntegerElement(0));
            foreach (ElementBase arg in args)
            {
                if (arg is ComplexElement complexElement)
                {
                    product = ComplexElement.Multiply(product, complexElement);
                }
                else if (arg is IntegerElement integerElement)
                {
                    product = ComplexElement.Multiply(product, integerElement);
                }
            }
            return product;
        }


        if (args[0] is IntegerElement)
        {
            IntegerElement product = new IntegerElement(1);
            foreach (ElementBase arg in args)
            {
                if (arg is IntegerElement numberElement)
                {
                    product = IntegerElement.Multiply(product, numberElement);
                }
            }
            return product;
        }
        return null;
    }
}