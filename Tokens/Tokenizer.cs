namespace Calculator.Tokens;

internal class Tokenizer
{
#region Main
    public static List<Token> TokenizeExpression(string? expression)
    {
        if (expression is null or "")
        {
            return 
            [
                new() 
                {
                    Value = "EMPTY",
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
                throw new Exception
                (
                    $"""
                    Could not tokenize the inputted expression!
                    The char in question: {expression[index]},
                    Position: {index}
                    """
                );
            }
        }

        return parsedTerms;
    }
#endregion


#region Expression Readers
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
                    throw new Exception
                    (
                        $"""
                        You're inputting a variable with more than 1 decimal!
                        Error @ position: {index}
                        What the parser read: {expression[start .. (index + 1)]}
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
            
            break; // We've hit a char that's neither a digit nor decimal.
        }

        token = new()
        {
            Value = expression[start .. index],
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


#region Char Checkers
    private static bool IsDigit(char c) => c is >= '0' and <= '9';

    private static bool IsDecimal(char c) => c is '.';

    private static bool IsAMathematicalOperator(char c) => c is '+' or '-' or '*' or '/' or '%';

    private static bool IsWhitespace(char c) => c is ' ';
#endregion
}
