namespace Calculator;

class ExpressionParser
{
    public static List<object> ParseExpression(string? input)
    {
        if (input is null)
        {
            return [];
        }

        List<object> parsedInput = [];
        for (int i = 0; i < input.Length; i++)
        {
            // Logic for numerical values.
            if (Term.TryParse(
                input,
                i,
                out (Term? term, int newI) termTuple
            ))
            {
                parsedInput.Add(termTuple.term);
                i = termTuple.newI;
            }       

            // Logic for operands.
            if (Operation.TryParse(input[i], out Operation? operation))
            {
                parsedInput.Add(operation);
            }
        }

        return parsedInput;
    }
}
