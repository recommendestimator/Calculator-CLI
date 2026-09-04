using System.Collections;

namespace Calculator;

class Tokenizer
{
    public static List<Token> TokenizeExpression(string? expression)
    {
        if (expression is null)
        {
            return [
                new() 
                {
                    Value = "0"
                }
            ];
        }

        List<Token> parsedTerms = [];
        for (int index = 0; index < expression.Length; index++)
        {
            Token token = new()
            {
                Value = GetTokenAndNewIndex(expression, index, out int newIndex)
            };

            parsedTerms.Add(token);
            index = newIndex;
        }

        return parsedTerms;
    }

    private static string GetTokenAndNewIndex(string input, int index, out int newIndex)
    {      
        string token = "";
    
        // Numbers
        while (index < input.Length && IsDigit(input[index]))
        {
            token += input[index];
            index++;
        }
        if (token.Length != 0)
        {
            newIndex = index - 1;
            return token;
        }

        // Operators
        if (IsAMathematicalOperator(input[index]))
        {
            token += input[index];
            newIndex = index;

            return token;
        }

        throw new InvalidDataException(
            $"""
            You inputted some sort of invalid data!
            Invalid char: {input[index]}
            Position: {index}
            """
        );
    }

    /// <summary>
    /// Checks if the given char is a digit.
    /// 
    /// This works since, converting char to an int, '0' = 48 and '9' = 57. 
    /// We simply check if the int-represntation of the char is between these values.
    /// </summary>
    /// 
    /// <param name="c">The inputted char.</param>
    /// 
    /// <returns>TRUE if the given char is a digit, FALSE otherwise.</returns>
    public static bool IsDigit(char c) => c is >= '0' and <= '9';

    /// <summary>
    /// Checks if the given char is a mathematical operator. The supported operators for now is as follows: 
    /// <br/>
    /// ADD         (+),    <br/>
    /// SUBTRACT    (-),    <br/>
    /// MULTIPLY    (*),    <br/>
    /// DIVIDE      (/),    <br/>
    /// MODULO      (%)     <br/>
    /// </summary>
    /// 
    /// <param name="c">The inputted char.</param>
    /// 
    /// <returns>TRUE if the char is any of the listed chars above, FALSE otherwise.</returns>
    public static bool IsAMathematicalOperator(char c) => c is '+' or '-' or '*' or '/' or '%';
}
