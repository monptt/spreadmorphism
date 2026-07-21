using System.Collections.Generic;

public class Mat3Element : ElementBase
{
    IntegerElement[] elements = new IntegerElement[9];
    public IntegerElement[] Elements => elements;

    public Mat3Element()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                elements[i * 3 + j] = new IntegerElement(0);
            }
        }
    }

    public Mat3Element(IntegerElement a, IntegerElement b, IntegerElement c,
                        IntegerElement d, IntegerElement e, IntegerElement f,
                        IntegerElement g, IntegerElement h, IntegerElement i)
    {
        elements[0] = a;
        elements[1] = b;
        elements[2] = c;
        elements[3] = d;
        elements[4] = e;
        elements[5] = f;
        elements[6] = g;
        elements[7] = h;
        elements[8] = i;
    }
}