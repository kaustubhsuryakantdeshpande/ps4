using System;
using System.Diagnostics;

namespace GuessTheNumberGame
{
    public enum GameMode
    {
        SinglePlayer = 1,
        MultiPlayer = 2
    }

    public enum Difficulty
    {
        Easy = 1,
        Medium = 2,
        Hard = 3
    }

    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int LivesUsed { get; set; }
        public double TimeUsed { get; set; }

        public Player(string name)
        {
            Name = name;
            Score = 0;
            LivesUsed = 0;
            TimeUsed = 0;
        }
    }

    public class GameSettings
    {
        public int MaxNumber { get; set; }
        public int Lives { get; set; }
        public int TimeLimit { get; set; }
        public int ScoreMultiplier { get; set; }

        public static GameSettings GetSettings(Difficulty difficulty)
        {
            return difficulty switch
            {
                Difficulty.Easy => new GameSettings { MaxNumber = 7, Lives = 3, TimeLimit = 30, ScoreMultiplier = 1 },
                Difficulty.Medium => new GameSettings { MaxNumber = 10, Lives = 2, TimeLimit = 25, ScoreMultiplier = 2 },
                Difficulty.Hard => new GameSettings { MaxNumber = 15, Lives = 2, TimeLimit = 15, ScoreMultiplier = 3 },
                _ => throw new ArgumentException("Invalid difficulty level")
            };
        }
    }

    public class GuessTheNumberGame
    {
        private static Random random = new Random();

        public static void Main(string[] args)
        {
            Console.WriteLine("🎮 Welcome to the ULTIMATE Number Guessing Challenge! 🎮");
            Console.WriteLine("═══════════════════════════════════════════════════════");
            Console.WriteLine();
            
            while (true)
            {
                DisplayMainMenu();
                GameMode gameMode = GetGameModeFromUser();
                
                if (gameMode == GameMode.SinglePlayer)
                {
                    PlaySinglePlayerMode();
                }
                else
                {
                    PlayMultiPlayerMode();
                }

                if (!AskToPlayAgain())
                    break;
            }

            Console.WriteLine("Thanks for playing! See you next time! 👋");
        }

        private static void PlaySinglePlayerMode()
        {
            Console.WriteLine();
            Console.WriteLine("🎯 Single Player Mode Activated!");
            Console.WriteLine("Time to show the computer who's boss! 💪");
            
            Player player = new Player("Champion");
            Difficulty difficulty = GetDifficultyFromUser();
            
            PlayGame(player, difficulty);
            DisplayFinalScore(player, difficulty);
        }

        private static void PlayMultiPlayerMode()
        {
            Console.WriteLine();
            Console.WriteLine("⚔️  Multi Player Mode Activated!");
            Console.WriteLine("Let the battle begin! May the best guesser win! 🏆");
            
            Player player1 = new Player(GetPlayerName(1));
            Player player2 = new Player(GetPlayerName(2));
            
            Difficulty difficulty = GetDifficultyFromUser();
            
            Console.WriteLine($"\n🎮 {player1.Name}, your turn to face the challenge!");
            PlayGame(player1, difficulty);
            
            Console.WriteLine($"\n🎮 {player2.Name}, show us what you've got!");
            PlayGame(player2, difficulty);
            
            DeclareWinner(player1, player2, difficulty);
        }

        private static void DisplayMainMenu()
        {
            Console.WriteLine("🚀 Choose your battle mode:");
            Console.WriteLine("   1️⃣  Single Player (You vs The Computer)");
            Console.WriteLine("   2️⃣  Multi Player (Battle of Champions)");
            Console.WriteLine();
        }

        private static GameMode GetGameModeFromUser()
        {
            while (true)
            {
                Console.Write("Enter your choice (1 for Single, 2 for Multi): ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && (choice == 1 || choice == 2))
                {
                    return (GameMode)choice;
                }

                Console.WriteLine("❌ Oops! Please enter 1 for Single Player or 2 for Multi Player.");
                Console.WriteLine();
            }
        }

        private static Difficulty GetDifficultyFromUser()
        {
            Console.WriteLine();
            Console.WriteLine("🎯 Choose your difficulty level:");
            Console.WriteLine("   1️⃣  Easy (0-7, 3 lives, 30 seconds) - Perfect for beginners!");
            Console.WriteLine("   2️⃣  Medium (0-10, 2 lives, 25 seconds) - Getting serious!");
            Console.WriteLine("   3️⃣  Hard (0-15, 2 lives, 15 seconds) - For the brave hearts!");
            Console.WriteLine();

            while (true)
            {
                Console.Write("Select difficulty (1/2/3): ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 3)
                {
                    return (Difficulty)choice;
                }

                Console.WriteLine("❌ Please enter 1 for Easy, 2 for Medium, or 3 for Hard.");
                Console.WriteLine();
            }
        }

        private static void PlayGame(Player player, Difficulty difficulty)
        {
            GameSettings settings = GameSettings.GetSettings(difficulty);
            int targetNumber = random.Next(0, settings.MaxNumber + 1);
            int lives = settings.Lives;
            
            Console.WriteLine();
            Console.WriteLine($"🎮 Game Started for {player.Name}!");
            Console.WriteLine($"🎯 Difficulty: {difficulty}");
            Console.WriteLine($"🔢 Guess a number between 0 and {settings.MaxNumber}");
            Console.WriteLine($"💖 Lives: {lives}");
            Console.WriteLine($"⏰ Time limit: {settings.TimeLimit} seconds");
            Console.WriteLine("Good luck! 🍀");
            Console.WriteLine();

            Stopwatch stopwatch = Stopwatch.StartNew();
            bool gameWon = false;
            bool timeExpired = false;
            int attempts = 0;

            while (lives > 0 && !gameWon && stopwatch.Elapsed.TotalSeconds < settings.TimeLimit)
            {
                Console.Write($"💭 Your guess (Lives left: {lives}): ");
                string? input = Console.ReadLine();

                // Check if time expired while waiting for input
                if (stopwatch.Elapsed.TotalSeconds >= settings.TimeLimit)
                {
                    timeExpired = true;
                    break;
                }

                if (!int.TryParse(input, out int guess))
                {
                    Console.WriteLine("❌ Please enter a valid number!");
                    continue;
                }

                if (guess < 0 || guess > settings.MaxNumber)
                {
                    Console.WriteLine($"❌ Please enter a number between 0 and {settings.MaxNumber}!");
                    continue;
                }

                attempts++;

                if (guess == targetNumber)
                {
                    gameWon = true;
                    stopwatch.Stop();
                    Console.WriteLine("🎉 BINGO! You nailed it! 🎉");
                    Console.WriteLine($"🏆 You won in {attempts} attempts!");
                }
                else if (guess < targetNumber)
                {
                    Console.WriteLine("📈 Think HIGHER! You're getting warmer! 🔥");
                    lives--;
                }
                else
                {
                    Console.WriteLine("📉 Think LOWER! You're on the right track! 🎯");
                    lives--;
                }

                if (lives == 0 && !gameWon)
                {
                    Console.WriteLine($"💀 Game Over! The number was {targetNumber}");
                    Console.WriteLine("Don't worry, champions never give up! 💪");
                    break;
                }
            }

            // Handle timeout scenario
            if (!gameWon && (stopwatch.Elapsed.TotalSeconds >= settings.TimeLimit || timeExpired))
            {
                stopwatch.Stop();
                Console.WriteLine();
                Console.WriteLine("⏰ TIME'S UP! The clock beat you this time!");
                Console.WriteLine($"🔢 The correct number was {targetNumber}");
                Console.WriteLine("💀 Session Terminated - Time Limit Exhausted!");
                Console.WriteLine();
            }

            player.LivesUsed = settings.Lives - lives;
            player.TimeUsed = stopwatch.Elapsed.TotalSeconds;
            
            if (gameWon)
            {
                CalculateScore(player, settings, gameWon);
            }
            else
            {
                player.Score = 0; // Ensure score is 0 for failed attempts
            }
        }

        private static void CalculateScore(Player player, GameSettings settings, bool gameWon)
        {
            if (!gameWon)
            {
                player.Score = 0;
                return;
            }

            // Base score
            int baseScore = 100;
            
            // Time bonus (remaining time)
            double timeBonus = Math.Max(0, settings.TimeLimit - player.TimeUsed) * 2;
            
            // Lives bonus
            int livesBonus = (settings.Lives - player.LivesUsed) * 20;
            
            // Difficulty multiplier
            double totalScore = (baseScore + timeBonus + livesBonus) * settings.ScoreMultiplier;
            
            player.Score = (int)Math.Round(totalScore);
        }

        private static void DisplayFinalScore(Player player, Difficulty difficulty)
        {
            Console.WriteLine();
            Console.WriteLine("📊 FINAL SCORE REPORT 📊");
            Console.WriteLine("═══════════════════════");
            Console.WriteLine($"🏆 Player: {player.Name}");
            Console.WriteLine($"🎯 Difficulty: {difficulty}");
            Console.WriteLine($"💖 Lives Used: {player.LivesUsed}");
            Console.WriteLine($"⏰ Time Used: {player.TimeUsed:F2} seconds");
            Console.WriteLine($"🌟 Final Score: {player.Score} points");
            
            GameSettings settings = GameSettings.GetSettings(difficulty);
            
            if (player.Score > 0)
            {
                Console.WriteLine("🎉 Fantastic performance! 🎉");
            }
            else
            {
                if (player.TimeUsed >= settings.TimeLimit)
                {
                    Console.WriteLine("⏰ Session ended due to time limit!");
                    Console.WriteLine("🏃‍♂️ Speed up next time, champion!");
                }
                else if (player.LivesUsed >= settings.Lives)
                {
                    Console.WriteLine("💀 Session ended - all lives used!");
                    Console.WriteLine("🎯 Better luck next time, champion!");
                }
                else
                {
                    Console.WriteLine("🎯 Better luck next time, champion!");
                }
            }
        }

        private static void DeclareWinner(Player player1, Player player2, Difficulty difficulty)
        {
            Console.WriteLine();
            Console.WriteLine("🏆 BATTLE RESULTS 🏆");
            Console.WriteLine("═══════════════════════");
            Console.WriteLine($"🥇 {player1.Name}: {player1.Score} points");
            Console.WriteLine($"🥈 {player2.Name}: {player2.Score} points");
            Console.WriteLine();

            if (player1.Score > player2.Score)
            {
                Console.WriteLine($"🎉 {player1.Name} WINS! 🎉");
                Console.WriteLine("👑 Congratulations, Champion!");
            }
            else if (player2.Score > player1.Score)
            {
                Console.WriteLine($"🎉 {player2.Name} WINS! 🎉");
                Console.WriteLine("👑 Congratulations, Champion!");
            }
            else
            {
                Console.WriteLine("🤝 IT'S A TIE! 🤝");
                Console.WriteLine("🏆 Both players are champions!");
            }
        }

        private static string GetPlayerName(int playerNumber)
        {
            Console.Write($"Enter Player {playerNumber} name: ");
            string? name = Console.ReadLine();
            return string.IsNullOrWhiteSpace(name) ? $"Player {playerNumber}" : name;
        }

        private static bool AskToPlayAgain()
        {
            Console.WriteLine();
            Console.Write("Want another round of fun? (y/n): ");
            string? response = Console.ReadLine()?.ToLower();
            return response == "y" || response == "yes";
        }
    }
}
