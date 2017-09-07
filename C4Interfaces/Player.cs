using System.Collections.Generic;

namespace C4Interfaces
{
	public abstract class Player
	{
		public Color Color { get; private set; }
		public abstract string Name { get; }

		/// <summary>
		/// Initialize the player with some information about the game to play
		/// </summary>
		/// <param name="playerColor">The color the player is going to play with.</param>
		/// <param name="totalTimeInSecs">Total game time the player has to play. A player loses if they use more time than this.</param>
		/// <param name="machineCores">How many logical cores the machine running the game has.</param>
		public Player(Color playerColor, int totalTimeInSecs, int machineCores)
		{
			Color = playerColor;
		}

		/// <summary>
		/// Calculate this player's next move.
		/// </summary>
		/// <param name="board">The collection of cells used so far. (1,1) is at the bottom/left. A cell is empty if it's not on the board collection.</param>
		/// <returns>The column the player wants to play in.</returns>
		public abstract int ColumnToPlay(Dictionary<Cell, Color> board);
	}
}
