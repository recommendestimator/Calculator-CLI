using Calculator.Tokens;
using Calculator.Terms;

Console.Write("Input your expression here: ");
string? input = Console.ReadLine()?.Trim();

List<Token> tokenizedInput = Tokenizer.TokenizeExpression(input);
List<ITerm> terminizedTokens = TermParser.ParseTokens(tokenizedInput);

Console.WriteLine();
Console.Write($"You inputted the following: ");
foreach (ITerm term in terminizedTokens)
{
    Console.Write(term + ", ");    
}