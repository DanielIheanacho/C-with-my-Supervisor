/// Write a program that reads a string, reverse it and prints it to the 
/// console. For example: "introduction"  "noitcudortni". 
using System;
using System.Text;

StringBuilder  word = new StringBuilder();
int wordLength = 0;
bool nullCheck = true;
string? input= null;
string upper = null;

Console.WriteLine("Welcome to the word reverse Machine \n");
     InsertInput();


while(nullCheck){
    Console.WriteLine("Do you want to Exit? \nY - yes N - no");

    string? ans = Console.ReadLine();
    if(!string.IsNullOrEmpty(ans))
    {
        upper = ans.ToUpper();
    }

    if(upper == "N")
    {
        InsertInput();
    }
    else if(upper == "Y")
    {
        Console.WriteLine("Bye");
        return;
    }
    else if(string.IsNullOrEmpty(ans))
    {
        Console.WriteLine("Please insert an option");
    }
    else 
    {
        Console.WriteLine("Invalid Option");
    }
}

Reverse(word);





// METHODS
void InsertInput(){
Console.WriteLine("Insert a word to be reversed");

input = Console.ReadLine();

if(string.IsNullOrEmpty(input))
{   
    nullCheck = true;
    Console.WriteLine("Input can't be empty");
}

else
{
    nullCheck = false;
    wordLength = (int)input.Length;

 }

}

void Reverse(StringBuilder word){

   for(int i = wordLength - 1; i >= 0; i--)
    {
    word.Append(input[i]);
    }

    Console.WriteLine("You Inserted {0}, And when reversed, you have {1}", input, word);

}