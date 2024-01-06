using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Conway.Core;

namespace Conway.Core.Test
{
    [TestFixture]
    public class GameOfLifeTests
    {
        [Test]
        public void CalculateState_BoardWithOneCell_AfterOneTick_Dies()
        {
            // Arrange
            var board = new List<List<bool>> { new List<bool> { true } };
            IEnumerable<IEnumerable<bool>> expected = new List<List<bool>> { new List<bool> { false } };

            // Act
            var result = GameOfLife.CalculateState(board, 1);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateState_EmptyBoard_AfterOneTick_RemainsEmpty()
        {
            // Arrange
            var board = new List<List<bool>> { new List<bool> { false, false, false }, new List<bool> { false, false, false }, new List<bool> { false, false, false } };

            // Act
            var result = GameOfLife.CalculateState(board, 1);

            // Assert
            Assert.That(result, Is.EqualTo(board));
        }

        [Test]
        public void CalculateState_BoardWithBlinkerPattern_AfterOneTick_SwitchesToVerticalPattern()
        {
            // Arrange
            var board = new List<List<bool>> { new List<bool> { false, true, false }, new List<bool> { false, true, false }, new List<bool> { false, true, false } };
            IEnumerable<IEnumerable<bool>> expected = new List<List<bool>> { new List<bool> { false, false, false }, new List<bool> { true, true, true }, new List<bool> { false, false, false } };

            // Act
            var result = GameOfLife.CalculateState(board, 1);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateState_BoardWithBlinkerPattern_AfterTwoTicks_ReturnsToOriginalState()
        {
            // Arrange
            var board = new List<List<bool>> { new List<bool> { false, true, false }, new List<bool> { false, true, false }, new List<bool> { false, true, false } };
            IEnumerable<IEnumerable<bool>> expected = new List<List<bool>> { new List<bool> { false, true, false }, new List<bool> { false, true, false }, new List<bool> { false, true, false } };

            // Act
            var result = GameOfLife.CalculateState(board, 2);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateState_BoardWithGliderPattern_AfterOneTick_MovesToNextState()
        {
            // Arrange
            var board = new List<List<bool>> { new List<bool> { false, true, false }, new List<bool> { false, false, true }, new List<bool> { true, true, true } };
            IEnumerable<IEnumerable<bool>> expected = new List<List<bool>> { new List<bool> { false, false, false }, new List<bool> { true, false, true }, new List<bool> { false, true, true } };

            // Act
            var result = GameOfLife.CalculateState(board, 1);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateState_BoardWithGliderPattern_AfterTwoTicks_MovesToNextState()
        {
            // Arrange
            var board = new List<List<bool>> { new List<bool> { false, true, false }, new List<bool> { false, false, true }, new List<bool> { true, true, true } };
            IEnumerable<IEnumerable<bool>> expected = new List<List<bool>> { new List<bool> { false, false, false }, new List<bool> { false, false, true }, new List<bool> { false, true, true } };

            // Act
            var result = GameOfLife.CalculateState(board, 2);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateState_BoardWithBlockPattern_AfterOneTick_RemainsSame()
        {
            // Arrange
            var board = new List<List<bool>> { new List<bool> { true, true }, new List<bool> { true, true } };

            // Act
            var result = GameOfLife.CalculateState(board, 1);

            // Assert
            Assert.That(result, Is.EqualTo(board));
        }

        [Test]
        public void CalculateState_BoardWithBlinkerPattern_AfterTwoTicks_SwitchesBackToHorizontalPattern()
        {
            // Arrange
            var board = new List<List<bool>> { new List<bool> { false, true, false }, new List<bool> { false, true, false }, new List<bool> { false, true, false } };
            IEnumerable<IEnumerable<bool>> expected = new List<List<bool>> { new List<bool> { false, true, false }, new List<bool> { false, true, false }, new List<bool> { false, true, false } };

            // Act
            var result = GameOfLife.CalculateState(board, 2);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateState_BoardWithToadPattern_AfterOneTick_MovesToNextState()
        {
            // Arrange
            var board = new List<List<bool>>
            {
                new List<bool> { false, false, false, false, false, false },
                new List<bool> { false, false, false, false, false, false },
                new List<bool> { false, false, true, true, true, false },
                new List<bool> { false, true, true, true, false, false },
                new List<bool> { false, false, false, false, false, false },
                new List<bool> { false, false, false, false, false, false }
            };

            IEnumerable<IEnumerable<bool>> expected = new List<List<bool>>
            {
                new List<bool> { false, false, false, false, false, false },
                new List<bool> { false, false, false, true, false, false },
                new List<bool> { false, true, false, false, true, false },
                new List<bool> { false, true, false, false, true, false },
                new List<bool> { false, false, true, false, false, false },
                new List<bool> { false, false, false, false, false, false }
            };

            // Act
            var result = GameOfLife.CalculateState(board, 1);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateState_NonSquareBoard_AfterOneTick_MovesToNextState()
        {
            // Arrange
            var board = new List<List<bool>>
            {
                new List<bool> { false, true, false, false, false },
                new List<bool> { true, true, false, true, false },
                new List<bool> { false, false, true, false, false }
            };

            IEnumerable<IEnumerable<bool>> expected = new List<List<bool>>
            {
                new List<bool> { true, true, true, false, false },
                new List<bool> { true, true, false, false, false },
                new List<bool> { false, true, true, false, false }
            };

            // Act
            var result = GameOfLife.CalculateState(board, 1);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateState_NullBoard_ThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<IEnumerable<bool>>? board = null;
            int ticks = 1;

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => GameOfLife.CalculateState(board, ticks));
        }

        [Test]
        public void CalculateState_ZeroTicks_ThrowsException()
        {
            // Arrange
            var board = new List<List<bool>> { new List<bool> { false, true, false }, new List<bool> { true, false, true }, new List<bool> { false, true, false } };
            int ticks = 0;

            // Act and Assert
            Assert.Throws<Exception>(() => GameOfLife.CalculateState(board, ticks));
        }

        [Test]
        public void CalculateState_NegativeTicks_ThrowsException()
        {
            // Arrange
            var board = new List<List<bool>> { new List<bool> { false, true, false }, new List<bool> { true, false, true }, new List<bool> { false, true, false } };
            int ticks = -1;

            // Act and Assert
            Assert.Throws<Exception>(() => GameOfLife.CalculateState(board, ticks));
        }
    }
}
