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
}