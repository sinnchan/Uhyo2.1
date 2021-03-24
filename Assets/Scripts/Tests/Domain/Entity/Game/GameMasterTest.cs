using System.Collections.Generic;
using System.Linq;
using Main.Domain.Entities.Game;
using NUnit.Framework;
using static Tests.Util.TestUtil;

namespace Tests.Domain.Entity.Game
{
    public class GameMasterTest
    {
        [TestCaseSourceAttribute(typeof(GetSuggestPositionsTestData), nameof(GetSuggestPositionsTestData.TestData))]
        public void GetSuggestPositionsTest(PieceState turn, Board board, List<BoardPosition> expected)
        {
            var gameMaster = new GameMaster(board, turn);
            var actual = gameMaster.GetSuggestPositions(turn);

            //リストのもっとスマートなアサーション無いの…？
            // TODO Nunitならあるかもしれない
            Assert.AreEqual(expected.Count, actual.Count);
            for (var i = 0; i < expected.Count; i++)
                Assert.True(expected[i].IsEqualTo(actual[i]), $"expected{expected[i]} : actual{actual[i]}");
        }

        [TestCaseSourceAttribute(typeof(PlaceTestData), nameof(PlaceTestData.TestData))]
        public void PlaceTest(
            PieceState turn,
            Piece piece,
            BoardPosition placePosition,
            Board testBoard,
            Board expectedBoard,
            List<BoardPosition> expectedPositions)
        {
            var gameMaster = new GameMaster(testBoard, turn);
            var placeResult = gameMaster.Place(piece, placePosition);
            var actual = gameMaster.GetBoardData();

            Assert.True(placeResult.valid);
            Assert.AreEqual(expectedPositions.Count, placeResult.data.Count);
            foreach (var boardPosition in placeResult.data)
                Assert.True(expectedPositions.Exists(position => position.IsEqualTo(boardPosition)));

            Assert.AreEqual(expectedBoard.VisualizeData(), actual.VisualizeData());
        }

        [TestCaseSourceAttribute(typeof(PlaceErrorTestData), nameof(PlaceErrorTestData.TestData))]
        public void PlaceErrorTest(
            PieceState turn,
            Piece piece,
            BoardPosition placePosition,
            Board testBoard)
        {
            var gameMaster = new GameMaster(testBoard, turn);
            var placeResult = gameMaster.Place(piece, placePosition);
            var actual = gameMaster.GetBoardData();

            Assert.False(placeResult.valid);
            Assert.Null(placeResult.data);
            Assert.AreEqual(testBoard.VisualizeData(), actual.VisualizeData());
        }

        
        /// <summary>
        ///     最短全白テスト
        /// </summary>
        [TestCaseSourceAttribute(typeof(GamePlayTestData), nameof(GamePlayTestData.TestData))]
        public void PlayTest(List<(Piece piece, BoardPosition position)> gameProgressList)
        {
            var gameMaster = new GameMaster();
            foreach (var progress in gameProgressList)
            {
                var result = gameMaster.Place(progress.piece, progress.position);
                Assert.True(result.valid, progress.ToString());
                if (progress != gameProgressList.Last())
                    Assert.False(gameMaster.GameEndFlag);
                else
                    Assert.True(gameMaster.GameEndFlag);
            }
        }
    }

    public class GetSuggestPositionsTestData
    {
        public static object[][] TestData()
        {
            return new[]
            {
                // 黒のターンで置ける場所テスト
                new object[]
                {
                    PieceState.Black,
                    new Board(new[,]
                    {
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, W, B, _, _, _},
                        {_, _, _, B, W, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _}
                    }),
                    new List<BoardPosition>
                    {
                        new BoardPosition(3, 2),
                        new BoardPosition(2, 3),
                        new BoardPosition(5, 4),
                        new BoardPosition(4, 5)
                    }
                },
                new object[]
                {
                    PieceState.Black,
                    new Board(new[,]
                    {
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, W, W, W, _, _, _},
                        {_, _, B, B, W, _, _, _},
                        {_, _, _, B, W, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _}
                    }),
                    new List<BoardPosition>
                    {
                        new BoardPosition(1, 1),
                        new BoardPosition(2, 1),
                        new BoardPosition(3, 1),
                        new BoardPosition(4, 1),
                        new BoardPosition(5, 1),
                        new BoardPosition(5, 2),
                        new BoardPosition(5, 3),
                        new BoardPosition(5, 4),
                        new BoardPosition(5, 5)
                    }
                },
                new object[]
                {
                    PieceState.Black,
                    new Board(new[,]
                    {
                        {B, _, W, _, _, _, _, _},
                        {B, _, _, W, B, B, _, _},
                        {B, W, B, B, W, B, W, _},
                        {_, B, W, W, W, B, W, W},
                        {W, W, B, W, W, W, _, _},
                        {_, W, B, _, W, _, _, _},
                        {_, _, W, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _}
                    }),
                    new List<BoardPosition>
                    {
                        new BoardPosition(3, 0),
                        new BoardPosition(4, 0),
                        new BoardPosition(1, 1),
                        new BoardPosition(2, 1),
                        new BoardPosition(7, 1),
                        new BoardPosition(7, 2),
                        new BoardPosition(0, 3),
                        new BoardPosition(6, 4),
                        new BoardPosition(7, 4),
                        new BoardPosition(0, 5),
                        new BoardPosition(3, 5),
                        new BoardPosition(5, 5),
                        new BoardPosition(6, 5),
                        new BoardPosition(0, 6),
                        new BoardPosition(1, 6),
                        new BoardPosition(4, 6),
                        new BoardPosition(5, 6),
                        new BoardPosition(2, 7)
                    }
                },

                // 白のターンで置ける場所テスト
                new object[]
                {
                    PieceState.White,
                    new Board(new[,]
                    {
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, B, _, _, _, _, _},
                        {_, _, _, B, B, _, _, _},
                        {_, _, _, W, W, B, _, _},
                        {_, _, _, W, B, _, B, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _}
                    }),
                    new List<BoardPosition>
                    {
                        new BoardPosition(1, 1),
                        new BoardPosition(3, 2),
                        new BoardPosition(4, 2),
                        new BoardPosition(5, 2),
                        new BoardPosition(6, 4),
                        new BoardPosition(5, 5),
                        new BoardPosition(4, 6),
                        new BoardPosition(5, 6)
                    }
                },
                new object[]
                {
                    PieceState.White,
                    new Board(new[,]
                    {
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, B, _, W, _, _, _},
                        {_, _, _, B, B, W, _, _},
                        {_, _, B, W, W, W, W, _},
                        {_, _, _, B, B, W, B, _},
                        {_, _, _, _, B, _, _, _},
                        {_, _, _, _, _, _, _, _}
                    }),
                    new List<BoardPosition>
                    {
                        new BoardPosition(1, 1),
                        new BoardPosition(3, 2),
                        new BoardPosition(5, 2),
                        new BoardPosition(2, 3),
                        new BoardPosition(1, 4),
                        new BoardPosition(1, 5),
                        new BoardPosition(2, 5),
                        new BoardPosition(7, 5),
                        new BoardPosition(2, 6),
                        new BoardPosition(3, 6),
                        new BoardPosition(5, 6),
                        new BoardPosition(6, 6),
                        new BoardPosition(7, 6),
                        new BoardPosition(3, 7),
                        new BoardPosition(4, 7)
                    }
                },
                new object[]
                {
                    PieceState.White,
                    new Board(new[,]
                    {
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, B, _, _},
                        {_, B, B, _, B, B, W, W},
                        {B, B, B, B, B, W, W, W},
                        {B, B, W, B, W, W, W, W},
                        {B, B, B, W, W, B, W, W},
                        {W, B, W, W, B, W, W, W},
                        {B, B, W, W, W, W, W, W}
                    }),
                    new List<BoardPosition>
                    {
                        new BoardPosition(4, 0),
                        new BoardPosition(5, 0),
                        new BoardPosition(6, 0),
                        new BoardPosition(0, 1),
                        new BoardPosition(1, 1),
                        new BoardPosition(2, 1),
                        new BoardPosition(3, 1),
                        new BoardPosition(4, 1),
                        new BoardPosition(0, 2),
                        new BoardPosition(3, 2)
                    }
                }
            };
        }
    }

    public class PlaceTestData
    {
        public static object[][] TestData()
        {
            return new[]
            {
                new object[]
                {
                    PieceState.Black,
                    Piece.CreateBlack(),
                    new BoardPosition(3, 2),
                    new Board(new[,]
                    {
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, W, B, _, _, _},
                        {_, _, _, B, W, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _}
                    }),
                    new Board(new[,]
                    {
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, B, _, _, _, _},
                        {_, _, _, B, B, _, _, _},
                        {_, _, _, B, W, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _}
                    }),
                    new List<BoardPosition>
                    {
                        new BoardPosition(3, 3)
                    }
                },
                new object[]
                {
                    PieceState.Black,
                    Piece.CreateBlack(),
                    new BoardPosition(0, 5),
                    new Board(new[,]
                    {
                        {B, _, W, _, _, _, _, _},
                        {B, _, _, W, B, B, _, _},
                        {B, W, B, B, W, B, W, _},
                        {_, B, W, W, W, B, W, W},
                        {W, W, B, W, W, W, _, _},
                        {_, W, B, _, W, _, _, _},
                        {_, _, W, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _}
                    }),
                    new Board(new[,]
                    {
                        {B, _, W, _, _, _, _, _},
                        {B, _, _, W, B, B, _, _},
                        {B, W, B, B, W, B, W, _},
                        {_, B, B, W, W, B, W, W},
                        {W, B, B, W, W, W, _, _},
                        {B, B, B, _, W, _, _, _},
                        {_, _, W, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _}
                    }),
                    new List<BoardPosition>
                    {
                        new BoardPosition(2, 3),
                        new BoardPosition(1, 4),
                        new BoardPosition(1, 5)
                    }
                },
                new object[]
                {
                    PieceState.White,
                    Piece.CreateWhite(),
                    new BoardPosition(3, 2),
                    new Board(new[,]
                    {
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, B, _, _},
                        {_, B, B, _, B, B, W, W},
                        {B, B, B, B, B, W, W, W},
                        {B, B, W, B, W, W, W, W},
                        {B, B, B, W, W, B, W, W},
                        {W, B, W, W, B, W, W, W},
                        {B, B, W, W, W, W, W, W}
                    }),
                    new Board(new[,]
                    {
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, B, _, _},
                        {_, B, B, W, W, W, W, W},
                        {B, B, B, W, W, W, W, W},
                        {B, B, W, W, W, W, W, W},
                        {B, B, B, W, W, B, W, W},
                        {W, B, W, W, B, W, W, W},
                        {B, B, W, W, W, W, W, W}
                    }),
                    new List<BoardPosition>
                    {
                        new BoardPosition(4, 2),
                        new BoardPosition(5, 2),
                        new BoardPosition(4, 3),
                        new BoardPosition(3, 3),
                        new BoardPosition(3, 4)
                    }
                }
            };
        }
    }

    public class PlaceErrorTestData
    {
        public static object[][] TestData()
        {
            return new[]
            {
                new object[]
                {
                    PieceState.Black,
                    Piece.CreateWhite(),
                    new BoardPosition(3, 2),
                    new Board(new[,]
                    {
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, W, B, _, _, _},
                        {_, _, _, B, W, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _}
                    })
                },
                new object[]
                {
                    PieceState.Black,
                    Piece.CreateBlack(),
                    new BoardPosition(2, 2),
                    new Board(new[,]
                    {
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, W, B, _, _, _},
                        {_, _, _, B, W, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _}
                    })
                },
                new object[]
                {
                    PieceState.Black,
                    Piece.CreateBlack(),
                    new BoardPosition(5, 3),
                    new Board(new[,]
                    {
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, W, B, _, _, _},
                        {_, _, _, B, W, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _},
                        {_, _, _, _, _, _, _, _}
                    })
                }
            };
        }
    }


    public class GamePlayTestData
    {
        public static object[][] TestData()
        {
            return new[]
            {
                new object[]
                {
                    // 全部白で終了
                    new List<(Piece piece, BoardPosition position)>
                    {
                        (Piece.CreateBlack(), new BoardPosition(5, 4)),
                        (Piece.CreateWhite(), new BoardPosition(5, 5)),
                        (Piece.CreateBlack(), new BoardPosition(4, 5)),
                        (Piece.CreateWhite(), new BoardPosition(5, 3)),
                        (Piece.CreateBlack(), new BoardPosition(4, 2)),
                        (Piece.CreateWhite(), new BoardPosition(3, 1)),
                        (Piece.CreateBlack(), new BoardPosition(3, 2)),
                        (Piece.CreateWhite(), new BoardPosition(3, 5)),
                        (Piece.CreateBlack(), new BoardPosition(2, 3)),
                        (Piece.CreateWhite(), new BoardPosition(1, 3))
                    }
                },
                new object[]
                {
                    // 全部黒で終了
                    new List<(Piece piece, BoardPosition position)>
                    {
                        (Piece.CreateBlack(), new BoardPosition(5, 4)),
                        (Piece.CreateWhite(), new BoardPosition(3, 5)),
                        (Piece.CreateBlack(), new BoardPosition(2, 4)),
                        (Piece.CreateWhite(), new BoardPosition(5, 3)),
                        (Piece.CreateBlack(), new BoardPosition(4, 6)),
                        (Piece.CreateWhite(), new BoardPosition(5, 5)),
                        (Piece.CreateBlack(), new BoardPosition(6, 4)),
                        (Piece.CreateWhite(), new BoardPosition(4, 5)),
                        (Piece.CreateBlack(), new BoardPosition(4, 2))
                    }
                },
                new object[]
                {
                    // スキップありフルテスト
                    new List<(Piece piece, BoardPosition position)>
                    {
                        (Piece.CreateBlack(), new BoardPosition(4, 5)),
                        (Piece.CreateWhite(), new BoardPosition(5, 3)),
                        (Piece.CreateBlack(), new BoardPosition(4, 2)),
                        (Piece.CreateWhite(), new BoardPosition(3, 5)),
                        (Piece.CreateBlack(), new BoardPosition(2, 5)),
                        (Piece.CreateWhite(), new BoardPosition(2, 4)),
                        (Piece.CreateBlack(), new BoardPosition(3, 2)),
                        (Piece.CreateWhite(), new BoardPosition(2, 3)),
                        (Piece.CreateBlack(), new BoardPosition(5, 4)),
                        (Piece.CreateWhite(), new BoardPosition(5, 5)),
                        (Piece.CreateBlack(), new BoardPosition(6, 4)),
                        (Piece.CreateWhite(), new BoardPosition(3, 6)),
                        (Piece.CreateBlack(), new BoardPosition(2, 6)),
                        (Piece.CreateWhite(), new BoardPosition(4, 6)),
                        (Piece.CreateBlack(), new BoardPosition(1, 4)),
                        (Piece.CreateWhite(), new BoardPosition(1, 5)),
                        (Piece.CreateBlack(), new BoardPosition(3, 7)),
                        (Piece.CreateWhite(), new BoardPosition(2, 2)),
                        (Piece.CreateBlack(), new BoardPosition(1, 3)),
                        (Piece.CreateWhite(), new BoardPosition(0, 3)),
                        (Piece.CreateBlack(), new BoardPosition(0, 5)),
                        (Piece.CreateWhite(), new BoardPosition(2, 7)),
                        (Piece.CreateBlack(), new BoardPosition(1, 2)),
                        (Piece.CreateWhite(), new BoardPosition(4, 7)),
                        (Piece.CreateBlack(), new BoardPosition(1, 6)),
                        (Piece.CreateWhite(), new BoardPosition(3, 1)),
                        (Piece.CreateBlack(), new BoardPosition(2, 1)),
                        (Piece.CreateWhite(), new BoardPosition(1, 7)),
                        (Piece.CreateBlack(), new BoardPosition(4, 1)),
                        (Piece.CreateWhite(), new BoardPosition(5, 7)),
                        (Piece.CreateBlack(), new BoardPosition(5, 6)),
                        (Piece.CreateWhite(), new BoardPosition(6, 7)),
                        (Piece.CreateBlack(), new BoardPosition(6, 6)),
                        (Piece.CreateWhite(), new BoardPosition(0, 2)),
                        (Piece.CreateBlack(), new BoardPosition(0, 4)),
                        (Piece.CreateWhite(), new BoardPosition(0, 6)),
                        (Piece.CreateWhite(), new BoardPosition(0, 7)),
                        (Piece.CreateWhite(), new BoardPosition(7, 7)),
                        (Piece.CreateBlack(), new BoardPosition(7, 6)),
                        (Piece.CreateWhite(), new BoardPosition(6, 5)),
                        (Piece.CreateWhite(), new BoardPosition(1, 1)),
                        (Piece.CreateBlack(), new BoardPosition(0, 1)),
                        (Piece.CreateWhite(), new BoardPosition(3, 0)),
                        (Piece.CreateBlack(), new BoardPosition(2, 0)),
                        (Piece.CreateWhite(), new BoardPosition(0, 0)),
                        (Piece.CreateBlack(), new BoardPosition(4, 0)),
                        (Piece.CreateWhite(), new BoardPosition(7, 3)),
                        (Piece.CreateBlack(), new BoardPosition(7, 5)),
                        (Piece.CreateWhite(), new BoardPosition(1, 0)),
                        (Piece.CreateWhite(), new BoardPosition(5, 0)),
                        (Piece.CreateBlack(), new BoardPosition(5, 1)),
                        (Piece.CreateWhite(), new BoardPosition(7, 4)),
                        (Piece.CreateBlack(), new BoardPosition(6, 3)),
                        (Piece.CreateWhite(), new BoardPosition(7, 2)),
                        (Piece.CreateWhite(), new BoardPosition(6, 1)),
                        (Piece.CreateBlack(), new BoardPosition(6, 0)),
                        (Piece.CreateWhite(), new BoardPosition(5, 2)),
                        (Piece.CreateBlack(), new BoardPosition(6, 2)),
                        (Piece.CreateWhite(), new BoardPosition(7, 1)),
                        (Piece.CreateWhite(), new BoardPosition(7, 0))
                    }
                }
            };
        }
    }
}