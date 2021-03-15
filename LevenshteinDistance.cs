using System;

namespace Levenshtein
{

public class LevenshteinDistance
{
    static void Main()
    {
        string firstWord = "bike";
        string secondWord = "*ike";

        int ldistance; 

        ldistance = (LevenshteinCalculator.Compute(firstWord, secondWord));

        Console.WriteLine("Levenshtein distance: "+ ldistance);
    }
}

public static class LevenshteinCalculator
{
    static char wildcard = '*';
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

        //check for wildcard, trim off string at wildcard
        if (second[0].Equals(wildcard) && !first[0].Equals(wildcard) && second.Length <= first.Length){
            first = first.Substring((first.Length - second.Length) + 1);
        }
        
        if (first[0].Equals(wildcard) && !second[0].Equals(wildcard) && first.Length <= second.Length){
            second = second.Substring((second.Length - first.Length) + 1);
        }

        first = (first[0].Equals(wildcard)) ? first.Trim(wildcard) : first;
        second = (second[0].Equals(wildcard)) ? second.Trim(wildcard) : second;

        //create matrix of both words
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
                     d[i - 1, j] + 1, //top, delete
                     d[i, j - 1] + 1, //left, insert
                     d[i - 1, j - 1] + cost //top left, replace
                ); 
            } 
        } 
        return d[first.Length, second.Length]; 
    } 

    private static int Min(int e1, int e2, int e3) =>
        Math.Min(Math.Min(e1, e2), e3);
}
}