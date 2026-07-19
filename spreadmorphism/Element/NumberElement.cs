/// <summary>
/// 数
/// </summary>
public class NumberElement : ElementBase
{
    int value;
    public int Value => value;

    public NumberElement(int value)
    {
        this.value = value;
    }

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static NumberElement Sum(NumberElement a, NumberElement b)
    {
        return new NumberElement(a.Value + b.Value);
    }

    /// <summary>
    /// 積
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static NumberElement Multiply(NumberElement a, NumberElement b)
    {
        return new NumberElement(a.Value * b.Value);
    }
}