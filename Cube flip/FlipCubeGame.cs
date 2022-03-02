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
		private DesiredColorSide currentDesiredColorSide;

		public enum DesiredColorSide
		{
			right,
			left,
			top,
			bottom,
			front,
			behind
		}

		public enum TurningSide
		{
			top,
			bottom,
			left,
			right
		}


		public FlipCubeGame(int startPanelX, int startPanelY, int finishPanelX, int finishPanelY, DesiredColorSide currentDesiredColorSide)
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

		public DesiredColorSide CurrentDesiredColorSide
		{
			get { return currentDesiredColorSide; }
			set { currentDesiredColorSide = value; }
		}

		public void ChangeCurrentColor(TurningSide receivedTurningSide)
		{
			switch (receivedTurningSide)
			{
				case TurningSide.left:
					switch (currentDesiredColorSide)
					{
						case DesiredColorSide.top:
							currentDesiredColorSide = DesiredColorSide.left;
							break;
						case DesiredColorSide.left:
							currentDesiredColorSide = DesiredColorSide.bottom;
							break;
						case DesiredColorSide.right:
							currentDesiredColorSide = DesiredColorSide.top;
							break;
						case DesiredColorSide.bottom:
							currentDesiredColorSide = DesiredColorSide.right;
							break;
						case DesiredColorSide.front:
							currentDesiredColorSide = DesiredColorSide.front;
							break;
						case DesiredColorSide.behind:
							currentDesiredColorSide = DesiredColorSide.behind;
							break;
					}
					break;
				case TurningSide.right:
					switch (currentDesiredColorSide)
					{
						case DesiredColorSide.top:
							currentDesiredColorSide = DesiredColorSide.right;
							break;
						case DesiredColorSide.left:
							currentDesiredColorSide = DesiredColorSide.top;
							break;
						case DesiredColorSide.right:
							currentDesiredColorSide = DesiredColorSide.bottom;
							break;
						case DesiredColorSide.bottom:
							currentDesiredColorSide = DesiredColorSide.left;
							break;
						case DesiredColorSide.front:
							currentDesiredColorSide = DesiredColorSide.front;
							break;
						case DesiredColorSide.behind:
							currentDesiredColorSide = DesiredColorSide.behind;
							break;
					}
					break;
				case TurningSide.top:
					switch (currentDesiredColorSide)
					{
						case DesiredColorSide.top:
							currentDesiredColorSide = DesiredColorSide.front;
							break;
						case DesiredColorSide.left:
							currentDesiredColorSide = DesiredColorSide.left;
							break;
						case DesiredColorSide.right:
							currentDesiredColorSide = DesiredColorSide.right;
							break;
						case DesiredColorSide.bottom:
							currentDesiredColorSide = DesiredColorSide.behind;
							break;
						case DesiredColorSide.front:
							currentDesiredColorSide = DesiredColorSide.bottom;
							break;
						case DesiredColorSide.behind:
							currentDesiredColorSide = DesiredColorSide.top;
							break;
					}
					break;
				case TurningSide.bottom:
					switch (currentDesiredColorSide)
					{
						case DesiredColorSide.top:
							currentDesiredColorSide = DesiredColorSide.behind;
							break;
						case DesiredColorSide.left:
							currentDesiredColorSide = DesiredColorSide.left;
							break;
						case DesiredColorSide.right:
							currentDesiredColorSide = DesiredColorSide.right;
							break;
						case DesiredColorSide.bottom:
							currentDesiredColorSide = DesiredColorSide.front;
							break;
						case DesiredColorSide.front:
							currentDesiredColorSide = DesiredColorSide.top;
							break;
						case DesiredColorSide.behind:
							currentDesiredColorSide = DesiredColorSide.bottom;
							break;
					}
					break;
			}
		}
	}
}