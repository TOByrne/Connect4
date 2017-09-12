using System;
using System.Collections.Generic;
using C4Interfaces;

namespace C4Mediator.Tests
{
	public class TestReferee : Referee
	{
		public static bool Vertical(Dictionary<Cell, Color> state, Cell move)
		{
			return CheckVerticalWin(state, move);
		}

		public static bool Horizontal(Dictionary<Cell, Color> state, Cell move)
		{
			return CheckHorizontalWin(state, move);
		}

		public static bool Diagonal(Dictionary<Cell, Color> state, Cell move)
		{
			return CheckDiagonalWin(state, move);
		}

		public static void Draw(Dictionary<Cell, Color> state)
		{
			for (int y = Constants.BOARD_HEIGHT; y > 0; y--)
			{
				for (int x = 1; x <= Constants.BOARD_WIDTH; x++)
				{
					var cell = new Cell { X = x, Y = y };
					if (state.ContainsKey(cell))
					{
						if (state[cell] == Color.Red)
						{
							Console.Write(" X ");
						}
						else
						{
							Console.Write(" O ");
						}
					}
					else
					{
						Console.Write("   ");
					}
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}
	}
}