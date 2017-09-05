namespace C4Interfaces
{
	public interface IPlayer
	{
		IBoard PlayMove(IBoard currentState, SquareValue player);
	}
}
