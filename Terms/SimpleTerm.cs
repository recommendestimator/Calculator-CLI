namespace Calculator.Terms;

internal class SimpleTerm(float value) : ITerm
{
    private float Value { get; init; } = value;

    public float Calculate() => Value;
}
