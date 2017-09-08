using System;

namespace C4Interfaces
{
	public struct Cell
	{
		public int X; // 1-7
		public int Y; // 1-6
        public override string ToString()
        {
            return "(" + X + "," + Y + ")";
        }
    }
}
