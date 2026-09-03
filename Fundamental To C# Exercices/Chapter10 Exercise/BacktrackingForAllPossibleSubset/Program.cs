List<string>  set = new List<string>(["a","b","c","d",]);

PrintCurrentCombination(set);

void PrintCurrentCombination<T>(List<T> currentCombination)
{   Console.Write("{");
    for(int i = 0; i < currentCombination.Count; i++)
    {
        Console.Write(currentCombination[i] as string);
    }
    Console.WriteLine("}");
}