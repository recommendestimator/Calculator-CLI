namespace Calculator;

enum Operands
{
    ADD,
    SUBSTRACT,
    MULTIPLY,
    DIVIDE,
    MODULO
}

enum TokenType
{
    NUMBER,
    OPERATION
}

readonly struct Token
{
    public readonly Operands? Operand { get; }
    public readonly int? Value { get; }
    public readonly TokenType? Type { get; }

    public Token()
    {
        Operand = null;
        Value = null;
        Type = null;
    }

    public Token(int value)
    {
        Operand = null;
        Value = value;
        Type = TokenType.NUMBER;
    }

    public Token(Operands operand)
    {
        Operand = operand;
        Value = null;
        Type = TokenType.OPERATION;
    }
}