public class Vec3Element : VecElement
{
    public override int Dim => 3;

    IntegerElement[] elements = new IntegerElement[3];
    public override IntegerElement[] Elements => elements;


    public IntegerElement X => elements[0];
    public IntegerElement Y => elements[1];
    public IntegerElement Z => elements[2];

    public Vec3Element(IntegerElement x, IntegerElement y, IntegerElement z)
    {
        elements[0] = x;
        elements[1] = y;
        elements[2] = z;
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