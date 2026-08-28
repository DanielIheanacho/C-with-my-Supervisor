
Console.WriteLine("GoodDay");
Console.WriteLine("Iheanacho Daniel\n");
Console.WriteLine("1\n101\n1001");

DateTime currentDateAndTime = DateTime.Now;
Console.WriteLine(currentDateAndTime);

double squareRoot = Math.Sqrt(12345);
Console.WriteLine("squareRoot of 12345 is {0}\n", squareRoot);

for(int i = 1; i < 100; i++)
{
    int number = (int)Math.Pow(-1,i+1)*(i+1);
    Console.Write(number+ ", "); 
}

Console.WriteLine("\n\nWhat is your age ?");
int age = int.Parse(Console.ReadLine());
Console.WriteLine("After 10 years you will be {0}", age + 10);
