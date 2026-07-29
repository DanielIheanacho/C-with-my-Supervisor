using System.IO;

StreamReader storyReader = new StreamReader("story.txt");

using(storyReader)
{
    string? line;
    string[]? wordsInLine;
    char[] splitBy = {',', ' '};
    int wordCount = 0;
    int lineCount = 0;
    while((line = storyReader.ReadLine()) != null)
    {
        wordsInLine = line.Split(splitBy);

        for(int i = 0;i < wordsInLine.Length; i++)
        {   
//            Console.WriteLine("{0}",wordsInLine[i]);  For test purposes
            wordCount++;
        }
        lineCount++;
    }
    Console.WriteLine("Number of lines: {0}", lineCount);
    Console.WriteLine("Number of words: {0}", wordCount);
}