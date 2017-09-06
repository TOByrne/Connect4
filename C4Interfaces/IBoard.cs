using System.Collections.Generic;

namespace C4Interfaces
{
	public interface IBoard
	{
		int Width { get; }		//	Readonly
		int Height { get; }		//	Readonly

		Dictionary<Cell, SquareValue> State { get; set; }
	}
}