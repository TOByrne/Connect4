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
		private List<Player> Players { get; set; }
	}
}
