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
}