using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Project02.Models;
using Project02.Services;
using Project02.Services.Abstractions;

namespace Project02.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void NotAllowTieSingleDeckFirstWin()
        {
            //Arrange
            var deckService = new Mock<IDeckService>();
            deckService.SetupSequence(x => x.PlayCard(It.IsAny<Deck>())).Returns(new Card(7, Suit.Spades)).Returns(new Card(5, Suit.Clubs));

            var suitService = new Mock<ISuitService>();
            var gameSettings = new GameSettings { AllowTie = false, DeckCount = 1, DeckCardCount = 52 };
            var gameService = new GameService(gameSettings, deckService.Object, suitService.Object);

            //Act
            var result = gameService.Play();

            //Assert
            Assert.AreEqual("Winning card: Spades, 7", result);
        }

        [TestMethod]
        public void NotAllowTieSingleDeckSecondWin()
        {
            //Arrange
            var deckService = new Mock<IDeckService>();
            deckService.SetupSequence(x => x.PlayCard(It.IsAny<Deck>())).Returns(new Card(7, Suit.Spades)).Returns(new Card(11, Suit.Hearts));

            var suitService = new Mock<ISuitService>();
            var gameSettings = new GameSettings { AllowTie = false, DeckCount = 1, DeckCardCount = 52 };
            var gameService = new GameService(gameSettings, deckService.Object, suitService.Object);

            //Act
            var result = gameService.Play();

            //Assert
            Assert.AreEqual("Winning card: Hearts, 11", result);
        }

        [TestMethod]
        public void AllowTieSingleDeck()
        {
            //Arrange
            var deckService = new Mock<IDeckService>();
            deckService.Setup(x => x.PlayCard(It.IsAny<Deck>())).Returns(new Card(7, Suit.Spades));

            var suitService = new Mock<ISuitService>();
            var gameSettings = new GameSettings { AllowTie = true, DeckCount = 1, DeckCardCount = 52 };
            var gameService = new GameService(gameSettings, deckService.Object, suitService.Object);

            //Act
            var result = gameService.Play();

            //Assert
            Assert.AreEqual("Tie. Card #1: Spades, 7; Card #2: Spades, 7;", result);
        }

        [TestMethod]
        public void NotAllowTieSingleDeckSuitPrecedence()
        {
            //Arrange
            var deckService = new Mock<IDeckService>();
            deckService.SetupSequence(x => x.PlayCard(It.IsAny<Deck>())).Returns(new Card(7, Suit.Clubs)).Returns(new Card(7, Suit.Hearts));

            var gameSettings = new GameSettings { AllowTie = false, DeckCount = 1, DeckCardCount = 52, SuitPrecedence = "Clubs,Diamonds,Hearts,Spades" };
            var suitService = new SuitService(gameSettings);
            var gameService = new GameService(gameSettings, deckService.Object, suitService);

            //Act
            var result = gameService.Play();

            //Assert
            Assert.AreEqual("Winning card: Hearts, 7", result);
        }

        [TestMethod]
        public void NotAllowTieSingleDeckNoSuitPrecedence()
        {
            //Arrange
            var deckService = new Mock<IDeckService>();
            deckService.SetupSequence(x => x.PlayCard(It.IsAny<Deck>())).Returns(new Card(7, Suit.Clubs)).Returns(new Card(7, Suit.Hearts)).Returns(new Card(7, Suit.Diamonds)).Returns(new Card(7, Suit.Spades)).Returns(new Card(11, Suit.Diamonds)).Returns(new Card(7, Suit.Hearts));

            var gameSettings = new GameSettings { AllowTie = false, DeckCount = 1, DeckCardCount = 52 };
            var suitService = new SuitService(gameSettings);
            var gameService = new GameService(gameSettings, deckService.Object, suitService);

            //Act
            var result = gameService.Play();

            //Assert
            Assert.AreEqual("Winning card: Diamonds, 11", result);
        }

        [TestMethod]
        public void AllowTieSingleDeckWithWildcard()
        {
            //Arrange
            var deckService = new Mock<IDeckService>();
            deckService.SetupSequence(x => x.PlayCard(It.IsAny<Deck>())).Returns(new Card(7, Suit.Spades)).Returns(new Card(101, Suit.Unknown));

            var suitService = new Mock<ISuitService>();
            var gameSettings = new GameSettings { AllowTie = true, DeckCount = 1, DeckCardCount = 52, AllowWildcard = true };
            var gameService = new GameService(gameSettings, deckService.Object, suitService.Object);

            //Act
            var result = gameService.Play();

            //Assert
            Assert.AreEqual("Winning card: Wildcard", result);
        }

        [TestMethod]
        public void NotAllowTieThreeDecksFirstWin()
        {
            //Arrange
            var deckService = new Mock<IDeckService>();
            deckService.SetupSequence(x => x.PlayCard(It.IsAny<Deck>())).Returns(new Card(7, Suit.Spades)).Returns(new Card(5, Suit.Clubs)).Returns(new Card(3, Suit.Hearts));

            var suitService = new Mock<ISuitService>();
            var gameSettings = new GameSettings { AllowTie = false, DeckCount = 3, DeckCardCount = 52 };
            var gameService = new GameService(gameSettings, deckService.Object, suitService.Object);

            //Act
            var result = gameService.Play();

            //Assert
            Assert.AreEqual("Winning card: Spades, 7", result);
        }

        [TestMethod]
        public void NotAllowTieThreeDecksSecondWin()
        {
            //Arrange
            var deckService = new Mock<IDeckService>();
            deckService.SetupSequence(x => x.PlayCard(It.IsAny<Deck>())).Returns(new Card(7, Suit.Spades)).Returns(new Card(9, Suit.Clubs)).Returns(new Card(3, Suit.Hearts));

            var suitService = new Mock<ISuitService>();
            var gameSettings = new GameSettings { AllowTie = false, DeckCount = 3, DeckCardCount = 52 };
            var gameService = new GameService(gameSettings, deckService.Object, suitService.Object);

            //Act
            var result = gameService.Play();

            //Assert
            Assert.AreEqual("Winning card: Clubs, 9", result);
        }

        [TestMethod]
        public void NotAllowTieThreeDecksThirdWin()
        {
            //Arrange
            var deckService = new Mock<IDeckService>();
            deckService.SetupSequence(x => x.PlayCard(It.IsAny<Deck>())).Returns(new Card(7, Suit.Spades)).Returns(new Card(9, Suit.Clubs)).Returns(new Card(11, Suit.Hearts));

            var suitService = new Mock<ISuitService>();
            var gameSettings = new GameSettings { AllowTie = false, DeckCount = 3, DeckCardCount = 52 };
            var gameService = new GameService(gameSettings, deckService.Object, suitService.Object);

            //Act
            var result = gameService.Play();

            //Assert
            Assert.AreEqual("Winning card: Hearts, 11", result);
        }

        [TestMethod]
        public void AllowTieThreeDecksThreeInTie()
        {
            //Arrange
            var deckService = new Mock<IDeckService>();
            deckService.SetupSequence(x => x.PlayCard(It.IsAny<Deck>())).Returns(new Card(7, Suit.Spades)).Returns(new Card(7, Suit.Clubs)).Returns(new Card(7, Suit.Hearts));

            var suitService = new Mock<ISuitService>();
            var gameSettings = new GameSettings { AllowTie = true, DeckCount = 3, DeckCardCount = 52 };
            var gameService = new GameService(gameSettings, deckService.Object, suitService.Object);

            //Act
            var result = gameService.Play();

            //Assert
            Assert.AreEqual("Tie. Card #1: Spades, 7; Card #2: Clubs, 7; Card #3: Hearts, 7;", result);
        }

        [TestMethod]
        public void NotAllowTieThreeDecksSuitPrecedence()
        {
            //Arrange
            var deckService = new Mock<IDeckService>();
            deckService.SetupSequence(x => x.PlayCard(It.IsAny<Deck>())).Returns(new Card(7, Suit.Diamonds)).Returns(new Card(7, Suit.Clubs)).Returns(new Card(7, Suit.Hearts));

            var gameSettings = new GameSettings { AllowTie = false, DeckCount = 3, DeckCardCount = 52, SuitPrecedence = "Clubs,Diamonds,Hearts,Spades" };
            var suitService = new SuitService(gameSettings);
            var gameService = new GameService(gameSettings, deckService.Object, suitService);

            //Act
            var result = gameService.Play();

            //Assert
            Assert.AreEqual("Winning card: Hearts, 7", result);
        }

        [TestMethod]
        public void AllowTieThreeDecksWithWildcard()
        {
            //Arrange
            var deckService = new Mock<IDeckService>();
            deckService.SetupSequence(x => x.PlayCard(It.IsAny<Deck>())).Returns(new Card(7, Suit.Spades)).Returns(new Card(101, Suit.Unknown)).Returns(new Card(11, Suit.Hearts));

            var suitService = new Mock<ISuitService>();
            var gameSettings = new GameSettings { AllowTie = true, DeckCount = 3, DeckCardCount = 52, AllowWildcard = true };
            var gameService = new GameService(gameSettings, deckService.Object, suitService.Object);

            //Act
            var result = gameService.Play();

            //Assert
            Assert.AreEqual("Winning card: Wildcard", result);
        }

        [TestMethod]
        public void AllowTieThreeDecksWithTwentyCardsPerDeck()
        {
            //Arrange
            var deckService = new Mock<IDeckService>();
            deckService.SetupSequence(x => x.PlayCard(It.IsAny<Deck>())).Returns(new Card(2, Suit.Spades)).Returns(new Card(3, Suit.Clubs)).Returns(new Card(4, Suit.Hearts));

            var suitService = new Mock<ISuitService>();
            var gameSettings = new GameSettings { AllowTie = true, DeckCount = 3, DeckCardCount = 20, AllowWildcard = true };
            var gameService = new GameService(gameSettings, deckService.Object, suitService.Object);

            //Act
            var result = gameService.Play();

            //Assert
            Assert.AreEqual("Winning card: Hearts, 4", result);
        }
    }
}
