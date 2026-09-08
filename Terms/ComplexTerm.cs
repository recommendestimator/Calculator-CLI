namespace Calculator.Terms;

internal class ComplexTerm(ITerm left, ITerm right, Func<float, float, float> operation) : ITerm
{
    public ITerm Left { get; init; } = left;
    public ITerm Right { get; init; } = right;
    private Func<float, float, float> Operation { get; init; } = operation;

    public float Calculate() => Operation(Left.Calculate(), Right.Calculate());

    public override string ToString()
    {
        return $"{Left} [op] {Right}";
    }
}