using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MisereTicTacToe.WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		// Obiekt przechowujący aktualny stan gry
		public Game GameSession { get; set; }
		public TextBlock[,] TextBlocks { get; set; }

		public MainWindow()
		{
			InitializeComponent();

			this.TextBlocks = new TextBlock[3, 3] {
				{ cell_0_0, cell_0_1, cell_0_2},
				{ cell_1_0, cell_1_1, cell_1_2},
				{ cell_2_0, cell_2_1, cell_2_2},
			};

			InitializeGame();
		}

		public void InitializeGame()
		{
			this.GameSession = new Game((n, m, color) =>
			{
				TextBlocks[n, m].Text = "X";
				TextBlocks[n, m].Foreground = color;
			}, () =>
			{
				string winner = (GameSession.State == GameState.PLAYER) ? "AI" : "gracz";
				MessageBox.Show("Gra została zakończona, " + winner + " jest zwycięzcą");
				ResetGame();
			});
		}

		public void ResetGame()
		{
			InitializeGame();
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					TextBlocks[i, j].Text = "";
				}
			}
			GameSession.State = GameState.PLAYER;
		}

		private void Cell_0_0_MouseDown(object sender, MouseButtonEventArgs e)
		{
			GameSession.ExecutePlayerAction(0, 0);
		}

		private void Cell_0_1_MouseDown(object sender, MouseButtonEventArgs e)
		{
			GameSession.ExecutePlayerAction(0, 1);
		}

		private void Cell_0_2_MouseDown(object sender, MouseButtonEventArgs e)
		{
			GameSession.ExecutePlayerAction(0, 2);
		}

		private void Cell_1_0_MouseDown(object sender, MouseButtonEventArgs e)
		{
			GameSession.ExecutePlayerAction(1, 0);
		}

		private void Cell_1_1_MouseDown(object sender, MouseButtonEventArgs e)
		{
			GameSession.ExecutePlayerAction(1, 1);
		}

		private void Cell_1_2_MouseDown(object sender, MouseButtonEventArgs e)
		{
			GameSession.ExecutePlayerAction(1, 2);
		}

		private void Cell_2_0_MouseDown(object sender, MouseButtonEventArgs e)
		{
			GameSession.ExecutePlayerAction(2, 0);
		}

		private void Cell_2_1_MouseDown(object sender, MouseButtonEventArgs e)
		{
			GameSession.ExecutePlayerAction(2, 1);
		}

		private void Cell_2_2_MouseDown(object sender, MouseButtonEventArgs e)
		{
			GameSession.ExecutePlayerAction(2, 2);
		}

		private void Restart_Button_Click(object sender, RoutedEventArgs e)
		{
			ResetGame();
		}

		private void Start_Button_Click(object sender, RoutedEventArgs e)
		{
			GameSession.State = GameState.PLAYER;
		}
	}
}
