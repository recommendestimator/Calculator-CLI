namespace Calculator.Tokens;

internal class Tokenizer
{
#region Orchestrator
    public static List<Token> TokenizeExpression(string? expression)
    {
        if (expression is null)
        {
            return 
            [
                new() 
                {
                    Value = "0",
                    TokenType = TokenType.DIGIT
                }
            ];
        }

        List<Token> parsedTerms = [];
        int index = 0;
        while (index < expression.Length)
        {
            if (IsWhitespace(expression[index]))
            {
                index++;
            }
            else if (TryReadNumber(expression, ref index, out Token numberToken))
            {
                parsedTerms.Add(numberToken);
            } 
            else if (TryReadOperator(expression, ref index, out Token operatorToken))
            {
                parsedTerms.Add(operatorToken);
            }
            else
            {
                throw new InvalidDataException(
                    $"""
                    The data you inputted was invalid!
                    The char in question: {expression[index]},
                    Position: {index}
                    """
                );
            }
        }

        return parsedTerms;
    }
#endregion


#region Token Readers
    private static bool TryReadNumber(string expression, ref int index, out Token token)
    {
        if(!IsDigit(expression[index]) && !IsDecimal(expression[index]))
        {
            token = default;
            return false;
        }

        bool hasDecimal = false;
        int start = index;
        while (index < expression.Length)
        {
            if (IsDigit(expression[index]))
            {
                index++;
                continue;
            }
            else if (IsDecimal(expression[index])) 
            {
                if (hasDecimal)
                {
                    throw new InvalidDataException(
                        $"""
                        You're inputting a variable with more than 1 decimal!
                        Error @ position: {index}
                        What the parser read: {expression[start..(index + 1)]}
                        """
                    );
                }
                else
                {
                    hasDecimal = true;
                    index++;
                    continue;
                }
            }
            else
            {
                break; // We've hit a char that's neither a digit nor decimal.
            }
        }

        token = new()
        {
            Value = expression[start..index],
            TokenType = TokenType.DIGIT
        };
        return true;
    }

    private static bool TryReadOperator(string expression, ref int index, out Token token)
    {
        if (!IsAMathematicalOperator(expression[index]))
        {
            token = default;
            return false;
        }

        token = new()
        {
            Value = expression[index].ToString(),
            TokenType = TokenType.OPERATOR
        };
        index++;
        return true;
    }
#endregion


#region Char Checking
    public static bool IsDigit(char c) => c is >= '0' and <= '9';

    public static bool IsDecimal(char c) => c is '.';

    public static bool IsAMathematicalOperator(char c) => c is '+' or '-' or '*' or '/' or '%';

    public static bool IsWhitespace(char c) => c is ' ';
#endregion
}
