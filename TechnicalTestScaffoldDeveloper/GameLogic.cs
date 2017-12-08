using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnicalTestScaffoldDeveloper
{
    public static class GameLogic
    {

        private static Cards.Hand GetInput(int numOfCards)
        {
            var hand = new Cards.Hand();

            for (int i = 0; i < numOfCards; i++)
            {
                string input = null;
                do
                {
                    Console.Write($"Card {i}: ");
                    input = Console.ReadLine();

                    var intCheck = Validation.InputValue.IsInt(input);
                    if (!intCheck.IsValid)
                    {
                        input = null;
                        Helpers.ConsoleHelper.OverwriteLastLine("");
                        Console.WriteLine(intCheck.FailureReason);
                        continue;
                    }

                    var handCheck = hand.AddCard(intCheck.Result.Value);
                    if (!handCheck.IsValid)
                    {
                        input = null;
                        Helpers.ConsoleHelper.OverwriteLastLine("");
                        Console.WriteLine(handCheck.FailureReason);
                        continue;
                    }

                } while (string.IsNullOrWhiteSpace(input));
            }

            return hand;
        }

        private static void OutputScore(int score)
        {
            Console.Out.WriteLine(String.Format("Your score is: {0}", score));
        }

        public static void RunQuestion1()
        {
            Cards.Hand hand = GetInput(5);
            int score = hand.CalculateScore();
            OutputScore(score);
        }

        public static void RunQuestion2()
        {
            //foreach(var t in Cards.Deck.AllHands(5))
            //{
            //    var hand = new Cards.Hand();
            //    foreach (int i in t)
            //    {
            //        if (!hand.AddCard(i).IsValid) {
            //            var foo = "";
            //        }
            //    }

            //    var score = hand.CalculateScore();
            //    if (hand.CalculateScore() == 0)
            //    {
            //        var face = "";
            //    }
            //}

            Console.WriteLine("Trying to generate a hand which scores 0 points. This may take some time...");

            var hand = new Cards.Hand();
            hand.GenerateZeroScoringhand(5);

            Console.WriteLine("Here is a randomly generated hand with a score of 0:");
            Console.WriteLine(string.Join(",", hand.CardValues));
        }

        public static void RunQuestion3()
        {
            var matchingHands = new List<string>();

            var cardValues = new List<int>() { 0, 1, 1, 1, 1 };

            do
            {
                if (cardValues[0] == GameSettings.Instance.LastCard)
                {
                    if (cardValues.Take(cardValues.Count).Sum() == cardValues.Count * GameSettings.Instance.LastCard)
                    {
                        break;
                    }
                    int? indexToRollOver = null;
                    for (int i = 0; i < cardValues.Count; i++)
                    {
                        if (cardValues[i] == GameSettings.Instance.LastCard && i + 1 <= cardValues.Count() && cardValues[i + 1] != GameSettings.Instance.LastCard)
                        {
                            indexToRollOver = i;
                            break;
                        }
                    }

                    if (indexToRollOver.HasValue)
                    {
                        cardValues[indexToRollOver.Value + 1] += 1;

                        for (int i = indexToRollOver.Value; i >= 0; i--)
                        {
                            cardValues[i] = GameSettings.Instance.FirstCard;
                        }
                    }
                    else
                    {
                        cardValues[0] = GameSettings.Instance.FirstCard;
                    }
                }
                else
                {
                    cardValues[0] += 1;
                }

                if (cardValues.Take(cardValues.Count).Sum() == 15)
                {
                    //Validate the hand
                    bool notValid = false;
                    var hand = new Cards.Hand();
                    foreach (int i in cardValues)
                    {
                        if (!hand.AddCard(i).IsValid)
                        {
                            notValid = true;
                            break;
                        }
                    }

                    if (!notValid)
                    {
                        string solution = string.Join(",", cardValues.OrderBy(i => i));
                        if (!matchingHands.Contains(solution))
                        {
                            matchingHands.Add(solution);
                        }
                    }
                }

                //Console.WriteLine(string.Join(",", cardValues));

            } while (true);

            Console.WriteLine($"There are {matchingHands.Count} hands which add up to 15");
            Console.WriteLine(string.Join("\n", matchingHands));
        }


    }
}
