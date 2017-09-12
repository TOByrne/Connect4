using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using C4Interfaces;

namespace C4Mediator
{
	public class Mediator
	{
		private Dictionary<Cell, Color> BoardState { get; set; }
		private List<Player> Players { get; set; }
		private Color? Winner { get; set; }
		private string WinMessage { get; set; }

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

			var currentPlayer = Players[0];
			var otherPlayer = Players[1];

			var winningMove = false;
			while (!Referee.GameOver(BoardState) && !winningMove && Winner == null)
			{
				int playerMove;

				var copiedBoard = CopyBoard(BoardState);
				if (Constants.ALLOW_RETRIES)
				{
					do
					{
						playerMove = currentPlayer.ColumnToPlay(copiedBoard);

					} while (!IsLegalMove(playerMove));
				}
				else
				{
					playerMove = currentPlayer.ColumnToPlay(copiedBoard);

					if (!IsLegalMove(playerMove))
					{
						Winner = otherPlayer.Color;
						WinMessage = $"{currentPlayer.Color} made an illegal move. {Winner} wins.";
						ApplyMove(playerMove, currentPlayer.Color);
						DrawBoard();
					}
				}

				if (Winner == null)
				{
					var move = ApplyMove(playerMove, currentPlayer.Color);

					Thread.Sleep(10);
					DrawBoard();
					winningMove = Referee.IsWinningMove(BoardState, move);

					if (winningMove)
					{
						var pX = Console.CursorLeft;
						var pY = Console.CursorTop;
						Console.SetCursorPosition(1, 19);
						WinMessage = $"{BoardState[move]} ({currentPlayer.Name}) wins";
						Console.SetCursorPosition(pX, pY);
					}

					Thread.Sleep(1);

					currentPlayer = currentPlayer == Players[0] ? Players[1] : Players[0];
					otherPlayer = currentPlayer == Players[0] ? Players[1] : Players[0];
				}
			}

			Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(1, 19);

			if (Referee.GameOver(BoardState) && !winningMove)
			{
				WinMessage = "It's a tie!";
			}

			Console.Write(WinMessage);
		}

		private Cell ApplyMove(int playerMove, Color playerColor)
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
			return move;
		}

		private bool IsLegalMove(int playerMove)
		{
			//	The player's move is a column into which they drop their stone.
			//	It's a legal move if there's a cell available within that column
			//	and if that move is an actual column between 1 and 7 (inclusive)

			if (playerMove < 1 || playerMove > Constants.BOARD_WIDTH) return false;

			int highestPopulatedCellInColumn = 0;

			if (BoardState.Keys.Count > 0)
			{
				highestPopulatedCellInColumn = HighestPopulatedCellInColumn(playerMove);
			}

			return highestPopulatedCellInColumn < Constants.BOARD_HEIGHT;
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

			DrawAllCells(BoardState);

			return null;
		}

		protected static void DrawAllCells(Dictionary<Cell, Color> BoardState)
		{
			foreach (var key in BoardState.Keys)
			{
				DrawCell(BoardState, key);
			}
		}

		protected static void DrawCell(Dictionary<Cell, Color> BoardState, Cell key)
		{
			Console.SetCursorPosition(5 + key.X * 3, 17 - key.Y * 2);
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

		private Dictionary<Cell, Color> CopyBoard(Dictionary<Cell, Color> board)
		{
			var copy = new Dictionary<Cell, Color>();

			foreach (var cell in board.Keys)
			{
				copy[cell] = board[cell];
			}

			return copy;
		}
	}
}
