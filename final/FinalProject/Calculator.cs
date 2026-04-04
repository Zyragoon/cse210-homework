
public class Calculator : CalculatorBase
{
    private double _firstOperand;
    private double _currentValue;
    private Operation _pendingOperation;
    private bool _isOperationPending;

    public Calculator() : base()
    {
        _firstOperand = 0;
        _currentValue = 0;
        _pendingOperation = null;
        _isOperationPending = false;
    }

    
    public void EnterNumber(double number)
    {
        _currentValue = number;
    }

    public bool SetOperation(Operation operation)
    {
        bool calculated = false;
        if (_isOperationPending)
        {
            // If operation is pending, calculate with current value as second operand
            _result = _pendingOperation.Execute(_firstOperand, _currentValue);
            _firstOperand = _result;
            calculated = true;
        }
        else
        {
            _firstOperand = _currentValue;
        }
        _pendingOperation = operation;
        _isOperationPending = true;
        return calculated;
    }

    public double Calculate()
    {
        if (_pendingOperation != null && _isOperationPending)
        {
            _result = _pendingOperation.Execute(_firstOperand, _currentValue);
            _pendingOperation = null;
            _isOperationPending = false;
            _firstOperand = _result;
        }
        return _result;
    }

    public override double GetResult() => _result;

    public override void Reset()
    {
        base.Reset();
        _firstOperand = 0;
        _currentValue = 0;
        _pendingOperation = null;
        _isOperationPending = false;
    }
}