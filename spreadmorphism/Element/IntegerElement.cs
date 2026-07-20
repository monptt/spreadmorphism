/// <summary>
/// 数
/// </summary>
public class IntegerElement : ElementBase, IInverse
{
    int value;
    public int Value => value;

    public IntegerElement(int value)
    {
        this.value = value;
    }

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static IntegerElement Sum(IntegerElement a, IntegerElement b)
    {
        return new IntegerElement(a.Value + b.Value);
    }

    /// <summary>
    /// 差
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static IntegerElement Subtract(IntegerElement a, IntegerElement b)
    {
        return new IntegerElement(a.Value - b.Value);
    }

    /// <summary>
    /// 積
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static IntegerElement Multiply(IntegerElement a, IntegerElement b)
    {
        return new IntegerElement(a.Value * b.Value);
    }

    public static IntegerElement operator +(IntegerElement a, IntegerElement b)
    {
        return Sum(a, b);
    }

    public static IntegerElement operator -(IntegerElement a, IntegerElement b)
    {
        return Subtract(a, b);
    }

    public static IntegerElement operator *(IntegerElement a, IntegerElement b)
    {
        return Multiply(a, b);
    }

    public static IntegerElement operator *(IntegerElement a, RationalElement b)
    {
        return a * (b.Inverse() as RationalElement);
    }

    public ElementBase Inverse()
    {
        return new RationalElement(new IntegerElement(1), this);
    }
}