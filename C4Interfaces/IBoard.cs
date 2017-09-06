using System.Collections.Generic;

namespace C4Interfaces
{
	public interface IBoard
	{
		int Width { get; }
		int Height { get; }

		Dictionary<Cell, Color> State { get; set; }
	}
}