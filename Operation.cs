namespace Calculator;

struct Operation
{
    private Operands Operand { get; set; }

    public Operation(Operands operand)
    {
        this.Operand = operand;   
    }

    public static bool TryParse(char input, out Operation? operation)
    {
        operation = input switch
        {
            '+' => new (Operands.ADD),
            '-' => new (Operands.SUBSTRACT),
            '*' => new (Operands.MULTIPLY),
            '/' => new (Operands.DIVIDE),
            '%' => new (Operands.MODULO),
            _ => null
        };

        return operation is not null;
    }

    public override readonly string ToString()
    {
        return Operand switch
        {
            Operands.ADD => "+",
            Operands.SUBSTRACT => "-",
            Operands.MULTIPLY => "*",
            Operands.DIVIDE => "/",
            Operands.MODULO => "%",
            _ => "[UNDEFINED OPERATOR]"
        };
    }
}
