using Microsoft.VisualStudio.TestTools.UnitTesting;
using MisereTicTacToe.WPF;
using System;

namespace MisereTicTacToe.Tests
{
	[TestClass]
	public class GameTests
	{
		#region Constructor
		[TestMethod]
		public void Constructor_DefaultFieldStateTest()
		{
			Game game = new Game();

			Assert.IsTrue(game.Fields.Rank == 2);
			Assert.IsTrue(game.Fields.Length == 9);
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					Assert.IsTrue(game.Fields[i, j] == FieldState.EMPTY);
				}
			}
		}

		[TestMethod]
		public void Constructor_CustomFieldsTest()
		{
			Game game = new Game(null, null, new FieldState[,]
			{
				{ FieldState.PLAYER_X, FieldState.PLAYER_X, FieldState.PLAYER_X },
				{ FieldState.PLAYER_X, FieldState.PLAYER_X, FieldState.PLAYER_X },
				{ FieldState.PLAYER_X, FieldState.PLAYER_X, FieldState.PLAYER_X }
			});

			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					Assert.IsTrue(game.Fields[i, j] == FieldState.PLAYER_X);
				}
			}
		}
		#endregion

		[TestMethod]
		public void ClickField_BeforeGameStartedTest()
		{
			//Arrange
			Game game = new Game();

			//Act 
			bool result = game.ExecutePlayerAction(0, 0);

			//Assert
			Assert.IsFalse(result);
		}

		#region Checking methods
		[TestMethod]
		public void IsFieldOccupied_DefaultFields()
		{
			Game game = new Game();

			bool result = game.IsFieldOccupied(0, 0);

			Assert.IsFalse(result);
		}
		#endregion

		#region Executing methods
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void OccupyField_InvalidNewState()
		{
			Game game = new Game();

			game.OccupyField(0, 0, FieldState.EMPTY);
		}

		[TestMethod]
		public void OccupyField_OccupedField()
		{
			Game game = new Game();

			game.Fields[0, 0] = FieldState.PLAYER_X;
			Assert.IsFalse(game.OccupyField(0, 0, FieldState.PLAYER_X));
		}

		[TestMethod]
		public void SetNewTurn_ChangeTurnTest()
		{
			Game game = new Game();
			game.State = GameState.AI;

			game.SetNewTurn();
			Assert.IsTrue(game.State == GameState.PLAYER);
		}
		#endregion

		#region Check if game has ended
		[TestMethod]
		public void CheckIfGameHasEnded_RowTest()
		{
			Game game = new Game(null, null, new FieldState[,]
			{
				{ FieldState.EMPTY, FieldState.AI_X, FieldState.EMPTY },
				{ FieldState.PLAYER_X, FieldState.PLAYER_X, FieldState.AI_X },
				{ FieldState.EMPTY, FieldState.EMPTY, FieldState.EMPTY },
			});

			Assert.IsTrue(game.CheckIfGameHasEnded());
		}

		[TestMethod]
		public void CheckIfGameHasEnded_ColumnTest()
		{
			Game game = new Game(null, null, new FieldState[,]
			{
				{ FieldState.EMPTY, FieldState.AI_X, FieldState.EMPTY },
				{ FieldState.EMPTY, FieldState.PLAYER_X, FieldState.AI_X },
				{ FieldState.EMPTY, FieldState.PLAYER_X, FieldState.EMPTY },
			});

			Assert.IsTrue(game.CheckIfGameHasEnded());
		}

		[TestMethod]
		public void CheckIfGameHasEnded_DiagonalLRTest()
		{
			Game game = new Game(null, null, new FieldState[,]
			{
				{ FieldState.PLAYER_X, FieldState.AI_X, FieldState.EMPTY },
				{ FieldState.EMPTY, FieldState.PLAYER_X, FieldState.AI_X },
				{ FieldState.EMPTY, FieldState.EMPTY, FieldState.AI_X },
			});

			Assert.IsTrue(game.CheckIfGameHasEnded());
		}

		[TestMethod]
		public void CheckIfGameHasEnded_DiagonalRLTest()
		{
			Game game = new Game(null, null, new FieldState[,]
			{
				{ FieldState.EMPTY, FieldState.AI_X, FieldState.PLAYER_X },
				{ FieldState.EMPTY, FieldState.PLAYER_X, FieldState.AI_X },
				{ FieldState.AI_X, FieldState.EMPTY, FieldState.EMPTY },
			});

			Assert.IsTrue(game.CheckIfGameHasEnded());
		}

		[TestMethod]
		public void CheckIfGameHasEnded_UnfinishedGameTest()
		{
			Game game = new Game(null, null, new FieldState[,]
			{
				{ FieldState.EMPTY, FieldState.AI_X, FieldState.EMPTY },
				{ FieldState.PLAYER_X, FieldState.EMPTY, FieldState.AI_X },
				{ FieldState.EMPTY, FieldState.PLAYER_X, FieldState.EMPTY },
			});

			Assert.IsFalse(game.CheckIfGameHasEnded());
		}

		[TestMethod]
		public void CheckIfGameHasEnded_DefaultGameTest()
		{
			Game game = new Game(null);

			Assert.IsFalse(game.CheckIfGameHasEnded());
		}

		[TestMethod]
		public void FindAnyEmptyField_PositiveTest()
		{
			Game game = new Game(null, null, new FieldState[,]
			{
				{ FieldState.AI_X, FieldState.EMPTY, FieldState.EMPTY },
				{ FieldState.EMPTY, FieldState.EMPTY, FieldState.EMPTY },
				{ FieldState.EMPTY, FieldState.EMPTY, FieldState.EMPTY },
			});

			Tuple<int, int> result = game.FindAnyEmptyField();

			Assert.IsFalse(result.Item1 == 0 && result.Item2 == 0);
		}
		#endregion
	}
}
