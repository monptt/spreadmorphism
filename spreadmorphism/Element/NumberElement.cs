/// <summary>
/// 数
/// </summary>
public class NumberElement : ElementBase
{
    int value;
    public int Value => value;

    public NumberElement(int value)
    {
        this.value = value;
    }
}