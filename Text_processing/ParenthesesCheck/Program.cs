// Write a program that checks whether the parentheses are placed correctly in an
// arithmetic expression. Example of expression with correctly placed brackets:
//  ((a+b)/5-d). Example of an incorrect expression: )(a+b)).

using System;
using System.Text;

Console.WriteLine("This Procgram checks whether the parentheses in an expression are placed correctly");

// Label to jump back to whenever the user chooses to try again instead of exiting.
noExit:

Console.WriteLine("Insert an Expression with Parenthesis");

// Raw user input and a normalized (upper-case) copy used for menu comparisons.
string? input = Console.ReadLine();
string? upper = null;

// initBrackets: scratch buffer, same length as the input, used to temporarily
// collect every '(' / ')' character found while scanning the string.
char[] initBrackets;

// trimmedBracket: the "cleaned" array containing ONLY the bracket characters
// found in the input, in the order they appeared (no letters/numbers/operators).
char[]? trimmedBracket;

// trimmedBracketIndex: index positions for each entry in trimmedBracket
// (currently populated but not really used elsewhere in the logic).
int[]? trimmedBracketIndex;

// Total number of bracket characters found in the input string.
int bracketLength = 0;

// completeBracketPair: a "matched" flag array, one slot per bracket in
// trimmedBracket. A value of 1 means that bracket has already been matched
// with its partner; 0 means it is still unmatched.
int[]? completeBracketPair;

// Flag used to control the matching loop: true means another pass found
// at least one new matching pair, so the loop should run again.
bool thereIsPossibleCombination = false;

    // Label used to re-enter expression processing (e.g. after an empty-input retry)
    // without asking "insert an expression" again.
    inputExpression:

    if(!String.IsNullOrEmpty(input)){
        
        int length = input.Length;

        initBrackets = new char[length];

        // Extract just the bracket characters from the input and set bracketLength.
        BracketLogs(input);

        // One "matched" slot per bracket, all initialized to 0 (unmatched).
        completeBracketPair = new int[bracketLength];

        // Repeatedly scan for matching '(' ')' pairs until no new pairs are found
        // in a full pass. This progressively "cancels out" matched pairs so that
        // nested brackets can also be resolved on subsequent passes.
        do
        {   
            thereIsPossibleCombination = false;

            // Try to match adjacent/aligned brackets based on current match state.
            IsBracketInOrder();

            // Check whether the remaining (still-unmatched) brackets are all the
            // same character (e.g. all leftover are '(' or all are ')'), which
            // would mean no more valid pairs can be formed.
            IsRemainderTheSame();

            // Debug print: show the current matched/unmatched state of every bracket.
            for(int i = 0; i<bracketLength; i++){
                Console.Write("{0} ", completeBracketPair[i]);
            }
            Console.Write("\n");
        } while(thereIsPossibleCombination);

        // After matching is done, print out any brackets that were never matched
        // (i.e. flagged as 0) — these are the ones causing the expression to be invalid.
        for(int j = 0; j<bracketLength; j++){
            if(completeBracketPair[j] == 0){
                Console.Write("{0}", trimmedBracket[j]);
                }
        }
        
        return;
    }

    else
    {
        // Handles empty input, and asks the user if they want to exit
        Console.WriteLine("Input can not be empty");

        exitOption:

        Console.WriteLine("Do you want to exit");
        Console.WriteLine("Y - yes \nN - no");
        string? exitCheck = Console.ReadLine();

        if(!String.IsNullOrEmpty(exitCheck))
        {
        upper = exitCheck.ToUpper();
        }

        if(upper == "Y")
        {
            // User confirmed exit — end the program.
            return;
        }
        else if(upper == "N")
        {
            // User wants to try again — go back to the very start and ask for new input.
            goto noExit;
        }
        else if (String.IsNullOrEmpty(upper) )
        {
            // User pressed Enter without typing Y/N — treat as invalid and retry
            // the expression-processing step (note: input is still empty here).
            Console.WriteLine("Input can not be empty");
            
            goto inputExpression;
        }
        else if (upper != "Y" && upper !="N")
        {
            // Anything other than Y/N/empty is an invalid menu choice.
            Console.WriteLine("Invalid Option");
            goto exitOption;
        }
        
        
    }

// Scans the full input string, pulls out every '(' and ')' character (ignoring
// everything else), and stores them — in order — into trimmedBracket.
// Also sets bracketLength to the total count of brackets found.
void BracketLogs(string inputString)
{
    int stringLength = input.Length;
    
    // Identifies the indexes of each bracket be it "(" or ")"
    // (copies each bracket character found into initBrackets, packed at the front).
    int j = 0;
    for(int i = 0; i < stringLength; i++)
    {
        if(inputString[i] == '(' || inputString [i] == ')')
        {
            initBrackets[j] = inputString[i];
            j += 1;
        }
    }

    // Processes the length of the brackets.
    // Counts how many bracket characters were collected (stops at the first
    // empty/default char, since initBrackets was only partially filled above).
    for(int i = 0; i < stringLength; i++)
    {
        if (initBrackets[i] == '(' || initBrackets[i] == ')')
        {
            bracketLength += 1;
        }
        else if (initBrackets[i] == '\0')
        {
            break;
        }
    }
    
    // Build a right-sized array containing only the bracket characters.
    trimmedBracket = new char[bracketLength];

    for (int i = 0; i < bracketLength; i++)
    {
        trimmedBracket[i] = initBrackets[i];
        Console.Write("{0}",trimmedBracket[i]); //---- For debuging purposess
    }

    // Populate index positions (0..bracketLength-1) for each bracket entry.
    trimmedBracketIndex = new int[bracketLength];

    for(int i = 1; i < bracketLength; i++)
    {
        trimmedBracketIndex[i] = i;
    }
}


// Walks through the trimmed bracket list and tries to pair up an unmatched
// '(' immediately followed by an unmatched ')'. Every time it finds such a
// pair, it flags both positions as matched (1) in completeBracketPair and
// sets thereIsPossibleCombination = true so the outer loop runs another pass.
void IsBracketInOrder()
{

    int completeIndex = 0;
    int previous = 0;
    int current = 0;

    for(int i = 0; i < bracketLength ; i++)
    {

        if (current == bracketLength)
        {
            break;
        }

        // Skip over brackets that are already matched from a previous pass.
        if (completeBracketPair[i] == 1)
        {   
            previous += 2;
            current++;
            Console.WriteLine("\n1 {0}", i);
            continue;
        }
    
        if(current == 0)
        {
            current ++;
        }

        // Found an unmatched '(' directly followed by an unmatched ')': mark
        // both as matched and advance the pointers past this pair.
        if(trimmedBracket[previous] == '(' && trimmedBracket[current] == ')' )
        {
            completeBracketPair[previous] = 1;
            completeBracketPair[current] = 1;
            Console.WriteLine("\nThe backets at nth position are comeplete pairs, {0} and {1}", i, i + 1);
            previous += 2; 
            current++;
            thereIsPossibleCombination = true;
        }
        else
        {
            // Not a matching pair — shift the window forward by one and keep looking.
            previous = current++;
        }

    }
}

// Looks at all the still-unmatched brackets (flag == 0). If every remaining
// unmatched bracket is the same character (all '(' or all ')'), then no more
// pairs can ever be formed and matching should stop (isSame stays true).
// Otherwise, it double-checks whether there is still at least one unmatched
// '(' immediately followed by an unmatched ')' worth trying again.
void IsRemainderTheSame ()
{
    int? current = null;
    int? previous = null ;
    bool isSame = false;
    int? check = null;
    for(int k = 0; k < bracketLength; k++)
        {
            if(completeBracketPair[k] == 0 )
            {
                if(!check.HasValue)
                {
                    // First unmatched bracket found — use it as the reference to compare against.
                    check = k;
                    continue;
                }

                if(trimmedBracket[check.Value] == trimmedBracket[k])
                {
                    // Still matches the reference character so far — keep checking.
                    isSame = true;
                    check = k;
                }
                else
                {
                    // Found two different unmatched bracket characters — not all the same.
                    isSame = false;
                    goto skip;
                }
            }
        }

    skip:
    if(!isSame)
    {
        // Remaining unmatched brackets are a mix of '(' and ')' — scan them to see
        // if there's still a valid '(' immediately followed by ')' worth matching.
        for (int i = 0; i < bracketLength; i++)
        {   
            if(completeBracketPair[i] == 0)
            {
                previous = current;
                current = i;
        
            }
            else 
            {
                continue;
            }
            if(current.HasValue  && previous.HasValue)
            {
                int prevIdx = previous.Value;
                int currIdx = current.Value;

                if (trimmedBracket[prevIdx] == '(' && trimmedBracket[currIdx] == ')')
                {
                    // Found a matchable pair among the remainder — signal another pass.
                    thereIsPossibleCombination = true;
                    break;
                }
            

            }
        }
    }

    else if(isSame == true){
        // All remaining unmatched brackets are identical (e.g. all '(' or all ')'),
        // meaning the expression can't be fully resolved — stop the matching loop.
        thereIsPossibleCombination = false;
    }
}