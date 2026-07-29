using System;
using System.Text;

string? expression;

Console.Write("Input Expression: ");
expression = Console.ReadLine();

if(String.IsNullOrEmpty(expression))
{
    Console.WriteLine("Input cannot be empty");
}

int expressionLength = expression.Length;
char[] stack = new char[expressionLength];
bool skip = false;
int j = 0;

for (int i = 0; i < expressionLength; i++)
{
    if (expression[i] == '(')
    {
        stack[j++] = '('; 
    }
    else if (expression[i] ==')')
    {
        if(j == 0)
        {
            Console.WriteLine("Parenthesis {0} is Unmatched", i);
            skip = true;
        }
        else 
        {
            stack[--j] = '\0';

        }
    }
}

if(!skip)
{
    if(j == 0)
    {
        Console.WriteLine("Parenthesis is Balanced");
    }
    else
    {
        Console.WriteLine("Parenthesis is Unmatched");
    }
}