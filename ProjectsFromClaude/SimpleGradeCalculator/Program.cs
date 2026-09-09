//Simple grade calculator — input several exam 
//scores, output average, min, max, and letter 
// grade using a rule table.s

using System.ComponentModel.DataAnnotations;

List<int> scores = new List<int>();

Console.WriteLine("Type a score and press enter\nExit buy entering a non-numeriacl value.");

while(true)
{
    if(int.TryParse(
        Console.ReadLine(),
        out int input))
    {
        if(input>0)scores.Add(input);
    }
    else
    {
        Console.WriteLine("\nInvalid Input");
        break;
    }
}

if(scores.Count == 0) return;

int max = scores[0];
int min = scores[0];
int sum = 0;

for(int i =  0; i < scores.Count; i++)
{
    if(scores[i] > max) max = scores[i];
    if (scores[i] < min) min = scores[i];
    sum += scores[i];
}

int avg = sum/scores.Count;
char? grade;
if (avg >=70) grade = 'A'; 
else if (avg >=60) grade = 'B';
else if (avg >=50) grade = 'C';
else if (avg >=45) grade = 'D';
else if (avg >=40)grade = 'E';
else if (avg >=0) grade = 'F';
else grade ='?';

Console.WriteLine("Average: {0}\nMinimum: {1}\nMaximum: {2}\nLetterGrade: {3}", avg, min, max, grade);