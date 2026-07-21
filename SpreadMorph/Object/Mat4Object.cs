public partial class Mat4Object : ObjectBase
{
    public override ObjectType Type => ObjectType.Mat4;

    Mat4Element element = new Mat4Element();

    protected override void InitView()
    {
        ObjectView.GetCells()[0].SetFormula("0");
        ObjectView.GetCells()[1].SetFormula("0");
        ObjectView.GetCells()[2].SetFormula("0");
        ObjectView.GetCells()[3].SetFormula("0");
        ObjectView.GetCells()[4].SetFormula("0");
        ObjectView.GetCells()[5].SetFormula("0");
        ObjectView.GetCells()[6].SetFormula("0");
        ObjectView.GetCells()[7].SetFormula("0");
        ObjectView.GetCells()[8].SetFormula("0");
        ObjectView.GetCells()[9].SetFormula("0");
        ObjectView.GetCells()[10].SetFormula("0");
        ObjectView.GetCells()[11].SetFormula("0");
        ObjectView.GetCells()[12].SetFormula("0");
        ObjectView.GetCells()[13].SetFormula("0");
        ObjectView.GetCells()[14].SetFormula("0");
        ObjectView.GetCells()[15].SetFormula("0");
    }

    public override void UpdateObject()
    {
        if (IsOneObject)
        {
            bool result = EvaluateFormula(this.Formula);
            this.SetIsError(!result);
        }
        else
        {
            ElementBase a = ObjectView.GetCells()[0].Formula.Evaluate();
            ElementBase b = ObjectView.GetCells()[1].Formula.Evaluate();
            ElementBase c = ObjectView.GetCells()[2].Formula.Evaluate();
            ElementBase d = ObjectView.GetCells()[3].Formula.Evaluate();
            ElementBase e = ObjectView.GetCells()[4].Formula.Evaluate();
            ElementBase f = ObjectView.GetCells()[5].Formula.Evaluate();
            ElementBase g = ObjectView.GetCells()[6].Formula.Evaluate();
            ElementBase h = ObjectView.GetCells()[7].Formula.Evaluate();
            ElementBase i = ObjectView.GetCells()[8].Formula.Evaluate();
            ElementBase j = ObjectView.GetCells()[9].Formula.Evaluate();
            ElementBase k = ObjectView.GetCells()[10].Formula.Evaluate();
            ElementBase l = ObjectView.GetCells()[11].Formula.Evaluate();
            ElementBase m = ObjectView.GetCells()[12].Formula.Evaluate();
            ElementBase n = ObjectView.GetCells()[13].Formula.Evaluate();
            ElementBase o = ObjectView.GetCells()[14].Formula.Evaluate();
            ElementBase p = ObjectView.GetCells()[15].Formula.Evaluate();
            if (a is IntegerElement numA && b is IntegerElement numB && c is IntegerElement numC && d is IntegerElement numD && e is IntegerElement numE && f is IntegerElement numF && g is IntegerElement numG && h is IntegerElement numH && i is IntegerElement numI && j is IntegerElement numJ && k is IntegerElement numK && l is IntegerElement numL && m is IntegerElement numM && n is IntegerElement numN && o is IntegerElement numO && p is IntegerElement numP)
            {
                SetElement(new Mat4Element(numA, numB, numC, numD, numE, numF, numG, numH, numI, numJ, numK, numL, numM, numN, numO, numP));
            }
            else
            {
                SetElement(new Mat4Element());
            }
        }
    }

    public override ElementBase GetElement()
    {
        IntegerElement a = ObjectView.GetCells()[0].Element as IntegerElement;
        IntegerElement b = ObjectView.GetCells()[1].Element as IntegerElement;
        IntegerElement c = ObjectView.GetCells()[2].Element as IntegerElement;
        IntegerElement d = ObjectView.GetCells()[3].Element as IntegerElement;
        IntegerElement e = ObjectView.GetCells()[4].Element as IntegerElement;
        IntegerElement f = ObjectView.GetCells()[5].Element as IntegerElement;
        IntegerElement g = ObjectView.GetCells()[6].Element as IntegerElement;
        IntegerElement h = ObjectView.GetCells()[7].Element as IntegerElement;
        IntegerElement i = ObjectView.GetCells()[8].Element as IntegerElement;
        IntegerElement j = ObjectView.GetCells()[9].Element as IntegerElement;
        IntegerElement k = ObjectView.GetCells()[10].Element as IntegerElement;
        IntegerElement l = ObjectView.GetCells()[11].Element as IntegerElement;
        IntegerElement m = ObjectView.GetCells()[12].Element as IntegerElement;
        IntegerElement n = ObjectView.GetCells()[13].Element as IntegerElement;
        IntegerElement o = ObjectView.GetCells()[14].Element as IntegerElement;
        IntegerElement p = ObjectView.GetCells()[15].Element as IntegerElement;
        return new Mat4Element(a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p);
    }

    void SetElement(Mat4Element element)
    {
        this.element = element;
        ObjectView.GetCells()[0].SetElement(element.Elements[0]);
        ObjectView.GetCells()[1].SetElement(element.Elements[1]);
        ObjectView.GetCells()[2].SetElement(element.Elements[2]);
        ObjectView.GetCells()[3].SetElement(element.Elements[3]);
        ObjectView.GetCells()[4].SetElement(element.Elements[4]);
        ObjectView.GetCells()[5].SetElement(element.Elements[5]);
        ObjectView.GetCells()[6].SetElement(element.Elements[6]);
        ObjectView.GetCells()[7].SetElement(element.Elements[7]);
        ObjectView.GetCells()[8].SetElement(element.Elements[8]);
        ObjectView.GetCells()[9].SetElement(element.Elements[9]);
        ObjectView.GetCells()[10].SetElement(element.Elements[10]);
        ObjectView.GetCells()[11].SetElement(element.Elements[11]);
        ObjectView.GetCells()[12].SetElement(element.Elements[12]);
        ObjectView.GetCells()[13].SetElement(element.Elements[13]);
        ObjectView.GetCells()[14].SetElement(element.Elements[14]);
        ObjectView.GetCells()[15].SetElement(element.Elements[15]);
    }

    protected override bool EvaluateFormula(Formula formula)
    {
        ElementBase result = formula.Evaluate();
        if (result is Mat4Element mat4Element)
        {
            SetElement(mat4Element);
            return true;
        }
        else
        {
            SetElement(new Mat4Element());
        }
        return false;
    }
}
