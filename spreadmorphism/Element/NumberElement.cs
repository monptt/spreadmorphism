/// <summary>
/// 数
/// </summary>
public class IntegerElement : ElementBase
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
    /// 積
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static IntegerElement Multiply(IntegerElement a, IntegerElement b)
    {
        return new IntegerElement(a.Value * b.Value);
    }
}