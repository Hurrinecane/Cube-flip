using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using static Cube_flip.FlipCubeGame;

namespace Cube_flip
{
	partial class UninformedSearch
	{
		private Queue<Cell> PathsListW;
		private Stack<Cell> PathsListD;
		private Queue<Cell> ProcessedCellsW;
		private Queue<Cell> ProcessedCellsD;
		private Cell startState;
		private Cell finalState;

		private int fieldSize;
		private int[,] field;

		List<int[,]> fieldMapMoves;

		private bool noExit = true;

		public UninformedSearch(int startStatePanelX, int startStatePanelY, BoxSides startStateSide, int finalStatePanelX, int finalStatePanelY, BoxSides finalStateSide, int[,] field, int fieldSize)
		{
			startState = new Cell(startStatePanelX, startStatePanelY, startStateSide);
			finalState = new Cell(finalStatePanelX, finalStatePanelY, finalStateSide);

			this.fieldSize = fieldSize;

			this.field = new int[this.fieldSize, this.fieldSize];
			this.field = field;
		}

		public void FindingWayWidth()
		{
			noExit = true;

			PathsListW = new Queue<Cell>();
			ProcessedCellsW = new Queue<Cell>();

			PathsListW.Enqueue(startState);

			while (PathsListW.Count > 0)
			{
				Cell temp = PathsListW.Dequeue();

				if (temp == finalState)
				{
					finalState.from = temp.from;
					ProcessedCellsW.Enqueue(temp);
					noExit = false;
					return;
				}

				foreach (Cell p in Moves(temp))
					if (!ProcessedCellsW.Contains(p) && !PathsListW.Contains(p))
						PathsListW.Enqueue(p);


				ProcessedCellsW.Enqueue(temp);
			}

			MessageBox.Show("К выбранной цели нет пути!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public void FindingWayDepth()
		{
			noExit = true;

			PathsListD = new Stack<Cell>();
			ProcessedCellsD = new Queue<Cell>();

			PathsListD.Push(startState);

			while (PathsListD.Count > 0)
			{
				Cell temp = PathsListD.Pop();

				if (temp == finalState)
				{
					finalState.from = temp.from;
					ProcessedCellsD.Enqueue(temp);
					noExit = false;
					return;
				}

				foreach (Cell p in Moves(temp))
					if (!ProcessedCellsD.Contains(p) && !PathsListD.Contains(p))
						PathsListD.Push(p);

				ProcessedCellsD.Enqueue(temp);
			}

			MessageBox.Show("К выбранной цели нет пути!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private Queue<Cell> Moves(Cell currentPosition)
		{
			Queue<Cell> way = new Queue<Cell>();

			Cell temporaryWay;

			int x = currentPosition.GetX;
			int y = currentPosition.GetY;

			if (x != 16)
				if (field[x + 1, y] == 1 || field[x + 1, y] == 3)
				{
					temporaryWay = new Cell(x + 1, y, CalcClrSideFlip(FlipDirection.down, currentPosition.GetSide))
					{
						from = currentPosition
					};
					way.Enqueue(temporaryWay);
				}


			if (x != 0)
				if (field[x - 1, y] == 1 || field[x - 1, y] == 3)
				{
					temporaryWay = new Cell(x - 1, y, CalcClrSideFlip(FlipDirection.up, currentPosition.GetSide))
					{
						from = currentPosition
					};
					way.Enqueue(temporaryWay);
				}

			if (y != 16)
				if (field[x, y + 1] == 1 || field[x, y + 1] == 3)
				{
					temporaryWay = new Cell(x, y + 1, CalcClrSideFlip(FlipDirection.right, currentPosition.GetSide))
					{
						from = currentPosition
					};
					way.Enqueue(temporaryWay);
				}

			if (y != 0)
				if (field[x, y - 1] == 1 || field[x, y - 1] == 3)
				{
					temporaryWay = new Cell(x, y - 1, CalcClrSideFlip(FlipDirection.left, currentPosition.GetSide))
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

			Stack<Cell> way = new Stack<Cell>();
			Cell temp = finalState;

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
			Stack<Cell> way = new Stack<Cell>();
			Queue<int> pathPanel = new Queue<int>();
			Cell temp = finalState;

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
			Stack<Cell> way = new Stack<Cell>();
			Queue<BoxSides> pathPanel = new Queue<BoxSides>();
			Cell temp = finalState;

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

			statistics += "Количество перебранных вариантов (C): " + Convert.ToString(ProcessedCellsW.Count) + Environment.NewLine;

			statistics += "Количество путей на рассмотрение (O): " + Convert.ToString(PathsListW.Count) + Environment.NewLine;

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

			statistics += "Количество перебранных вариантов (C): " + Convert.ToString(ProcessedCellsD.Count) + Environment.NewLine;

			statistics += "Количество путей на рассмотрение (O): " + Convert.ToString(PathsListD.Count) + Environment.NewLine;

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

			PathsListW = new Queue<Cell>();
			ProcessedCellsW = new Queue<Cell>();

			PathsListW.Enqueue(startState);

			while (PathsListW.Count > 0)
			{
				Cell temp = PathsListW.Dequeue();

				if (temp == finalState)
				{
					finalState.from = temp.from;
					ProcessedCellsW.Enqueue(temp);
					noExit = false;
					return;
				}

				foreach (Cell p in Moves(temp))
					if (!ProcessedCellsW.Contains(p) && !PathsListW.Contains(p))
					{
						PathsListW.Enqueue(p);
						fieldMapMovesСurrent[p.GetX, p.GetY]++;

						int[,] fieldMapMovesСurrentRecord = new int[this.fieldSize, this.fieldSize];
						for (int i = 0; i < fieldSize; i++)
							for (int j = 0; j < fieldSize; j++)
								fieldMapMovesСurrentRecord[i, j] = fieldMapMovesСurrent[i, j];

						fieldMapMovesСurrentRecord[temp.GetX, temp.GetY] = -1;

						fieldMapMoves.Add(fieldMapMovesСurrentRecord);
					}

				ProcessedCellsW.Enqueue(temp);
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

			PathsListD = new Stack<Cell>();
			ProcessedCellsD = new Queue<Cell>();

			PathsListD.Push(startState);

			while (PathsListD.Count > 0)
			{
				Cell temp = PathsListD.Pop();

				if (temp == finalState)
				{
					finalState.from = temp.from;
					ProcessedCellsD.Enqueue(temp);
					noExit = false;
					return;
				}

				foreach (Cell p in Moves(temp))
					if (!ProcessedCellsD.Contains(p) && !PathsListD.Contains(p))
					{
						PathsListD.Push(p);
						fieldMapMovesСurrent[p.GetX, p.GetY]++;

						int[,] fieldMapMovesСurrentRecord = new int[this.fieldSize, this.fieldSize];
						for (int i = 0; i < fieldSize; i++)
							for (int j = 0; j < fieldSize; j++)
								fieldMapMovesСurrentRecord[i, j] = fieldMapMovesСurrent[i, j];

						fieldMapMovesСurrentRecord[temp.GetX, temp.GetY] = -1;

						fieldMapMoves.Add(fieldMapMovesСurrentRecord);
					}

				ProcessedCellsD.Enqueue(temp);
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
			return fieldMapMoves;
		}
	}
}