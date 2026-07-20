public class Vec2Element : ElementBase
{
    IntegerElement x;
    IntegerElement y;

    public IntegerElement X => x;
    public IntegerElement Y => y;

    public Vec2Element(IntegerElement x, IntegerElement y)
    {
        this.x = x;
        this.y = y;
    }

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Vec2Element Sum(Vec2Element a, Vec2Element b)
    {
        return new Vec2Element(IntegerElement.Sum(a.X, b.X), IntegerElement.Sum(a.Y, b.Y));
    }
}