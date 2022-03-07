namespace Cube_flip
{
	class FlipCubeGame
	{
		private int currentX;
		private int currentY;
		private int startX;
		private int startY;
		private int finishX;
		private int finishY;
		private BoxSides clrSide;

		public enum CellTypes
		{
			wall,
			space,
			box,
			target,
			reset
		}

		public enum BoxSides
		{
			top,
			bottom,
			left,
			right,
			front,
			back
		}

		public enum FlipDirection
		{
			up,
			down,
			left,
			right
		}


		public FlipCubeGame(int startX, int startY, int finishX, int finishY, BoxSides currentDesiredColorSide)
		{
			this.startX = startX;
			this.startY = startY;
			this.finishX = finishX;
			this.finishY = finishY;
			this.clrSide = currentDesiredColorSide;
			currentX = startX;
			currentY = startY;
		}

		public int CurrentX
		{
			get { return currentX; }
			set { currentX = value; }
		}

		public int CurrentY
		{
			get { return currentY; }
			set { currentY = value; }
		}

		public int StartX
		{
			get { return startX; }
			set { startX = value; }
		}

		public int StartY
		{
			get { return startY; }
			set { startY = value; }
		}

		public int FinishX
		{
			get { return finishX; }
			set { finishX = value; }
		}

		public int FinishY
		{
			get { return finishY; }
			set { finishY = value; }
		}

		public BoxSides ColoredSide
		{
			get { return clrSide; }
			set { clrSide = value; }
		}

		public void ChangeCurrentColor(FlipDirection flipDir)
		{
			clrSide = CalcClrSideFlip(flipDir, clrSide);
		}

		public static BoxSides CalcClrSideFlip(FlipDirection flipDir, BoxSides currentClrSide)
		{
			switch (flipDir)
			{
				case FlipDirection.left:
					switch (currentClrSide)
					{
						case BoxSides.top:
							return BoxSides.left;
						case BoxSides.left:
							return BoxSides.bottom;
						case BoxSides.right:
							return BoxSides.top;
						case BoxSides.bottom:
							return BoxSides.right;
						case BoxSides.front:
							return BoxSides.front;
						case BoxSides.back:
							return BoxSides.back;
					}
					break;
				case FlipDirection.right:
					switch (currentClrSide)
					{
						case BoxSides.top:
							return BoxSides.right;
						case BoxSides.left:
							return BoxSides.top;
						case BoxSides.right:
							return BoxSides.bottom;
						case BoxSides.bottom:
							return BoxSides.left;
						case BoxSides.front:
							return BoxSides.front;
						case BoxSides.back:
							return BoxSides.back;
					}
					break;
				case FlipDirection.up:
					switch (currentClrSide)
					{
						case BoxSides.top:
							return BoxSides.front;
						case BoxSides.left:
							return BoxSides.left;
						case BoxSides.right:
							return BoxSides.right;
						case BoxSides.bottom:
							return BoxSides.back;
						case BoxSides.front:
							return BoxSides.bottom;
						case BoxSides.back:
							return BoxSides.top;
					}
					break;
				case FlipDirection.down:
					switch (currentClrSide)
					{
						case BoxSides.top:
							return BoxSides.back;
						case BoxSides.left:
							return BoxSides.left;
						case BoxSides.right:
							return BoxSides.right;
						case BoxSides.bottom:
							return BoxSides.front;
						case BoxSides.front:
							return BoxSides.top;
						case BoxSides.back:
							return BoxSides.bottom;
					}
					break;
			}

			return BoxSides.top;
		}

	}
}