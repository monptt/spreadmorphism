using System.Collections.Generic;

public class Mat2Element : ElementBase
{
    IntegerElement[] elements = new IntegerElement[4];
    public IntegerElement[] Elements => elements;

    public Mat2Element()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                elements[i * 2 + j] = new IntegerElement(0);
            }
        }
    }

    public Mat2Element(IntegerElement a, IntegerElement b, IntegerElement c, IntegerElement d)
    {
        elements[0] = a;
        elements[1] = b;
        elements[2] = c;
        elements[3] = d;
    }
}