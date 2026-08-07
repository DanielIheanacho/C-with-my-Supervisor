using System.Text;

List<int> sequence = new List<int>();

///<Summary> Takes In input from console, check if it is an integer
///and stores it in sequence if true and returns message if false </Summary>


Console.WriteLine("Insert a \"Positive Number \" to add to your sequence");
Console.WriteLine("Inser Empty Space When Done");

string? input ;

    while(!string.IsNullOrEmpty(input = Console.ReadLine()))
    {
        bool success = int.TryParse(input, out int insert);
        if (success)
            {
                sequence.Add(insert);
            }
        else
            {
                Console.WriteLine("Invalid input.\nOnly Positive Integers Allowed");
            }
    }

///Prints out Sequence
foreach(int value in sequence)
    {
        Console.Write(value + ", ");
    }

///Adds Sequence
int sum = 0;
foreach(int value in sequence)
    {
        sum = sum + value;
    }
Console.WriteLine("\nThe sum of your sequence is " + sum);

///Averages Sequence
float average = (float)sum/(float)(sequence.Count);
Console.WriteLine("The average of your sequence is " + average);