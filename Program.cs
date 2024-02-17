// Idea: Instead of casting the user inputs to int, convert the random number to a string for easier comparison
// Have to deal with non-num inputs in that case though -- Done.

var randomNumber = new Random();
int upperLimit = 300;
string target = randomNumber.Next(upperLimit).ToString();
int count = 1;
int result;
//Console.WriteLine(target);

Console.WriteLine($"Guess the number between 0 and {upperLimit}. Enter '-1' to quit.");

bool loopStatus = true;

while (loopStatus)
{
    string guess = Console.ReadLine();
    if (guess == "-1") { Console.WriteLine("Aborting."); break; }
    result = string.Compare(guess, target); // returns 0 if both strings are identical, -1 if guess is bigger, 1 if target is bigger
    if (!NoLetters(guess)) { result = 2; } // There were a couple of ways to handle detecting letters, I chose to do it via a function that returns a bool and then sets result to a number that
                                           // string.Compare() can never be, such as 2 in this case. Error message is handled in the function.
    switch (result)
    {
        case 0:
            if (count == 1) { Console.WriteLine($"You did it! You got it on the first try, wow!"); }
            else { Console.WriteLine($"You did it! It took you {count} attempts."); }
            loopStatus = false;
            break;
        case -1:
            Console.WriteLine("Your guess was too low.");
            count++;
            break;
        case 1:
            Console.WriteLine("Your guess was too high.");
            count++;
            break;
    }
}




static bool NoLetters(string guess)
{
    foreach (char ch in guess)
    {
        if (char.IsLetter(ch))
        {
            Console.WriteLine("Please only enter numbers.");
            return false;
        }
    }
    return true;
}
