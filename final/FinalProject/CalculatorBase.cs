
public class CalculatorBase
{
    protected double _result;

    public CalculatorBase()
    {
        _result = 0;
    }

    public virtual double GetResult()
    {
        return _result;
    }

    public virtual void Reset()
    {
        _result = 0;
    }
}