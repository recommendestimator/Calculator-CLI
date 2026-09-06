namespace Calculator.Terms;

internal class ComplexTerm(float[] factors, char[] operands) : ITerm
{
    private float[] Factors { get; init; } = factors;
    private char[] Operands { get; init; } = operands;

    public float Calculate()
    {
        if (Factors is null || Factors.Length == 0)
        {
            return 0f;
        }
        else if (Factors.Length < 2)
        {
            return Factors[0];
        }

        float result = Factors[0];
        for (int index = 1; index < Factors.Length; index++)
        {
            result = Operands[index - 1] switch
            {
                '*' => result * Factors[index],
                '/' => result / Factors[index],
                '%' => result % Factors[index],
                _ => throw new InvalidOperationException(
                    $"""
                    Unsupported or Invalid operator!
                    The operator in question: {Operands[index - 1]}
                    The term in question: {ToString()}
                    """)
            };
        }

        return result;
    }

    public override string ToString()
    {
        if (Factors is null || Factors.Length == 0)
        {
            return string.Empty;
        }

        // If we have the following properties:
        // Factors = [3, 25.5, .25]
        // Operands = [*, /]
        // Then output should be smth like:
        // "3*25.5/.25"
        return string.Concat
        (
            Factors.Select
            (
                (factor, index) => index == 0 ? factor.ToString() : $"{Operands[index - 1]}{factor}"
            )
        );
    }
}
