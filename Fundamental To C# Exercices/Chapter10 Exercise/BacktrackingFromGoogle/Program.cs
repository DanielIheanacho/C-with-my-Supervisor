using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Example list of items (works with strings, ints, etc.)
        List<string> items = new List<string> { "A", "B", "C", "D" };

        Console.WriteLine("All unique combinations:");
        PrintAllCombinations(items);
    }

    /// <summary>
    /// Core function that orchestrates finding and printing combinations of all possible lengths.
    /// </summary>
    public static void PrintAllCombinations<T>(List<T> list)
    {
        List<T> currentCombination = new List<T>();
        // Start backtracking from index 0
        FindCombinations(list, 0, currentCombination);
    }

    /// <summary>
    /// Recursive backtracking function to generate unique combinations.
    /// </summary>
    private static void FindCombinations<T>(List<T> list, int startIndex, List<T> currentCombination)
    {
        // Print the combination if it is not empty
        if (currentCombination.Count > 0)
        {
            Console.WriteLine("[" + string.Join(", ", currentCombination) + "]");
        }

        // Loop through the remaining elements to build combinations without duplicating pairs
        for (int i = startIndex; i < list.Count; i++)
        {
            // 1. Choose the current element
            currentCombination.Add(list[i]);

            // 2. Explore further combinations starting from the next index
            FindCombinations(list, i + 1, currentCombination);

            // 3. Un-choose the element (backtrack) to try the next option
            currentCombination.RemoveAt(currentCombination.Count - 1);
        }
    }
}