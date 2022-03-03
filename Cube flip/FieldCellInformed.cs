using System;
using static Cube_flip.FlipCubeGame;

namespace Cube_flip
{
    partial class InformedSearch
	{
        class FieldCell : IEquatable<FieldCell>
        {
			private int X;
			private int Y;

			private BoxSides Side;
			public FieldCell from = null;

			private int g;
			private int h;

			public FieldCell(int currentX, int currentY, BoxSides currentDesiredSide)
			{
				this.X = currentX;
				this.Y = currentY;
				this.Side = currentDesiredSide;

				g = 0;
				h = 0;
			}

			public int GetX
			{
				get { return X; }
			}

			public int GetY
			{
				get { return Y; }
			}

			public int G
			{
				get { return g; }
				set { g = value; }
			}

			public int H
			{
				get { return h; }
				set { h = value; }
			}

			public int Value
			{
				get { return g+h; }
			}

			public BoxSides GetSide
			{
				get { return Side; }
			}

			public static bool operator ==(FieldCell A, FieldCell B)
			{
				if (A.X == B.X && A.Y == B.Y && A.Side == B.Side)
					return true;

				return false;
			}

			public static bool operator !=(FieldCell A, FieldCell B)
			{
				if (A.X != B.X || A.Y != B.Y || A.Side != B.Side)
					return true;

				return false;
			}

			public static bool operator <(FieldCell A, FieldCell B)
			{
				if (A.Value < B.Value)
					return true;

				return false;
			}

			public static bool operator >(FieldCell A, FieldCell B)
			{
				if (A.Value > B.Value)
					return true;

				return false;
			}

			public bool Equals(FieldCell other)
			{
				if (this.X == other.X && this.Y == other.Y && this.Side == other.Side) return true;
				else return false;
			}
		}
    }
}
