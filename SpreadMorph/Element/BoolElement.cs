/// <summary>
/// bool
/// </summary>
public class BoolElement : ElementBase
{
    bool value = false;
    public bool Value => value;

    public BoolElement(bool value)
    {
        this.value = value;
    }

    public override string ToString()
    {
        if (value)
        {
            return "true";
        }
        else
        {
            return "false";
        }
    }
}