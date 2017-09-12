using System.Collections.Generic;
using C4Interfaces;

namespace C4Mediator
{
	public class Referee
	{
		internal static bool IsWinningMove(Dictionary<Cell, Color> boardState, Cell move)
		{
			return CheckVerticalWin(boardState, move) || CheckHorizontalWin(boardState, move) || CheckDiagonalWin(boardState, move);
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
				for (var x = move.X + 1; x <= Constants.BOARD_WIDTH && numConnected <= 4; x++)
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
				for (int x = move.X + 1, y = move.Y + 1; x <= Constants.BOARD_WIDTH && y <= Constants.BOARD_HEIGHT && numConnected <= 4; x++, y++)
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
					for (int x = move.X + 1, y = move.Y - 1; x <= Constants.BOARD_WIDTH && y >= 1 && numConnected <= 4; x++, y--)
					{
						var tempCell = new Cell { X = x, Y = y };

						if (BoardState.ContainsKey(tempCell) && BoardState[tempCell] == moveColor)
						{
							numConnected++;
						}
						else break;
					}

					//	Check up and to the left
					for (int x = move.X - 1, y = move.Y + 1; x >= 1 && y <= Constants.BOARD_HEIGHT && numConnected <= 4; x--, y++)
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

		internal static bool GameOver(Dictionary<Cell, Color> boardState)
		{
			//	There are WIDTH x HEIGHT cells to be played.
			//	The game proceeds (for now) until all cells are occupied.
			var numCellsPlayed = boardState.Keys.Count;

			return numCellsPlayed >= (Constants.BOARD_WIDTH * Constants.BOARD_HEIGHT);
		}
	}
}
