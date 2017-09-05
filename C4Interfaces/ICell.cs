/*
	I'm uncertain how to manage the SquareValue without creating some abstract
	class that'll then need to be inherited, so for now it's just a kinda-global
	enum on the C4Interfaces namespace.
 */
namespace C4Interfaces
{
	public interface ICell
	{
		int X { get; set; }
		int Y { get; set; }

		SquareValue Value { get; set; }
	}

	public enum SquareValue
	{
		Red,
		Yellow,
		Empty
	}
}