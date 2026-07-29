using System.Text;
using System.IO;

StreamReader reader = new StreamReader(@"student.txt");
StreamWriter writer = new StreamWriter(@"report.txt");

using(reader)
using(writer)
{
    string? line;
    string[] nameAndScore;

    writer.WriteLine("Student Report\n");   
    int score;
 
    while( (line = reader.ReadLine()) != null)
    {
        nameAndScore = line.Split(',');
        Console.WriteLine("Student: {0} \nScore:   {1}", nameAndScore[0], nameAndScore[1]);
        Console.WriteLine("--------------------");

        if(nameAndScore.Length >= 2)
        {
            if(int.TryParse(nameAndScore[1].Trim(), out score))
            {
                switch (score)
                {
                    case < 70:
                        writer.WriteLine("{0} Scored {1} (Needs Improvement)", nameAndScore[0], nameAndScore[1]);
                        break;
                    case (>= 70) and (<= 89):
                        writer.WriteLine("{0} Scored {1} (Pass)", nameAndScore[0], nameAndScore[1]);
                        break;
                    case >= 90:
                        writer.WriteLine("{0} Scored {1} (Excellent)", nameAndScore[0], nameAndScore[1]);
                        break;                
                }
            }
        }
    }

}
