using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        var answer = "";
        List<char> validChars = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        var totalAnswers = new List<string>();
        for (int i = 0; i < N; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');

            string guess = inputs[0];
            int bulls = int.Parse(inputs[1]);
            int cows = int.Parse(inputs[2]);
            //answer = guess;
            Console.Error.WriteLine("Guess: " + guess + " bulls: " + bulls + "  cows: " + cows);
            if (bulls == 4)
            {
                answer = guess;
            }
            else
            {
                List<string> answers = GetPossibilities(guess, bulls, cows, ref validChars);
                var finalAnswers = new List<string>();


                Console.Error.WriteLine("answers: " + string.Join(";", answers));
                foreach (var a in answers)
                {
                    if (!totalAnswers.Any() || totalAnswers.Contains(a))
                    {
                        finalAnswers.Add(a);
                    }
                }
                totalAnswers = finalAnswers;


                Console.Error.WriteLine("answers: " + string.Join(";", totalAnswers));

                if (totalAnswers.Count == 1)
                {
                    answer = totalAnswers[0];
                    break;
                }
            }

        }






        // Write an answer using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(answer);
    }

    private static List<string> GetPossibilities(string guess, int bulls, int cows, ref List<char> validChars)
    {
        var results = new List<string>();
        var guessValues = new List<char>();
        foreach (var g in guess)
        {
            guessValues.Add(g);
        }
        var guessCharMandatories = bulls + cows;
        var n = guessValues.Count;
        Console.Error.WriteLine("guessCharMandatories " + guessCharMandatories);
        if (guessCharMandatories == 0)
        {
            foreach (var gv in guessValues)
            {
                validChars.Remove(gv);
            }
            return printCombination(validChars, n, n);
        }

        
        if (n == guessCharMandatories)
        {
            var guessCombinaisonresults = guessValues.CombineWithRepetitions(n);
            return guessCombinaisonresults;
        }
        var guessCombinaisons = printCombination(guessValues, n, guessCharMandatories);

        foreach (var gc in guessCombinaisons)
        {
            foreach (var vc in validChars)
            {
                var inputChars = gc.ToList();
                inputChars.Add(vc);
                results.AddRange(GetCombinations(inputChars));
            }

        }

        return results;
    }

    private static List<string> GetCombinations(IEnumerable<char> items)
    {
        var combinations = new List<string> { string.Empty };

        foreach (var item in items)
        {
            var newCombinations = new List<string>();

            foreach (var combination in combinations)
            {
                for (var i = 0; i <= combination.Length; i++)
                {
                    newCombinations.Add(
                      combination.Substring(0, i) +
                      item +
                      combination.Substring(i));
                }
            }

            combinations.AddRange(newCombinations);
        }

        return combinations;
    }

    private static List<string> printCombination(List<char> guessValues, int n, int guessCharMandatories)
    {
        // A temporary array to store  
        // all combination one by one 
        var data = new List<string>();

        foreach (var c in guessValues.CombinationsWithoutRepetition(ofLength: n, guessCharMandatories))
        {
            string value = string.Join(string.Empty, c);
            //Console.WriteLine(value);
            data.Add(value);
        }

        //foreach (var gv in guessValues)
        //{
        //    var tmpl = guessValues.Where( tgv => tgv!= gv).ToList();

        //    var combi = string.Join(string.Empty,tmpl);
        //    data.Add(combi);

        //}

        // Print all combination  
        // using temprary array 'data[]' 
        return combinationUtil(guessValues, data, 0, n - 1, 0, guessCharMandatories);
    }


    /* arr[] ---> Input Array 
 data[] ---> Temporary array to 
             store current combination 
 start & end ---> Staring and Ending  
                  indexes in arr[] 
 index ---> Current index in data[] 
 r ---> Size of a combination 
         to be printed */
    static List<string> combinationUtil(List<char> arr, List<string> data,
                                int start, int end,
                                int index, int r)
    {
        // Current combination is  
        // ready to be printed,  
        // print it 
        if (index == r)
        {
            //for (int j = 0; j < r; j++)
            //{

            //    Console.Write(data[j] + " ");
            //}

            //Console.WriteLine("");
            return data;
        }

        // replace index with all 
        // possible elements. The  
        // condition "end-i+1 >=  
        // r-index" makes sure that  
        // including one element 
        // at index will make a  
        // combination with remaining  
        // elements at remaining positions 
        for (int i = start; i <= end &&
                  end - i + 1 >= r - index; i++)
        {
            Console.Error.WriteLine("arr[i] " + arr[i]);
            //Console.Error.WriteLine("data[index] " + data[index]);
            //data[index] += arr[i];
            data.AddRange(combinationUtil(arr, data, i + 1,
                             end, index + 1, r));
        }
        return data;
    }


}

static class LinqExtensions
{
    public static IEnumerable<IEnumerable<T>> CombinationsWithoutRepetition<T>(
        this IEnumerable<T> items,
        int ofLength)
    {
        return (ofLength == 1) ?
            items.Select(item => new[] { item }) :
            items.SelectMany((item, i) => items.Skip(i + 1)
                                               .CombinationsWithoutRepetition(ofLength - 1)
                                               .Select(result => new T[] { item }.Concat(result)));
    }

    public static IEnumerable<IEnumerable<T>> CombinationsWithoutRepetition<T>(
        this IEnumerable<T> items,
        int ofLength,
        int upToLength)
    {
        return Enumerable.Range(ofLength, Math.Max(0, upToLength - ofLength + 1))
                         .SelectMany(len => items.CombinationsWithoutRepetition(ofLength: len));
    }


    public static List<string> CombineWithRepetitions(this IEnumerable<char> input, int take)
    {
        var output = new List<string>();
        IList<char> item = new char[take];

        CombineWithRepetitions(output, input, item, 0);

        return output;
    }

    private static void CombineWithRepetitions(List<string> output, IEnumerable<char> input, IList<char> item, int count)
    {
        if (count < item.Count)
        {
            var enumerable = input as IList<char> ?? input.ToList();
            foreach (var symbol in enumerable)
            {
                item[count] = symbol;
                CombineWithRepetitions(output, enumerable, item, count + 1);
            }
        }
        else
        {
            output.Add(string.Join(string.Empty,item));
        }
    }
}