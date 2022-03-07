using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Cube_flip.FlipCubeGame;

namespace Cube_flip
{
	public partial class Form1 : Form
	{
		private FlipCubeGame flipCubeGame;

		private const int fieldSize = 17;
		private int[,] field = new int[fieldSize, fieldSize];

		private Panel[,] panel = new Panel[fieldSize, fieldSize];

		private bool exitDemo = false;

		public Form1()
		{
			InitializeComponent();
			Initialization();
		}

		private void Initialization()
		{
			ReadArrayFromFile();

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
					switch (field[i, j])
					{
						case (int)CellTypes.box:
							numericUpDownStartPositionX.Value = i;
							numericUpDownStartPositionY.Value = j;
							break;
						case (int)CellTypes.target:
							numericUpDownEndPositionX.Value = i;
							numericUpDownEndPositionY.Value = j;
							break;
					}

			flipCubeGame = new FlipCubeGame((int)numericUpDownStartPositionX.Value,
											(int)numericUpDownStartPositionY.Value,
											(int)numericUpDownEndPositionX.Value,
											(int)numericUpDownEndPositionY.Value,
											BoxSides.top);

			ResetGameField();
		}

		#region GameField

		private void ResetGameField()
		{
			gameField.Controls.Clear();

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
				{
					panel[i, j] = new Panel
					{
						Name = "Panel_" + Convert.ToString(j) + "_" + Convert.ToString(i),
						BorderStyle = BorderStyle.Fixed3D,
						Size = new Size(32, 32),
						Location = new Point(i * 32 + 2, j * 32 + 2)
					};
					panel[i, j].MouseClick += new MouseEventHandler(GameFieldMouseClick);

					panel[i, j].Paint += new PaintEventHandler(NumberDrawingPanel);

					switch (field[j, i])
					{
						case (int)CellTypes.wall:                                                             //wall
							panel[i, j].BackColor = Color.Black;
							break;
						case 1:                                                             //space
							panel[i, j].BackColor = Color.White;
							break;
						case (int)CellTypes.box:                                                             //box					
							panel[i, j].Paint += new PaintEventHandler(CubeRenderingPanel);
							panel[i, j].BackColor = Color.Green;
							break;
						case (int)CellTypes.target:                                                             //target
							panel[i, j].Paint += new PaintEventHandler(TargetDrawingPanel);
							panel[i, j].BackColor = Color.Yellow;
							break;
					}
					panel[i, j].Invalidate();

					gameField.Controls.Add(panel[i, j]);
				}
		}

		private void RedrawGameField(object sender, PaintEventArgs e)
		{
			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
					switch (field[j, i])
					{
						case (int)CellTypes.box:                                                             //box
							if (panel[i, j].BackColor != Color.Green)
							{
								panel[i, j].Paint -= new PaintEventHandler(TargetDrawingPanel);
								panel[i, j].Paint += new PaintEventHandler(CubeRenderingPanel);
								panel[i, j].BackColor = Color.Green;
								panel[i, j].Invalidate();
							}
							break;
						case (int)CellTypes.target:                                                             //target
							if (panel[i, j].BackColor != Color.Yellow)
							{
								panel[i, j].Paint += new PaintEventHandler(TargetDrawingPanel);
								panel[i, j].Paint -= new PaintEventHandler(CubeRenderingPanel);
								panel[i, j].BackColor = Color.Yellow;
								panel[i, j].Invalidate();
							}
							break;
						case (int)CellTypes.reset:                                                             //reset
							panel[i, j].Paint -= new PaintEventHandler(CubeRenderingPanel);
							panel[i, j].Paint -= new PaintEventHandler(TargetDrawingPanel);
							panel[i, j].BackColor = Color.White;
							panel[i, j].Invalidate();

							field[j, i] = 1;
							break;
					}
		}


		private void NumberDrawingPanel(object sender, PaintEventArgs e)
		{
			int cellCounterX = 0;
			int cellCounterY = 0;

			if (sender is Panel)
			{
				string namePanel = (sender as Control).Name.ToString();

				if (namePanel != "gameField")
				{
					namePanel = namePanel.Remove(0, 6);

					int indexOfChar = namePanel.IndexOf('_');
					string xString, yString;

					xString = namePanel.Substring(0, indexOfChar);
					yString = namePanel.Remove(0, indexOfChar + 1);

					cellCounterX = Convert.ToInt32(xString);
					cellCounterY = Convert.ToInt32(yString);

				}
			}

			Point pText = new Point(1, 0);
			Font drawFont = new Font("Arial", 7);

			e.Graphics.DrawString(Convert.ToString(cellCounterX) + "," + Convert.ToString(cellCounterY), drawFont, Brushes.Black, pText);
		}

		private void TargetDrawingPanel(object sender, PaintEventArgs e)
		{
			Pen pen = new Pen(Color.Red) { Width = 3 };
			e.Graphics.DrawEllipse(pen, 3, 3, 21, 21);
		}

		private void CubeRenderingPanel(object sender, PaintEventArgs e)
		{
			switch (flipCubeGame.ColoredSide)
			{
				case BoxSides.top:
					e.Graphics.FillRectangle(Brushes.Red, 0, 0, 28, 28);
					break;

				case BoxSides.bottom:
					break;

				case BoxSides.left:
					e.Graphics.FillRectangle(Brushes.Red, 0, 0, 3, 28);
					break;

				case BoxSides.right:
					e.Graphics.FillRectangle(Brushes.Red, 25, 0, 3, 28);
					break;

				case BoxSides.front:
					e.Graphics.FillRectangle(Brushes.Red, 0, 0, 28, 3);
					break;

				case BoxSides.back:
					e.Graphics.FillRectangle(Brushes.Red, 0, 25, 28, 3);
					break;
			}
		}

		#endregion

		#region WidthAI

		private void ButtonFindSolutionWidthClick(object sender, EventArgs e)
		{
			BoxSides current = flipCubeGame.ColoredSide;

			int[,] fieldTemp = new int[fieldSize, fieldSize];

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
				{
					fieldTemp[i, j] = field[i, j];
					if (fieldTemp[i, j] == 2 || fieldTemp[i, j] == 3)
						fieldTemp[i, j] = 1;
				}

			fieldTemp[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 2;
			fieldTemp[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;

			UninformedSearch artificialIntelligenceSystem = new UninformedSearch(flipCubeGame.CurrentX, flipCubeGame.CurrentY, current, flipCubeGame.FinishX, flipCubeGame.FinishY, BoxSides.bottom, fieldTemp, fieldSize);
			artificialIntelligenceSystem.FindingWayWidth();
			textBox1.Text = artificialIntelligenceSystem.PathOutput();
		}

		async private void ButtonSolutionDemoWidthClick(object sender, EventArgs e)
		{
			BoxSides current = flipCubeGame.ColoredSide;

			int[,] fieldTemp = new int[fieldSize, fieldSize];

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
				{
					fieldTemp[i, j] = field[i, j];

					if (fieldTemp[i, j] == 2 || fieldTemp[i, j] == 3)
						fieldTemp[i, j] = 1;
				}

			fieldTemp[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 2;
			fieldTemp[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;

			UninformedSearch artificialIntelligenceSystem = new UninformedSearch(flipCubeGame.CurrentX, flipCubeGame.CurrentY, current, flipCubeGame.FinishX, flipCubeGame.FinishY, BoxSides.bottom, fieldTemp, fieldSize);
			artificialIntelligenceSystem.FindingWayWidth();
			textBox1.Text = artificialIntelligenceSystem.PathOutput();

			Queue<int> pathPanel = artificialIntelligenceSystem.GetWayPanel();
			Queue<BoxSides> ColorSide = artificialIntelligenceSystem.GetWayColorSide();

			if (pathPanel.Count == 0)
				return;

			pathPanel.Dequeue();
			pathPanel.Dequeue();
			ColorSide.Dequeue();

			exitDemo = false;

			while (pathPanel.Count > 0 && !exitDemo)
			{
				int x = pathPanel.Dequeue();
				int y = pathPanel.Dequeue();

				flipCubeGame.ColoredSide = (BoxSides)ColorSide.Dequeue();

				field[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;
				field[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 4;
				field[x, y] = 2;
				flipCubeGame.CurrentX = x;
				flipCubeGame.CurrentY = y;

				await Task.Run(() => { gameField.Invalidate(); });
				await Task.Delay((int)numericUpDownTime.Value);
			}
			ResetAll();
		}

		private void ButtonShowmapExploredPassagesWidth_Click(object sender, EventArgs e)
		{
			BoxSides current = flipCubeGame.ColoredSide;

			int[,] fieldTemp = new int[fieldSize, fieldSize];

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
				{
					fieldTemp[i, j] = field[i, j];
					if (fieldTemp[i, j] == 2 || fieldTemp[i, j] == 3)
						fieldTemp[i, j] = 1;
				}

			fieldTemp[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 2;
			fieldTemp[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;

			UninformedSearch uninformedSearch = new UninformedSearch(flipCubeGame.CurrentX, flipCubeGame.CurrentY, current, flipCubeGame.FinishX, flipCubeGame.FinishY, BoxSides.bottom, fieldTemp, fieldSize);
			List<int[,]> listMoves = uninformedSearch.GetMapMovesWidth();

			AlgorithmMoveMap algorithmMoveMap = new AlgorithmMoveMap(listMoves, flipCubeGame.CurrentX, flipCubeGame.CurrentY, flipCubeGame.FinishX, flipCubeGame.FinishY);
			algorithmMoveMap.Show();
		}

		#endregion

		#region DepthAI

		private void ButtonFindSolutionDepthClick(object sender, EventArgs e)
		{
			BoxSides current = flipCubeGame.ColoredSide;

			int[,] fieldTemp = new int[fieldSize, fieldSize];

			for (int i = 0; i < fieldSize; i++)
			{
				for (int j = 0; j < fieldSize; j++)
				{
					fieldTemp[i, j] = field[i, j];

					if (fieldTemp[i, j] == 2 || fieldTemp[i, j] == 3)
					{
						fieldTemp[i, j] = 4;
					}
				}
			}

			fieldTemp[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 2;
			fieldTemp[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;

			UninformedSearch artificialIntelligenceSystem = new UninformedSearch(flipCubeGame.CurrentX, flipCubeGame.CurrentY, current, flipCubeGame.FinishX, flipCubeGame.FinishY, BoxSides.bottom, fieldTemp, fieldSize);
			artificialIntelligenceSystem.FindingWayDepth();
			textBox2.Text = artificialIntelligenceSystem.PathOutput();
		}

		async private void ButtonSolutionDemoDepthClick(object sender, EventArgs e)
		{
			BoxSides current = flipCubeGame.ColoredSide;

			int[,] fieldTemp = new int[fieldSize, fieldSize];

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
				{
					fieldTemp[i, j] = field[i, j];

					if (fieldTemp[i, j] == 2 || fieldTemp[i, j] == 3)
						fieldTemp[i, j] = 4;
				}

			fieldTemp[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 2;
			fieldTemp[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;

			UninformedSearch artificialIntelligenceSystem = new UninformedSearch(flipCubeGame.CurrentX, flipCubeGame.CurrentY, current, flipCubeGame.FinishX, flipCubeGame.FinishY, BoxSides.bottom, fieldTemp, fieldSize);
			artificialIntelligenceSystem.FindingWayDepth();
			textBox2.Text = artificialIntelligenceSystem.PathOutput();

			Queue<int> pathPanel = artificialIntelligenceSystem.GetWayPanel();
			Queue<BoxSides> ColorSide = artificialIntelligenceSystem.GetWayColorSide();

			if (pathPanel.Count == 0)
				return;

			pathPanel.Dequeue();
			pathPanel.Dequeue();
			ColorSide.Dequeue();

			exitDemo = false;

			while (pathPanel.Count > 0 && !exitDemo)
			{
				int x = pathPanel.Dequeue();
				int y = pathPanel.Dequeue();

				flipCubeGame.ColoredSide = ColorSide.Dequeue();

				field[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;
				field[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 4;
				field[x, y] = 2;
				flipCubeGame.CurrentX = x;
				flipCubeGame.CurrentY = y;

				field[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;
				field[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 2;

				await Task.Run(() => { gameField.Invalidate(); });
				await Task.Delay((int)numericUpDownTime.Value);
			}
			ResetAll();
		}

		private void ButtonShowmapExploredPassagesDepthClick(object sender, EventArgs e)
		{
			BoxSides current = flipCubeGame.ColoredSide;

			int[,] fieldTemp = new int[fieldSize, fieldSize];

			for (int i = 0; i < fieldSize; i++)
			{
				for (int j = 0; j < fieldSize; j++)
				{
					fieldTemp[i, j] = field[i, j];

					if (fieldTemp[i, j] == 2 || fieldTemp[i, j] == 3)
					{
						fieldTemp[i, j] = 1;
					}
				}
			}

			fieldTemp[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 2;
			fieldTemp[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;

			UninformedSearch artificialIntelligenceSystem = new UninformedSearch(flipCubeGame.CurrentX, flipCubeGame.CurrentY, current, flipCubeGame.FinishX, flipCubeGame.FinishY, BoxSides.bottom, fieldTemp, fieldSize);
			List<int[,]> listMoves = artificialIntelligenceSystem.GetMapMovesDepth();

			AlgorithmMoveMap algorithmMoveMap = new AlgorithmMoveMap(listMoves, flipCubeGame.CurrentX, flipCubeGame.CurrentY, flipCubeGame.FinishX, flipCubeGame.FinishY);
			algorithmMoveMap.Show();
		}

		#endregion

		#region InformedSearchA* Algorithm1

		private void ButtonAlgorithm1Click(object sender, EventArgs e)
		{
			int[,] fieldTemp = new int[fieldSize, fieldSize];

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
				{
					fieldTemp[i, j] = field[i, j];
					if (fieldTemp[i, j] == 2 || fieldTemp[i, j] == 3)
						fieldTemp[i, j] = 1;

				}

			fieldTemp[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 2;
			fieldTemp[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;

			InformedSearch informedSearch = new InformedSearch(flipCubeGame.CurrentX, flipCubeGame.CurrentY, flipCubeGame.ColoredSide, flipCubeGame.FinishX, flipCubeGame.FinishY, BoxSides.bottom, fieldTemp, fieldSize);
			informedSearch.FindWayA1();
			textBox6.Text = informedSearch.PathOutput();
		}

		async private void ButtonSolutionDemoAlgorithm1Click(object sender, EventArgs e)
		{
			BoxSides current = flipCubeGame.ColoredSide;

			int[,] fieldTemp = new int[fieldSize, fieldSize];

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
				{
					fieldTemp[i, j] = field[i, j];
					if (fieldTemp[i, j] == 2 || fieldTemp[i, j] == 3)
						fieldTemp[i, j] = 1;
				}

			fieldTemp[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 2;
			fieldTemp[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;

			InformedSearch artificialIntelligenceSystem = new InformedSearch(flipCubeGame.CurrentX, flipCubeGame.CurrentY, current, flipCubeGame.FinishX, flipCubeGame.FinishY, BoxSides.bottom, fieldTemp, fieldSize);
			artificialIntelligenceSystem.FindWayA1();
			textBox6.Text = artificialIntelligenceSystem.PathOutput();

			Queue<int> pathPanel = artificialIntelligenceSystem.GetWayPanel();
			Queue<BoxSides> ColorSide = artificialIntelligenceSystem.GetWayColorSide();

			if (pathPanel.Count == 0)
				return;

			pathPanel.Dequeue();
			pathPanel.Dequeue();
			ColorSide.Dequeue();

			exitDemo = false;

			while (pathPanel.Count > 0 && !exitDemo)
			{
				int x = pathPanel.Dequeue();
				int y = pathPanel.Dequeue();

				flipCubeGame.ColoredSide = (BoxSides)ColorSide.Dequeue();

				field[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;
				field[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 4;
				field[x, y] = 2;
				flipCubeGame.CurrentX = x;
				flipCubeGame.CurrentY = y;

				await Task.Run(() => { gameField.Invalidate(); });
				await Task.Delay((int)numericUpDownTime2.Value);
			}
			ResetAll();
		}

		private void ButtonShowmapExploredPassagesAlgorithm1Click(object sender, EventArgs e)
		{
			BoxSides current = flipCubeGame.ColoredSide;

			int[,] fieldTemp = new int[fieldSize, fieldSize];

			for (int i = 0; i < fieldSize; i++)
			{
				for (int j = 0; j < fieldSize; j++)
				{
					fieldTemp[i, j] = field[i, j];

					if (fieldTemp[i, j] == 2 || fieldTemp[i, j] == 3)
					{
						fieldTemp[i, j] = 1;
					}
				}
			}

			fieldTemp[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 2;
			fieldTemp[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;

			InformedSearch artificialIntelligenceSystem = new InformedSearch(flipCubeGame.CurrentX, flipCubeGame.CurrentY, current, flipCubeGame.FinishX, flipCubeGame.FinishY, BoxSides.bottom, fieldTemp, fieldSize);
			List<int[,]> listMoves = artificialIntelligenceSystem.GetMapMovesAlgorithm1();
			List<int[,,]> listMovesInformation = artificialIntelligenceSystem.GetMapMovesInformationAlgorithm1();

			AlgorithmMoveMap algorithmMoveMap = new AlgorithmMoveMap(listMoves, flipCubeGame.CurrentX, flipCubeGame.CurrentY, flipCubeGame.FinishX, flipCubeGame.FinishY, listMovesInformation);
			algorithmMoveMap.Show();
		}

		#endregion

		#region InformedSearchA* Algorithm2

		private void ButtonAlgorithm2Click(object sender, EventArgs e)
		{
			BoxSides current = flipCubeGame.ColoredSide;

			int[,] fieldTemp = new int[fieldSize, fieldSize];

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
				{
					fieldTemp[i, j] = field[i, j];

					if (fieldTemp[i, j] == 2 || fieldTemp[i, j] == 3)
					{
						fieldTemp[i, j] = 1;
					}
				}

			fieldTemp[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 2;
			fieldTemp[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;

			InformedSearch artificialIntelligenceSystem = new InformedSearch(flipCubeGame.CurrentX, flipCubeGame.CurrentY, current, flipCubeGame.FinishX, flipCubeGame.FinishY, BoxSides.bottom, fieldTemp, fieldSize);
			artificialIntelligenceSystem.FindingWayAlgorithm2();
			textBox5.Text = artificialIntelligenceSystem.PathOutput();
		}

		async private void ButtonSolutionDemoAlgorithm2Click(object sender, EventArgs e)
		{
			BoxSides current = flipCubeGame.ColoredSide;

			int[,] fieldTemp = new int[fieldSize, fieldSize];

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
				{
					fieldTemp[i, j] = field[i, j];

					if (fieldTemp[i, j] == 2 || fieldTemp[i, j] == 3)
						fieldTemp[i, j] = 1;
				}

			fieldTemp[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 2;
			fieldTemp[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;

			InformedSearch artificialIntelligenceSystem = new InformedSearch(flipCubeGame.CurrentX, flipCubeGame.CurrentY, current, flipCubeGame.FinishX, flipCubeGame.FinishY, BoxSides.bottom, fieldTemp, fieldSize);
			artificialIntelligenceSystem.FindingWayAlgorithm2();
			textBox5.Text = artificialIntelligenceSystem.PathOutput();

			Queue<int> pathPanel = artificialIntelligenceSystem.GetWayPanel();
			Queue<BoxSides> ColorSide = artificialIntelligenceSystem.GetWayColorSide();

			if (pathPanel.Count == 0)
				return;

			pathPanel.Dequeue();
			pathPanel.Dequeue();
			ColorSide.Dequeue();

			exitDemo = false;

			while (pathPanel.Count > 0 && !exitDemo)
			{
				int x = pathPanel.Dequeue();
				int y = pathPanel.Dequeue();

				flipCubeGame.ColoredSide = ColorSide.Dequeue();

				field[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;
				field[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 4;
				field[x, y] = 2;
				flipCubeGame.CurrentX = x;
				flipCubeGame.CurrentY = y;

				await Task.Run(() => { gameField.Invalidate(); });
				await Task.Delay((int)numericUpDownTime2.Value);
			}
			ResetAll();
		}

		private void ButtonShowmapExploredPassagesAlgorithm2Click(object sender, EventArgs e)
		{
			BoxSides current = flipCubeGame.ColoredSide;

			int[,] fieldTemp = new int[fieldSize, fieldSize];

			for (int i = 0; i < fieldSize; i++)
			{
				for (int j = 0; j < fieldSize; j++)
				{
					fieldTemp[i, j] = field[i, j];

					if (fieldTemp[i, j] == 2 || fieldTemp[i, j] == 3)
					{
						fieldTemp[i, j] = 1;
					}
				}
			}

			fieldTemp[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 2;
			fieldTemp[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;

			InformedSearch artificialIntelligenceSystem = new InformedSearch(flipCubeGame.CurrentX, flipCubeGame.CurrentY, current, flipCubeGame.FinishX, flipCubeGame.FinishY, BoxSides.bottom, fieldTemp, fieldSize);
			List<int[,]> listMoves = artificialIntelligenceSystem.GetMapMovesAlgorithm2();
			List<int[,,]> listMovesInformation = artificialIntelligenceSystem.GetMapMovesInformationAlgorithm2();

			AlgorithmMoveMap algorithmMoveMap = new AlgorithmMoveMap(listMoves, flipCubeGame.CurrentX, flipCubeGame.CurrentY, flipCubeGame.FinishX, flipCubeGame.FinishY, listMovesInformation);
			algorithmMoveMap.Show();
		}

		#endregion

		#region Other

		private void FormKeyDown(object sender, KeyEventArgs e)
		{
			int currentX = flipCubeGame.CurrentX;
			int currentY = flipCubeGame.CurrentY;

			switch (e.KeyValue)
			{
				case (char)Keys.A:
				case (char)Keys.Left:
					if (currentY == 0)
						return;
					else if (field[currentX, currentY - 1] == 1 || field[currentX, currentY - 1] == 3)
					{
						flipCubeGame.CurrentY = currentY - 1;

						field[currentX, currentY] = 4;
						field[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 2;

						flipCubeGame.ChangeCurrentColor(FlipCubeGame.FlipDirection.left);

						exitDemo = true;
					}
					break;

				case (char)Keys.D:
				case (char)Keys.Right:
					if (currentY == 16)
						return;
					else if (field[currentX, currentY + 1] == 1 || field[currentX, currentY + 1] == 3)
					{
						flipCubeGame.CurrentY = currentY + 1;

						field[currentX, currentY] = 4;
						field[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 2;

						flipCubeGame.ChangeCurrentColor(FlipCubeGame.FlipDirection.right);

						exitDemo = true;
					}
					break;

				case (char)Keys.S:
				case (char)Keys.Down:
					if (currentX == 16)
						return;
					else if (field[currentX + 1, currentY] == 1 || field[currentX + 1, currentY] == 3)
					{
						flipCubeGame.CurrentX = currentX + 1;

						field[currentX, currentY] = 4;
						field[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 2;

						flipCubeGame.ChangeCurrentColor(FlipDirection.down);

						exitDemo = true;
					}
					break;

				case (char)Keys.W:
				case (char)Keys.Up:
					if (currentX == 0)
						return;
					else if (field[currentX - 1, currentY] == 1 || field[currentX - 1, currentY] == 3)
					{
						flipCubeGame.CurrentX = currentX - 1;

						field[currentX, currentY] = 4;
						field[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 2;

						flipCubeGame.ChangeCurrentColor(FlipDirection.up);

						exitDemo = true;
					}
					break;

				case (char)Keys.R:
					exitDemo = true;
					ResetAll();
					break;
			}

			field[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;
			field[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 2;

			if (flipCubeGame.CurrentX == flipCubeGame.FinishX
				&& flipCubeGame.CurrentY == flipCubeGame.FinishY
				&& BoxSides.bottom == flipCubeGame.ColoredSide)
			{
				MessageBox.Show("Победа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
				ResetAll();
			}

			gameField.Invalidate();
		}

		private void GameFieldMouseClick(object sender, MouseEventArgs e)
		{
			if (sender is Panel)
			{
				string namePanel = (sender as Control).Name.ToString();
				namePanel = namePanel.Remove(0, 6);

				int indexOfChar = namePanel.IndexOf('_');
				string xString, yString;

				xString = namePanel.Substring(0, indexOfChar);
				yString = namePanel.Remove(0, indexOfChar + 1);

				int x = Convert.ToInt32(xString);
				int y = Convert.ToInt32(yString);

				switch (e.Button)
				{
					case MouseButtons.Left:
						if (field[x, y] == 1)
							field[x, y] = 0;
						else if (field[x, y] == 0)
							field[x, y] = 1;

						exitDemo = true;
						ResetGameField();
						break;

					case MouseButtons.Right:
						if (field[x, y] != 2 && field[x, y] != 3)
						{
							field[flipCubeGame.StartX, flipCubeGame.StartY] = 1;
							field[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 1;
							field[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;
							field[x, y] = 2;

							flipCubeGame.CurrentX = flipCubeGame.StartX = (int)(numericUpDownStartPositionX.Value = x);
							flipCubeGame.CurrentY = flipCubeGame.StartY = (int)(numericUpDownStartPositionY.Value = y);

							switch (domainUpDownSideRedFace.Text)
							{
								case "top":
									flipCubeGame.ColoredSide = BoxSides.top;
									break;
								case "bottom":
									flipCubeGame.ColoredSide = BoxSides.bottom;
									break;
								case "left":
									flipCubeGame.ColoredSide = BoxSides.left;
									break;
								case "right":
									flipCubeGame.ColoredSide = BoxSides.right;
									break;
								case "front":
									flipCubeGame.ColoredSide = BoxSides.front;
									break;
								case "back":
									flipCubeGame.ColoredSide = BoxSides.back;
									break;

							}

							exitDemo = true;
							ResetGameField();
						}
						break;

					case MouseButtons.Middle:
						if (field[x, y] != 2 && field[x, y] != 3)
						{
							field[flipCubeGame.FinishX, flipCubeGame.FinishY] = 1;
							field[flipCubeGame.StartX, flipCubeGame.StartY] = 2;
							field[x, y] = 3;

							flipCubeGame.FinishX = (int)(numericUpDownEndPositionX.Value = x);
							flipCubeGame.FinishY = (int)(numericUpDownEndPositionY.Value = y);

							exitDemo = true;
							ResetGameField();
						}
						break;
				}
			}
		}

		private void ButtonNewGameClick(object sender, EventArgs e)
		{
			ResetAll();
		}

		private void CollectionStatisticsClick(object sender, EventArgs e)
		{
			BoxSides current = flipCubeGame.ColoredSide;

			int[,] fieldTemp = new int[fieldSize, fieldSize];

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
				{
					fieldTemp[i, j] = field[i, j];
					if (fieldTemp[i, j] == 2 || fieldTemp[i, j] == 3)
						fieldTemp[i, j] = 1;
				}

			fieldTemp[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 2;
			fieldTemp[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;

			UninformedSearch artificialIntelligenceSystem = new UninformedSearch(flipCubeGame.CurrentX, flipCubeGame.CurrentY, current, flipCubeGame.FinishX, flipCubeGame.FinishY, BoxSides.bottom, fieldTemp, fieldSize);
			textBox3.Text = artificialIntelligenceSystem.GetStatisticsWidth();
			textBox4.Text = artificialIntelligenceSystem.GetStatisticsDepth();
		}

		private void CollectionStatistics2Click(object sender, EventArgs e)
		{
			BoxSides current = flipCubeGame.ColoredSide;

			int[,] fieldTemp = new int[fieldSize, fieldSize];

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
				{
					fieldTemp[i, j] = field[i, j];
					if (fieldTemp[i, j] == 2 || fieldTemp[i, j] == 3)
						fieldTemp[i, j] = 1;
				}

			fieldTemp[flipCubeGame.CurrentX, flipCubeGame.CurrentY] = 2;
			fieldTemp[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;

			InformedSearch artificialIntelligenceSystem = new InformedSearch(flipCubeGame.CurrentX, flipCubeGame.CurrentY, current, flipCubeGame.FinishX, flipCubeGame.FinishY, BoxSides.bottom, fieldTemp, fieldSize);
			textBox8.Text = artificialIntelligenceSystem.GetStatisticsAlgorithm1();
			textBox7.Text = artificialIntelligenceSystem.GetStatisticsAlgorithm2();
		}

		private void ReadArrayFromFile()
		{
			try
			{
				string[] lines;

				if (domainUpDownLevelSelection.Text == "Level 1")
					lines = File.ReadAllLines("Level_1.txt");
				else if (domainUpDownLevelSelection.Text == "Level 2")
					lines = File.ReadAllLines("Level_2.txt");
				else if (domainUpDownLevelSelection.Text == "Level 3")
					lines = File.ReadAllLines("Level_3.txt");
				else if (domainUpDownLevelSelection.Text == "Level 4")
					lines = File.ReadAllLines("Level_4.txt");
				else if (domainUpDownLevelSelection.Text == "Level 5")
					lines = File.ReadAllLines("Level_5.txt");
				else if (domainUpDownLevelSelection.Text == "Level 6")
					lines = File.ReadAllLines("Level_6.txt");
				else throw new NullReferenceException("File not found! ");

				int[,] num = new int[lines.Length, lines[0].Split(' ').Length];

				for (int i = 0; i < lines.Length; i++)
				{
					string[] temp = lines[i].Split(' ');
					for (int j = 0; j < temp.Length; j++)
						num[i, j] = Convert.ToInt32(temp[j]);
				}

				for (int i = 0; i < fieldSize; i++)
					for (int j = 0; j < fieldSize; j++)
						field[i, j] = num[i, j];
			}
			catch
			{
				for (int i = 0; i < fieldSize; i++)
					for (int j = 0; j < fieldSize; j++)
						field[i, j] = 1;

				field[1, 8] = 3;
				field[15, 8] = 2;

				MessageBox.Show("Выбранный файл не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ResetAll()
		{
			switch (domainUpDownSideRedFace.Text)
			{
				case "top":
					flipCubeGame.ColoredSide = BoxSides.top;
					break;
				case "bottom":
					flipCubeGame.ColoredSide = BoxSides.bottom;
					break;
				case "left":
					flipCubeGame.ColoredSide = BoxSides.left;
					break;
				case "right":
					flipCubeGame.ColoredSide = BoxSides.right;
					break;
				case "front":
					flipCubeGame.ColoredSide = BoxSides.front;
					break;
				case "back":
					flipCubeGame.ColoredSide = BoxSides.back;
					break;
			}

			flipCubeGame.FinishX = (int)numericUpDownEndPositionX.Value;
			flipCubeGame.FinishY = (int)numericUpDownEndPositionY.Value;
			flipCubeGame.CurrentX = (int)numericUpDownStartPositionX.Value;
			flipCubeGame.CurrentY = (int)numericUpDownStartPositionY.Value;
			flipCubeGame.StartX = (int)numericUpDownStartPositionX.Value;
			flipCubeGame.StartY = (int)numericUpDownStartPositionY.Value;

			if ((flipCubeGame.StartX == flipCubeGame.FinishX && flipCubeGame.StartY == flipCubeGame.FinishY) ||
				(field[(int)numericUpDownEndPositionX.Value, (int)numericUpDownEndPositionY.Value] == 0) ||
				(field[(int)numericUpDownStartPositionX.Value, (int)numericUpDownStartPositionY.Value] == 0))
			{
				MessageBox.Show("Стартовая позиция и конечная должны отличаться!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
				flipCubeGame.FinishX = 1;
				flipCubeGame.FinishY = 8;
				numericUpDownEndPositionX.Value = 1;
				numericUpDownEndPositionY.Value = 8;
				flipCubeGame.CurrentX = 15;
				flipCubeGame.CurrentY = 8;
				numericUpDownStartPositionX.Value = 15;
				numericUpDownStartPositionY.Value = 8;
				flipCubeGame.StartX = 15;
				flipCubeGame.StartY = 8;
				numericUpDownStartPositionX.Value = 15;
				numericUpDownStartPositionY.Value = 8;
				flipCubeGame.ColoredSide = BoxSides.top;
			}

			ReadArrayFromFile();

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
					if (field[i, j] == 2 || field[i, j] == 3)
						field[i, j] = 4;

			field[flipCubeGame.StartX, flipCubeGame.StartY] = 2;
			field[flipCubeGame.FinishX, flipCubeGame.FinishY] = 3;

			exitDemo = true;

			ResetGameField();
		}

		private void MenuItemUninformativeSearchClick(object sender, EventArgs e)
		{
			toolStripMenuItem2.Checked = true;
			toolStripMenuItem3.Checked = false;

			groupBox2.Visible = true;
			groupBox5.Visible = true;

			groupBox6.Visible = false;
			groupBox9.Visible = false;
		}

		private void MenuItemInformativeSearchClick(object sender, EventArgs e)
		{
			toolStripMenuItem2.Checked = false;
			toolStripMenuItem3.Checked = true;

			groupBox2.Visible = false;
			groupBox5.Visible = false;

			groupBox6.Visible = true;
			groupBox9.Visible = true;
		}

		private void DomainUpDownLevelSelectionSelectedItemChanged(object sender, EventArgs e)
		{
			ResetAll();
		}

		private void DomainUpDownSideRedFace_SelectedItemChanged(object sender, EventArgs e)
		{
			ResetAll();
		}
		#endregion
	}
}