using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using static Cube_flip.FlipCubeGame;

namespace Cube_flip
{
	partial class UninformedSearch
	{
		private Queue<FieldCell> OW;
		private Stack<FieldCell> OD;
		private Queue<FieldCell> CW;
		private Queue<FieldCell> CD;
		private FieldCell startState;
		private FieldCell finalState;

		private int fieldSize;
		private int[,] field;

		List<int[,]> fieldMapMoves;

		private bool noExit = true;

		public UninformedSearch(int startStatePanelX, int startStatePanelY, BoxSides startStateSide, int finalStatePanelX, int finalStatePanelY, BoxSides finalStateSide, int[,] field, int fieldSize)
		{
			startState = new FieldCell(startStatePanelX, startStatePanelY, startStateSide);
			finalState = new FieldCell(finalStatePanelX, finalStatePanelY, finalStateSide);

			this.fieldSize = fieldSize;

			this.field = new int[this.fieldSize, this.fieldSize];
			this.field = field;
		}

		public void FindingWayWidth()
		{
			noExit = true;

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
					}

				CW.Enqueue(temp);
			}

			MessageBox.Show("К выбранной цели нет пути!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public void FindingWayDepth()
		{
			noExit = true;

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
					if (!CD.Contains(p) && !OD.Contains(p))
					{
						OD.Push(p);
					}
				
				CD.Enqueue(temp);
			}

			MessageBox.Show("К выбранной цели нет пути!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private Queue<FieldCell> Moves(FieldCell currentPosition)
		{
			Queue<FieldCell> way = new Queue<FieldCell>();

			FieldCell temporaryWay;

			int x = currentPosition.GetX;
			int y = currentPosition.GetY;

			if (x != 16)			
				if (field[x + 1, y] == 1 || field[x + 1, y] == 3)
				{
					temporaryWay = new FieldCell(x + 1, y, ChangeCurrentSide(FlipDirection.down, currentPosition.GetSide))
					{
						from = currentPosition
					};
					way.Enqueue(temporaryWay);
				}
			

			if (x != 0)			
				if (field[x - 1, y] == 1 || field[x - 1, y] == 3)
				{
					temporaryWay = new FieldCell(x - 1, y, ChangeCurrentSide(FlipDirection.up, currentPosition.GetSide))
					{
						from = currentPosition
					};
					way.Enqueue(temporaryWay);
				}			

			if (y != 16)			
				if (field[x, y + 1] == 1 || field[x, y + 1] == 3)
				{
					temporaryWay = new FieldCell(x, y + 1, ChangeCurrentSide(FlipDirection.right, currentPosition.GetSide))
					{
						from = currentPosition
					};
					way.Enqueue(temporaryWay);
				}
			
			if (y != 0)			
				if (field[x, y - 1] == 1 || field[x, y - 1] == 3)
				{
					temporaryWay = new FieldCell(x, y - 1, ChangeCurrentSide(FlipDirection.left, currentPosition.GetSide))
					{
						from = currentPosition
					};
					way.Enqueue(temporaryWay);
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

			statistics += "Количество перебранных вариантов (C): " + Convert.ToString(CW.Count) + Environment.NewLine;

			statistics += "Количество путей на рассмотрение (O): " + Convert.ToString(OW.Count) + Environment.NewLine;

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

			statistics += "Количество перебранных вариантов (C): " + Convert.ToString(CD.Count) + Environment.NewLine;

			statistics += "Количество путей на рассмотрение (O): " + Convert.ToString(OD.Count) + Environment.NewLine;

			return statistics;
		}

		private void FindingMovesWayWidth()
		{
			noExit = true;

			int[,] fieldMapMovesСurrent = new int[this.fieldSize, this.fieldSize];
			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
					fieldMapMovesСurrent[i, j] = 0;

			fieldMapMoves = new List<int[,]>();

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
						fieldMapMovesСurrent[p.GetX, p.GetY]++;

						int[,] fieldMapMovesСurrentRecord = new int[this.fieldSize, this.fieldSize];
						for (int i = 0; i < fieldSize; i++)
							for (int j = 0; j < fieldSize; j++)
								fieldMapMovesСurrentRecord[i, j] = fieldMapMovesСurrent[i, j];

						fieldMapMovesСurrentRecord[temp.GetX, temp.GetY] = -1;

						fieldMapMoves.Add(fieldMapMovesСurrentRecord);
					}

				CW.Enqueue(temp);
			}

			MessageBox.Show("К выбранной цели нет пути!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void FindingMovesWayDepth()
		{
			noExit = true;

			int[,] fieldMapMovesСurrent = new int[this.fieldSize, this.fieldSize];
			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
					fieldMapMovesСurrent[i, j] = 0;

			fieldMapMoves = new List<int[,]>();

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
					if (!CD.Contains(p) && !OD.Contains(p))
					{
						OD.Push(p);
						fieldMapMovesСurrent[p.GetX, p.GetY]++;

						int[,] fieldMapMovesСurrentRecord = new int[this.fieldSize, this.fieldSize];
						for (int i = 0; i < fieldSize; i++)
							for (int j = 0; j < fieldSize; j++)
								fieldMapMovesСurrentRecord[i, j] = fieldMapMovesСurrent[i, j];

						fieldMapMovesСurrentRecord[temp.GetX, temp.GetY] = -1;

						fieldMapMoves.Add(fieldMapMovesСurrentRecord);
					}

				CD.Enqueue(temp);
			}

			MessageBox.Show("К выбранной цели нет пути!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public List<int[,]> GetMapMovesWidth()
		{
			FindingMovesWayWidth();
			return fieldMapMoves;
		}

		public List<int[,]> GetMapMovesDepth()
		{
			FindingMovesWayDepth();
			return  fieldMapMoves;
		}

		private BoxSides ChangeCurrentSide(FlipDirection receivedTurningSide, BoxSides currentDesiredColorSide)
		{
			switch (receivedTurningSide)
			{
				case FlipDirection.left:
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
				case FlipDirection.right:
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
				case FlipDirection.up:
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
				case FlipDirection.down:
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