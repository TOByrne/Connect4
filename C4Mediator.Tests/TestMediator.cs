using System;
using System.Collections.Generic;
using C4Interfaces;

namespace C4Mediator.Tests
{
	public class TestMediator : Mediator
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
			for (int y = BOARD_HEIGHT - 1; y >= 0; y--)
			{
				for (int x = 0; x < BOARD_WIDTH; x++)
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