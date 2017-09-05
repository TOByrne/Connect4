/*
	Dictionary<ICell, Square.SquareValue State> State { get; set; } is more complicated than
	ICell[,] State { get; set; } since we'd have to track every instance of ICell created.

	Using the dictionary doesn't allow us to reference a square using
		State[new ICell{ X = x, Y = y }]
	as that new ICell is a new reference.
	Assert.AreEqual(Square.SquareValue.Empty, board.State[new Square {X = x, Y = y}]);
		(Fails)

	ICell[x,y] is just WAY easier and doesn't require any extra tracking.
 */

namespace C4Interfaces
{
	public interface IBoard
	{
		int Width { get; }		//	Readonly
		int Height { get; }		//	Readonly
		//Dictionary<ICell, SquareValue> State { get; set; }

		ICell[,] State { get; set; }
	}
}