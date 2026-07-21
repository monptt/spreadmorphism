public partial class Mat3Object : ObjectBase
{
    public override ObjectType Type => ObjectType.Mat3;

    Mat3Element element = new Mat3Element();

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
            if (a is IntegerElement numA && b is IntegerElement numB && c is IntegerElement numC && d is IntegerElement numD && e is IntegerElement numE && f is IntegerElement numF && g is IntegerElement numG && h is IntegerElement numH && i is IntegerElement numI)
            {
                SetElement(new Mat3Element(numA, numB, numC, numD, numE, numF, numG, numH, numI));
            }
            else
            {
                SetElement(new Mat3Element());
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
        return new Mat3Element(a, b, c, d, e, f, g, h, i);
    }

    void SetElement(Mat3Element element)
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
    }

    protected override bool EvaluateFormula(Formula formula)
    {
        ElementBase result = formula.Evaluate();
        if (result is Mat3Element mat3Element)
        {
            SetElement(mat3Element);
            return true;
        }
        else
        {
            SetElement(new Mat3Element());
        }
        return false;
    }
}
