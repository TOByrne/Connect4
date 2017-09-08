using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C4Interfaces;
using NUnit.Framework;

namespace C4Mediator.Tests
{
	[TestFixture]
	public class WinningConditionTests
	{
		public Cell Cell(int x, int y)
		{
			return new Cell { X = x, Y = y };
		}

		[Test]
		public void DiagonalWins()
		{
			Diagonal1();
			Diagonal2();
			Diagonal3();
			Diagonal4();

			Diagonal5();
			Diagonal6();
			Diagonal7();
			Diagonal8();
		}

		[Test]
		public void HorizontalWins()
		{
			var boardState = new Dictionary<Cell, Color>();

			boardState[Cell(1, 1)] = Color.Red;
			boardState[Cell(2, 1)] = Color.Red;
			boardState[Cell(3, 1)] = Color.Red;
			boardState[Cell(4, 1)] = Color.Red;

			boardState[Cell(1, 2)] = Color.Yellow;
			boardState[Cell(2, 2)] = Color.Yellow;
			boardState[Cell(3, 2)] = Color.Yellow;

			//	Sending different Cells for it to test against forces the verification to assume
			//	that particular last move.  So rather than just checking from one end to the other,
			//	it takes the move played and looks to the left and then to the right (if necessary)
			TestMediator.Horizontal(boardState, Cell(1, 1));
			TestMediator.Horizontal(boardState, Cell(2, 1));
			TestMediator.Horizontal(boardState, Cell(3, 1));
			TestMediator.Horizontal(boardState, Cell(4, 1));
		}

		[Test]
		public void VerticalWins()
		{
			var boardState = new Dictionary<Cell, Color>();

			boardState[Cell(1, 1)] = Color.Red;
			boardState[Cell(1, 2)] = Color.Red;
			boardState[Cell(1, 3)] = Color.Red;
			boardState[Cell(1, 4)] = Color.Red;

			boardState[Cell(2, 1)] = Color.Yellow;
			boardState[Cell(2, 2)] = Color.Yellow;
			boardState[Cell(2, 3)] = Color.Yellow;

			TestMediator.Horizontal(boardState, Cell(1, 1));
			TestMediator.Horizontal(boardState, Cell(1, 2));
			TestMediator.Horizontal(boardState, Cell(1, 3));
			TestMediator.Horizontal(boardState, Cell(1, 4));
		}

		private void Diagonal1()
		{
			var boardState = new Dictionary<Cell, Color>();

			boardState.Add(Cell(1, 1), Color.Yellow);
			boardState.Add(Cell(2, 1), Color.Yellow);
			boardState.Add(Cell(3, 1), Color.Yellow);
			boardState.Add(Cell(1, 2), Color.Yellow);
			boardState.Add(Cell(2, 2), Color.Yellow);
			boardState.Add(Cell(1, 3), Color.Yellow);

			boardState.Add(Cell(1, 4), Color.Red);
			boardState.Add(Cell(2, 3), Color.Red);
			boardState.Add(Cell(3, 2), Color.Red);
			boardState.Add(Cell(4, 1), Color.Red);

			TestMediator.Draw(boardState);

			Assert.True(TestMediator.Diagonal(boardState, Cell(1, 4)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(2, 3)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(3, 2)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(4, 1)));
		}

		private void Diagonal2()
		{
			var boardState = new Dictionary<Cell, Color>();

			boardState.Add(Cell(5, 1), Color.Yellow);
			boardState.Add(Cell(6, 1), Color.Yellow);
			boardState.Add(Cell(6, 2), Color.Yellow);
			boardState.Add(Cell(7, 1), Color.Yellow);
			boardState.Add(Cell(7, 2), Color.Yellow);
			boardState.Add(Cell(7, 3), Color.Yellow);

			boardState.Add(Cell(7, 4), Color.Red);
			boardState.Add(Cell(6, 3), Color.Red);
			boardState.Add(Cell(5, 2), Color.Red);
			boardState.Add(Cell(4, 1), Color.Red);

			TestMediator.Draw(boardState);

			Assert.True(TestMediator.Diagonal(boardState, Cell(7, 4)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(6, 3)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(5, 2)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(4, 1)));
		}

		private void Diagonal3()
		{
			var boardState = new Dictionary<Cell, Color>();

			boardState.Add(Cell(1, 1), Color.Yellow);
			boardState.Add(Cell(2, 1), Color.Yellow);
			boardState.Add(Cell(3, 1), Color.Yellow);
			boardState.Add(Cell(1, 2), Color.Yellow);
			boardState.Add(Cell(2, 2), Color.Yellow);
			boardState.Add(Cell(1, 3), Color.Yellow);

			boardState.Add(Cell(4, 4), Color.Red);
			boardState.Add(Cell(5, 3), Color.Red);
			boardState.Add(Cell(6, 2), Color.Red);
			boardState.Add(Cell(7, 1), Color.Red);

			TestMediator.Draw(boardState);

			Assert.True(TestMediator.Diagonal(boardState, Cell(4, 4)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(5, 3)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(6, 2)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(7, 1)));
		}

		private void Diagonal4()
		{
			var boardState = new Dictionary<Cell, Color>();

			boardState.Add(Cell(5, 1), Color.Yellow);
			boardState.Add(Cell(6, 1), Color.Yellow);
			boardState.Add(Cell(6, 2), Color.Yellow);
			boardState.Add(Cell(7, 1), Color.Yellow);
			boardState.Add(Cell(7, 2), Color.Yellow);
			boardState.Add(Cell(7, 3), Color.Yellow);

			boardState.Add(Cell(1, 1), Color.Red);
			boardState.Add(Cell(2, 2), Color.Red);
			boardState.Add(Cell(3, 3), Color.Red);
			boardState.Add(Cell(4, 4), Color.Red);

			TestMediator.Draw(boardState);

			Assert.True(TestMediator.Diagonal(boardState, Cell(1, 1)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(2, 2)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(3, 3)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(4, 4)));
		}

		private void Diagonal5()
		{
			var boardState = new Dictionary<Cell, Color>();

			boardState.Add(Cell(1, 1), Color.Yellow);
			boardState.Add(Cell(2, 1), Color.Yellow);
			boardState.Add(Cell(3, 1), Color.Yellow);
			boardState.Add(Cell(1, 2), Color.Yellow);
			boardState.Add(Cell(2, 2), Color.Yellow);
			boardState.Add(Cell(1, 3), Color.Yellow);

			boardState.Add(Cell(1, 6), Color.Red);
			boardState.Add(Cell(2, 5), Color.Red);
			boardState.Add(Cell(3, 4), Color.Red);
			boardState.Add(Cell(4, 3), Color.Red);

			TestMediator.Draw(boardState);

			Assert.True(TestMediator.Diagonal(boardState, Cell(1, 6)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(2, 5)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(3, 4)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(4, 3)));
		}

		private void Diagonal6()
		{
			var boardState = new Dictionary<Cell, Color>();

			boardState.Add(Cell(5, 1), Color.Yellow);
			boardState.Add(Cell(6, 1), Color.Yellow);
			boardState.Add(Cell(6, 2), Color.Yellow);
			boardState.Add(Cell(7, 1), Color.Yellow);
			boardState.Add(Cell(7, 2), Color.Yellow);
			boardState.Add(Cell(7, 3), Color.Yellow);

			boardState.Add(Cell(7, 6), Color.Red);
			boardState.Add(Cell(6, 5), Color.Red);
			boardState.Add(Cell(5, 4), Color.Red);
			boardState.Add(Cell(4, 3), Color.Red);

			TestMediator.Draw(boardState);

			Assert.True(TestMediator.Diagonal(boardState, Cell(7, 6)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(6, 5)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(5, 4)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(4, 3)));
		}

		private void Diagonal7()
		{
			var boardState = new Dictionary<Cell, Color>();

			boardState.Add(Cell(1, 1), Color.Yellow);
			boardState.Add(Cell(2, 1), Color.Yellow);
			boardState.Add(Cell(3, 1), Color.Yellow);
			boardState.Add(Cell(1, 2), Color.Yellow);
			boardState.Add(Cell(2, 2), Color.Yellow);
			boardState.Add(Cell(1, 3), Color.Yellow);

			boardState.Add(Cell(4, 6), Color.Red);
			boardState.Add(Cell(5, 5), Color.Red);
			boardState.Add(Cell(6, 4), Color.Red);
			boardState.Add(Cell(7, 3), Color.Red);

			TestMediator.Draw(boardState);

			Assert.True(TestMediator.Diagonal(boardState, Cell(4, 6)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(5, 5)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(6, 4)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(7, 3)));
		}

		private void Diagonal8()
		{
			var boardState = new Dictionary<Cell, Color>();

			boardState.Add(Cell(5, 1), Color.Yellow);
			boardState.Add(Cell(6, 1), Color.Yellow);
			boardState.Add(Cell(6, 2), Color.Yellow);
			boardState.Add(Cell(7, 1), Color.Yellow);
			boardState.Add(Cell(7, 2), Color.Yellow);
			boardState.Add(Cell(7, 3), Color.Yellow);

			boardState.Add(Cell(1, 3), Color.Red);
			boardState.Add(Cell(2, 4), Color.Red);
			boardState.Add(Cell(3, 5), Color.Red);
			boardState.Add(Cell(4, 6), Color.Red);

			TestMediator.Draw(boardState);

			Assert.True(TestMediator.Diagonal(boardState, Cell(1, 3)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(2, 4)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(3, 5)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(4, 6)));
		}
	}
}