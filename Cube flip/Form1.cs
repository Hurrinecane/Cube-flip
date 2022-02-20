using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cube_flip
{
	public partial class Form1 : Form
	{
		private FlipCubeGame flipCubeGame;

		private const int fieldSize = 17;
		private int[,] field = new int[fieldSize, fieldSize];

		Panel[,] panel = new Panel[fieldSize, fieldSize];
		private int cellCounterX = 0;
		private int cellCounterY = 0;

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
						case 2:
							numericUpDownStartPositionX.Value = i;
							numericUpDownStartPositionY.Value = j;
							break;
						case 3:
							numericUpDownEndPositionX.Value = i;
							numericUpDownEndPositionY.Value = j;
							break;
					}

			flipCubeGame = new FlipCubeGame((int)numericUpDownStartPositionX.Value,
											(int)numericUpDownStartPositionY.Value,
											(int)numericUpDownEndPositionX.Value,
											(int)numericUpDownEndPositionY.Value,
											FlipCubeGame.DesiredColorSide.top);

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
						case 0:                                                             //wall
							panel[i, j].BackColor = Color.Black;
							break;
						case 1:                                                             //space
							panel[i, j].BackColor = Color.White;
							break;
						case 2:                                                             //box					
							panel[i, j].Paint += new PaintEventHandler(CubeRenderingPanel);
							panel[i, j].BackColor = Color.Green;
							break;
						case 3:                                                             //target
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
						case 2:                                                             //box
							if (panel[i, j].BackColor != Color.Green)
							{
								panel[i, j].Paint -= new PaintEventHandler(TargetDrawingPanel);
								panel[i, j].Paint += new PaintEventHandler(CubeRenderingPanel);
								panel[i, j].BackColor = Color.Green;
								panel[i, j].Invalidate();
							}
							break;
						case 3:                                                             //target
							if (panel[i, j].BackColor != Color.Yellow)
							{
								panel[i, j].Paint += new PaintEventHandler(TargetDrawingPanel);
								panel[i, j].Paint -= new PaintEventHandler(CubeRenderingPanel);
								panel[i, j].BackColor = Color.Yellow;
								panel[i, j].Invalidate();
							}
							break;
						case 4:                                                             //reset
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
			if (cellCounterX == fieldSize)
			{
				cellCounterX = 0;
				cellCounterY++;
			}

			if (cellCounterY == fieldSize)
				cellCounterY = 0;

			Point pText = new Point(1, 0);
			Font drawFont = new Font("Arial", 7);

			e.Graphics.DrawString(Convert.ToString(cellCounterX) + "," + Convert.ToString(cellCounterY), drawFont, Brushes.Black, pText);

			cellCounterX++;
		}

		private void TargetDrawingPanel(object sender, PaintEventArgs e)
		{
			Pen pen = new Pen(Color.Red) { Width = 3 };
			e.Graphics.DrawEllipse(pen, 3, 3, 21, 21);
		}

		private void CubeRenderingPanel(object sender, PaintEventArgs e)
		{
			switch (flipCubeGame.CurrentDesiredColorSide)
			{
				case FlipCubeGame.DesiredColorSide.top:
					e.Graphics.FillRectangle(Brushes.Red, 0, 0, 28, 28);
					break;

				case FlipCubeGame.DesiredColorSide.left:
					e.Graphics.FillRectangle(Brushes.Red, 0, 0, 3, 28);
					break;

				case FlipCubeGame.DesiredColorSide.right:
					e.Graphics.FillRectangle(Brushes.Red, 25, 0, 3, 28);
					break;

				case FlipCubeGame.DesiredColorSide.front:
					e.Graphics.FillRectangle(Brushes.Red, 0, 0, 28, 3);
					break;

				case FlipCubeGame.DesiredColorSide.behind:
					e.Graphics.FillRectangle(Brushes.Red, 0, 25, 28, 3);
					break;
			}
		}

		#endregion


		#region WidthAI

		private void ButtonFindSolutionWidthClick(object sender, EventArgs e)
		{
			AISystem.BoxSides current = (AISystem.BoxSides)flipCubeGame.CurrentDesiredColorSide;


			int[,] fieldTemp = new int[fieldSize, fieldSize];

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
				{
					fieldTemp[i, j] = field[i, j];

					if (fieldTemp[i, j] == 2 || fieldTemp[i, j] == 3)
					{
						fieldTemp[i, j] = 4;
					}
				}

			fieldTemp[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 2;
			fieldTemp[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;

			AISystem artificialIntelligenceSystem = new AISystem(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, AISystem.BoxSides.bottom, fieldTemp, fieldSize);
			artificialIntelligenceSystem.FindingWayWidth();
			textBox1.Text = artificialIntelligenceSystem.PathOutput();
		}

		async private void ButtonSolutionDemoWidthClick(object sender, EventArgs e)
		{
			AISystem.BoxSides current = (AISystem.BoxSides)flipCubeGame.CurrentDesiredColorSide;
			int[,] fieldTemp = new int[fieldSize, fieldSize];

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
				{
					fieldTemp[i, j] = field[i, j];

					if (fieldTemp[i, j] == 2 || fieldTemp[i, j] == 3)
						fieldTemp[i, j] = 4;
				}

			fieldTemp[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 2;
			fieldTemp[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;

			AISystem artificialIntelligenceSystem = new AISystem(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, AISystem.BoxSides.bottom, fieldTemp, fieldSize);
			artificialIntelligenceSystem.FindingWayWidth();
			textBox1.Text = artificialIntelligenceSystem.PathOutput();

			Queue<int> pathPanel = artificialIntelligenceSystem.GetWayPanel();
			Queue<AISystem.BoxSides> ColorSide = artificialIntelligenceSystem.GetWayColorSide();

			pathPanel.Dequeue();
			pathPanel.Dequeue();
			ColorSide.Dequeue();

			while (pathPanel.Count > 0)
			{
				int x = pathPanel.Dequeue();
				int y = pathPanel.Dequeue();

				flipCubeGame.CurrentDesiredColorSide = (FlipCubeGame.DesiredColorSide)ColorSide.Dequeue();

				field[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;
				field[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 4;
				field[x, y] = 2;
				flipCubeGame.СurrentPanelX = x;
				flipCubeGame.СurrentPanelY = y;

				await Task.Run(() => { gameField.Invalidate(); });
				await Task.Delay((int)numericUpDownTime.Value);
			}
			ResetGameField();
		}

		private void ButtonShowmapExploredPassagesWidth_Click(object sender, EventArgs e)
		{
			AISystem.BoxSides current = (AISystem.BoxSides)flipCubeGame.CurrentDesiredColorSide;

			int[,] fieldTemp = new int[fieldSize, fieldSize];

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
				{
					fieldTemp[i, j] = field[i, j];
					if (fieldTemp[i, j] == 2 || fieldTemp[i, j] == 3)
						fieldTemp[i, j] = 1;
				}

			fieldTemp[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 2;
			fieldTemp[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;

			AISystem artificialIntelligenceSystem = new AISystem(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, AISystem.BoxSides.bottom, fieldTemp, fieldSize);
			fieldTemp = artificialIntelligenceSystem.GetMapMovesWidth();

			AlgorithmMoveMap algorithmMoveMap = new AlgorithmMoveMap(fieldTemp, flipCubeGame.StartPanelX, flipCubeGame.StartPanelY, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY);
			algorithmMoveMap.Show();
		}

		#endregion


		#region DepthAI

		private void ButtonFindSolutionDepthClick(object sender, EventArgs e)
		{
			AISystem.BoxSides current = (AISystem.BoxSides)flipCubeGame.CurrentDesiredColorSide;

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

			fieldTemp[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 2;
			fieldTemp[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;

			AISystem artificialIntelligenceSystem = new AISystem(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, AISystem.BoxSides.bottom, fieldTemp, fieldSize);
			artificialIntelligenceSystem.FindingWayDepth();
			textBox2.Text = artificialIntelligenceSystem.PathOutput();
		}

		async private void ButtonSolutionDemoDepthClick(object sender, EventArgs e)
		{
			AISystem.BoxSides current = (AISystem.BoxSides)flipCubeGame.CurrentDesiredColorSide;

			int[,] fieldTemp = new int[fieldSize, fieldSize];

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
				{
					fieldTemp[i, j] = field[i, j];

					if (fieldTemp[i, j] == 2 || fieldTemp[i, j] == 3)
						fieldTemp[i, j] = 4;
				}

			fieldTemp[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 2;
			fieldTemp[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;

			AISystem artificialIntelligenceSystem = new AISystem(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, AISystem.BoxSides.bottom, fieldTemp, fieldSize);
			artificialIntelligenceSystem.FindingWayDepth();
			textBox2.Text = artificialIntelligenceSystem.PathOutput();

			Queue<int> pathPanel = artificialIntelligenceSystem.GetWayPanel();
			Queue<AISystem.BoxSides> ColorSide = artificialIntelligenceSystem.GetWayColorSide();

			pathPanel.Dequeue();
			pathPanel.Dequeue();
			ColorSide.Dequeue();

			while (pathPanel.Count > 0)
			{
				int x = pathPanel.Dequeue();
				int y = pathPanel.Dequeue();

				flipCubeGame.CurrentDesiredColorSide = (FlipCubeGame.DesiredColorSide)ColorSide.Dequeue();

				field[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;
				field[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 4;
				field[x, y] = 2;
				flipCubeGame.СurrentPanelX = x;
				flipCubeGame.СurrentPanelY = y;

				field[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;
				field[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 2;

				await Task.Run(() => { gameField.Invalidate(); });
				await Task.Delay((int)numericUpDownTime.Value);
			}
			ResetGameField();
		}

		private void ButtonShowmapExploredPassagesDepth_Click(object sender, EventArgs e)
		{
			AISystem.BoxSides current = (AISystem.BoxSides)flipCubeGame.CurrentDesiredColorSide;

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

			fieldTemp[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 2;
			fieldTemp[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;

			AISystem artificialIntelligenceSystem = new AISystem(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, AISystem.BoxSides.bottom, fieldTemp, fieldSize);
			fieldTemp = artificialIntelligenceSystem.GetMapMovesDepth();

			AlgorithmMoveMap algorithmMoveMap = new AlgorithmMoveMap(fieldTemp, flipCubeGame.StartPanelX, flipCubeGame.StartPanelY, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY);
			algorithmMoveMap.Show();
		}

		#endregion


		#region Other

		private void FormKeyDown(object sender, KeyEventArgs e)
		{
			int currentX = flipCubeGame.СurrentPanelX;
			int currentY = flipCubeGame.СurrentPanelY;

			switch (e.KeyValue)
			{
				case (char)Keys.A:
				case (char)Keys.Left:
					if (currentY == 0)
						return;
					else if (field[currentX, currentY - 1] == 1 || field[currentX, currentY - 1] == 3)
					{
						flipCubeGame.СurrentPanelY = currentY - 1;

						field[currentX, currentY] = 4;
						field[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 2;

						flipCubeGame.ChangeCurrentColor(FlipCubeGame.TurningSide.left);
					}
					break;

				case (char)Keys.D:
				case (char)Keys.Right:
					if (currentY == 16)
						return;
					else if (field[currentX, currentY + 1] == 1 || field[currentX, currentY + 1] == 3)
					{
						flipCubeGame.СurrentPanelY = currentY + 1;

						field[currentX, currentY] = 4;
						field[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 2;

						flipCubeGame.ChangeCurrentColor(FlipCubeGame.TurningSide.right);
					}
					break;

				case (char)Keys.S:
				case (char)Keys.Down:
					if (currentX == 16)
						return;
					else if (field[currentX + 1, currentY] == 1 || field[currentX + 1, currentY] == 3)
					{
						flipCubeGame.СurrentPanelX = currentX + 1;

						field[currentX, currentY] = 4;
						field[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 2;

						flipCubeGame.ChangeCurrentColor(FlipCubeGame.TurningSide.bottom);
					}
					break;

				case (char)Keys.W:
				case (char)Keys.Up:
					if (currentX == 0)
						return;
					else if (field[currentX - 1, currentY] == 1 || field[currentX - 1, currentY] == 3)
					{
						flipCubeGame.СurrentPanelX = currentX - 1;

						field[currentX, currentY] = 4;
						field[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 2;

						flipCubeGame.ChangeCurrentColor(FlipCubeGame.TurningSide.top);
					}
					break;

				case (char)Keys.R:
					ResetAll();
					break;
			}

			field[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;
			field[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 2;

			if (flipCubeGame.СurrentPanelX == flipCubeGame.FinishPanelX
				&& flipCubeGame.СurrentPanelY == flipCubeGame.FinishPanelY
				&& FlipCubeGame.DesiredColorSide.bottom == flipCubeGame.CurrentDesiredColorSide)
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
						ResetGameField();
						break;

					case MouseButtons.Right:
						if (field[x, y] != 2 && field[x, y] != 3)
						{
							field[flipCubeGame.StartPanelX, flipCubeGame.StartPanelY] = 1;
							field[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;
							field[x, y] = 2;

							flipCubeGame.СurrentPanelX = flipCubeGame.StartPanelX = (int)(numericUpDownStartPositionX.Value = x);
							flipCubeGame.СurrentPanelY = flipCubeGame.StartPanelY = (int)(numericUpDownStartPositionY.Value = y);
							ResetGameField();
						}
						break;

					case MouseButtons.Middle:
						if (field[x, y] != 2 && field[x, y] != 3)
						{
							field[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 1;
							field[flipCubeGame.StartPanelX, flipCubeGame.StartPanelY] = 2;
							field[x, y] = 3;

							flipCubeGame.FinishPanelX = (int)(numericUpDownEndPositionX.Value = x);
							flipCubeGame.FinishPanelY = (int)(numericUpDownEndPositionY.Value = y);
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
			AISystem.BoxSides current = (AISystem.BoxSides)flipCubeGame.CurrentDesiredColorSide;

			int[,] fieldTemp = new int[fieldSize, fieldSize];

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
				{
					fieldTemp[i, j] = field[i, j];
					if (fieldTemp[i, j] == 2 || fieldTemp[i, j] == 3)
						fieldTemp[i, j] = 1;
				}


			fieldTemp[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 2;
			fieldTemp[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;

			AISystem artificialIntelligenceSystem = new AISystem(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, AISystem.BoxSides.bottom, fieldTemp, fieldSize);
			textBox3.Text = artificialIntelligenceSystem.GetStatisticsWidth();
			textBox4.Text = artificialIntelligenceSystem.GetStatisticsDepth();
		}

		private void ReadArrayFromFile()
		{
			string[] lines = File.ReadAllLines("field.txt");
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

		private void ResetAll()
		{
			cellCounterX = 0;
			cellCounterY = 0;

			switch (domainUpDownSideRedFace.Text)
			{
				case "top":
					flipCubeGame.CurrentDesiredColorSide = FlipCubeGame.DesiredColorSide.top;
					break;
				case "right":
					flipCubeGame.CurrentDesiredColorSide = FlipCubeGame.DesiredColorSide.right;
					break;
				case "left":
					flipCubeGame.CurrentDesiredColorSide = FlipCubeGame.DesiredColorSide.left;
					break;
				case "behind":
					flipCubeGame.CurrentDesiredColorSide = FlipCubeGame.DesiredColorSide.behind;
					break;
				case "bottom":
					flipCubeGame.CurrentDesiredColorSide = FlipCubeGame.DesiredColorSide.bottom;
					break;
				case "straight":
					flipCubeGame.CurrentDesiredColorSide = FlipCubeGame.DesiredColorSide.front;
					break;
			}

			flipCubeGame.FinishPanelX = (int)numericUpDownEndPositionX.Value;
			flipCubeGame.FinishPanelY = (int)numericUpDownEndPositionY.Value;
			flipCubeGame.СurrentPanelX = (int)numericUpDownStartPositionX.Value;
			flipCubeGame.СurrentPanelY = (int)numericUpDownStartPositionY.Value;
			flipCubeGame.StartPanelX = (int)numericUpDownStartPositionX.Value;
			flipCubeGame.StartPanelY = (int)numericUpDownStartPositionY.Value;

			if ((flipCubeGame.StartPanelX == flipCubeGame.FinishPanelX && flipCubeGame.StartPanelY == flipCubeGame.FinishPanelY) ||
				(field[(int)numericUpDownEndPositionX.Value, (int)numericUpDownEndPositionY.Value] == 0) ||
				(field[(int)numericUpDownStartPositionX.Value, (int)numericUpDownStartPositionY.Value] == 0))
			{
				MessageBox.Show("Стартовая позиция и конечная должны отличаться!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
				flipCubeGame.FinishPanelX = 1;
				flipCubeGame.FinishPanelY = 8;
				numericUpDownEndPositionX.Value = 1;
				numericUpDownEndPositionY.Value = 8;
				flipCubeGame.СurrentPanelX = 15;
				flipCubeGame.СurrentPanelY = 8;
				numericUpDownStartPositionX.Value = 15;
				numericUpDownStartPositionY.Value = 8;
				flipCubeGame.StartPanelX = 15;
				flipCubeGame.StartPanelY = 8;
				numericUpDownStartPositionX.Value = 15;
				numericUpDownStartPositionY.Value = 8;
				flipCubeGame.CurrentDesiredColorSide = FlipCubeGame.DesiredColorSide.top;
			}

			ReadArrayFromFile();

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
					if (field[i, j] == 2 || field[i, j] == 3)
						field[i, j] = 4;

			field[flipCubeGame.StartPanelX, flipCubeGame.StartPanelY] = 2;
			field[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;

			ResetGameField();
		}

		#endregion
	}
}