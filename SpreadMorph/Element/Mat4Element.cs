using System.Collections.Generic;

public class Mat4Element : ElementBase
{
    IntegerElement[] elements = new IntegerElement[16];
    public IntegerElement[] Elements => elements;

    public Mat4Element()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                elements[i * 4 + j] = new IntegerElement(0);
            }
        }
    }

    public Mat4Element(IntegerElement a, IntegerElement b, IntegerElement c, IntegerElement d,
                        IntegerElement e, IntegerElement f, IntegerElement g, IntegerElement h,
                        IntegerElement i, IntegerElement j, IntegerElement k, IntegerElement l,
                        IntegerElement m, IntegerElement n, IntegerElement o, IntegerElement p)
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
        elements[9] = j;
        elements[10] = k;
        elements[11] = l;
        elements[12] = m;
        elements[13] = n;
        elements[14] = o;
        elements[15] = p;
    }
}