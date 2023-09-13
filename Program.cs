namespace NumbersGame
{
    internal class Program
    {

        static void Main(string[] args)
        {
            int guessCounter=0;
            int roundCounter=0;
            int topScore=0;
            int randomNumberRange = 20;
            int failThreshold = 5;
            int burnIndicator = 5;
            bool play = true;
            do
            {
                Console.Clear();
                Console.WriteLine("NumbersGame \n" +
                    "\n 1. Play" +
                    "\n 2. Options" +
                    "\n 3. Scoreboard" +
                    "\n 0. Exit" +
                    "\n");
                string temp = Console.ReadLine();
                switch (temp)
                {
                    case "0":
                        play = false;
                        break;
                    case "1":
                        topScore=Play(randomNumberRange, guessCounter, failThreshold, topScore, burnIndicator);
                        roundCounter++;
                        guessCounter = 0;
                        break;
                    case "2":
                        bool runOptions = true;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Options:\n" +
                            "\n 1. Set random number range" +
                            "\n 2. Set number of guesses" +
                            "\n 3. Set close guess indicator" +
                            "\n 0. Return" +
                            $"\n\nRange: 1-{randomNumberRange}\t GuessNr: {failThreshold}\t Close: +-{burnIndicator}");
                            string temp2 = Console.ReadLine();
                            switch (temp2)
                            {
                                case "0":
                                    runOptions = false;
                                    break;
                                case "1":
                                    randomNumberRange = Guess();
                                    break;
                                case "2":
                                    failThreshold = Guess();
                                    break;
                                case "3":
                                    burnIndicator = Guess();
                                    break;
                                default:
                                    break;
                            }
                        } while (runOptions);
                        break;
                    case "3":
                        if (topScore != 0)
                        {
                            Console.WriteLine($"Top score is {topScore} guesses!");
                        }
                        else
                        {
                            Console.WriteLine("No Top score yet!");
                        }
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }
            } while (play);
        }
        static int Play(int randomNumberRange, int guessCounter, int failThreshold, int topScore, int burnIndicator)
        {
            Console.Clear();
            bool play = true;
            Random random = new Random();
            int answer = random.Next(1, randomNumberRange);
            do
            {
                Console.WriteLine($"Guess a number between 1 and {randomNumberRange}" +
                    $"\n you have {failThreshold-guessCounter} more guesses.\n");
                int guess = Guess();
                guessCounter++;
                if (guess == answer)
                {
                    Console.WriteLine("\nYou Win!\n");
                    if (guessCounter < topScore || topScore == 0)
                    {
                        topScore = guessCounter;
                        Console.WriteLine($"You have the top score with only {guessCounter} guesses!");
                    }
                    Console.ReadKey();
                    play = false;
                }
                else if (guessCounter >= failThreshold)
                {
                    Console.WriteLine("\nGame Over\n");
                    Console.ReadKey();
                }
                else if((guess < answer && guess >= answer - burnIndicator) ||(guess > answer && guess <= answer + burnIndicator))
                {
                    Console.WriteLine("\nYou're close!\n");
                }

            } while (guessCounter<failThreshold^play == false);
            return (topScore);
        }
        static int Guess()
        {
            bool valid=false;
            int guess = 0;
            do
            {
                string input = Console.ReadLine();
                try
                {
                    guess = Int32.Parse(input);
                    valid = true;
                }
                catch
                {
                    Console.WriteLine("Invalid input!");
                }
            } while (!valid);

            return guess;
        }
    }
}