using System;
using static Cube_flip.FlipCubeGame;

namespace Cube_flip
{
	internal class Cell : IEquatable<Cell>
		{
			private int X;
			private int Y;

			private BoxSides Side;
			public Cell from = null;

			public Cell(int currentX, int currentY, BoxSides currentDesiredSide)
			{
				this.X = currentX;
				this.Y = currentY;
				this.Side = currentDesiredSide;
			}

			public int GetX
			{
				get { return X; }
			}

			public int GetY
			{
				get { return Y; }
			}

			public BoxSides GetSide
			{
				get { return Side; }
			}

			public static bool operator ==(Cell A, Cell B)
			{
				if (A.X == B.X && A.Y == B.Y && A.Side == B.Side)
					return true;

				return false;
			}

			public static bool operator !=(Cell A, Cell B)
			{
				if (A.X != B.X || A.Y != B.Y || A.Side != B.Side)
					return true;

				return false;
			}

			public bool Equals(Cell other)
			{
				if (this.X == other.X && this.Y == other.Y && this.Side == other.Side) return true;
				else return false;
			}
		}
	}