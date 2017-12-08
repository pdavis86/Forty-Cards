using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnicalTestScaffoldDeveloper.Cards
{
    public class Deck
    {

        public static IEnumerable<List<int>> AllHands(int numberOfCards)
        {
            var cardValues = new List<int>();
            cardValues.Add(0);
            for (int i = 1; i < numberOfCards; i++)
            {
                cardValues.Add(1);
            }

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
                    var hand = new Hand();
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
                        yield return cardValues.Take(cardValues.Count).ToList();
                    }
                }

                Console.WriteLine(string.Join(",", cardValues)); //todo: comment out

            } while (true);

        }


    }
}
