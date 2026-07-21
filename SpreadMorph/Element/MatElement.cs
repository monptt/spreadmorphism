public abstract class MatElement : ElementBase
{
    public abstract int Rows { get; }
    public abstract int Columns { get; }

    public abstract IntegerElement[] Elements { get; }

    public IntegerElement GetElement(int i, int j)
    {
        return Elements[i * Columns + j];
    }
}