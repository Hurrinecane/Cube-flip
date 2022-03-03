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
		private BoxSides winSide;

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
			this.winSide = currentDesiredColorSide;
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
			get { return winSide; }
			set { winSide = value; }
		}

		public void ChangeCurrentColor(FlipDirection receivedTurningSide)
		{
			switch (receivedTurningSide)
			{
				case FlipDirection.left:
					switch (winSide)
					{
						case BoxSides.top:
							winSide = BoxSides.left;
							break;
						case BoxSides.left:
							winSide = BoxSides.bottom;
							break;
						case BoxSides.right:
							winSide = BoxSides.top;
							break;
						case BoxSides.bottom:
							winSide = BoxSides.right;
							break;
						case BoxSides.front:
							winSide = BoxSides.front;
							break;
						case BoxSides.back:
							winSide = BoxSides.back;
							break;
					}
					break;
				case FlipDirection.right:
					switch (winSide)
					{
						case BoxSides.top:
							winSide = BoxSides.right;
							break;
						case BoxSides.left:
							winSide = BoxSides.top;
							break;
						case BoxSides.right:
							winSide = BoxSides.bottom;
							break;
						case BoxSides.bottom:
							winSide = BoxSides.left;
							break;
						case BoxSides.front:
							winSide = BoxSides.front;
							break;
						case BoxSides.back:
							winSide = BoxSides.back;
							break;
					}
					break;
				case FlipDirection.up:
					switch (winSide)
					{
						case BoxSides.top:
							winSide = BoxSides.front;
							break;
						case BoxSides.left:
							winSide = BoxSides.left;
							break;
						case BoxSides.right:
							winSide = BoxSides.right;
							break;
						case BoxSides.bottom:
							winSide = BoxSides.back;
							break;
						case BoxSides.front:
							winSide = BoxSides.bottom;
							break;
						case BoxSides.back:
							winSide = BoxSides.top;
							break;
					}
					break;
				case FlipDirection.down:
					switch (winSide)
					{
						case BoxSides.top:
							winSide = BoxSides.back;
							break;
						case BoxSides.left:
							winSide = BoxSides.left;
							break;
						case BoxSides.right:
							winSide = BoxSides.right;
							break;
						case BoxSides.bottom:
							winSide = BoxSides.front;
							break;
						case BoxSides.front:
							winSide = BoxSides.top;
							break;
						case BoxSides.back:
							winSide = BoxSides.bottom;
							break;
					}
					break;
			}
		}
	}
}