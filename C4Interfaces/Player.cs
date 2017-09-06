using System;

namespace C4Interfaces
{
	public abstract class Player
	{
		public Color PlayerColor { get; private set; }

		public Player(Color playerColor)
		{
			PlayerColor = playerColor;
		}

		public abstract int PlayMove(IBoard currentState, DateTime timeRemaining);
	}
}
