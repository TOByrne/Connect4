using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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

			var winningMove = false;
			while (!GameOver() && !winningMove)
			{
				int playerMove;
				do
				{
					playerMove = currentPlayer.ColumnToPlay(BoardState);

				} while (!IsLegalMove(playerMove));

				var move = ApplyMove(playerMove, currentPlayer.Color);

				DrawBoard();
				winningMove = IsWinningMove(move);

				Thread.Sleep(200);

				currentPlayer = currentPlayer == Players[0] ? Players[1] : Players[0];
			}
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
			//	It's a legal move if there's a cell available within that column.
			int highestPopulatedCellInColumn = 0;

			if (BoardState.Keys.Count > 0)
			{
				highestPopulatedCellInColumn = HighestPopulatedCellInColumn(playerMove);
			}

			return highestPopulatedCellInColumn < BOARD_HEIGHT;
		}

		private bool IsWinningMove(Cell move)
		{
			//	We shouldn't need to check the entire board every time.
			//	Just check to see if this move has connected 4

			bool isWinningMove = false;

			//	Check vertical (if the move is high enough)
			if (!isWinningMove && move.Y >= 4)
			{
				isWinningMove = isWinningMove || CheckVerticalWin(BoardState, move);
			}

			if (!isWinningMove)
			{
				isWinningMove = isWinningMove || CheckHorizontalWin(BoardState, move);
			}

			if (!isWinningMove)
			{
				isWinningMove = isWinningMove || CheckDiagonalWin(BoardState, move);
			}

			return isWinningMove;
		}

		protected static bool CheckVerticalWin(Dictionary<Cell, Color> BoardState, Cell move)
		{
			var numConnected = 1;
			var moveColor = BoardState[move];

			for (var y = move.Y; y > 0 && numConnected <= 4; y--)
			{
				var tempCell = new Cell {X = move.X, Y = y};

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
				for (var x = move.X - 1; x > 0 && numConnected <= 4; x--)
				{
					var tempCell = new Cell {X = x, Y = move.Y};

					if (BoardState.ContainsKey(tempCell) && BoardState[tempCell] == moveColor)
					{
						numConnected++;
					}
					else break;
				}

				//	look to the move's right...
				for (var x = move.X + 1; x < BOARD_WIDTH && numConnected <= 4; x++)
				{
					var tempCell = new Cell {X = x, Y = move.Y};

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
				for (int x = move.X - 1, y = move.Y - 1; x >= 0 && y >= 0 && numConnected <= 4; x--, y--)
				{
					var tempCell = new Cell { X = x, Y = y };

					if (BoardState.ContainsKey(tempCell) && BoardState[tempCell] == moveColor)
					{
						numConnected++;
					}
					else break;
				}

				//	Then up and to the right
				for (int x = move.X + 1, y = move.Y + 1; x < BOARD_WIDTH && y < BOARD_HEIGHT && numConnected <= 4; x++, y++)
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
					for (int x = move.X + 1, y = move.Y - 1; x < BOARD_WIDTH && y >= 0 && numConnected <= 4; x++, y--)
					{
						var tempCell = new Cell {X = x, Y = y};

						if (BoardState.ContainsKey(tempCell) && BoardState[tempCell] == moveColor)
						{
							numConnected++;
						}
						else break;
					}

					//	Check up and to the left
					for (int x = move.X - 1, y = move.Y + 1; x >= 0 && y < BOARD_HEIGHT && numConnected <= 4; x--, y++)
					{
						var tempCell = new Cell {X = x, Y = y};

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
