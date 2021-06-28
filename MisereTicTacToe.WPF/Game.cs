using System;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MisereTicTacToe.WPF
{
	public enum GameState
	{
		NOT_STARTED,
		PLAYER,
		AI
	}

	public enum FieldState
	{
		EMPTY,
		PLAYER_X,
		AI_X
	}

	public class Game
	{
		public GameState State { get; set; }
		public FieldState[,] Fields { get; set; }

		public Action<int, int, SolidColorBrush> ColorAction { get; set; }
		public Action GameFinishedAction { get; set; }

		public Game(Action<int, int, SolidColorBrush> colorAction = null,
					Action gameFinishedAction = null,
					FieldState[,] fields = null)
		{
			State = GameState.NOT_STARTED;
			Fields = fields ?? new FieldState[3, 3];

			ColorAction = colorAction;
			GameFinishedAction = gameFinishedAction;
		}

		// n - wiersz, m - kolumna
		public bool ExecutePlayerAction(int n, int m)
		{
			switch (State)
			{
				case GameState.NOT_STARTED:
					return false;

				case GameState.PLAYER:
					OccupyField(n, m, FieldState.PLAYER_X);
					return true;

				case GameState.AI:
					return false;

				default:
					return false;
			}
		}

		public bool IsFieldOccupied(int n, int m)
		{
			return Fields[n, m] != FieldState.EMPTY;
		}

		public void SetNewTurn()
		{
			switch (State)
			{
				case GameState.PLAYER:
					State = GameState.AI;
					AI_move();
					break;

				case GameState.AI:
					State = GameState.PLAYER;
					break;

				default:
					throw new ArgumentException("[SetNewTurn] Game state is invalid");
			}
		}

		public bool OccupyField(int n, int m, FieldState newState)
		{
			if (IsFieldOccupied(n, m))
				return false;

			switch (newState)
			{
				case FieldState.PLAYER_X:
					ColorAction.Invoke(n, m, Brushes.Blue);
					break;
				case FieldState.AI_X:
					ColorAction.Invoke(n, m, Brushes.Red);
					break;
				default:
					throw new ArgumentException("[OccupyField] New state of field is invalid");
			}

			Fields[n, m] = newState;

			bool gameHasEnded = CheckIfGameHasEnded();
			if (gameHasEnded)
			{
				GameFinishedAction.Invoke();
			} 
			else
			{
				SetNewTurn();
			}
			
			return true;
		}

		public bool CheckIfGameHasEnded()
		{
			// Sprawdź kolumny
			for (int i = 0; i < 3; i++)
			{
				int yAmount = 0;
				for (int j = 0; j < 3; j++)
				{
					if (Fields[j, i] != FieldState.EMPTY)
						yAmount++;
				}

				if (yAmount == 3)
					return true;
			}

			// Sprawdź wiersze
			for (int i = 0; i < 3; i++)
			{
				int xAmount = 0;
				for (int j = 0; j < 3; j++)
				{
					if (Fields[i, j] != FieldState.EMPTY)
						xAmount++;
				}

				if (xAmount == 3)
					return true;
			}

			// Sprawdź przekątną (z lewej górnej do prawej dolnej)
			int xyAmount = 0;
			for (int i = 0; i < 3; i++)
			{
				if (Fields[i, i] != FieldState.EMPTY)
					xyAmount++;
			}

			if (xyAmount == 3)
				return true;

			// Sprawdź przekątną (z prawej górnej do lewej dolnej)
			int yzAmount = 0;
			for (int i = 0; i < 3; i++)
			{
				if (Fields[i, 2 - i] != FieldState.EMPTY)
					yzAmount++;
			}

			if (yzAmount == 3)
				return true;

			// Nie znaleziono wzoru spełniającego wymagania - gra jeszcze się nie zakończyła
			return false;
		}

		public async void AI_move()
		{
			await Task.Delay(750);
			Tuple<int, int> result = FindAnyEmptyField();
			OccupyField(result.Item1, result.Item2, FieldState.AI_X);
		}

		public Tuple<int, int> FindAnyEmptyField()
		{
			Random rng = new Random();
			Tuple<int, int> pair;
			while (true)
			{
				pair = new Tuple<int, int>(rng.Next(0, 2), rng.Next(0, 2));
				if (!IsFieldOccupied(pair.Item1, pair.Item2))
				{
					return pair;
				}
			}
		}
	}
}
