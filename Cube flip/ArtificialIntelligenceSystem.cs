using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Cube_flip
{
	partial class AISystem
	{
		private Queue<FieldCell> OW;
		private Stack<FieldCell> OD;
		private Queue<FieldCell> CW;
		private Queue<FieldCell> CD;
		private FieldCell startState;
		private FieldCell finalState;

		private int fieldSize;
		private int[,] field;
		private int[,] fieldMapMoves;

		private bool noExit = true;

		public enum BoxSides
		{
			left,
			right,
			top,
			bottom,
			front,
			back
		}

		public enum TurningSide
		{
			up,
			down,
			left,
			right
		}

		public AISystem(int startStatePanelX, int startStatePanelY, BoxSides startStateSide, int finalStatePanelX, int finalStatePanelY, BoxSides finalStateSide, int[,] field, int fieldSize)
		{
			startState = new FieldCell(startStatePanelX, startStatePanelY, startStateSide);
			finalState = new FieldCell(finalStatePanelX, finalStatePanelY, finalStateSide);

			this.fieldSize = fieldSize;

			this.field = new int[this.fieldSize, this.fieldSize];
			this.fieldMapMoves = new int[this.fieldSize, this.fieldSize];

			for (int i = 0; i < this.fieldSize; i++)
				for (int j = 0; j < this.fieldSize; j++)
				{
					this.field[i, j] = field[i, j];
					fieldMapMoves[i, j] = 0;
				}
		}


		public void FindingWayWidth()
		{
			noExit = true;

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
					fieldMapMoves[i, j] = 0;

			OW = new Queue<FieldCell>();
			CW = new Queue<FieldCell>();

			OW.Enqueue(startState);

			while (OW.Count > 0)
			{
				FieldCell temp = OW.Dequeue();

				if (temp == finalState)
				{
					finalState.from = temp.from;
					CW.Enqueue(temp);
					noExit = false;
					return;
				}

				foreach (FieldCell p in Moves(temp))
					if (!CW.Contains(p) && !OW.Contains(p))
					{
						OW.Enqueue(p);
						fieldMapMoves[p.GetX, p.GetY]++;
					}

				CW.Enqueue(temp);
			}

			MessageBox.Show("К выбранной цели нет пути!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public void FindingWayDepth()
		{
			noExit = true;

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
					fieldMapMoves[i, j] = 0;

			OD = new Stack<FieldCell>();
			CD = new Queue<FieldCell>();

			OD.Push(startState);

			while (OD.Count > 0)
			{
				FieldCell temp = OD.Pop();

				if (temp == finalState)
				{
					finalState.from = temp.from;
					CD.Enqueue(temp);
					noExit = false;
					return;
				}

				foreach (FieldCell p in Moves(temp))
				{
					if (!CD.Contains(p) && !OD.Contains(p))
					{
						OD.Push(p);
						fieldMapMoves[p.GetX, p.GetY]++;
					}
				}

				CD.Enqueue(temp);
			}

			MessageBox.Show("К выбранной цели нет пути!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private Queue<FieldCell> Moves(FieldCell currentPosition)
		{
			Queue<FieldCell> way = new Queue<FieldCell>();

			FieldCell temporaryWay1;
			FieldCell temporaryWay2;
			FieldCell temporaryWay3;
			FieldCell temporaryWay4;

			int x = currentPosition.GetX;
			int y = currentPosition.GetY;

			if (x != 16)
			{
				if (field[x + 1, y] == 1 || field[x + 1, y] == 3)
				{
					temporaryWay1 = new FieldCell(x + 1, y, ChangeCurrentSide(TurningSide.down, currentPosition.GetSide))
					{
						from = currentPosition
					};
					way.Enqueue(temporaryWay1);
				}
			}

			if (x != 0)
			{
				if (field[x - 1, y] == 1 || field[x - 1, y] == 3)
				{
					temporaryWay2 = new FieldCell(x - 1, y, ChangeCurrentSide(TurningSide.up, currentPosition.GetSide))
					{
						from = currentPosition
					};
					way.Enqueue(temporaryWay2);
				}
			}

			if (y != 16)
			{
				if (field[x, y + 1] == 1 || field[x, y + 1] == 3)
				{
					temporaryWay3 = new FieldCell(x, y + 1, ChangeCurrentSide(TurningSide.right, currentPosition.GetSide))
					{
						from = currentPosition
					};
					way.Enqueue(temporaryWay3);
				}
			}

			if (y != 0)
			{
				if (field[x, y - 1] == 1 || field[x, y - 1] == 3)
				{
					temporaryWay4 = new FieldCell(x, y - 1, ChangeCurrentSide(TurningSide.left, currentPosition.GetSide))
					{
						from = currentPosition
					};
					way.Enqueue(temporaryWay4);
				}
			}

			return way;
		}

		public string PathOutput()
		{
			if (noExit)
				return "К выбранной цели нет пути!";

			Stack<FieldCell> way = new Stack<FieldCell>();
			FieldCell temp = finalState;

			while (temp != startState)
			{
				way.Push(temp);
				temp = temp.from;
			}
			way.Push(temp);

			string receivedPath = "";
			int counter = 0;
			while (way.Count > 0)
			{
				temp = way.Pop();
				receivedPath += "Ход " + Convert.ToString(counter) + ". Позиция: " + Convert.ToString(temp.GetX) + "," + Convert.ToString(temp.GetY) + Environment.NewLine + "Красная сторона куба: " + Convert.ToString(temp.GetSide) + Environment.NewLine + Environment.NewLine;
				counter++;
			}

			return receivedPath;
		}

		public Queue<int> GetWayPanel()
		{
			Stack<FieldCell> way = new Stack<FieldCell>();
			Queue<int> pathPanel = new Queue<int>();
			FieldCell temp = finalState;

			if (noExit)
				return pathPanel;

			while (temp != startState)
			{
				way.Push(temp);
				temp = temp.from;
			}
			way.Push(temp);

			while (way.Count > 0)
			{
				temp = way.Pop();
				pathPanel.Enqueue(temp.GetX);
				pathPanel.Enqueue(temp.GetY);
			}

			return pathPanel;
		}

		public Queue<BoxSides> GetWayColorSide()
		{
			Stack<FieldCell> way = new Stack<FieldCell>();
			Queue<BoxSides> pathPanel = new Queue<BoxSides>();
			FieldCell temp = finalState;

			if (noExit)
				return pathPanel;

			while (temp != startState)
			{
				way.Push(temp);
				temp = temp.from;
			}
			way.Push(temp);

			while (way.Count > 0)
			{
				temp = way.Pop();
				pathPanel.Enqueue(temp.GetSide);
			}

			return pathPanel;
		}

		public string GetStatisticsWidth()
		{
			string statistics = "";

			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();
			FindingWayWidth();
			stopWatch.Stop();
			statistics += "Время работы алгоритма: " + Convert.ToString(stopWatch.Elapsed) + Environment.NewLine;

			Queue<int> pathPanel = GetWayPanel();
			statistics += "Количество ходов: " + Convert.ToString((pathPanel.Count - 2) / 2) + Environment.NewLine;

			statistics += "Количество перебранных вариантов: " + Convert.ToString(CW.Count) + Environment.NewLine;

			return statistics;
		}

		public string GetStatisticsDepth()
		{
			string statistics = "";

			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();
			FindingWayDepth();
			stopWatch.Stop();
			statistics += "Время работы алгоритма: " + Convert.ToString(stopWatch.Elapsed) + Environment.NewLine;

			Queue<int> pathPanel = GetWayPanel();
			statistics += "Количество ходов: " + Convert.ToString((pathPanel.Count - 2) / 2) + Environment.NewLine;

			statistics += "Количество перебранных вариантов: " + Convert.ToString(CD.Count) + Environment.NewLine;

			return statistics;
		}

		public int[,] GetMapMovesWidth()
		{
			FindingWayWidth();
			return fieldMapMoves;
		}

		public int[,] GetMapMovesDepth()
		{
			FindingWayDepth();
			return fieldMapMoves;
		}

		private BoxSides ChangeCurrentSide(TurningSide receivedTurningSide, BoxSides currentDesiredColorSide)
		{
			switch (receivedTurningSide)
			{
				case TurningSide.left:
					switch (currentDesiredColorSide)
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
				case TurningSide.right:
					switch (currentDesiredColorSide)
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
				case TurningSide.up:
					switch (currentDesiredColorSide)
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
				case TurningSide.down:
					switch (currentDesiredColorSide)
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