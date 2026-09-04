List<string>  set = new List<string>([]);

PrintCurrentCombination(set);

void PrintCurrentCombination<T>(List<T> currentCombination)
{   Console.Write("{");
    if(currentCombination.Count != 0)
    {
        for(int i = 0; i < currentCombination.Count; i++)
        {
            Console.Write(currentCombination[i]?.ToString());
            if(i < (currentCombination.Count - 1))
            {
                Console.Write(", ");
            }
        }
    }
    Console.WriteLine("}");
}