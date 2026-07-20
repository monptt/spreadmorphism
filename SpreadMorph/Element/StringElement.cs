public class StringElement : ElementBase
{
    string value;

    public string Value => value;

    public StringElement(string value)
    {
        this.value = value;
    }
}