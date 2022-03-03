namespace Cube_flip
{
	class FlipCubeGame
	{
		private int currentPanelX;
		private int currentPanelY;
		private int startPanelX;
		private int startPanelY;
		private int finishPanelX;
		private int finishPanelY;
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


		public FlipCubeGame(int startPanelX, int startPanelY, int finishPanelX, int finishPanelY, BoxSides currentDesiredColorSide)
		{
			this.startPanelX = startPanelX;
			this.startPanelY = startPanelY;
			this.finishPanelX = finishPanelX;
			this.finishPanelY = finishPanelY;
			this.winSide = currentDesiredColorSide;
			currentPanelX = startPanelX;
			currentPanelY = startPanelY;
		}

		public int СurrentPanelX
		{
			get { return currentPanelX; }
			set { currentPanelX = value; }
		}

		public int СurrentPanelY
		{
			get { return currentPanelY; }
			set { currentPanelY = value; }
		}

		public int StartPanelX
		{
			get { return startPanelX; }
			set { startPanelX = value; }
		}

		public int StartPanelY
		{
			get { return startPanelY; }
			set { startPanelY = value; }
		}

		public int FinishPanelX
		{
			get { return finishPanelX; }
			set { finishPanelX = value; }
		}

		public int FinishPanelY
		{
			get { return finishPanelY; }
			set { finishPanelY = value; }
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