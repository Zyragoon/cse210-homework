
public abstract class Operation
{
    public abstract double Execute(double a, double b);
}

// Concrete operations demonstrating Polymorphism
public class AddOperation : Operation
{
    public override double Execute(double a, double b) => a + b;
}

public class SubtractOperation : Operation
{
    public override double Execute(double a, double b) => a - b;
}

public class MultiplyOperation : Operation
{
    public override double Execute(double a, double b) => a * b;
}

public class DivideOperation : Operation
{
    public override double Execute(double a, double b)
    {
        if (b == 0) throw new DivideByZeroException("Cannot divide by zero");
        return a / b;
    }
}