
Console.WriteLine("Insert a number");

if (int.TryParse(Console.ReadLine(), out int result))
{
    if((result % 2) == 0)
    {
        Console.Write(result +" is even,");
    }
    else
    {
        Console.Write(result + " is odd,");
    }

    if(result > 0)
    {
        Console.Write(" Positive,");
    }
    else if(result < 0)
    {
        Console.Write(" Negative,");
    }
    else
    {
        Console.Write(" is 0,");
    }

    if(((result % 3)== 0) && ((result % 5) == 0))
    {
        Console.Write(" and divisible by both 3 and 5");
    }
    else if((result % 3) == 0)
    {
        Console.Write(" and divisble by 3");
    }
    else if((result % 5) == 0)
    {
        Console.Write(" and divisible by 5");
    }
   
}
else
{
    Console.WriteLine("Invalid Input");
}