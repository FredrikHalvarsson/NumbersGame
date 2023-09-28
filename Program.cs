namespace NumbersGame
{
    internal class Program
    {

        static void Main(string[] args)
        {
            //Skapar variabler som används senare i programmet
            int guessCounter=0;
            int roundCounter=0;
            int topScore=0;
            int randomNumberRange = 20;
            int failThreshold = 5;
            int burnIndicator = 5;

            //Loop som håller programmet igång tills användaren väljer att stänga
            bool play = true;
            do
            {
                //Skriver ut en meny som använder en switch på användarens input för att navigera
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
                        //play metoden startar själva spelet.
                        roundCounter++;
                        guessCounter = 0;
                        break;
                    case "2":
                        bool runOptions = true;
                        do
                        {
                            //yttligare en meny i menyn
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
                                    /*använder samma metod "Guess()" för alla fall där användaren ska mata in en int.
                                    Hade jag jobbat vidare på programmet så hade jag skapat olika metoder med restriktioner som 
                                    att antalet försök inte är mer än möjliga svar eller att indikatorn för att talet är nära
                                    inte kan vara utanför ramarna för det slumpade talet.*/
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
                            /*Jag hade yttligare planer på att skapa en lista som håller de hitils bästa resultaten
                            men jag kände att jag behövde sluta någonstans och börja med nästa labb so fick "scoreboard" vara endast
                            det bästa resultatet */ 
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
            //loop som fortsätter tills spelaren antingen vinner eller förlorar.
            do
            {
                Console.WriteLine($"Guess a number between 1 and {randomNumberRange}" +
                    $"\n you have {failThreshold-guessCounter} more guesses.\n");

                int guess = Guess();
                guessCounter++;
                // if satsen kollar först om du vunnit, sedan om du förlorat och vidare om du är nära eller hög/låg
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
                else if (guess < answer && guess >= answer - burnIndicator)
                {
                    Console.WriteLine("\nYou're close! Just a little higher.\n");
                }
                else if (guess > answer && guess <= answer + burnIndicator)
                {
                    Console.WriteLine("\nYou're close! Just a little lower.\n");
                }
                else if (guess < answer)
                {
                    Console.WriteLine("\nWrong! Higher.\n");
                }
                else if (guess > answer)
                {
                    Console.WriteLine("\nWrong! Lower.\n");
                }

            } while (guessCounter<failThreshold^play == false);
            return (topScore);
        }
        static int Guess()
        {
            bool valid=false;
            int guess = 0;
            // loop för felhantering, man vill ju förmodligen försöka igen om man skrev något fel
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
                    Console.WriteLine("Invalid input! \n\n Try again:");
                }
            } while (!valid);

            return guess;
        }
    }
}