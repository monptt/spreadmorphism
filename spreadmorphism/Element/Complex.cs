/// <summary>
/// 複素数
/// </summary>
public class ComplexElement : ElementBase
{
    IntegerElement re;
    IntegerElement im;
    public IntegerElement Re => re;
    public IntegerElement Im => im;

    public ComplexElement(IntegerElement re, IntegerElement im)
    {
        this.re = re;
        this.im = im;
    }

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static ComplexElement Sum(ComplexElement a, ComplexElement b)
    {
        return new ComplexElement(a.Re + b.Re, a.Im + b.Im);
    }

    public static ComplexElement Subtract(ComplexElement a, ComplexElement b)
    {
        return new ComplexElement(a.Re - b.Re, a.Im - b.Im);
    }

    /// <summary>
    /// 積
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static ComplexElement Multiply(ComplexElement a, ComplexElement b)
    {
        return new ComplexElement(
            a.Re * b.Re - a.Im * b.Im,
            a.Re * b.Im + a.Im * b.Re
        );
    }
}