using Calculator;

Console.Write("Input your expression here: ");
string? input = Console.ReadLine()?.Trim();

List<Token> parsedInput = Tokenizer.TokenizeExpression(input);

Console.WriteLine();
Console.Write($"You inputted the following: ");
foreach (Token item in parsedInput)
{
    Console.Write(item.Value);
}