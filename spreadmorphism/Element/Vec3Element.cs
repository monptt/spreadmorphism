public class Vec3Element : ElementBase
{
    NumberElement x;
    NumberElement y;
    NumberElement z;

    public NumberElement X => x;
    public NumberElement Y => y;
    public NumberElement Z => z;

    public Vec3Element(NumberElement x, NumberElement y, NumberElement z)
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
        return new Vec3Element(NumberElement.Sum(a.X, b.X), NumberElement.Sum(a.Y, b.Y), NumberElement.Sum(a.Z, b.Z));
    }
}