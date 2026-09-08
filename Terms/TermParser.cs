using Calculator.Tokens;

namespace Calculator.Terms;

internal class TermParser
{
    public static List<ITerm> ParseTokens(List<Token> tokens)
    {
        if (tokens is null || tokens.Count is 0)
        {
            return
            [
                new SimpleTerm(0f)
            ];
        }
        
        List<ITerm> terms = [];
        int index = 0;

        // All index-incrementing logic is within the Parsing Methods.
        while (index < tokens.Count)
        {
            if (TryParseTerm(tokens, ref index, out ITerm term))
            {
                terms.Add(term);
            }
            else
            {
                throw new Exception
                (
                    $"""
                    Could not parse tokens into a proper term!
                    Tokens the parsed tried to parse: {string.Concat(tokens.Select(t => t.Value))}
                    Issue on index: {index}
                    The value of that index: {tokens[index]}
                    """
                );
            }
        }

        return terms;
    }

    private static bool TryParseTerm(List<Token> tokens, ref int index, out ITerm term)
    {
        term = default;
        if (!TryParseDigit(tokens, ref index, out ITerm currentTerm))
        {
            return false;
        }

        while (index < tokens.Count)
        {
            Token currentToken = tokens[index];

            if 
            (
                currentToken.TokenType is TokenType.OPERATOR &&
                currentToken.Value is ("*" or "/" or "%")
            )
            {
                string operation = currentToken.Value;

                index++;
                if (!TryParseDigit(tokens, ref index, out ITerm nextTerm))
                {
                    return false;
                }

                // On the first loop, we're taking the Digit we got from the first TryParseTerm(), all the way up in the if-statement.
                // On every reoccuring loop, currentTerm just becomes the partial parsing of the Term so far.
                //
                // ie.
                // 3 * 2 / 3 --> MUL(3, 2) / 3 -> DIV(MUL(3, 2), 3)
                currentTerm = operation switch
                {
                    "*" => new ComplexTerm(currentTerm, nextTerm, (a, b) => a * b),
                    "/" => new ComplexTerm(currentTerm, nextTerm, (a, b) => a / b),
                    "%" => new ComplexTerm(currentTerm, nextTerm, (a, b) => a % b),
                    _ => throw new Exception("This exception should never occur! There might possibly be some logic error in the TryParseTerm method.")
                };
            } 
            else
            {
                break;
            }
        }

        term = currentTerm;
        return true;   
    }

    private static bool TryParseDigit(List<Token> tokens, ref int index, out ITerm term)
    {
        term = default;
        Token? current = null;
        Token? next = null;

        if (index < tokens.Count)
        {
            current = tokens[index];
        }
        if (index + 1 < tokens.Count)
        {
            next = tokens[index + 1];
        }

        // Positive digit, no explicit sign
        // ["123"] --> 123f
        // ["45.67"] --> 45.67f
        if (current is Token currentToken)
        {
            if (currentToken.TokenType == TokenType.DIGIT)
            {
                term = new SimpleTerm 
                (
                    float.Parse(currentToken.Value)
                );
                index++;
                
                return true;
            }

            // Digit, sign explicitly shown
            // Can be both + or - (usually a negative digit tho)
            // ["+", "123"] --> 123f
            // ["-", "45.67"] --> -45.67f
            if (next is Token nextToken)
            {
                if 
                (
                    currentToken.TokenType == TokenType.OPERATOR &&
                    (currentToken.Value == "+" || currentToken.Value == "-") &&
                    nextToken.TokenType == TokenType.DIGIT
                )
                {
                    float sign = currentToken.Value == "-" ? -1f : +1f;
                    term = new SimpleTerm
                    (
                        sign * float.Parse(nextToken.Value)
                    );
                    index += 2;

                    return true;
                }
            }

        }

        return false;
    }
}
