using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechnicalTestScaffoldDeveloper;
using TechnicalTestScaffoldDeveloper.Cards;
using TechnicalTestScaffoldDeveloper.Validation;

namespace TechnicalTestScaffoldingUnitTests
{
    [TestClass]
    public class Input
    {
        [TestMethod]
        public void InputIsAnInt()
        {
            var invalidStringResult = InputValue.IsInt("a");
            Assert.IsFalse(invalidStringResult.IsValid);

            var decimalRoundingResult = InputValue.IsInt("1.2");
            Assert.IsFalse(decimalRoundingResult.IsValid);

            var validIntResult = InputValue.IsInt("1");
            Assert.IsTrue(validIntResult.IsValid);
        }

        [TestMethod]
        public void InputIntIsInRange()
        {
            var lowerBound = GameSettings.Instance.FirstCard;
            var upperBound = GameSettings.Instance.LastCard;

            var lowResult = InputValue.IsIntInRange(-20, lowerBound, upperBound);
            Assert.IsFalse(lowResult.IsValid);

            var highResult = InputValue.IsIntInRange(50, lowerBound, upperBound);
            Assert.IsFalse(highResult.IsValid);

            var okResult = InputValue.IsIntInRange(5, lowerBound, upperBound);
            Assert.IsTrue(okResult.IsValid);
        }

        [TestMethod]
        public void RepeatLimitCannotBeExceeded()
        {
            var hand = new Hand();

            for (int i = 0; i < GameSettings.Instance.RepeatLimit; i++)
            {
                hand.AddCard(1);
            }

            var repeatCheck = hand.AddCard(1);
            Assert.IsFalse(repeatCheck.IsValid);
        }

    }
}
