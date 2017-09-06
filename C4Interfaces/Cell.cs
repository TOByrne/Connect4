using System;

namespace C4Interfaces
{
	public class Cell: IEquatable<Cell>
	{
		public int X { get; set; } // 1-7
		public int Y { get; set; } // 1-6
		public Color Stone { get; set; }

		// Equals & GetHashCode
		public bool Equals(Cell other)
		{
			return this.GetHashCode() == other.GetHashCode();
		}
		public override bool Equals(object obj)
		{
			var other = obj as Cell;
			return other != null && Equals(other);
		}
		public override int GetHashCode()
		{
			return (X * 10) + Y;
		}
	}
}
