namespace Calculator;

class ExpressionParser
{
    public static List<Token> ParseExpression(string? input)
    {
        if (input is null)
        {
            return [];
        }

        List<Token> parsedInput = [];
        for (int i = 0; i < input.Length; i++)
        {
            // Logic for numerical values
            if (TryParseNumberToken(input, i, out var tokenTuple))
            {
                parsedInput.Add(tokenTuple.token);
                i = tokenTuple.index;
            }
            else if (TryParseOperatorToken(input[i], out var opToken))
            {
                parsedInput.Add(opToken);
            }
        }

        return parsedInput;
    }

    private static bool TryParseNumberToken(string input, int index, out (Token token, int index) tokenTuple)
    {
        string numberString = "";
        while (index < input.Length && char.IsDigit(input[index]))
        {
            numberString += input[index];
            index++;
        }

        if (numberString.Length == 0)
        {
            tokenTuple = (new(), 0);
        } 
        else
        {
            int number = int.Parse(numberString);
            int newIndex = index - 1; // Will be passed into a for-loop, so needs to be decremented.

            tokenTuple = (new (number), newIndex);
        }

        return tokenTuple.token.Type is not null;
    }

    private static bool TryParseOperatorToken(char input, out Token token)
    {
        token = input switch
        {
            '+' => new (Operands.ADD),
            '-' => new (Operands.SUBSTRACT),
            '*' => new (Operands.MULTIPLY),
            '/' => new (Operands.DIVIDE),
            '%' => new (Operands.MODULO),
            _ => new()
        };

        return token.Type is not null;
    }
}
