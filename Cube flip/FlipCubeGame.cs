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
		private BoxSides currentDesiredColorSide;

		public enum BoxSides
		{
			top,
			bottom,
			left,
			right,
			front,
			back
		}

		public enum TurnDirection
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
			this.currentDesiredColorSide = currentDesiredColorSide;
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
			get { return currentDesiredColorSide; }
			set { currentDesiredColorSide = value; }
		}

		public void ChangeCurrentColor(TurnDirection receivedTurningSide)
		{
			switch (receivedTurningSide)
			{
				case TurnDirection.left:
					switch (currentDesiredColorSide)
					{
						case BoxSides.top:
							currentDesiredColorSide = BoxSides.left;
							break;
						case BoxSides.left:
							currentDesiredColorSide = BoxSides.bottom;
							break;
						case BoxSides.right:
							currentDesiredColorSide = BoxSides.top;
							break;
						case BoxSides.bottom:
							currentDesiredColorSide = BoxSides.right;
							break;
						case BoxSides.front:
							currentDesiredColorSide = BoxSides.front;
							break;
						case BoxSides.back:
							currentDesiredColorSide = BoxSides.back;
							break;
					}
					break;
				case TurnDirection.right:
					switch (currentDesiredColorSide)
					{
						case BoxSides.top:
							currentDesiredColorSide = BoxSides.right;
							break;
						case BoxSides.left:
							currentDesiredColorSide = BoxSides.top;
							break;
						case BoxSides.right:
							currentDesiredColorSide = BoxSides.bottom;
							break;
						case BoxSides.bottom:
							currentDesiredColorSide = BoxSides.left;
							break;
						case BoxSides.front:
							currentDesiredColorSide = BoxSides.front;
							break;
						case BoxSides.back:
							currentDesiredColorSide = BoxSides.back;
							break;
					}
					break;
				case TurnDirection.up:
					switch (currentDesiredColorSide)
					{
						case BoxSides.top:
							currentDesiredColorSide = BoxSides.front;
							break;
						case BoxSides.left:
							currentDesiredColorSide = BoxSides.left;
							break;
						case BoxSides.right:
							currentDesiredColorSide = BoxSides.right;
							break;
						case BoxSides.bottom:
							currentDesiredColorSide = BoxSides.back;
							break;
						case BoxSides.front:
							currentDesiredColorSide = BoxSides.bottom;
							break;
						case BoxSides.back:
							currentDesiredColorSide = BoxSides.top;
							break;
					}
					break;
				case TurnDirection.down:
					switch (currentDesiredColorSide)
					{
						case BoxSides.top:
							currentDesiredColorSide = BoxSides.back;
							break;
						case BoxSides.left:
							currentDesiredColorSide = BoxSides.left;
							break;
						case BoxSides.right:
							currentDesiredColorSide = BoxSides.right;
							break;
						case BoxSides.bottom:
							currentDesiredColorSide = BoxSides.front;
							break;
						case BoxSides.front:
							currentDesiredColorSide = BoxSides.top;
							break;
						case BoxSides.back:
							currentDesiredColorSide = BoxSides.bottom;
							break;
					}
					break;
			}
		}
	}
}