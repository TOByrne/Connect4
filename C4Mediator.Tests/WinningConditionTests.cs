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

			boardState[Cell(0, 0)] = Color.Red;
			boardState[Cell(1, 0)] = Color.Red;
			boardState[Cell(2, 0)] = Color.Red;
			boardState[Cell(3, 0)] = Color.Red;

			boardState[Cell(0, 1)] = Color.Yellow;
			boardState[Cell(1, 1)] = Color.Yellow;
			boardState[Cell(2, 1)] = Color.Yellow;

			//	Sending different Cells for it to test against forces the verification to assume
			//	that particular last move.  So rather than just checking from one end to the other,
			//	it takes the move played and looks to the left and then to the right (if necessary)
			TestMediator.Horizontal(boardState, Cell(0, 0));
			TestMediator.Horizontal(boardState, Cell(1, 0));
			TestMediator.Horizontal(boardState, Cell(2, 0));
			TestMediator.Horizontal(boardState, Cell(3, 0));
		}

		[Test]
		public void VerticalWins()
		{
			var boardState = new Dictionary<Cell, Color>();

			boardState[Cell(0, 0)] = Color.Red;
			boardState[Cell(0, 1)] = Color.Red;
			boardState[Cell(0, 2)] = Color.Red;
			boardState[Cell(0, 3)] = Color.Red;

			boardState[Cell(1, 0)] = Color.Yellow;
			boardState[Cell(1, 1)] = Color.Yellow;
			boardState[Cell(1, 2)] = Color.Yellow;

			TestMediator.Horizontal(boardState, Cell(0, 0));
			TestMediator.Horizontal(boardState, Cell(0, 1));
			TestMediator.Horizontal(boardState, Cell(0, 2));
			TestMediator.Horizontal(boardState, Cell(0, 3));
		}

		private void Diagonal1()
		{
			var boardState = new Dictionary<Cell, Color>();

			boardState.Add(Cell(0, 0), Color.Yellow);
			boardState.Add(Cell(1, 0), Color.Yellow);
			boardState.Add(Cell(2, 0), Color.Yellow);
			boardState.Add(Cell(0, 1), Color.Yellow);
			boardState.Add(Cell(1, 1), Color.Yellow);
			boardState.Add(Cell(0, 2), Color.Yellow);

			boardState.Add(Cell(0, 3), Color.Red);
			boardState.Add(Cell(1, 2), Color.Red);
			boardState.Add(Cell(2, 1), Color.Red);
			boardState.Add(Cell(3, 0), Color.Red);

			TestMediator.Draw(boardState);

			Assert.True(TestMediator.Diagonal(boardState, Cell(0, 3)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(1, 2)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(2, 1)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(3, 0)));
		}

		private void Diagonal2()
		{
			var boardState = new Dictionary<Cell, Color>();

			boardState.Add(Cell(4, 0), Color.Yellow);
			boardState.Add(Cell(5, 0), Color.Yellow);
			boardState.Add(Cell(5, 1), Color.Yellow);
			boardState.Add(Cell(6, 0), Color.Yellow);
			boardState.Add(Cell(6, 1), Color.Yellow);
			boardState.Add(Cell(6, 2), Color.Yellow);

			boardState.Add(Cell(6, 3), Color.Red);
			boardState.Add(Cell(5, 2), Color.Red);
			boardState.Add(Cell(4, 1), Color.Red);
			boardState.Add(Cell(3, 0), Color.Red);

			TestMediator.Draw(boardState);

			Assert.True(TestMediator.Diagonal(boardState, Cell(6, 3)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(5, 2)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(4, 1)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(3, 0)));
		}

		private void Diagonal3()
		{
			var boardState = new Dictionary<Cell, Color>();

			boardState.Add(Cell(0, 0), Color.Yellow);
			boardState.Add(Cell(1, 0), Color.Yellow);
			boardState.Add(Cell(2, 0), Color.Yellow);
			boardState.Add(Cell(0, 1), Color.Yellow);
			boardState.Add(Cell(1, 1), Color.Yellow);
			boardState.Add(Cell(0, 2), Color.Yellow);

			boardState.Add(Cell(3, 3), Color.Red);
			boardState.Add(Cell(4, 2), Color.Red);
			boardState.Add(Cell(5, 1), Color.Red);
			boardState.Add(Cell(6, 0), Color.Red);

			TestMediator.Draw(boardState);

			Assert.True(TestMediator.Diagonal(boardState, Cell(3, 3)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(4, 2)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(5, 1)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(6, 0)));
		}

		private void Diagonal4()
		{
			var boardState = new Dictionary<Cell, Color>();

			boardState.Add(Cell(4, 0), Color.Yellow);
			boardState.Add(Cell(5, 0), Color.Yellow);
			boardState.Add(Cell(5, 1), Color.Yellow);
			boardState.Add(Cell(6, 0), Color.Yellow);
			boardState.Add(Cell(6, 1), Color.Yellow);
			boardState.Add(Cell(6, 2), Color.Yellow);

			boardState.Add(Cell(0, 0), Color.Red);
			boardState.Add(Cell(1, 1), Color.Red);
			boardState.Add(Cell(2, 2), Color.Red);
			boardState.Add(Cell(3, 3), Color.Red);

			TestMediator.Draw(boardState);

			Assert.True(TestMediator.Diagonal(boardState, Cell(0, 0)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(1, 1)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(2, 2)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(3, 3)));
		}

		private void Diagonal5()
		{
			var boardState = new Dictionary<Cell, Color>();

			boardState.Add(Cell(0, 0), Color.Yellow);
			boardState.Add(Cell(1, 0), Color.Yellow);
			boardState.Add(Cell(2, 0), Color.Yellow);
			boardState.Add(Cell(0, 1), Color.Yellow);
			boardState.Add(Cell(1, 1), Color.Yellow);
			boardState.Add(Cell(0, 2), Color.Yellow);

			boardState.Add(Cell(0, 5), Color.Red);
			boardState.Add(Cell(1, 4), Color.Red);
			boardState.Add(Cell(2, 3), Color.Red);
			boardState.Add(Cell(3, 2), Color.Red);

			TestMediator.Draw(boardState);

			Assert.True(TestMediator.Diagonal(boardState, Cell(0, 5)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(1, 4)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(2, 3)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(3, 2)));
		}

		private void Diagonal6()
		{
			var boardState = new Dictionary<Cell, Color>();

			boardState.Add(Cell(4, 0), Color.Yellow);
			boardState.Add(Cell(5, 0), Color.Yellow);
			boardState.Add(Cell(5, 1), Color.Yellow);
			boardState.Add(Cell(6, 0), Color.Yellow);
			boardState.Add(Cell(6, 1), Color.Yellow);
			boardState.Add(Cell(6, 2), Color.Yellow);

			boardState.Add(Cell(6, 5), Color.Red);
			boardState.Add(Cell(5, 4), Color.Red);
			boardState.Add(Cell(4, 3), Color.Red);
			boardState.Add(Cell(3, 2), Color.Red);

			TestMediator.Draw(boardState);

			Assert.True(TestMediator.Diagonal(boardState, Cell(6, 5)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(5, 4)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(4, 3)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(3, 2)));
		}

		private void Diagonal7()
		{
			var boardState = new Dictionary<Cell, Color>();

			boardState.Add(Cell(0, 0), Color.Yellow);
			boardState.Add(Cell(1, 0), Color.Yellow);
			boardState.Add(Cell(2, 0), Color.Yellow);
			boardState.Add(Cell(0, 1), Color.Yellow);
			boardState.Add(Cell(1, 1), Color.Yellow);
			boardState.Add(Cell(0, 2), Color.Yellow);

			boardState.Add(Cell(3, 5), Color.Red);
			boardState.Add(Cell(4, 4), Color.Red);
			boardState.Add(Cell(5, 3), Color.Red);
			boardState.Add(Cell(6, 2), Color.Red);

			TestMediator.Draw(boardState);

			Assert.True(TestMediator.Diagonal(boardState, Cell(3, 5)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(4, 4)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(5, 3)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(6, 2)));
		}

		private void Diagonal8()
		{
			var boardState = new Dictionary<Cell, Color>();

			boardState.Add(Cell(4, 0), Color.Yellow);
			boardState.Add(Cell(5, 0), Color.Yellow);
			boardState.Add(Cell(5, 1), Color.Yellow);
			boardState.Add(Cell(6, 0), Color.Yellow);
			boardState.Add(Cell(6, 1), Color.Yellow);
			boardState.Add(Cell(6, 2), Color.Yellow);

			boardState.Add(Cell(0, 2), Color.Red);
			boardState.Add(Cell(1, 3), Color.Red);
			boardState.Add(Cell(2, 4), Color.Red);
			boardState.Add(Cell(3, 5), Color.Red);

			TestMediator.Draw(boardState);

			Assert.True(TestMediator.Diagonal(boardState, Cell(0, 2)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(1, 3)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(2, 4)));
			Assert.True(TestMediator.Diagonal(boardState, Cell(3, 5)));
		}
	}
}