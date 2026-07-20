/// <summary>
/// 有理数クラス
/// </summary>
public class RationalElement : ElementBase, IInverse
{
    IntegerElement numerator = new IntegerElement(0);
    public IntegerElement Numerator => numerator;

    IntegerElement denominator = new IntegerElement(1);
    public IntegerElement Denominator => denominator;

    public RationalElement(IntegerElement numerator, IntegerElement denominator)
    {
        this.numerator = numerator;
        this.denominator = denominator;

        // 分子が0
        if (numerator.Value == 0)
        {
            this.denominator = new IntegerElement(1);
        }

        // 約分
        IntegerElement gcd = FuncGCD.GCD(numerator, denominator);
        this.numerator = (numerator / gcd) as IntegerElement;
        this.denominator = (denominator / gcd) as IntegerElement;
    }

    public ElementBase Inverse()
    {
        return new RationalElement(this.denominator, this.numerator);
    }

    public static RationalElement Multiply(RationalElement a, RationalElement b)
    {
        return new RationalElement(a.numerator * b.numerator, a.denominator * b.denominator);
    }

    public static RationalElement operator *(RationalElement a, RationalElement b)
    {
        return Multiply(a, b);
    }
}