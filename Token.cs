namespace Calculator;

readonly struct Token(string value)
{
    public required string Value { get; init; }
}
