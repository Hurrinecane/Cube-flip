using System;
using static Cube_flip.FlipCubeGame;

namespace Cube_flip
{
	partial class UninformedSearch
	{
		class FieldCell : IEquatable<FieldCell>
		{
			private int X;
			private int Y;
			private BoxSides Side;
			public FieldCell from = null;

			public FieldCell(int currentPanelX, int currentPanelY, BoxSides currentDesiredSide)
			{
				this.X = currentPanelX;
				this.Y = currentPanelY;
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

			public bool Equals(FieldCell other)
			{
				if (this.X == other.X && this.Y == other.Y && this.Side == other.Side) return true;
				else return false;
			}
		}
	}
}