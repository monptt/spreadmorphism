public abstract class VecElement : ElementBase
{
    public abstract int Dim { get; }

    public abstract IntegerElement[] Elements { get; }

    public IntegerElement GetElement(int i)
    {
        return Elements[i];
    }
}