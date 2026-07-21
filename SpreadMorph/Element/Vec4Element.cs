public class Vec4Element : VecElement
{
    public override int Dim => 4;

    IntegerElement[] elements = new IntegerElement[4];
    public override IntegerElement[] Elements => elements;


    public IntegerElement X => elements[0];
    public IntegerElement Y => elements[1];
    public IntegerElement Z => elements[2];
    public IntegerElement W => elements[3];

    public Vec4Element(IntegerElement x, IntegerElement y, IntegerElement z, IntegerElement w)
    {
        elements[0] = x;
        elements[1] = y;
        elements[2] = z;
        elements[3] = w;
    }

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Vec4Element Sum(Vec4Element a, Vec4Element b)
    {
        return new Vec4Element(IntegerElement.Sum(a.X, b.X), IntegerElement.Sum(a.Y, b.Y), IntegerElement.Sum(a.Z, b.Z), IntegerElement.Sum(a.W, b.W));
    }
}