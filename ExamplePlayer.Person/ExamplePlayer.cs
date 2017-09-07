using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C4Interfaces;

namespace Example.Person
{
	public class ExamplePlayer : Player
	{
		public ExamplePlayer(Color playerColor, int totalTimeInSecs, int machineCores)
			: base(playerColor, totalTimeInSecs, machineCores)
		{
		}

		public override int ColumnToPlay(Dictionary<Cell, Color> board)
		{
			//	Getting the available columns amounts to looking for unset cells
			//	We know the board is a 7wide x 6wide grid...

			var availableColumns = new List<int>();

			for (var x = 0; x < 7; x++)
			{
				int highestPopulatedCellInColumn = 0;

				if (board.Keys.Count > 0)
				{
					var column = board.Keys.Where(c => c.X == x);
					if(column.Count() > 0)
						highestPopulatedCellInColumn = column.Max(c => c.Y);
				}

				if (highestPopulatedCellInColumn < 6)
				{
					availableColumns.Add(x);
				}
			}

			//	Pick one at random...
			var r = new Random(DateTime.Now.Millisecond);
			var availableColumn = r.Next(availableColumns.Count);

			return availableColumns[availableColumn];
		}
	}
}
