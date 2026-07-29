class FileWriter
{
static void Main()
{
// Create a StreamWriter instance
StreamWriter writer = new StreamWriter("number.txt");
// Ensure the writer will be closed when no longer used
using(writer)
{
// Loop through the numbers from 1 to 20 and write them
for (int i = 1; i <= 20; i++)
{
writer.WriteLine(i);
}
}
}
}