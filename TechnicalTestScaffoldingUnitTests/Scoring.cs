using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechnicalTestScaffoldDeveloper;
using TechnicalTestScaffoldDeveloper.Cards;

namespace TechnicalTestScaffoldingUnitTests
{
    [TestClass]
    public class Scoring
    {
        [TestClass]
        public class ScoreTests
        {
            [TestMethod]
            public void PairsCountedCorrectly()
            {
                var twoPairHand = new Hand();
                twoPairHand.AddCard(1);
                twoPairHand.AddCard(1);
                twoPairHand.AddCard(2);
                twoPairHand.AddCard(3);
                twoPairHand.AddCard(3);
                var twoPairScore = twoPairHand.CalculateScore();
                Assert.AreEqual(2, twoPairScore);

                var eightPairHand = new Hand();
                eightPairHand.AddCard(1);
                eightPairHand.AddCard(1);
                eightPairHand.AddCard(2);
                eightPairHand.AddCard(3);
                eightPairHand.AddCard(3);
                var eightPairScore = eightPairHand.CalculateScore();
                Assert.AreEqual(2, eightPairScore);
            }

            [TestMethod]
            public void FifteensCountedCorrectly()
            {
                var twoFifteenHand = new Hand();
                twoFifteenHand.AddCard(3);
                twoFifteenHand.AddCard(4);
                twoFifteenHand.AddCard(5);
                twoFifteenHand.AddCard(6);
                twoFifteenHand.AddCard(7);
                var twoFifteenScore = twoFifteenHand.CalculateScore();
                Assert.AreEqual(2, twoFifteenScore);

                var threeFifteenHand = new Hand();
                threeFifteenHand.AddCard(9);
                threeFifteenHand.AddCard(6);
                threeFifteenHand.AddCard(8);
                threeFifteenHand.AddCard(7);
                threeFifteenHand.AddCard(1);
                var threeFifteenScore = threeFifteenHand.CalculateScore();
                Assert.AreEqual(3, threeFifteenScore);
            }
        }


    }
}
