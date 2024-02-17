// Idea: Instead of casting the user inputs to int, convert the random number to a string for easier comparison
// Have to deal with non-num inputs in that case though -- Done.
// Using string.Compare() for this was a bad idea and won't always work; Target of 188 and a guess of 1000 will tell you that 1000 is too low, presumably because the first 3 digits (100) are smaller than 118
// Using Int32.Parse() instead in this version


var randomNumber = new Random();
int upperRange = 300;

int count = 1;
int result;


Console.WriteLine("[1]Play the guessing game yourself\n[2]Have the computer play against itself.");

string gameMode = Console.ReadLine();

if (gameMode is not ("1" or "2")) { Console.WriteLine("Invalid input, defaulting to interactive mode."); gameMode = "1"; }

if (gameMode == "1")
{
    Console.WriteLine($"Guess the number between 0 and {upperRange}. Enter '-1' to quit.");
    int target = randomNumber.Next(upperRange);
    Console.WriteLine(target);
    bool loopStatus = true;

    while (loopStatus)
    {
        int guess = Int32.Parse(Console.ReadLine());
        if (guess == -1) { Console.WriteLine("Aborting."); break; }
        if (guess == target)
        {
            if (count == 1) { Console.WriteLine($"You did it! You got it on the first try, wow!"); }
            else { Console.WriteLine($"You did it! It took you {count} attempts."); }
            loopStatus = false;
        }
        else if (guess < target)
        {
            Console.WriteLine("Your guess was too low.");
            count++;
        }

        else if (guess > target)
        {

            Console.WriteLine("Your guess was too high.");
            count++;
        }
    }
}


if (gameMode == "2")
{
    Console.WriteLine($"The Computer will play against itself. You may set a custom upper range. Press Enter to use the default (0-{upperRange}).");
    string choice = Console.ReadLine();
    if (choice != "")
    {

        upperRange = Int32.Parse(choice);

    }

    int target2 = randomNumber.Next(upperRange);
    Console.WriteLine($"Target num {target2}");
    int randDelay = randomNumber.Next(300, 1000);
    int low = 0;
    int high = upperRange;
    bool loopStatus = true;
    int oldGuess, duplicateGuessOccurance = 0;
    int guess2 = (low + high) / 2;

    while (loopStatus)
    {
        oldGuess = guess2;
        guess2 = (low + high) / 2;
        if (oldGuess == guess2)
        {
            duplicateGuessOccurance++;
            if (duplicateGuessOccurance == 2)
            {
                guess2++; duplicateGuessOccurance = 0;
            } //Due to integer rounding the guess sometimes gets stuck 1 off of the target, fix this here if the guess doesn't change between loops more than once.

        }

        Console.WriteLine(guess2);
        Thread.Sleep(randomNumber.Next(300, 1000)); //add a (random) pause so the human can actually follow the exchange

        switch (ConvertToSwitchCase(guess2, target2)) // gameMode1 above was using switch cases before which I wanted to reproduce; it now uses normal int comparison operators, but I am keeping this in here as practice
        {
            case 0:
                if (count == 1) { Console.WriteLine($"You did it! You got it on the first try, wow!"); }
                else { Console.WriteLine($"You did it! It took you {count} attempts."); }
                loopStatus = false;
                break;
            case -1:
                Console.WriteLine("Your guess was too low.");
                count++;
                low = guess2;
                break;
            case 1:
                Console.WriteLine("Your guess was too high.");
                high = guess2;
                count++;
                break;
        }
    }
}


static int ConvertToSwitchCase(int guess, int target)
{
    if (guess > target) { return 1; }
    if (guess < target) { return -1; }
    else { return 0; }
}

/* Faulty version that uses String Comparison, keeping this here as a reminder for myself; see L3 about why this did not work.
int guess = Int32.Parse(Console.ReadLine());
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

*/