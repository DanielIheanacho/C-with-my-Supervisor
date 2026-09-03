string[] set = {"a","b","c","d","e"};
List<string> currentSubset= new List<string>();
int subSetLength = 2;
int endAt = set.Length - subSetLength + 1 ;

// Console.WriteLine(set.Length);
// Console.WriteLine(subSetLength);
// Console.WriteLine(endAt);



PrintIterate(0,subSetLength,set,endAt,currentSubset);


void PrintIterate(int firstElementIndex,int subSetLength, string[] set,int endAt,List<string> currentSet)
{   
    if(firstElementIndex > (set.Length - subSetLength))
    {   
        return;
    }
    int iterateStart = firstElementIndex + 1;
    currentSet.Clear();
    currentSet.Add(set[firstElementIndex]);
    if(subSetLength > 1 && subSetLength <= set.Length)
    {
        while(iterateStart <= endAt)
        {
            for(int i = iterateStart, count = 1; count < subSetLength; i++,count++)
            {
                currentSet.Add(set[i]);
            }
            PrintList(currentSet);
            currentSet.RemoveRange(1,(subSetLength - 1));
            iterateStart++;
        }
    }
    else if(subSetLength == 1)
    {
        PrintList(currentSet);    
    }
    else
    {
        Console.WriteLine("{}");
        return;
    }

    Console.WriteLine();
    firstElementIndex++;
    PrintIterate(firstElementIndex,subSetLength,set,endAt,currentSet);
}




void PrintList(List<string> input)
{
    Console.Write("{");
    foreach(string element in input)
    {   
        Console.Write(element);
    }
    Console.Write("}");
    Console.Write(" ");
    return;
}