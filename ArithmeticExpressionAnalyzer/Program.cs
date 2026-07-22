using System;
using System.Text;

Console.WriteLine("Thid is an Arithmetic Analyzer \n");
Console.WriteLine("Inser an Expression: ");

string? expression = Console.ReadLine();
Console.WriteLine("\n");

if (String.IsNullOrEmpty(expression))
{
    Console.WriteLine("Input Can't be Empty");
    return;
}

int expressionLength = expression.Length;
//Creates Different Stacks for the Different bracket type
int[] parenthesisStack = new int[expressionLength];
int[] curlyBracketStack = new int[expressionLength];
int[] squareBracketStack = new int[expressionLength];

// Bracket Indices
int parenthesisIndex = 0;
int curlyBracketIndex = 0;
int squareBracketIndex = 0;

//Closed Bracket Overflow
int closedBracketOverFlow = 0; 

// Max Bracket Level
int nestingLevel = 0;
int maxNestingLevel = 0;
int actualNestingLevel = 0;

// Number of Open Brackets of all type 
int openBracketNumber = 0;

// Number of Closed Bracket of all type
int closeBracketNumber = 0;

// Number of Operators
int numberOfOperators = 0;

// Number of Operands
int numberOfOperands = 0;

for (int i = 0, j = 0; i < expressionLength; i++)
{
    switch (expression[i])
    {
        case '(' :
            openBracketNumber++;
            parenthesisStack[parenthesisIndex++] = j++;
            NestLevel(i);
            continue;

        case ')' :
            j++;
            closeBracketNumber ++;
            NestLevel(i);
            if(parenthesisIndex == 0)
            {
                Console.WriteLine("This Bracker in the {0}th position has no match", j);
                closedBracketOverFlow ++;
            }
            else
            {
            Console.WriteLine("The Brackets in the {0}th and {1}th positions are a complete pair",parenthesisStack[parenthesisIndex - 1],j);
            parenthesisStack[--parenthesisIndex] = '\0';

            }
            continue;

        case '{' :
            openBracketNumber++;
            curlyBracketStack[curlyBracketIndex++] = j++;
            NestLevel(i);
            continue;

        case '}' : 
            j++;
            closeBracketNumber ++;
            NestLevel(i);
            if(curlyBracketIndex == 0)
            {
                Console.WriteLine("This Bracker in the {0}th position has no match", j);
                closedBracketOverFlow ++;
            }
            else
            {
                Console.WriteLine("The Brackets in the {0}th and {1}th positions are a complete pair",curlyBracketStack[curlyBracketIndex - 1],j);
                curlyBracketStack[--curlyBracketIndex] = '\0';
            }
             continue;

        case '[' :
            openBracketNumber++;
            squareBracketStack[squareBracketIndex++] = j++;
            NestLevel(i);
            continue;

        case ']' :
            j++;  
            closeBracketNumber ++;
            NestLevel(i);
            if(squareBracketIndex == 0)
            {
                Console.WriteLine("This Bracker in the {0}th position has no match", j);
                closedBracketOverFlow ++;
            }
            else
            {
                Console.WriteLine("The Brackets in the {0}th and {1}th positions are a complete pair",squareBracketStack[squareBracketIndex - 1],j); 
                squareBracketStack[--squareBracketIndex] = '\0';          
            }
            continue;

        default:
            if(expression[i] == '+' || expression[i] == '-' || expression[i] == '*' || expression[i] == '/')
            {
                numberOfOperators++;
            }
            else
            {
                numberOfOperands++;
            }
            continue;
    }
}

if(openBracketNumber > closeBracketNumber)
    {
        actualNestingLevel = maxNestingLevel - (openBracketNumber - closeBracketNumber);
    }
else
    {
        actualNestingLevel = maxNestingLevel;
    }

if(parenthesisIndex == 0 && curlyBracketIndex == 0 && squareBracketIndex == 0 && closedBracketOverFlow == 0)
{
    Console.WriteLine("\n\nExpression is Balanced\n");
}
else
{
    Console.WriteLine("\n\nExpression is Not Balanced\n");
}

Console.WriteLine("Opening Brackets: {0}", openBracketNumber);
Console.WriteLine("Opening Brackets: {0}", closeBracketNumber);
Console.WriteLine("Operators       : {0}", numberOfOperators);
Console.WriteLine("Operands        : {0}\n", numberOfOperands);

Console.WriteLine("Maximum Nesting Level:{0}", actualNestingLevel);

//Used to store the Maximum Nesting Level
void NestLevel(int i){

    if(expression[i] == '(' || expression[i] == '{' || expression[i] == '[')
    {
        nestingLevel++;
    }
    else if (expression[i] == ')' || expression[i] == ')' || expression[i] == ')')
    {
        nestingLevel--;
    }

    if (nestingLevel > maxNestingLevel)
    {
        maxNestingLevel = nestingLevel;
    }
}