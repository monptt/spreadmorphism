/// <summary>
/// 有理数クラス
/// </summary>
public class RationalElement : ElementBase
{
    int numerator = 0;
    public int Numerator => numerator;

    int denominator = 1;
    public int Denominator => denominator;

    public RationalElement(int numerator, int denominator)
    {
        this.numerator = numerator;
        this.denominator = denominator;
    }
}