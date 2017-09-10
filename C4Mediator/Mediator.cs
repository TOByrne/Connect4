using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using C4Interfaces;

namespace C4Mediator
{
	public class Mediator
	{
		const bool ALLOW_RETRIES = false;

		public const int BOARD_HEIGHT = 6;
		public const int BOARD_WIDTH = 7;

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
			while (!GameOver() && !winningMove && Winner == null)
			{
				int playerMove;

				var copiedBoard = CopyBoard(BoardState);
				if (ALLOW_RETRIES)
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
					winningMove = IsWinningMove(move);

					if (winningMove)
					{
						var pX = Console.CursorLeft;
						var pY = Console.CursorTop;
						Console.SetCursorPosition(1, 19);
						WinMessage = Enum.GetName(typeof (Color), BoardState[move]) + " wins";
						Console.SetCursorPosition(pX, pY);
					}

					Thread.Sleep(1);

					currentPlayer = currentPlayer == Players[0] ? Players[1] : Players[0];
					otherPlayer = currentPlayer == Players[0] ? Players[1] : Players[0];
				}
			}

			Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(1, 19);

			if (GameOver() && !winningMove)
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

			if (playerMove < 1 || playerMove > BOARD_WIDTH) return false;

			int highestPopulatedCellInColumn = 0;

			if (BoardState.Keys.Count > 0)
			{
				highestPopulatedCellInColumn = HighestPopulatedCellInColumn(playerMove);
			}

			return highestPopulatedCellInColumn < BOARD_HEIGHT;
		}

		private bool IsWinningMove(Cell move)
		{
			return CheckVerticalWin(BoardState, move) || CheckHorizontalWin(BoardState, move) || CheckDiagonalWin(BoardState, move);
		}

		protected static bool CheckVerticalWin(Dictionary<Cell, Color> BoardState, Cell move)
		{
			if (move.Y < 4) return false;
			var numConnected = 1;
			var moveColor = BoardState[move];

			for (var y = move.Y; y >= 1 && numConnected <= 4; y--)
			{
				var tempCell = new Cell { X = move.X, Y = y };

				if (BoardState[tempCell] == moveColor)
				{
					numConnected++;
				}
				else
				{
					return false;
				}
			}

			return true;
		}

		protected static bool CheckHorizontalWin(Dictionary<Cell, Color> BoardState, Cell move)
		{
			var numConnected = 1;
			var moveColor = BoardState[move];

			//	If enough moves have been played
			if (BoardState.Keys.Count > 6)
			{
				//	look to the move's left...
				for (var x = move.X - 1; x >= 1 && numConnected <= 4; x--)
				{
					var tempCell = new Cell { X = x, Y = move.Y };

					if (BoardState.ContainsKey(tempCell) && BoardState[tempCell] == moveColor)
					{
						numConnected++;
					}
					else break;
				}

				//	look to the move's right...
				for (var x = move.X + 1; x <= BOARD_WIDTH && numConnected <= 4; x++)
				{
					var tempCell = new Cell { X = x, Y = move.Y };

					if (BoardState.ContainsKey(tempCell) && BoardState[tempCell] == moveColor)
					{
						numConnected++;
					}
					else break;
				}
			}
			return numConnected >= 4;
		}

		protected static bool CheckDiagonalWin(Dictionary<Cell, Color> BoardState, Cell move)
		{
			var numConnected = 1;
			var moveColor = BoardState[move];


			//	If enough moves have been played
			if (BoardState.Keys.Count > 9)
			{
				//	Check down and to the left
				for (int x = move.X - 1, y = move.Y - 1; x >= 1 && y >= 1 && numConnected <= 4; x--, y--)
				{
					var tempCell = new Cell { X = x, Y = y };

					if (BoardState.ContainsKey(tempCell) && BoardState[tempCell] == moveColor)
					{
						numConnected++;
					}
					else break;
				}

				//	Then up and to the right
				for (int x = move.X + 1, y = move.Y + 1; x <= BOARD_WIDTH && y <= BOARD_HEIGHT && numConnected <= 4; x++, y++)
				{
					var tempCell = new Cell { X = x, Y = y };

					if (BoardState.ContainsKey(tempCell) && BoardState[tempCell] == moveColor)
					{
						numConnected++;
					}
					else break;
				}

				//	If those don't have a win,
				if (numConnected < 4)
				{
					//	We're checking in a different direction, so reset the connected counter
					numConnected = 1;

					//	Check down and to the right
					for (int x = move.X + 1, y = move.Y - 1; x <= BOARD_WIDTH && y >= 1 && numConnected <= 4; x++, y--)
					{
						var tempCell = new Cell { X = x, Y = y };

						if (BoardState.ContainsKey(tempCell) && BoardState[tempCell] == moveColor)
						{
							numConnected++;
						}
						else break;
					}

					//	Check up and to the left
					for (int x = move.X - 1, y = move.Y + 1; x >= 1 && y <= BOARD_HEIGHT && numConnected <= 4; x--, y++)
					{
						var tempCell = new Cell { X = x, Y = y };

						if (BoardState.ContainsKey(tempCell) && BoardState[tempCell] == moveColor)
						{
							numConnected++;
						}
						else break;
					}
				}
			}

			return numConnected >= 4;
		}

		private bool GameOver()
		{
			//	There are WIDTH x HEIGHT cells to be played.
			//	The game proceeds (for now) until all cells are occupied.

			var numCellsPlayed = BoardState.Keys.Count;

			if (numCellsPlayed >= (BOARD_WIDTH * BOARD_HEIGHT)) return true;
			for (var x = 1; x <= 7; x++)
			{
				for (var y = 1; y <= 6; y++)
				{
					var cell = new Cell { X = x, Y = y };


				}
			}
			return false;
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
