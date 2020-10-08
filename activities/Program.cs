using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace activities
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Jack 'n Poy World Cup Series!");

            Thread.Sleep(1000);

            Console.Write("Register your player name: ");

            int hpInit = 5;

            var names = new[]{
                "Al",
                "Dan",
                "Joe",
                "Kim",
                "Rev",
                "Czar",
                "Tom",
                "Clint",
                "Tracy",
                "Roy"
            };

            //assign player values
            var playerName = Console.ReadLine();

            var playerHP = hpInit;

            var playerScore = 0;

            var playerPotion = 0;

            Console.WriteLine("Waiting for other player....");

            Thread.Sleep(500);

            Console.WriteLine("Game commencing...\n");

            //assign enemy values
            var enemyName = names[new Random().Next(0, names.Length - 1)];

            var enemyHP = hpInit;

            var enemyScore = 0;

            var enemyPotion = 0;

            var gameRounds = new Dictionary<int, string>();

            var hasWinner = playerHP == 0 || enemyHP == 0;

            var actions = new[] { "R", "P", "S", };

            var predictedOutcomes = new Dictionary<string, string>{
                {"RP",enemyName},
                {"RS",playerName},
                {"PR",playerName},
                {"PS",enemyName},
                {"SR",enemyName},
                {"SP",playerName}
            };

            var stack = new List<string>();

            while (!hasWinner)
            {

                var round = gameRounds.Count() + 1;
                var winner = "";

                Thread.Sleep(500);

                if (playerPotion > 0)
                {
                    Console.Write("Do you wish to use your Potion? [Y/N]: ");

                    var choice = Console.ReadLine();

                    Thread.Sleep(500);

                    if (choice.ToUpper() != "Y" || choice.ToUpper() != "N")
                    {
                        Console.WriteLine("You've wasted your Potion!");
                    }
                    else
                    {
                        playerPotion -= 1;
                        playerHP += 1;
                    }
                }

                if (enemyPotion > 0)
                {
                    Console.WriteLine($"{enemyName} wishes to use Potion.");

                    Thread.Sleep(500);

                    enemyPotion -= 1;
                    enemyHP += 1;
                }

                Console.WriteLine($"{playerName} S:{playerScore} HP: {playerHP} Potion:{playerPotion} | {enemyName} S:{enemyScore} HP:{enemyHP} Potion:{enemyPotion}\n");

                Console.WriteLine($"Round {round} start!\n");



                Console.Write("Enter action ([R]ock, [P]aper,[S]cissor): ");

                var playerAction = Console.ReadLine();

                var isValid = actions.Contains(playerAction.ToUpper());

                Thread.Sleep(500);

                if (!isValid)
                {
                    Console.WriteLine($"Invalid action, round is awarded to {enemyName}");

                    Thread.Sleep(500);

                    Console.WriteLine($"\n{playerName} loses 1 HP");

                    playerHP--;

                    winner = enemyName;

                    gameRounds.Add(round, enemyName);

                    Console.WriteLine($"Round {round} winner: {winner}\n\n");

                }
                else
                {

                    var enemyAction = actions[new Random().Next(0, actions.Count() - 1)].ToUpper();

                    Console.WriteLine($"{playerName} action: {playerAction.ToUpper()} vs {enemyName} action: {enemyAction}");

                    Thread.Sleep(500);

                    if (playerAction.ToUpper() == enemyAction.ToUpper())
                    {

                        winner = "DRAW";

                    }
                    else
                    {

                        var combo = $"{playerAction}{enemyAction}".ToUpper();

                        predictedOutcomes.TryGetValue(combo, out winner);

                        if (winner == playerName)
                        {
                            playerScore++;
                            enemyHP--;
                        }
                        else if (winner == enemyName)
                        {
                            enemyScore++;
                            playerHP--;
                        }

                    }

                    Console.WriteLine($"Round {round} winner: {winner}\n\n");

                    gameRounds.Add(round, winner);

                }
                if (winner != "DRAW")
                {
                    if (stack.LastOrDefault() == null)
                    {

                        stack.Add(winner);
                    }
                    else
                    {

                        if (stack.LastOrDefault() == winner)
                        {

                            stack.Add(winner);

                            if (stack.Count(i => i == winner) == 3)
                            {

                                if (winner == playerName)
                                {

                                    playerPotion++;

                                }
                                else
                                {

                                    enemyPotion++;

                                }

                                stack.Clear();
                            }
                        }
                        else
                        {
                            stack.Clear();
                            stack.Add(winner);
                        }
                    }
                }


                hasWinner = playerHP == 0 || enemyHP == 0;

            }

            Console.WriteLine("Game stats");
            foreach (var r in gameRounds)
            {
                Console.WriteLine($"Round {r.Key}:{r.Value}");
            }

            var gameWinner = "";


            if (playerScore > enemyScore)
            {
                gameWinner = playerName;
            }
            else if (playerScore < enemyScore)
            {
                gameWinner = enemyName;
            }

            Thread.Sleep(300);

            Console.WriteLine($"\nThe grand winner is {gameWinner}");

        }
    }
}