public class Vec2Element : ElementBase
{
    NumberElement x;
    NumberElement y;

    public NumberElement X => x;
    public NumberElement Y => y;

    public Vec2Element(NumberElement x, NumberElement y)
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
        return new Vec2Element(NumberElement.Sum(a.X, b.X), NumberElement.Sum(a.Y, b.Y));
    }
}