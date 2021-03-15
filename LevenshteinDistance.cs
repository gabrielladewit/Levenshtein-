using System;

namespace Levenshtein
{

public class LevenshteinDistance
{
    static void Main()
    {
        string firstWord = "swimming";
        string secondWord = "winning";

        Console.WriteLine("Levenshtein distance: "+ LevenshteinCalculator.Compute(firstWord, secondWord));
    }
}

public static class LevenshteinCalculator
{
    public static int Compute(
        string first,
        string second
    )
    {
        //if one string is empty, distance is length of other
        if (first.Length == 0)
        {
            return second.Length;
        }

        if (second.Length == 0)
        {
            return first.Length;
        }

        //create arrays for both words
        var d = new int[first.Length + 1, second.Length + 1];
        for (var i = 0; i <= first.Length; i++)
        {
            d[i, 0] = i;
        }

        for (var j = 0; j <= second.Length; j++)
        {
            d[0, j] = j;
        }

        //fill matrix
        for (var i = 1; i <= first.Length; i++)
        {
            for (var j = 1; j <= second.Length; j++)
            {
                var cost = (second[j - 1] == first[i - 1]) ? 0 : 1; 
                d[i, j] = Min( 
                     d[i - 1, j] + 1,
                     d[i, j - 1] + 1,
                     d[i - 1, j - 1] + cost
                ); 
            } 
        } 
        return d[first.Length, second.Length]; 
    } 

    private static int Min(int e1, int e2, int e3) =>
        Math.Min(Math.Min(e1, e2), e3);
}
}