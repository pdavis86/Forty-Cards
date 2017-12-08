using System.Collections.Generic;

namespace TechnicalTestScaffoldDeveloper.Validation
{
    public class CardList
    {
        public static Models.IntValidationResult CanAdd(int candidate, List<int> cardList)
        {
            var repeatCheck = new Dictionary<int, int>();
            var repeatLimit = GameSettings.Instance.RepeatLimit;

            foreach (int card in cardList)
            {
                if (repeatCheck.ContainsKey(card))
                {
                    if (repeatCheck[card] < repeatLimit)
                    {
                        repeatCheck[card] += 1;
                    }
                    else
                    {
                        return new Models.IntValidationResult()
                        {
                            IsValid = false,
                            FailureReason = $"There are already {repeatLimit} instances of {card} chosen"
                        };
                    }
                }
                else
                {
                    repeatCheck.Add(card, 1);
                }
            }

            if()

            return new Models.IntValidationResult()
            {
                IsValid = true
            };
        }

    }
}
