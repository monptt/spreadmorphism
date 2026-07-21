using System.Collections.Generic;

public class Mat2Element : MatElement
{
    public override int Rows => 2;
    public override int Columns => 2;

    IntegerElement[] elements = new IntegerElement[4];
    public override IntegerElement[] Elements => elements;

    public Mat2Element()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                elements[i * Columns + j] = new IntegerElement(0);
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