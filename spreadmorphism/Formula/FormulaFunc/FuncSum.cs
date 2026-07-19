using System.Collections.Generic;

/// <summary>
/// 引数の合計を計算する
/// </summary>
/// <param name="args">引数リスト</param>
/// <returns>合計値</returns>
public class FuncSum : FormulaFuncBase
{
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