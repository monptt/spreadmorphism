public class Vec4Element : ElementBase
{
    IntegerElement x;
    IntegerElement y;
    IntegerElement z;
    IntegerElement w;

    public IntegerElement X => x;
    public IntegerElement Y => y;
    public IntegerElement Z => z;
    public IntegerElement W => w;

    public Vec4Element(IntegerElement x, IntegerElement y, IntegerElement z, IntegerElement w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
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