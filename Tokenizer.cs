namespace Calculator;

class Tokenizer
{
#region Orchestrator
    public static List<Token> TokenizeExpression(string? expression)
    {
        if (expression is null)
        {
            return [ new() { Value = "0"} ];
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
    // private static bool TryReadInt(string expression, ref int index, out Token token)
    // {
    //     if (!IsDigit(expression[index]))
    //     {
    //         token = default;
    //         return false;
    //     }

    //     int start = index;
    //     while (index < expression.Length && IsDigit(expression[index]))
    //     {
    //         index++;
    //     }

    //     token = new() { Value = expression[start..index] }; // Gets all chars from 'start' to 'index' (not including index itself).
    //     return true;
    // }

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
                        What the parser read: {expression[start..index]}
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

            break; // We've hit a char that neither a digit nor decimal.
        }

        token = new() { Value = expression[start..index] };
        return true;
    }

    private static bool TryReadOperator(string expression, ref int index, out Token token)
    {
        if (!IsAMathematicalOperator(expression[index]))
        {
            token = default;
            return false;
        }

        token = new() { Value = expression[index].ToString() };
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
