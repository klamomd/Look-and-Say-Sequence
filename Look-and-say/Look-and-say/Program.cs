using System;
using System.Linq;

namespace Look_and_say
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Welcome to the 'Look-and-Say' Sequence Calculator!");

            int seed = 0;

            while (seed == 0)
            {
                Console.Write($"Please enter a seed value (a digit from 1-9): ");
                string input = Console.ReadLine().Trim();

                // Ensure seed is an int from 1-9, otherwise reset it and complain.
                if (!int.TryParse(input, out seed) || seed < 1 || seed > 9 )
                {
                    Console.WriteLine($"Invalid input: '{input}'\n");
                    seed = 0;
                }
            }

            Console.WriteLine();

            int numberOfSequences = 0;

            while (numberOfSequences == 0)
            {
                Console.Write($"Please enter the desired number of sequences: (a digit greater than 0) (HIGH VALUES TAKE A WHILE): ");
                string input = Console.ReadLine().Trim();

                // Ensure numberOfSequences is an int greater than 1.
                if (!int.TryParse(input, out numberOfSequences) || numberOfSequences < 1)
                {
                    Console.WriteLine($"Invalid input: '{input}'\n");
                    numberOfSequences = 0;
                }
            }

            Console.WriteLine();

            Console.WriteLine($"GENERATING {numberOfSequences} SEQUENCES FOR SEED {seed}:");

            string currentSequence = seed.ToString();

            // For each sequence, print the sequence (prefixed with what number sequence it is), then calculate and store the next sequence.
            for (int i=1; i<=numberOfSequences; i++)
            {
                Console.WriteLine($"Seq {i}:\t{currentSequence}");

                // Don't bother calculating the next sequence if we're done.
                if (i < numberOfSequences)
                    currentSequence = CalculateNextSequence(currentSequence);
            }

            Console.WriteLine();

            Console.WriteLine("FINISHED! Press 'enter' to exit.");

            Console.ReadLine();
        }


        private static string CalculateNextSequence(string currentSequence)
        {
            if (string.IsNullOrWhiteSpace(currentSequence) || currentSequence.Length < 1)
                throw new Exception($"Invalid sequence was passed to '{nameof(CalculateNextSequence)}'.");

            string newSequence = "";

            while (currentSequence.Length > 0)
            {
                // Store the first digit in the sequence.
                char firstDigit = currentSequence.First();

                // Determine the number of times this digit repeats without interruption.
                int numberOfMatchingDigits = currentSequence.TakeWhile(digit => digit == firstDigit).Count();

                // Use these values to continue to build the new sequence following the pattern - the total number of that digit, then the digit itself.
                newSequence += numberOfMatchingDigits;
                newSequence += firstDigit;

                // Remove all the characters we've just looked at using .Substring - ensuring that we don't call .Substring with an invalid startIndex.
                if (numberOfMatchingDigits == currentSequence.Length)
                {
                    currentSequence = "";
                }
                else
                {
                    currentSequence = currentSequence.Substring(numberOfMatchingDigits);
                }
            }
            
            return newSequence;
        }
    }
}
