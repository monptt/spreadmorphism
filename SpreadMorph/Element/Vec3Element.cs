public class Vec3Element : ElementBase
{
    IntegerElement x;
    IntegerElement y;
    IntegerElement z;

    public IntegerElement X => x;
    public IntegerElement Y => y;
    public IntegerElement Z => z;

    public Vec3Element(IntegerElement x, IntegerElement y, IntegerElement z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Vec3Element Sum(Vec3Element a, Vec3Element b)
    {
        return new Vec3Element(IntegerElement.Sum(a.X, b.X), IntegerElement.Sum(a.Y, b.Y), IntegerElement.Sum(a.Z, b.Z));
    }
}