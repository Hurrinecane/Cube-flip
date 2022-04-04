using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using static Cube_flip.FlipCubeGame;

namespace Cube_flip
{
	partial class InformedSearch
	{
		private List<CellInf> PathsListA1;
		private List<CellInf> ProcessedCellsA1;
		private List<CellInf> O2;
		private List<CellInf> C2;
		private CellInf startState;
		private CellInf finalState;

		List<int[,,]> listMovesInformation;
		List<int[,]> fieldMapMoves;

		private int fieldSize;
		private int[,] field;
		private int[] branchingFactor;
		private int maxDepth;

		private bool noExit = true;

		public InformedSearch(int startStatePanelX, int startStatePanelY, BoxSides startStateSide, int finalStatePanelX, int finalStatePanelY, BoxSides finalStateSide, int[,] field, int fieldSize)
		{
			startState = new CellInf(startStatePanelX, startStatePanelY, startStateSide)
			{
				HeuristicValue = 0,
				Depth = 0
			};
			finalState = new CellInf(finalStatePanelX, finalStatePanelY, finalStateSide)
			{
				HeuristicValue = 0,
				Depth = 0
			};

			this.fieldSize = fieldSize;

			this.field = new int[this.fieldSize, this.fieldSize];
			this.field = field;
			branchingFactor = new int[10000];
		}

		#region Algorithm1

		public void FindWayA1() //Игнорируем стены и стороны куба
		{   //рассчитать эффективный коэффициент ветвления для поиска с эвристикой и поиска в ширину
			noExit = true;

			PathsListA1 = new List<CellInf>();
			ProcessedCellsA1 = new List<CellInf>();

			PathsListA1.Add(startState);

			while (PathsListA1.Count > 0)
			{
				CellInf temp = GetMinimumValue(PathsListA1);
				PathsListA1.Remove(temp);

				if (temp == finalState)
				{
					finalState.from = temp.from;
					ProcessedCellsA1.Add(temp);
					noExit = false;
					return;
				}

				foreach (CellInf p in CalcMovesA1(temp))
					if (!PathsListA1.Contains(p) && !ProcessedCellsA1.Contains(p))          //Если видем клетку в первый раз
						PathsListA1.Add(p);                                                 //Добавляем ее в пути
					else if (PathsListA1.Contains(p))                                       //Если она есть в путях
					{
						int index = PathsListA1.IndexOf(p);
						CellInf similar = PathsListA1[index];

						if (p.Value < similar.Value)                                        //Проверяем, оптимальный ли до нее путь
						{
							PathsListA1.Remove(similar);                                    //Если да, заменяем
							PathsListA1.Add(p);
						}
					}
					else if (ProcessedCellsA1.Contains(p))                                  //Если ее нет в путях, но она есть среди обработанных ячеек
					{
						int index = ProcessedCellsA1.IndexOf(p);
						CellInf similar = ProcessedCellsA1[index];

						if (p.Value < similar.Value)                                        //Проверяем, оптимальный ли до нее путь
						{
							ProcessedCellsA1.Remove(similar);                               //Если да, убираем ее из обработанных ячеек
							PathsListA1.Add(p);                                             //И переносим в пути
						}
					}

				ProcessedCellsA1.Add(temp);                                                 //Добавляем клетку в обработанные ячейки

				//if (ProcessedCellsA1.Count < 100)                                           //Если список вершин не переполнен
				ProcessedCellsA1.Add(temp);                                             //Добавляем клетку в обработанные ячейки
																						//else
																						//{
																						//	ProcessedCellsA1.Remove(FindMaxValue(ProcessedCellsA1, x => x.Value));  // Иначе находим ячейку с наихудшим значением, удаляем ее
																						//	ProcessedCellsA1.Add(temp);                                             //И добавляем клетку в обработанные ячейки
																						//}
			}

			MessageBox.Show("К выбранной цели нет пути!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private List<CellInf> CalcMovesA1(CellInf currentPosition)
		{
			List<CellInf> way = new List<CellInf>();

			CellInf temporaryWay;

			int x = currentPosition.GetX;
			int y = currentPosition.GetY;
			int depth = currentPosition.Depth;

			if (x != 16)                                                                                                        //can go down
				if (field[x + 1, y] == (int)CellTypes.space || field[x + 1, y] == (int)CellTypes.target)
				{
					temporaryWay = new CellInf(x + 1, y, CalcClrSideFlip(FlipDirection.down, currentPosition.GetSide))
					{
						from = currentPosition
					};
					int h = GetHeuristicValueAlgorithm1(temporaryWay.GetX, temporaryWay.GetY, finalState.GetX, finalState.GetY);
					temporaryWay.HeuristicValue = h;
					temporaryWay.Depth = depth + 1;

					maxDepth = temporaryWay.Depth > maxDepth ? temporaryWay.Depth : maxDepth;
					branchingFactor[temporaryWay.Depth]++;

					way.Add(temporaryWay);
				}


			if (x != 0)                                                                                                         //can go up
				if (field[x - 1, y] == (int)CellTypes.space || field[x - 1, y] == (int)CellTypes.target)
				{
					temporaryWay = new CellInf(x - 1, y, CalcClrSideFlip(FlipDirection.up, currentPosition.GetSide))
					{
						from = currentPosition
					};
					int h = GetHeuristicValueAlgorithm1(temporaryWay.GetX, temporaryWay.GetY, finalState.GetX, finalState.GetY);
					temporaryWay.HeuristicValue = h;
					temporaryWay.Depth = depth + 1;

					maxDepth = temporaryWay.Depth > maxDepth ? temporaryWay.Depth : maxDepth;
					branchingFactor[temporaryWay.Depth]++;

					way.Add(temporaryWay);
				}

			if (y != 16)                                                                                                        //can go right
				if (field[x, y + 1] == (int)CellTypes.space || field[x, y + 1] == (int)CellTypes.target)
				{
					temporaryWay = new CellInf(x, y + 1, CalcClrSideFlip(FlipDirection.right, currentPosition.GetSide))
					{
						from = currentPosition
					};
					int h = GetHeuristicValueAlgorithm1(temporaryWay.GetX, temporaryWay.GetY, finalState.GetX, finalState.GetY);
					temporaryWay.HeuristicValue = h;
					temporaryWay.Depth = depth + 1;

					maxDepth = temporaryWay.Depth > maxDepth ? temporaryWay.Depth : maxDepth;
					branchingFactor[temporaryWay.Depth]++;

					way.Add(temporaryWay);
				}

			if (y != 0)                                                                                                         //can go left
				if (field[x, y - 1] == (int)CellTypes.space || field[x, y - 1] == (int)CellTypes.target)
				{
					temporaryWay = new CellInf(x, y - 1, CalcClrSideFlip(FlipDirection.left, currentPosition.GetSide))
					{
						from = currentPosition
					};
					int h = GetHeuristicValueAlgorithm1(temporaryWay.GetX, temporaryWay.GetY, finalState.GetX, finalState.GetY);
					temporaryWay.HeuristicValue = h;
					temporaryWay.Depth = depth + 1;

					maxDepth = temporaryWay.Depth > maxDepth ? temporaryWay.Depth : maxDepth;
					branchingFactor[temporaryWay.Depth]++;

					way.Add(temporaryWay);
				}

			return way;
		}

		public List<int[,,]> GetMapMovesInformationAlgorithm1()
		{
			FindingMovesWayAlgorithm1();
			return listMovesInformation;
		}

		public List<int[,]> GetMapMovesAlgorithm1()
		{
			FindingMovesWayAlgorithm1();
			return fieldMapMoves;
		}

		private void FindingMovesWayAlgorithm1()
		{
			noExit = true;

			int[,,] fieldMapMovesСurrentInformation = new int[this.fieldSize, this.fieldSize, 3];
			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
					for (int k = 0; k < 3; k++)
						fieldMapMovesСurrentInformation[i, j, k] = 0;

			int[,] fieldMapMovesСurrent = new int[this.fieldSize, this.fieldSize];
			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
					fieldMapMovesСurrent[i, j] = 0;

			listMovesInformation = new List<int[,,]>();
			fieldMapMoves = new List<int[,]>();

			PathsListA1 = new List<CellInf>();
			ProcessedCellsA1 = new List<CellInf>();

			PathsListA1.Add(startState);

			while (PathsListA1.Count > 0)
			{
				CellInf temp = GetMinimumValue(PathsListA1);
				PathsListA1.Remove(temp);

				if (temp == finalState)
				{
					finalState.from = temp.from;
					ProcessedCellsA1.Add(temp);
					noExit = false;
					return;
				}

				foreach (CellInf p in CalcMovesA1(temp))
					if (!PathsListA1.Contains(p) && !ProcessedCellsA1.Contains(p))
					{
						PathsListA1.Add(p);
						fieldMapMovesСurrent[p.GetX, p.GetY]++;
						fieldMapMovesСurrentInformation[p.GetX, p.GetY, 0] = p.Depth;
						fieldMapMovesСurrentInformation[p.GetX, p.GetY, 1] = p.HeuristicValue;
						fieldMapMovesСurrentInformation[p.GetX, p.GetY, 2] = p.Value;

						int[,,] fieldMapMovesСurrentInformationRecord = new int[this.fieldSize, this.fieldSize, 3];
						for (int i = 0; i < fieldSize; i++)
							for (int j = 0; j < fieldSize; j++)
								for (int k = 0; k < 3; k++)
									fieldMapMovesСurrentInformationRecord[i, j, k] = fieldMapMovesСurrentInformation[i, j, k];

						int[,] fieldMapMovesСurrentRecord = new int[this.fieldSize, this.fieldSize];
						for (int i = 0; i < fieldSize; i++)
							for (int j = 0; j < fieldSize; j++)
								fieldMapMovesСurrentRecord[i, j] = fieldMapMovesСurrent[i, j];

						fieldMapMovesСurrentRecord[temp.GetX, temp.GetY] = -1;

						listMovesInformation.Add(fieldMapMovesСurrentInformationRecord);
						fieldMapMoves.Add(fieldMapMovesСurrentRecord);
					}
					else if (PathsListA1.Contains(p))
					{
						int index = PathsListA1.IndexOf(p);

						CellInf similar = PathsListA1[index];

						if (p.Value < similar.Value)
						{
							PathsListA1.Remove(similar);
							PathsListA1.Add(p);

							fieldMapMovesСurrent[p.GetX, p.GetY]++;
							fieldMapMovesСurrentInformation[p.GetX, p.GetY, 0] = p.Depth;
							fieldMapMovesСurrentInformation[p.GetX, p.GetY, 1] = p.HeuristicValue;
							fieldMapMovesСurrentInformation[p.GetX, p.GetY, 2] = p.Value;

							int[,,] fieldMapMovesСurrentInformationRecord = new int[this.fieldSize, this.fieldSize, 3];
							for (int i = 0; i < fieldSize; i++)
								for (int j = 0; j < fieldSize; j++)
									for (int k = 0; k < 3; k++)
										fieldMapMovesСurrentInformationRecord[i, j, k] = fieldMapMovesСurrentInformation[i, j, k];

							int[,] fieldMapMovesСurrentRecord = new int[this.fieldSize, this.fieldSize];
							for (int i = 0; i < fieldSize; i++)
								for (int j = 0; j < fieldSize; j++)
									fieldMapMovesСurrentRecord[i, j] = fieldMapMovesСurrent[i, j];

							fieldMapMovesСurrentRecord[temp.GetX, temp.GetY] = -1;

							listMovesInformation.Add(fieldMapMovesСurrentInformationRecord);
							fieldMapMoves.Add(fieldMapMovesСurrentRecord);
						}
					}
					else if (ProcessedCellsA1.Contains(p))
					{
						int index = ProcessedCellsA1.IndexOf(p);

						CellInf similar = ProcessedCellsA1[index];

						if (p.Value < similar.Value)
						{
							ProcessedCellsA1.Remove(similar);
							PathsListA1.Add(p);

							fieldMapMovesСurrent[p.GetX, p.GetY]++;
							fieldMapMovesСurrentInformation[p.GetX, p.GetY, 0] = p.Depth;
							fieldMapMovesСurrentInformation[p.GetX, p.GetY, 1] = p.HeuristicValue;
							fieldMapMovesСurrentInformation[p.GetX, p.GetY, 2] = p.Value;

							int[,,] fieldMapMovesСurrentInformationRecord = new int[this.fieldSize, this.fieldSize, 3];
							for (int i = 0; i < fieldSize; i++)
								for (int j = 0; j < fieldSize; j++)
									for (int k = 0; k < 3; k++)
										fieldMapMovesСurrentInformationRecord[i, j, k] = fieldMapMovesСurrentInformation[i, j, k];

							int[,] fieldMapMovesСurrentRecord = new int[this.fieldSize, this.fieldSize];
							for (int i = 0; i < fieldSize; i++)
								for (int j = 0; j < fieldSize; j++)
									fieldMapMovesСurrentRecord[i, j] = fieldMapMovesСurrent[i, j];

							fieldMapMovesСurrentRecord[temp.GetX, temp.GetY] = -1;

							listMovesInformation.Add(fieldMapMovesСurrentInformationRecord);
							fieldMapMoves.Add(fieldMapMovesСurrentRecord);
						}
					}

				ProcessedCellsA1.Add(temp);
			}

			MessageBox.Show("К выбранной цели нет пути!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public string GetStatisticsAlgorithm1()
		{
			string statistics = "";
			maxDepth = 0;

			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();
			FindWayA1();
			stopWatch.Stop();
			statistics += "Время работы алгоритма: " + Convert.ToString(stopWatch.Elapsed) + Environment.NewLine;

			Queue<int> pathPanel = GetWayPanel();
			statistics += "Количество ходов: " + Convert.ToString((pathPanel.Count - 2) / 2) + "	Максимальная глубина: " + Convert.ToString(maxDepth) + Environment.NewLine;

			statistics += "Количество перебранных вариантов (C): " + Convert.ToString(ProcessedCellsA1.Count) + Environment.NewLine;

			statistics += "Количество путей на рассмотрение (O): " + Convert.ToString(PathsListA1.Count) + Environment.NewLine;

			return statistics;
		}


		#endregion


		#region Algorithm2

		private int GetHeuristicValueA2(int startX, int startY, int finalX, int finalY, BoxSides currentStateSide)
		{
			int requiredTurns = 0;

			switch (currentStateSide)
			{
				case BoxSides.top:
					if ((Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 0) || (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 1))
						requiredTurns = 5;
					else if ((Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 0) || (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 2))
						requiredTurns = 2;
					else if ((Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 0) || (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 3))
						requiredTurns = 5;
					else if ((Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 0) || (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 4))
						requiredTurns = 6;
					else if ((Math.Abs(startX - finalX) == 5 && Math.Abs(startY - finalY) == 0) || (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 5))
						requiredTurns = 7;
					else if (Math.Abs(startY - finalY) == 1 && Math.Abs(startX - finalX) == 1)
						requiredTurns = 6;
					else if (Math.Abs(startY - finalY) == 2 && Math.Abs(startX - finalX) == 2)
						requiredTurns = 4;
					else if (Math.Abs(startY - finalY) == 3 && Math.Abs(startX - finalX) == 3)
						requiredTurns = 8;
					else if (Math.Abs(startY - finalY) == 4 && Math.Abs(startX - finalX) == 4)
						requiredTurns = 8;
					else if (Math.Abs(startY - finalY) == 2 && Math.Abs(startX - finalX) == 1)
						requiredTurns = 3;
					else if (Math.Abs(startY - finalY) == 3 && Math.Abs(startX - finalX) == 1)
						requiredTurns = 6;
					else if (Math.Abs(startY - finalY) == 4 && Math.Abs(startX - finalX) == 1)
						requiredTurns = 7;
					else if (Math.Abs(startY - finalY) == 5 && Math.Abs(startX - finalX) == 1)
						requiredTurns = 8;
					else if (Math.Abs(startY - finalY) == 1 && Math.Abs(startX - finalX) == 2)
						requiredTurns = 3;
					else if (Math.Abs(startY - finalY) == 1 && Math.Abs(startX - finalX) == 3)
						requiredTurns = 6;
					else if (Math.Abs(startY - finalY) == 1 && Math.Abs(startX - finalX) == 4)
						requiredTurns = 7;
					else if (Math.Abs(startY - finalY) == 1 && Math.Abs(startX - finalX) == 5)
						requiredTurns = 8;
					else if (Math.Abs(startY - finalY) == 2 && Math.Abs(startX - finalX) == 3)
						requiredTurns = 5;
					else if (Math.Abs(startY - finalY) == 3 && Math.Abs(startX - finalX) == 2)
						requiredTurns = 5;
					else
						requiredTurns = Math.Abs(startX - finalX) + Math.Abs(startY - finalY);
					break;
				case BoxSides.bottom:
					if ((Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 0) || (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 1))
						requiredTurns = 3;
					else if ((Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 0) || (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 2))
						requiredTurns = 4;
					else if ((Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 0) || (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 3))
						requiredTurns = 5;
					else if ((Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 0) || (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 4))
						requiredTurns = 4;
					else if ((Math.Abs(startX - finalX) == 5 && Math.Abs(startY - finalY) == 0) || (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 5))
						requiredTurns = 7;
					else if (Math.Abs(startY - finalY) == 1 && Math.Abs(startX - finalX) == 1)
						requiredTurns = 6;
					else if (Math.Abs(startY - finalY) == 2 && Math.Abs(startX - finalX) == 2)
						requiredTurns = 4;
					else if (Math.Abs(startY - finalY) == 3 && Math.Abs(startX - finalX) == 3)
						requiredTurns = 6;
					else if (Math.Abs(startY - finalY) == 4 && Math.Abs(startX - finalX) == 4)
						requiredTurns = 8;
					else if (Math.Abs(startY - finalY) == 2 && Math.Abs(startX - finalX) == 1)
						requiredTurns = 5;
					else if (Math.Abs(startY - finalY) == 3 && Math.Abs(startX - finalX) == 1)
						requiredTurns = 6;
					else if (Math.Abs(startY - finalY) == 4 && Math.Abs(startX - finalX) == 1)
						requiredTurns = 5;
					else if (Math.Abs(startY - finalY) == 5 && Math.Abs(startX - finalX) == 1)
						requiredTurns = 8;
					else if (Math.Abs(startY - finalY) == 1 && Math.Abs(startX - finalX) == 2)
						requiredTurns = 5;
					else if (Math.Abs(startY - finalY) == 1 && Math.Abs(startX - finalX) == 3)
						requiredTurns = 6;
					else if (Math.Abs(startY - finalY) == 1 && Math.Abs(startX - finalX) == 4)
						requiredTurns = 5;
					else if (Math.Abs(startY - finalY) == 1 && Math.Abs(startX - finalX) == 5)
						requiredTurns = 8;
					else if (Math.Abs(startY - finalY) == 2 && Math.Abs(startX - finalX) == 3)
						requiredTurns = 5;
					else if (Math.Abs(startY - finalY) == 3 && Math.Abs(startX - finalX) == 2)
						requiredTurns = 5;
					else
						requiredTurns = Math.Abs(startX - finalX) + Math.Abs(startY - finalY);
					break;
				case BoxSides.left:
					if (startY > finalY)
					{
						if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 1;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 5)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 2;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 8;
						else if (Math.Abs(startX - finalX) == 5 && Math.Abs(startY - finalY) == 5)
							requiredTurns = 10;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 3;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 5 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 7;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 6;
						else
							requiredTurns = Math.Abs(startX - finalX) + Math.Abs(startY - finalY);
					}
					else if (startY < finalY)
					{
						if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 3;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 5)
							requiredTurns = 7;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 8;
						else if (Math.Abs(startX - finalX) == 5 && Math.Abs(startY - finalY) == 5)
							requiredTurns = 10;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 7;
						else if (Math.Abs(startX - finalX) == 5 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 8;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 6;
						else
							requiredTurns = Math.Abs(startX - finalX) + Math.Abs(startY - finalY);
					}
					else if (startY == finalY)
					{
						if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 5 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 7;
						else
							requiredTurns = Math.Abs(startX - finalX) + Math.Abs(startY - finalY);
					}
					else
						requiredTurns = Math.Abs(startX - finalX) + Math.Abs(startY - finalY);
					break;
				case BoxSides.right:
					if (startY > finalY)
					{
						if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 3;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 5)
							requiredTurns = 7;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 8;
						else if (Math.Abs(startX - finalX) == 5 && Math.Abs(startY - finalY) == 5)
							requiredTurns = 10;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 3;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 5 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 6;
						else
							requiredTurns = Math.Abs(startX - finalX) + Math.Abs(startY - finalY);
					}
					else if (startY < finalY)
					{
						if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 1;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 5)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 2;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 8;
						else if (Math.Abs(startX - finalX) == 5 && Math.Abs(startY - finalY) == 5)
							requiredTurns = 10;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 3;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 5 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 7;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 6;
						else
							requiredTurns = Math.Abs(startX - finalX) + Math.Abs(startY - finalY);
					}
					else if (startY == finalY)
					{
						if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 5 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 7;
						else
							requiredTurns = Math.Abs(startX - finalX) + Math.Abs(startY - finalY);
					}
					else
						requiredTurns = Math.Abs(startX - finalX) + Math.Abs(startY - finalY);
					break;
				case BoxSides.front:
					if (startX > finalX)
					{
						if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 1;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 5 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 2;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 8;
						else if (Math.Abs(startX - finalX) == 5 && Math.Abs(startY - finalY) == 5)
							requiredTurns = 10;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 3;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 5)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 7;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 6;
						else
							requiredTurns = Math.Abs(startX - finalX) + Math.Abs(startY - finalY);
					}
					else if (startX < finalX)
					{
						if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 3;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 5 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 7;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 8;
						else if (Math.Abs(startX - finalX) == 5 && Math.Abs(startY - finalY) == 5)
							requiredTurns = 10;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 3;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 5)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 6;
						else
							requiredTurns = Math.Abs(startX - finalX) + Math.Abs(startY - finalY);
					}
					else if (startX == finalX)
					{
						if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 5)
							requiredTurns = 7;
						else
							requiredTurns = Math.Abs(startX - finalX) + Math.Abs(startY - finalY);
					}
					else
						requiredTurns = Math.Abs(startX - finalX) + Math.Abs(startY - finalY);
					break;
				case BoxSides.back:
					if (startX > finalX)
					{
						if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 3;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 5 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 7;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 8;
						else if (Math.Abs(startX - finalX) == 5 && Math.Abs(startY - finalY) == 5)
							requiredTurns = 10;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 3;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 5)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 6;
						else
							requiredTurns = Math.Abs(startX - finalX) + Math.Abs(startY - finalY);
					}
					else if (startX < finalX)
					{
						if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 1;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 5 && Math.Abs(startY - finalY) == 0)
							requiredTurns = 7;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 2;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 8;
						else if (Math.Abs(startX - finalX) == 5 && Math.Abs(startY - finalY) == 5)
							requiredTurns = 10;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 3;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 1 && Math.Abs(startY - finalY) == 5)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 7;
						else if (Math.Abs(startX - finalX) == 2 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 3 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 4 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 6;
						else
							requiredTurns = Math.Abs(startX - finalX) + Math.Abs(startY - finalY);
					}
					else if (startX == finalX)
					{
						if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 1)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 2)
							requiredTurns = 4;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 3)
							requiredTurns = 5;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 4)
							requiredTurns = 6;
						else if (Math.Abs(startX - finalX) == 0 && Math.Abs(startY - finalY) == 5)
							requiredTurns = 7;
						else
							requiredTurns = Math.Abs(startX - finalX) + Math.Abs(startY - finalY);
					}
					else
						requiredTurns = Math.Abs(startX - finalX) + Math.Abs(startY - finalY);
					break;
			}

			return requiredTurns;
		}

		private List<CellInf> CalcMovesA2(CellInf currentPosition)
		{
			List<CellInf> way = new List<CellInf>();

			CellInf temporaryWay;

			int x = currentPosition.GetX;
			int y = currentPosition.GetY;

			if (x != 16)
				if (field[x + 1, y] == 1 || field[x + 1, y] == 3)
				{
					temporaryWay = new CellInf(x + 1, y, CalcClrSideFlip(FlipDirection.down, currentPosition.GetSide))
					{
						from = currentPosition
					};
					int h = GetHeuristicValueA2(temporaryWay.GetX, temporaryWay.GetY, finalState.GetX, finalState.GetY, temporaryWay.GetSide);
					temporaryWay.HeuristicValue = h;
					temporaryWay.Depth = currentPosition.Depth + 1;
					maxDepth = temporaryWay.Depth > maxDepth ? temporaryWay.Depth : maxDepth;

					way.Add(temporaryWay);
				}


			if (x != 0)
				if (field[x - 1, y] == 1 || field[x - 1, y] == 3)
				{
					temporaryWay = new CellInf(x - 1, y, CalcClrSideFlip(FlipDirection.up, currentPosition.GetSide))
					{
						from = currentPosition
					};
					int h = GetHeuristicValueA2(temporaryWay.GetX, temporaryWay.GetY, finalState.GetX, finalState.GetY, temporaryWay.GetSide);
					temporaryWay.HeuristicValue = h;
					temporaryWay.Depth = currentPosition.Depth + 1;
					maxDepth = temporaryWay.Depth > maxDepth ? temporaryWay.Depth : maxDepth;

					way.Add(temporaryWay);
				}

			if (y != 16)
				if (field[x, y + 1] == 1 || field[x, y + 1] == 3)
				{
					temporaryWay = new CellInf(x, y + 1, CalcClrSideFlip(FlipDirection.right, currentPosition.GetSide))
					{
						from = currentPosition
					};
					int h = GetHeuristicValueA2(temporaryWay.GetX, temporaryWay.GetY, finalState.GetX, finalState.GetY, temporaryWay.GetSide);
					temporaryWay.HeuristicValue = h;
					temporaryWay.Depth = currentPosition.Depth + 1;
					maxDepth = temporaryWay.Depth > maxDepth ? temporaryWay.Depth : maxDepth;

					way.Add(temporaryWay);
				}

			if (y != 0)
				if (field[x, y - 1] == 1 || field[x, y - 1] == 3)
				{
					temporaryWay = new CellInf(x, y - 1, CalcClrSideFlip(FlipDirection.left, currentPosition.GetSide))
					{
						from = currentPosition
					};
					int h = GetHeuristicValueA2(temporaryWay.GetX, temporaryWay.GetY, finalState.GetX, finalState.GetY, temporaryWay.GetSide);
					temporaryWay.HeuristicValue = h;
					temporaryWay.Depth = currentPosition.Depth + 1;
					maxDepth = temporaryWay.Depth > maxDepth ? temporaryWay.Depth : maxDepth;

					way.Add(temporaryWay);
				}

			return way;
		}

		public void FindingWayA2()
		{
			noExit = true;

			O2 = new List<CellInf>();
			C2 = new List<CellInf>();

			O2.Add(startState);

			while (O2.Count > 0)
			{
				CellInf temp = GetMinimumValue(O2);
				O2.Remove(temp);

				if (temp == finalState)
				{
					finalState.from = temp.from;
					C2.Add(temp);
					noExit = false;
					return;
				}

				foreach (CellInf p in CalcMovesA2(temp))
					if (!O2.Contains(p) && !C2.Contains(p))
						O2.Add(p);
					else if (O2.Contains(p))
					{
						int index = O2.IndexOf(p);

						CellInf similar = O2[index];

						if (p.Value < similar.Value)
						{
							O2.Remove(similar);
							O2.Add(p);
						}
					}
					else if (C2.Contains(p))
					{
						int index = C2.IndexOf(p);

						CellInf similar = C2[index];

						if (p.Value < similar.Value)
						{
							C2.Remove(similar);
							O2.Add(p);
						}
					}

				C2.Add(temp);
			}

			MessageBox.Show("К выбранной цели нет пути!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public string GetStatisticsA2()
		{
			string statistics = "";
			maxDepth = 0;

			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();
			FindingWayA2();
			stopWatch.Stop();
			statistics += "Время работы алгоритма: " + Convert.ToString(stopWatch.Elapsed) + Environment.NewLine;

			Queue<int> pathPanel = GetWayPanel();
			statistics += "Количество ходов: " + Convert.ToString((pathPanel.Count - 2) / 2) + "	Максимальная глубина: " + Convert.ToString(maxDepth) + Environment.NewLine;

			statistics += "Количество перебранных вариантов (C): " + Convert.ToString(C2.Count) + Environment.NewLine;

			statistics += "Количество путей на рассмотрение (O): " + Convert.ToString(O2.Count) + Environment.NewLine;

			return statistics;
		}

		private void FindingMovesWayA2()
		{
			noExit = true;

			int[,,] fieldMapMovesСurrentInformation = new int[this.fieldSize, this.fieldSize, 3];
			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
					for (int k = 0; k < 3; k++)
						fieldMapMovesСurrentInformation[i, j, k] = 0;

			int[,] fieldMapMovesСurrent = new int[this.fieldSize, this.fieldSize];
			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
					fieldMapMovesСurrent[i, j] = 0;

			listMovesInformation = new List<int[,,]>();
			fieldMapMoves = new List<int[,]>();

			O2 = new List<CellInf>();
			C2 = new List<CellInf>();

			O2.Add(startState);

			while (O2.Count > 0)
			{
				CellInf temp = GetMinimumValue(O2);
				O2.Remove(temp);

				if (temp == finalState)
				{
					finalState.from = temp.from;
					C2.Add(temp);
					noExit = false;
					return;
				}

				foreach (CellInf p in CalcMovesA2(temp))
					if (!O2.Contains(p) && !C2.Contains(p))
					{
						O2.Add(p);
						fieldMapMovesСurrent[p.GetX, p.GetY]++;
						fieldMapMovesСurrentInformation[p.GetX, p.GetY, 0] = p.Depth;
						fieldMapMovesСurrentInformation[p.GetX, p.GetY, 1] = p.HeuristicValue;
						fieldMapMovesСurrentInformation[p.GetX, p.GetY, 2] = p.Value;

						int[,,] fieldMapMovesСurrentInformationRecord = new int[this.fieldSize, this.fieldSize, 3];
						for (int i = 0; i < fieldSize; i++)
							for (int j = 0; j < fieldSize; j++)
								for (int k = 0; k < 3; k++)
									fieldMapMovesСurrentInformationRecord[i, j, k] = fieldMapMovesСurrentInformation[i, j, k];

						int[,] fieldMapMovesСurrentRecord = new int[this.fieldSize, this.fieldSize];
						for (int i = 0; i < fieldSize; i++)
							for (int j = 0; j < fieldSize; j++)
								fieldMapMovesСurrentRecord[i, j] = fieldMapMovesСurrent[i, j];

						fieldMapMovesСurrentRecord[temp.GetX, temp.GetY] = -1;

						listMovesInformation.Add(fieldMapMovesСurrentInformationRecord);
						fieldMapMoves.Add(fieldMapMovesСurrentRecord);
					}
					else if (O2.Contains(p))
					{
						int index = O2.IndexOf(p);

						CellInf similar = O2[index];

						if (p.Value < similar.Value)
						{
							O2.Remove(similar);
							O2.Add(p);

							fieldMapMovesСurrent[p.GetX, p.GetY]++;
							fieldMapMovesСurrentInformation[p.GetX, p.GetY, 0] = p.Depth;
							fieldMapMovesСurrentInformation[p.GetX, p.GetY, 1] = p.HeuristicValue;
							fieldMapMovesСurrentInformation[p.GetX, p.GetY, 2] = p.Value;

							int[,,] fieldMapMovesСurrentInformationRecord = new int[this.fieldSize, this.fieldSize, 3];
							for (int i = 0; i < fieldSize; i++)
								for (int j = 0; j < fieldSize; j++)
									for (int k = 0; k < 3; k++)
										fieldMapMovesСurrentInformationRecord[i, j, k] = fieldMapMovesСurrentInformation[i, j, k];

							int[,] fieldMapMovesСurrentRecord = new int[this.fieldSize, this.fieldSize];
							for (int i = 0; i < fieldSize; i++)
								for (int j = 0; j < fieldSize; j++)
									fieldMapMovesСurrentRecord[i, j] = fieldMapMovesСurrent[i, j];

							fieldMapMovesСurrentRecord[temp.GetX, temp.GetY] = -1;

							listMovesInformation.Add(fieldMapMovesСurrentInformationRecord);
							fieldMapMoves.Add(fieldMapMovesСurrentRecord);
						}
					}
					else if (C2.Contains(p))
					{
						int index = C2.IndexOf(p);

						CellInf similar = C2[index];

						if (p.Value < similar.Value)
						{
							C2.Remove(similar);
							O2.Add(p);

							fieldMapMovesСurrent[p.GetX, p.GetY]++;
							fieldMapMovesСurrentInformation[p.GetX, p.GetY, 0] = p.Depth;
							fieldMapMovesСurrentInformation[p.GetX, p.GetY, 1] = p.HeuristicValue;
							fieldMapMovesСurrentInformation[p.GetX, p.GetY, 2] = p.Value;

							int[,,] fieldMapMovesСurrentInformationRecord = new int[this.fieldSize, this.fieldSize, 3];
							for (int i = 0; i < fieldSize; i++)
								for (int j = 0; j < fieldSize; j++)
									for (int k = 0; k < 3; k++)
										fieldMapMovesСurrentInformationRecord[i, j, k] = fieldMapMovesСurrentInformation[i, j, k];

							int[,] fieldMapMovesСurrentRecord = new int[this.fieldSize, this.fieldSize];
							for (int i = 0; i < fieldSize; i++)
								for (int j = 0; j < fieldSize; j++)
									fieldMapMovesСurrentRecord[i, j] = fieldMapMovesСurrent[i, j];

							fieldMapMovesСurrentRecord[temp.GetX, temp.GetY] = -1;

							listMovesInformation.Add(fieldMapMovesСurrentInformationRecord);
							fieldMapMoves.Add(fieldMapMovesСurrentRecord);
						}
					}

				C2.Add(temp);
			}

			MessageBox.Show("К выбранной цели нет пути!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public List<int[,]> GetMapMovesA2()
		{
			FindingMovesWayA2();
			return fieldMapMoves;
		}

		public List<int[,,]> GetMapMovesInformationA2()
		{
			FindingMovesWayA2();
			return listMovesInformation;
		}

		#endregion

		private int GetHeuristicValueAlgorithm1(int startX, int startY, int finalX, int finalY)
		{
			return (Math.Abs(startX - finalX) + Math.Abs(startY - finalY));
		}

		private CellInf GetMinimumValue(List<CellInf> O1)
		{
			CellInf result = O1[0];

			for (int i = 0; i < O1.Count - 1; i++)
				if (O1[i] < result)
					result = O1[i];

			return result;
		}

		public string PathOutput()
		{
			if (noExit)
				return "К выбранной цели нет пути!";

			Stack<CellInf> way = new Stack<CellInf>();
			CellInf temp = finalState;

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
				receivedPath += "Ход " + Convert.ToString(counter) + ". Позиция: " + Convert.ToString(temp.GetX) + "," + Convert.ToString(temp.GetY) + Environment.NewLine +
								"Красная сторона куба: " + Convert.ToString(temp.GetSide) + Environment.NewLine + Environment.NewLine;
				counter++;
			}

			return receivedPath;
		}

		public Queue<int> GetWayPanel()
		{
			Stack<CellInf> way = new Stack<CellInf>();
			Queue<int> pathPanel = new Queue<int>();
			CellInf temp = finalState;

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
			Stack<CellInf> way = new Stack<CellInf>();
			Queue<BoxSides> pathPanel = new Queue<BoxSides>();
			CellInf temp = finalState;

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

		public T FindMaxValue<T>(List<T> list, Converter<T, int> projection)
		{
			if (list.Count == 0)
				throw new InvalidOperationException("Empty list");

			T cell = list[0];
			foreach (T item in list)
				if (projection(item) > projection(cell))
					cell = item;

			return cell;
		}

	}
}