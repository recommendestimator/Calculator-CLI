using Calculator;

Console.Write("Input your expression here: ");
string input = Console.ReadLine().Trim();

List<object> parsedInput = ExpressionParser.ParseExpression(input);

Console.WriteLine();
Console.Write($"You inputted the following: ");
foreach (object item in parsedInput)
{
    Console.Write(item);
}