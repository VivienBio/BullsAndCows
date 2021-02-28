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
        var guessList = new List<GuessObject>();
        for (int i = 0; i < N; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');

            string guess = inputs[0];
            int bulls = int.Parse(inputs[1]);
            int cows = int.Parse(inputs[2]);
            //answer = guess;
            Console.Error.WriteLine("Guess: " + guess + " bulls: " + bulls + "  cows: " + cows);
            var guessObj = new GuessObject(guess, bulls, cows);
            guessList.Add(guessObj);
            Helper.GetPossibilities(guessObj, ref validChars, ref totalAnswers);

            Console.Error.WriteLine("answers: " + string.Join(";", totalAnswers));
            totalAnswers = totalAnswers.Distinct().ToList();
            if (totalAnswers.Count == 1)
            {
                answer = totalAnswers[0];
                break;
            }
        }

        if(string.IsNullOrEmpty(answer)&& totalAnswers.Any())
        {
            foreach(var ta in totalAnswers)
            {
                if(!Helper.IsValid(ta,guessList,ref validChars))
                {
                    totalAnswers.Remove(ta);
                }
               
            }

            if (totalAnswers.Count == 1)
            {
                answer = totalAnswers[0];             
            }
        }
        // Write an answer using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(answer);
    }
}

public class GuessObject
{
    public string Guess { get; }
    public int Bulls { get; }
    public int Cows { get; }

    public GuessObject(string guess, int bulls, int cows)
    {
        Guess = guess;
        Bulls = bulls;
        Cows = cows;
    }
}

public static class Helper
{

    public static void GetPossibilities(GuessObject guessObj, ref List<char> validChars, ref List<string> totalAnswers)
    {
         var bulls = guessObj.Bulls;
         var cows = guessObj.Cows;
         var guess = guessObj.Guess;
        var results = new List<string>();
        var guessValues = new List<char>();
        if (bulls == 4)
        {
            totalAnswers = new List<string>() { guess };
            return;
        }

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
            var valid = validChars;
            foreach (var ta in totalAnswers.ToList())
            {
                if (ta.Any(c => !valid.Contains(c)))
                {
                    totalAnswers.Remove(ta);
                }
            }
            return;
        }
        var c = guess.ToCharArray();
        if (n == guessCharMandatories)
        {
            var results2 = new List<string>();

            foreach (var v in c.Permutations())
            {
                string item = string.Join(string.Empty, v);
                if (item != guess && item.IsValid(guess, bulls, cows, validChars))
                {
                    results2.Add(item);
                }

            }

            if (totalAnswers.Any())
            {
                List<string> lists = totalAnswers.Intersect(results2).ToList();
                Console.Error.WriteLine("lists  " + lists.Count);
                totalAnswers = lists;
            }
            else
            {
                totalAnswers = results2;
            }

            return;
        }

        var results3 = new List<string>();

        if (bulls > 0)
        {

            if (totalAnswers.Any())
            {
                foreach (var ta in totalAnswers.ToList())
                {
                    if (ta == guess || !ta.IsValid(guess, bulls, cows, validChars))
                    {
                        totalAnswers.Remove(ta);
                    }
                }
            }
            else
            {
                var positions = new List<int>();
                for (var i = 0; i < n; i++)
                {
                    positions.Add(i);
                }
                var bullsCombination = positions.Combinations(bulls);
                var tmpAnswers = new List<string>();
                foreach (var bc in bullsCombination)
                {
                    var resultCombi = new List<string>();
                    var tmpanswer = string.Empty;
                    for (var i = 0; i < n; i++)
                    {
                        var tmpresultCombi = new List<string>();
                        if (bc.Contains(i))
                        {          
                            if(resultCombi.Any())
                            {
                                foreach (var rc in resultCombi)
                                {
                                    tmpresultCombi.Add(rc + guess[i]);
                                }
                            }
                            else
                            {
                                tmpresultCombi.Add(guess[i].ToString());
                            }
                                                       
                        }
                        else
                        {
                            if (resultCombi.Any())
                            {
                                foreach (var vc in validChars)
                            {
                                foreach (var rc in resultCombi)
                                {                                                          
                                    tmpresultCombi.Add(rc + vc);
                                }
                            }
                            }
                            else
                            {
                                foreach (var vc in validChars)
                                {
                                    tmpresultCombi.Add(vc.ToString());
                                }
                            }
                        }
                        resultCombi = tmpresultCombi;
                    }
                    results3.AddRange(resultCombi);
                }
                totalAnswers = results3;
            }
            
        }
        return;
    }

    public static bool IsValid(this string item, string guess, int bulls, int cows, List<char> validChars)
    {
        if (item.Any(c => !validChars.Contains(c)))
        {
            return false;
        }

        var moveCounter = 0;
        var stayPos = 0;
        for (var i = 0; i < guess.Length; i++)
        {
            if (item[i] == guess[i])
            {
                stayPos++;
            }
            else
            {
                moveCounter++;
            }

            if (stayPos > bulls)
                return false;
        }



        if (stayPos != bulls)
            return false;


        return true;
    }

    public static IEnumerable<T[]> Permutations<T>(this T[] values, int fromInd = 0)
    {
        if (fromInd + 1 == values.Length)
            yield return values;
        else
        {
            foreach (var v in Permutations(values, fromInd + 1))
                yield return v;

            for (var i = fromInd + 1; i < values.Length; i++)
            {
                SwapValues(values, fromInd, i);
                foreach (var v in Permutations(values, fromInd + 1))
                    yield return v;
                SwapValues(values, fromInd, i);
            }
        }
    }

    private static void SwapValues<T>(T[] values, int pos1, int pos2)
    {
        if (pos1 != pos2)
        {
            T tmp = values[pos1];
            values[pos1] = values[pos2];
            values[pos2] = tmp;
        }
    }


    public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> elements, int k)
    {
        return k == 0 ? new[] { new T[0] } :
          elements.SelectMany((e, i) =>
            elements.Skip(i + 1).Combinations(k - 1).Select(c => (new[] { e }).Concat(c)));
    }

    public static bool IsValid(string item, List<GuessObject> guessList, ref List<char> validChars)
    {
        var chars = validChars;
        var bullsChars = new List<char>();
        foreach (var gl in  guessList)
        {
            
            if (gl.Cows == 0 && gl.Bulls > 0)
            {
                var isValid = true;
                var moveCounter = 0;
                var stayPos = 0;
                for (var i = 0;i<gl.Guess.Length;i++)
                {
                    if (item[i] == gl.Guess[i])
                    {
                        bullsChars.Add(item[i]);
                        stayPos++;
                    }
                    else if (gl.Guess.Contains(item[i]) && item.Count(it => it == item[i]) == 1)
                    {
                        isValid = false;                       
                    }
                    else
                    {
                        chars.Remove(gl.Guess[i]);                      
                    }
                }

                foreach (var bc in bullsChars)
                {
                    if (!chars.Contains(bc))
                        chars.Add(bc);
                }
                validChars = chars;
                if (item.Any(c => !chars.Contains(c)))
                {
                    return false;
                }

                if (!isValid || stayPos != gl.Bulls)
                {
                    return false;
                }                        
            }                     
        }

        if (item.Any(c => !chars.Contains(c)))
        {
            return false;
        }
        return true;
    }
}