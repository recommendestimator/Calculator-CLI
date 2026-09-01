namespace Calculator;

struct Term
{
    private int Value { get; set; }

    public Term(int value)
    {
        this.Value = value;
    }

    public static bool TryParse(
        string input,
        int index,
        out (Term? term, int newIndex) tuple
    )
    {
        string termString = "";
        while (index < input.Length && char.IsDigit(input[index]))
        {
            termString += input[index];
            index++;
        }

        if (termString.Length == 0)
        {
            tuple = (null, index);
        } 
        else
        {
            Term term = new (int.Parse(termString));
            int newIndex = index - 1; // Will be passed into a for-loop, so needs to be decremented.
            
            tuple = (term, newIndex);
        }

        return tuple.term is not null;
    }

    public override readonly string ToString()
    {
        return $"{Value}";
    }
}
