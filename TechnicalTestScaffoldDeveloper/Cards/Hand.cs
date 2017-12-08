using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnicalTestScaffoldDeveloper.Cards
{
    public class Hand
    {
        public List<int> CardValues { get { return _cardValues; } }
        public Dictionary<int, int> RepeatedValues { get { return _repeatCheck; } }

        private List<int> _cardValues;
        private Dictionary<int, int> _repeatCheck;
        private Random _rand;

        public Hand()
        {
            _cardValues = new List<int>();
            _repeatCheck = new Dictionary<int, int>();
            _rand = new Random();
        }

        public Validation.Models.ValidationResult AddCard(int card)
        {
            var rangeCheck = Validation.InputValue.IsIntInRange(card, GameSettings.Instance.FirstCard, GameSettings.Instance.LastCard);
            if (!rangeCheck.IsValid)
            {
                return rangeCheck;
            }

            if (_repeatCheck.ContainsKey(card))
            {
                if (_repeatCheck[card] < GameSettings.Instance.RepeatLimit)
                {
                    _repeatCheck[card] += 1;
                }
                else
                {
                    return new Validation.Models.ValidationResult()
                    {
                        IsValid = false,
                        FailureReason = $"There are already {_repeatCheck[card]} instances of {card} chosen"
                    };
                }
            }
            else
            {
                _repeatCheck.Add(card, 1);
            }

            _cardValues.Add(card);

            return new Validation.Models.ValidationResult()
            {
                IsValid = true
            };
        }

        public int CalculateScore()
        {
            int score = 0;

            //Pairs are calculated by n(n-1)/2
            //https://stackoverflow.com/questions/18859430/how-do-i-get-the-total-number-of-unique-pairs-of-a-set-in-the-database
            var dupes = RepeatedValues.Where(r => r.Value > 1);
            foreach (var dupe in dupes)
            {
                score += (dupe.Value * (dupe.Value - 1)) / 2;
            }

            //Sum to 15 - will have to compute every combination
            //https://stackoverflow.com/questions/7802822/all-possible-combinations-of-a-list-of-values
            var sums = new List<int>();
            double count = Math.Pow(2, CardValues.Count);
            for (int i = 1; i <= count - 1; i++)
            {
                int cardSum = 0;
                string str = Convert.ToString(i, 2).PadLeft(CardValues.Count, '0');
                for (int j = 0; j < str.Length; j++)
                {
                    if (str[j] == '1')
                    {
                        cardSum += CardValues[j];
                    }
                }
                sums.Add(cardSum);
            }
            score += sums.Where(s => s == 15).Count();

            return score;
        }

        public void GenerateZeroScoringhand(int numberOfCards)
        {
            // e.g. { 10, 9, 8, 1, 2 }

            var lowerBound = GameSettings.Instance.FirstCard;
            var upperBound = GameSettings.Instance.LastCard;

            //todo: build this on the fly when calculating scores
            var twoCardFifteens = new Dictionary<int, int>();
            twoCardFifteens.Add(10, 5);
            twoCardFifteens.Add(9, 6);
            twoCardFifteens.Add(8, 7);
            twoCardFifteens.Add(7, 8);
            twoCardFifteens.Add(6, 9);
            twoCardFifteens.Add(5, 10);

            do
            {
                _cardValues.Clear();
                for (int i = 1; i <= numberOfCards; i++)
                {
                    do
                    {
                        var candidate = _rand.Next(lowerBound, upperBound);

                        //todo: avoid generating previously tried solutions

                        //Do not allow duplicates
                        if (!_cardValues.Contains(candidate))
                        {
                            //Avoid known two-card 15s
                            int? fifteenCheck = null;
                            if (twoCardFifteens.ContainsKey(candidate))
                            {
                                fifteenCheck = twoCardFifteens[candidate];
                            }
                            if (fifteenCheck == null || !_cardValues.Contains(fifteenCheck.Value))
                            {
                                _cardValues.Add(candidate);
                            }
                        }

                    } while (_cardValues.Count != i);
                }
                var score = CalculateScore();
            } while (CalculateScore() != 0);
        }


    }
}
