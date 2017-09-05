using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C4Interfaces;

namespace C4Mediator
{
	public class Mediator
	{
		private IBoard Board { get; set; }
		private List<IPlayer> Players { get; set; }

		/*
			Add players

			Get next player

			Request current player's turn

			Verify the player's move

			Has anyone won the game?
		 */
	}
}
