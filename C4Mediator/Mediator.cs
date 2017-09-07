using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using C4Interfaces;

namespace C4Mediator
{
	public class Mediator
	{
		public const int BOARD_HEIGHT = 6;
		public const int BOARD_WIDTH = 7;

		private Dictionary<Cell, Color> BoardState { get; set; } 
		private List<Player> Players { get; set; }

		public Mediator()
		{
			Players = new List<Player>();
			BoardState = new Dictionary<Cell, Color>();
		}

		public void AddPlayer(Player player)
		{
			if (Players == null)
			{
				Players = new List<Player>();
			}

			Players.Add(player);
		}

		public void StartGame()
		{
			if (Players.Count != 2)
			{
				return;
			}

			Player currentPlayer = Players[0];

			while (!GameOver())
			{
				int playerMove;
				do
				{
					playerMove = currentPlayer.ColumnToPlay(BoardState);

				} while (!IsLegalMove(playerMove));

				PlayMove(playerMove, currentPlayer.Color);

				Thread.Sleep(200);
				DrawBoard();

				currentPlayer = currentPlayer == Players[0] ? Players[1] : Players[0];
			}
		}

		private void PlayMove(int playerMove, Color playerColor)
		{
			int highestPopulatedCellInColumn = 0;

			if (BoardState.Keys.Count > 0)
			{
				highestPopulatedCellInColumn = HighestPopulatedCellInColumn(playerMove);
			}

			var move = new Cell
			{
				X = playerMove,
				Y = highestPopulatedCellInColumn + 1
			};

			BoardState[move] = playerColor;
		}

		private bool IsLegalMove(int playerMove)
		{
			//	The player's move is a column into which they drop their stone.
			//	It's a legal move if there's a cell available within that column.
			int highestPopulatedCellInColumn = 0;

			if (BoardState.Keys.Count > 0)
			{
				highestPopulatedCellInColumn = HighestPopulatedCellInColumn(playerMove);
			}

			return highestPopulatedCellInColumn < BOARD_HEIGHT;
		}

		private bool GameOver()
		{
			//	There are WIDTH x HEIGHT cells to be played.
			//	The game proceeds (for now) until all cells are occupied.

			var numCellsPlayed = BoardState.Keys.Count;

			return numCellsPlayed >= (BOARD_WIDTH*BOARD_HEIGHT);
		}

		public string DrawBoard()
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(0, 0);

			var player1Name = Players[0].Name;
			var player2Name = Players[1].Name;

			Console.ForegroundColor = ConsoleColor.Red;
			Console.SetCursorPosition(0, 0);
			Console.Write(player1Name);

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.SetCursorPosition(10 + player1Name.Length, 0);
			Console.Write(player2Name);

			foreach (var key in BoardState.Keys)
			{
				DrawCell(key);
			}

			return null;
		}

		private void DrawCell(Cell key)
		{
			Console.SetCursorPosition(5 + key.X*3, 17 - key.Y*2);
			Console.ForegroundColor = BoardState[key] == Color.Red ? ConsoleColor.Red : ConsoleColor.Yellow;

			Console.Write('\u2588');
		}

		private int HighestPopulatedCellInColumn(int playerMove)
		{
			int highestPopulatedCellInColumn = 0;
			var column = BoardState.Keys.Where(c => c.X == playerMove);
			if (column.Count() > 0)
				highestPopulatedCellInColumn = column.Max(c => c.Y);
			return highestPopulatedCellInColumn;
		}
	}
}
