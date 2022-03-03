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

			private int depth;
			private int heuristicValue;

			public FieldCell(int currentX, int currentY, BoxSides currentDesiredSide)
			{
				X = currentX;
				Y = currentY;
				Side = currentDesiredSide;

				depth = 0;
				heuristicValue = 0;
			}

			public int GetX
			{
				get { return X; }
			}

			public int GetY
			{
				get { return Y; }
			}

			public int Depth
			{
				get { return depth; }
				set { depth = value; }
			}

			public int HeuristicValue
			{
				get { return heuristicValue; }
				set { heuristicValue = value; }
			}

			public int Value
			{
				get { return depth+heuristicValue; }
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
