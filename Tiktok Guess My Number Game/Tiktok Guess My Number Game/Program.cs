using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<string> remaining = new List<string>();
        for (int i = 1; i <= 9; i++)
        {
            for (int j = 1; j <= 9; j++)
            {
                for (int k = 1; k <= 9; k++)
                {
                    for (int l = 1; l <= 9; l++)
                    {
                        if (i != j && i != k && i != l && j != k && j != l && k != l)
                        {
                            remaining.Add($"{i}{j}{k}{l}");
                        }
                    }
                }
            }
        }

        string guess = "1234";
        int correct = 0;
        int correctPos = 0;
        int sayac = 0;
        while (correctPos != 4)
        {
            Console.WriteLine($"Guess: {guess}");
            Console.Write("Correct Numbers? ");
            correct = int.Parse(Console.ReadLine());
            Console.Write("Correct Positions? ");
            correctPos = int.Parse(Console.ReadLine());

            remaining = EliminateImpossible(remaining, guess, correct, correctPos);

            guess = GetNextGuess(remaining);
            sayac++;
        }

        Console.WriteLine($"I guessed your number ({guess}) in {sayac} tries!");
        Console.Read();
    }

    static List<string> EliminateImpossible(List<string> remaining, string guess, int correct, int correctPos)
    {
        List<string> newRemaining = new List<string>();

        foreach (string possibility in remaining)
        {
            int countCorrect = 0;
            int countCorrectPos = 0;

            for (int i = 0; i < 4; i++)
            {
                if (possibility.Contains(guess[i]))
                {
                    countCorrect++;

                    if (possibility[i] == guess[i])
                    {
                        countCorrectPos++;
                    }
                }
            }

            if (countCorrect == correct && countCorrectPos == correctPos)
            {
                newRemaining.Add(possibility);
            }
        }

        return newRemaining;
    }

    static string GetNextGuess(List<string> remaining)
    {
        string bestGuess = "";
        int maxEliminations = 0;

        foreach (string guess in remaining)
        {
            Dictionary<string, List<string>> eliminationCounts = new Dictionary<string, List<string>>();

            foreach (string possibility in remaining)
            {
                string feedback = GetFeedback(guess, possibility);
                if (!eliminationCounts.ContainsKey(feedback))
                {
                    eliminationCounts[feedback] = new List<string>();
                }

                eliminationCounts[feedback].Add(possibility);
            }

            int eliminations =
                eliminationCounts.Values.Max(x => x.Count);
            if (eliminations > maxEliminations)
            {
                maxEliminations = eliminations;
                bestGuess = guess;
            }
        }
        return bestGuess;
    }

    static string GetFeedback(string guess, string possibility)
    {
        int correct = 0;
        int correctPos = 0;

        for (int i = 0; i < 4; i++)
        {
            if (possibility.Contains(guess[i]))
            {
                correct++;

                if (possibility[i] == guess[i])
                {
                    correctPos++;
                }
            }
        }

        return $"{correct}:{correctPos}";
    }

}