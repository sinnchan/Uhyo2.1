using System;
using Main.Domain.Entities.Game;
using NUnit.Framework;

namespace Tests.Domain.Entity.Game
{
    public class TestBoard
    {
        [Test]
        public void IsEmptyTest()
        {
            var board = new Board();
            Assert.True(board.IsEmpty());
            board.PlacePiece(new Piece(PieceState.Black), new BoardPosition(1, 1));
            Assert.False(board.IsEmpty());
            board.Clear();
            Assert.True(board.IsEmpty());
        }

        [TestCase(1, 1)]
        [TestCase(8, 1)]
        [TestCase(1, 8)]
        [TestCase(8, 8)]
        [TestCase(2, 2)]
        public void PlacePieceTest(int x, int y)
        {
            var board = new Board();
            board.PlacePiece(new Piece(PieceState.Black), new BoardPosition(x, y));
        }

        [Test]
        public void IsFullTest()
        {
            var board = new Board();
            Assert.False(board.IsFull());
            Board.LoopAccessAll(position => { board.PlacePiece(Piece.CreateBlack(), position); });
            Assert.True(board.IsFull());
        }

        [Test]
        public void CountPieceTest()
        {
            var board = new Board();
            Assert.AreEqual(Math.Pow(Board.Length, 2), board.Count(PieceState.Space));
        }
    }
}
