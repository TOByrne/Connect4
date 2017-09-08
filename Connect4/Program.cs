using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C4Interfaces;
using C4Mediator;
using Example.Person;

namespace Connect4
{
	class Program
	{
		static void Main(string[] args)
		{


            while(true)
            {
                Console.Clear();
                var mediator = new Mediator();

                var player1 = new ExamplePlayer(Color.Red, 600, 1);
                var player2 = new ExamplePlayer(Color.Yellow, 600, 1);

                mediator.AddPlayer(player1);
                mediator.AddPlayer(player2);
                mediator.StartGame();
                Console.ReadLine();
            }
			
		}
	}
}
