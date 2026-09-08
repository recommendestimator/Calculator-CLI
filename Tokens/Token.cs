namespace Calculator.Tokens;

internal readonly struct Token
{
    public required string Value { get; init; }
    public required TokenType TokenType { get; init; }

    public override string ToString()
    {
        return Value;
    }
}
