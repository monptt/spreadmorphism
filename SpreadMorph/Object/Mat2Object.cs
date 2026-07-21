public partial class Mat2Object : ObjectBase
{
    public override ObjectType Type => ObjectType.Mat2;

    Mat2Element element = new Mat2Element();

    protected override void InitView()
    {
        ObjectView.GetCells()[0].SetFormula("0");
        ObjectView.GetCells()[1].SetFormula("0");
        ObjectView.GetCells()[2].SetFormula("0");
        ObjectView.GetCells()[3].SetFormula("0");
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
            if (a is IntegerElement numA && b is IntegerElement numB && c is IntegerElement numC && d is IntegerElement numD)
            {
                SetElement(new Mat2Element(numA, numB, numC, numD));
            }
            else
            {
                SetElement(new Mat2Element());
            }
        }
    }

    public override ElementBase GetElement()
    {
        IntegerElement a = ObjectView.GetCells()[0].Element as IntegerElement;
        IntegerElement b = ObjectView.GetCells()[1].Element as IntegerElement;
        IntegerElement c = ObjectView.GetCells()[2].Element as IntegerElement;
        IntegerElement d = ObjectView.GetCells()[3].Element as IntegerElement;
        return new Mat2Element(a, b, c, d);
    }

    void SetElement(Mat2Element element)
    {
        this.element = element;
        ObjectView.GetCells()[0].SetElement(element.Elements[0]);
        ObjectView.GetCells()[1].SetElement(element.Elements[1]);
        ObjectView.GetCells()[2].SetElement(element.Elements[2]);
        ObjectView.GetCells()[3].SetElement(element.Elements[3]);
    }

    protected override bool EvaluateFormula(Formula formula)
    {
        ElementBase result = formula.Evaluate();
        if (result is Mat2Element mat2Element)
        {
            SetElement(mat2Element);
            return true;
        }
        else
        {
            SetElement(new Mat2Element());
        }
        return false;
    }
}
