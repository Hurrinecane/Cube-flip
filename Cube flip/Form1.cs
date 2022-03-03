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
											FlipCubeGame.BoxSides.top);

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
			switch (flipCubeGame.CurrentDesiredColorSide)
			{
				case FlipCubeGame.BoxSides.top:
					e.Graphics.FillRectangle(Brushes.Red, 0, 0, 28, 28);
					break;

				case FlipCubeGame.BoxSides.bottom:
					break;

				case FlipCubeGame.BoxSides.left:
					e.Graphics.FillRectangle(Brushes.Red, 0, 0, 3, 28);
					break;

				case FlipCubeGame.BoxSides.right:
					e.Graphics.FillRectangle(Brushes.Red, 25, 0, 3, 28);
					break;

				case FlipCubeGame.BoxSides.front:
					e.Graphics.FillRectangle(Brushes.Red, 0, 25, 28, 3);
					break;

				case FlipCubeGame.BoxSides.back:
					e.Graphics.FillRectangle(Brushes.Red, 0, 0, 28, 3);
					break;
			}
		}

		#endregion

		#region WidthAI

		private void ButtonFindSolutionWidthClick(object sender, EventArgs e)
		{
			UninformedSearch.BoxSides current = UninformedSearch.BoxSides.top;

			switch (flipCubeGame.CurrentDesiredColorSide)
			{
				case FlipCubeGame.BoxSides.top:
					current = UninformedSearch.BoxSides.top;
					break;
				case FlipCubeGame.BoxSides.bottom:
					current = UninformedSearch.BoxSides.bottom;
					break;
				case FlipCubeGame.BoxSides.right:
					current = UninformedSearch.BoxSides.right;
					break;
				case FlipCubeGame.BoxSides.left:
					current = UninformedSearch.BoxSides.left;
					break;
				case FlipCubeGame.BoxSides.back:
					current = UninformedSearch.BoxSides.back;
					break;
				case FlipCubeGame.BoxSides.front:
					current = UninformedSearch.BoxSides.front;
					break;
			}

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

			fieldTemp[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 2;
			fieldTemp[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;

			UninformedSearch artificialIntelligenceSystem = new UninformedSearch(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, UninformedSearch.BoxSides.bottom, fieldTemp, fieldSize);
			artificialIntelligenceSystem.FindingWayWidth();
			textBox1.Text = artificialIntelligenceSystem.PathOutput();
		}

		async private void ButtonSolutionDemoWidthClick(object sender, EventArgs e)
		{
			UninformedSearch.BoxSides current = UninformedSearch.BoxSides.top;

			switch (flipCubeGame.CurrentDesiredColorSide)
			{
				case FlipCubeGame.BoxSides.top:
					current = UninformedSearch.BoxSides.top;
					break;
				case FlipCubeGame.BoxSides.bottom:
					current = UninformedSearch.BoxSides.bottom;
					break;
				case FlipCubeGame.BoxSides.right:
					current = UninformedSearch.BoxSides.right;
					break;
				case FlipCubeGame.BoxSides.left:
					current = UninformedSearch.BoxSides.left;
					break;
				case FlipCubeGame.BoxSides.back:
					current = UninformedSearch.BoxSides.back;
					break;
				case FlipCubeGame.BoxSides.front:
					current = UninformedSearch.BoxSides.front;
					break;
			}

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

			UninformedSearch artificialIntelligenceSystem = new UninformedSearch(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, UninformedSearch.BoxSides.bottom, fieldTemp, fieldSize);
			artificialIntelligenceSystem.FindingWayWidth();
			textBox1.Text = artificialIntelligenceSystem.PathOutput();

			Queue<int> pathPanel = artificialIntelligenceSystem.GetWayPanel();
			Queue<UninformedSearch.BoxSides> ColorSide = artificialIntelligenceSystem.GetWayColorSide();

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

				flipCubeGame.CurrentDesiredColorSide = (FlipCubeGame.BoxSides)ColorSide.Dequeue();

				field[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;
				field[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 4;
				field[x, y] = 2;
				flipCubeGame.СurrentPanelX = x;
				flipCubeGame.СurrentPanelY = y;

				await Task.Run(() => { gameField.Invalidate(); });
				await Task.Delay((int)numericUpDownTime.Value);
			}
			ResetAll();
		}

		private void ButtonShowmapExploredPassagesWidth_Click(object sender, EventArgs e)
		{
			UninformedSearch.BoxSides current = UninformedSearch.BoxSides.top;

			switch (flipCubeGame.CurrentDesiredColorSide)
			{
				case FlipCubeGame.BoxSides.top:
					current = UninformedSearch.BoxSides.top;
					break;
				case FlipCubeGame.BoxSides.bottom:
					current = UninformedSearch.BoxSides.bottom;
					break;
				case FlipCubeGame.BoxSides.right:
					current = UninformedSearch.BoxSides.right;
					break;
				case FlipCubeGame.BoxSides.left:
					current = UninformedSearch.BoxSides.left;
					break;
				case FlipCubeGame.BoxSides.back:
					current = UninformedSearch.BoxSides.back;
					break;
				case FlipCubeGame.BoxSides.front:
					current = UninformedSearch.BoxSides.front;
					break;
			}

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

			UninformedSearch artificialIntelligenceSystem = new UninformedSearch(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, UninformedSearch.BoxSides.bottom, fieldTemp, fieldSize);
			List<int[,]> listMoves = artificialIntelligenceSystem.GetMapMovesWidth();

			AlgorithmMoveMap algorithmMoveMap = new AlgorithmMoveMap(listMoves, flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY);
			algorithmMoveMap.Show();
		}

		#endregion

		#region DepthAI

		private void ButtonFindSolutionDepthClick(object sender, EventArgs e)
		{
			UninformedSearch.BoxSides current = UninformedSearch.BoxSides.top;

			switch (flipCubeGame.CurrentDesiredColorSide)
			{
				case FlipCubeGame.BoxSides.top:
					current = UninformedSearch.BoxSides.top;
					break;
				case FlipCubeGame.BoxSides.bottom:
					current = UninformedSearch.BoxSides.bottom;
					break;
				case FlipCubeGame.BoxSides.right:
					current = UninformedSearch.BoxSides.right;
					break;
				case FlipCubeGame.BoxSides.left:
					current = UninformedSearch.BoxSides.left;
					break;
				case FlipCubeGame.BoxSides.back:
					current = UninformedSearch.BoxSides.back;
					break;
				case FlipCubeGame.BoxSides.front:
					current = UninformedSearch.BoxSides.front;
					break;
			}

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

			UninformedSearch artificialIntelligenceSystem = new UninformedSearch(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, UninformedSearch.BoxSides.bottom, fieldTemp, fieldSize);
			artificialIntelligenceSystem.FindingWayDepth();
			textBox2.Text = artificialIntelligenceSystem.PathOutput();
		}

		async private void ButtonSolutionDemoDepthClick(object sender, EventArgs e)
		{
			UninformedSearch.BoxSides current = UninformedSearch.BoxSides.top;

			switch (flipCubeGame.CurrentDesiredColorSide)
			{
				case FlipCubeGame.BoxSides.top:
					current = UninformedSearch.BoxSides.top;
					break;
				case FlipCubeGame.BoxSides.bottom:
					current = UninformedSearch.BoxSides.bottom;
					break;
				case FlipCubeGame.BoxSides.right:
					current = UninformedSearch.BoxSides.right;
					break;
				case FlipCubeGame.BoxSides.left:
					current = UninformedSearch.BoxSides.left;
					break;
				case FlipCubeGame.BoxSides.back:
					current = UninformedSearch.BoxSides.back;
					break;
				case FlipCubeGame.BoxSides.front:
					current = UninformedSearch.BoxSides.front;
					break;
			}

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

			UninformedSearch artificialIntelligenceSystem = new UninformedSearch(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, UninformedSearch.BoxSides.bottom, fieldTemp, fieldSize);
			artificialIntelligenceSystem.FindingWayDepth();
			textBox2.Text = artificialIntelligenceSystem.PathOutput();

			Queue<int> pathPanel = artificialIntelligenceSystem.GetWayPanel();
			Queue<UninformedSearch.BoxSides> ColorSide = artificialIntelligenceSystem.GetWayColorSide();

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

				flipCubeGame.CurrentDesiredColorSide = (FlipCubeGame.BoxSides)ColorSide.Dequeue();

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
			ResetAll();
		}

		private void ButtonShowmapExploredPassagesDepthClick(object sender, EventArgs e)
		{
			UninformedSearch.BoxSides current = UninformedSearch.BoxSides.top;

			switch (flipCubeGame.CurrentDesiredColorSide)
			{
				case FlipCubeGame.BoxSides.top:
					current = UninformedSearch.BoxSides.top;
					break;
				case FlipCubeGame.BoxSides.bottom:
					current = UninformedSearch.BoxSides.bottom;
					break;
				case FlipCubeGame.BoxSides.right:
					current = UninformedSearch.BoxSides.right;
					break;
				case FlipCubeGame.BoxSides.left:
					current = UninformedSearch.BoxSides.left;
					break;
				case FlipCubeGame.BoxSides.back:
					current = UninformedSearch.BoxSides.back;
					break;
				case FlipCubeGame.BoxSides.front:
					current = UninformedSearch.BoxSides.front;
					break;
			}

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

			UninformedSearch artificialIntelligenceSystem = new UninformedSearch(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, UninformedSearch.BoxSides.bottom, fieldTemp, fieldSize);
			List<int[,]> listMoves = artificialIntelligenceSystem.GetMapMovesDepth();

			AlgorithmMoveMap algorithmMoveMap = new AlgorithmMoveMap(listMoves, flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY);
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

						exitDemo = true;
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

						exitDemo = true;
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

						exitDemo = true;
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

						exitDemo = true;
					}
					break;

				case (char)Keys.R:
					exitDemo = true;
					ResetAll();
					break;
			}

			field[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;
			field[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 2;

			if (flipCubeGame.СurrentPanelX == flipCubeGame.FinishPanelX
				&& flipCubeGame.СurrentPanelY == flipCubeGame.FinishPanelY
				&& FlipCubeGame.BoxSides.bottom == flipCubeGame.CurrentDesiredColorSide)
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
							field[flipCubeGame.StartPanelX, flipCubeGame.StartPanelY] = 1;
							field[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 1;
							field[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;
							field[x, y] = 2;

							flipCubeGame.СurrentPanelX = flipCubeGame.StartPanelX = (int)(numericUpDownStartPositionX.Value = x);
							flipCubeGame.СurrentPanelY = flipCubeGame.StartPanelY = (int)(numericUpDownStartPositionY.Value = y);

							switch (domainUpDownSideRedFace.Text)
							{
								case "top":
									flipCubeGame.CurrentDesiredColorSide = FlipCubeGame.BoxSides.top;
									break;
								case "bottom":
									flipCubeGame.CurrentDesiredColorSide = FlipCubeGame.BoxSides.bottom;
									break;
								case "left":
									flipCubeGame.CurrentDesiredColorSide = FlipCubeGame.BoxSides.left;
									break;
								case "right":
									flipCubeGame.CurrentDesiredColorSide = FlipCubeGame.BoxSides.right;
									break;
								case "front":
									flipCubeGame.CurrentDesiredColorSide = FlipCubeGame.BoxSides.front;
									break;
								case "back":
									flipCubeGame.CurrentDesiredColorSide = FlipCubeGame.BoxSides.back;
									break;
								
							}

							exitDemo = true;
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
			UninformedSearch.BoxSides current = UninformedSearch.BoxSides.top;

			switch (flipCubeGame.CurrentDesiredColorSide)
			{
				case FlipCubeGame.BoxSides.top:
					current = UninformedSearch.BoxSides.top;
					break;
				case FlipCubeGame.BoxSides.bottom:
					current = UninformedSearch.BoxSides.bottom;
					break;
				case FlipCubeGame.BoxSides.right:
					current = UninformedSearch.BoxSides.right;
					break;
				case FlipCubeGame.BoxSides.left:
					current = UninformedSearch.BoxSides.left;
					break;
				case FlipCubeGame.BoxSides.back:
					current = UninformedSearch.BoxSides.back;
					break;
				case FlipCubeGame.BoxSides.front:
					current = UninformedSearch.BoxSides.front;
					break;
			}

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

			UninformedSearch artificialIntelligenceSystem = new UninformedSearch(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, UninformedSearch.BoxSides.bottom, fieldTemp, fieldSize);
			textBox3.Text = artificialIntelligenceSystem.GetStatisticsWidth();
			textBox4.Text = artificialIntelligenceSystem.GetStatisticsDepth();
		}

		private void CollectionStatistics2Click(object sender, EventArgs e)
		{
			InformedSearch.BoxSides current = InformedSearch.BoxSides.top;

			switch (flipCubeGame.CurrentDesiredColorSide)
			{
				case FlipCubeGame.BoxSides.top:
					current = InformedSearch.BoxSides.top;
					break;
				case FlipCubeGame.BoxSides.bottom:
					current = InformedSearch.BoxSides.bottom;
					break;
				case FlipCubeGame.BoxSides.right:
					current = InformedSearch.BoxSides.right;
					break;
				case FlipCubeGame.BoxSides.left:
					current = InformedSearch.BoxSides.left;
					break;
				case FlipCubeGame.BoxSides.back:
					current = InformedSearch.BoxSides.back;
					break;
				case FlipCubeGame.BoxSides.front:
					current = InformedSearch.BoxSides.front;
					break;
			}

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

			InformedSearch artificialIntelligenceSystem = new InformedSearch(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, InformedSearch.BoxSides.bottom, fieldTemp, fieldSize);
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
					flipCubeGame.CurrentDesiredColorSide = FlipCubeGame.BoxSides.top;
					break;
				case "bottom":
					flipCubeGame.CurrentDesiredColorSide = FlipCubeGame.BoxSides.bottom;
					break;
				case "left":
					flipCubeGame.CurrentDesiredColorSide = FlipCubeGame.BoxSides.left;
					break;
				case "right":
					flipCubeGame.CurrentDesiredColorSide = FlipCubeGame.BoxSides.right;
					break;
				case "front":
					flipCubeGame.CurrentDesiredColorSide = FlipCubeGame.BoxSides.front;
					break;
				case "back":
					flipCubeGame.CurrentDesiredColorSide = FlipCubeGame.BoxSides.back;
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
				flipCubeGame.CurrentDesiredColorSide = FlipCubeGame.BoxSides.top;
			}

			ReadArrayFromFile();

			for (int i = 0; i < fieldSize; i++)
				for (int j = 0; j < fieldSize; j++)
					if (field[i, j] == 2 || field[i, j] == 3)
						field[i, j] = 4;

			field[flipCubeGame.StartPanelX, flipCubeGame.StartPanelY] = 2;
			field[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;

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

		#endregion

		#region InformedSearchA* Algorithm1

		private void ButtonAlgorithm1Click(object sender, EventArgs e)
		{
			InformedSearch.BoxSides current = InformedSearch.BoxSides.top;

			switch (flipCubeGame.CurrentDesiredColorSide)
			{
				case FlipCubeGame.BoxSides.top:
					current = InformedSearch.BoxSides.top;
					break;
				case FlipCubeGame.BoxSides.bottom:
					current = InformedSearch.BoxSides.bottom;
					break;
				case FlipCubeGame.BoxSides.right:
					current = InformedSearch.BoxSides.right;
					break;
				case FlipCubeGame.BoxSides.left:
					current = InformedSearch.BoxSides.left;
					break;
				case FlipCubeGame.BoxSides.back:
					current = InformedSearch.BoxSides.back;
					break;
				case FlipCubeGame.BoxSides.front:
					current = InformedSearch.BoxSides.front;
					break;
			}

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

			InformedSearch artificialIntelligenceSystem = new InformedSearch(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, InformedSearch.BoxSides.bottom, fieldTemp, fieldSize);
			artificialIntelligenceSystem.FindingWayAlgorithm1();
			textBox6.Text = artificialIntelligenceSystem.PathOutput();
		}

		async private void ButtonSolutionDemoAlgorithm1Click(object sender, EventArgs e)
		{
			InformedSearch.BoxSides current = InformedSearch.BoxSides.top;

			switch (flipCubeGame.CurrentDesiredColorSide)
			{
				case FlipCubeGame.BoxSides.top:
					current = InformedSearch.BoxSides.top;
					break;
				case FlipCubeGame.BoxSides.bottom:
					current = InformedSearch.BoxSides.bottom;
					break;
				case FlipCubeGame.BoxSides.right:
					current = InformedSearch.BoxSides.right;
					break;
				case FlipCubeGame.BoxSides.left:
					current = InformedSearch.BoxSides.left;
					break;
				case FlipCubeGame.BoxSides.back:
					current = InformedSearch.BoxSides.back;
					break;
				case FlipCubeGame.BoxSides.front:
					current = InformedSearch.BoxSides.front;
					break;
			}

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

			InformedSearch artificialIntelligenceSystem = new InformedSearch(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, InformedSearch.BoxSides.bottom, fieldTemp, fieldSize);
			artificialIntelligenceSystem.FindingWayAlgorithm1();
			textBox6.Text = artificialIntelligenceSystem.PathOutput();

			Queue<int> pathPanel = artificialIntelligenceSystem.GetWayPanel();
			Queue<InformedSearch.BoxSides> ColorSide = artificialIntelligenceSystem.GetWayColorSide();

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

				flipCubeGame.CurrentDesiredColorSide = (FlipCubeGame.BoxSides)ColorSide.Dequeue();

				field[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;
				field[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 4;
				field[x, y] = 2;
				flipCubeGame.СurrentPanelX = x;
				flipCubeGame.СurrentPanelY = y;

				await Task.Run(() => { gameField.Invalidate(); });
				await Task.Delay((int)numericUpDownTime2.Value);
			}
			ResetAll();
		}

		private void ButtonShowmapExploredPassagesAlgorithm1Click(object sender, EventArgs e)
		{
			InformedSearch.BoxSides current = InformedSearch.BoxSides.top;

			switch (flipCubeGame.CurrentDesiredColorSide)
			{
				case FlipCubeGame.BoxSides.top:
					current = InformedSearch.BoxSides.top;
					break;
				case FlipCubeGame.BoxSides.bottom:
					current = InformedSearch.BoxSides.bottom;
					break;
				case FlipCubeGame.BoxSides.right:
					current = InformedSearch.BoxSides.right;
					break;
				case FlipCubeGame.BoxSides.left:
					current = InformedSearch.BoxSides.left;
					break;
				case FlipCubeGame.BoxSides.back:
					current = InformedSearch.BoxSides.back;
					break;
				case FlipCubeGame.BoxSides.front:
					current = InformedSearch.BoxSides.front;
					break;
			}


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

			InformedSearch artificialIntelligenceSystem = new InformedSearch(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, InformedSearch.BoxSides.bottom, fieldTemp, fieldSize);
			List<int[,]> listMoves = artificialIntelligenceSystem.GetMapMovesAlgorithm1();
			List<int[,,]> listMovesInformation = artificialIntelligenceSystem.GetMapMovesInformationAlgorithm1();

			AlgorithmMoveMap algorithmMoveMap = new AlgorithmMoveMap(listMoves, flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, listMovesInformation);
			algorithmMoveMap.Show();
		}

		#endregion

		#region InformedSearchA* Algorithm2

		private void ButtonAlgorithm2Click(object sender, EventArgs e)
		{
			InformedSearch.BoxSides current = InformedSearch.BoxSides.top;

			switch (flipCubeGame.CurrentDesiredColorSide)
			{
				case FlipCubeGame.BoxSides.top:
					current = InformedSearch.BoxSides.top;
					break;
				case FlipCubeGame.BoxSides.bottom:
					current = InformedSearch.BoxSides.bottom;
					break;
				case FlipCubeGame.BoxSides.right:
					current = InformedSearch.BoxSides.right;
					break;
				case FlipCubeGame.BoxSides.left:
					current = InformedSearch.BoxSides.left;
					break;
				case FlipCubeGame.BoxSides.back:
					current = InformedSearch.BoxSides.back;
					break;
				case FlipCubeGame.BoxSides.front:
					current = InformedSearch.BoxSides.front;
					break;
			}

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

			fieldTemp[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 2;
			fieldTemp[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;

			InformedSearch artificialIntelligenceSystem = new InformedSearch(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, InformedSearch.BoxSides.bottom, fieldTemp, fieldSize);
			artificialIntelligenceSystem.FindingWayAlgorithm2();
			textBox5.Text = artificialIntelligenceSystem.PathOutput();
		}

		async private void ButtonSolutionDemoAlgorithm2Click(object sender, EventArgs e)
		{
			InformedSearch.BoxSides current = InformedSearch.BoxSides.top;

			switch (flipCubeGame.CurrentDesiredColorSide)
			{
				case FlipCubeGame.BoxSides.top:
					current = InformedSearch.BoxSides.top;
					break;
				case FlipCubeGame.BoxSides.bottom:
					current = InformedSearch.BoxSides.bottom;
					break;
				case FlipCubeGame.BoxSides.right:
					current = InformedSearch.BoxSides.right;
					break;
				case FlipCubeGame.BoxSides.left:
					current = InformedSearch.BoxSides.left;
					break;
				case FlipCubeGame.BoxSides.back:
					current = InformedSearch.BoxSides.back;
					break;
				case FlipCubeGame.BoxSides.front:
					current = InformedSearch.BoxSides.front;
					break;
			}

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

			InformedSearch artificialIntelligenceSystem = new InformedSearch(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, InformedSearch.BoxSides.bottom, fieldTemp, fieldSize);
			artificialIntelligenceSystem.FindingWayAlgorithm2();
			textBox5.Text = artificialIntelligenceSystem.PathOutput();

			Queue<int> pathPanel = artificialIntelligenceSystem.GetWayPanel();
			Queue<InformedSearch.BoxSides> ColorSide = artificialIntelligenceSystem.GetWayColorSide();

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

				flipCubeGame.CurrentDesiredColorSide = (FlipCubeGame.BoxSides)ColorSide.Dequeue();

				field[flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY] = 3;
				field[flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY] = 4;
				field[x, y] = 2;
				flipCubeGame.СurrentPanelX = x;
				flipCubeGame.СurrentPanelY = y;

				await Task.Run(() => { gameField.Invalidate(); });
				await Task.Delay((int)numericUpDownTime2.Value);
			}
			ResetAll();
		}

		private void ButtonShowmapExploredPassagesAlgorithm2Click(object sender, EventArgs e)
		{
			InformedSearch.BoxSides current = InformedSearch.BoxSides.top;

			switch (flipCubeGame.CurrentDesiredColorSide)
			{
				case FlipCubeGame.BoxSides.top:
					current = InformedSearch.BoxSides.top;
					break;
				case FlipCubeGame.BoxSides.bottom:
					current = InformedSearch.BoxSides.bottom;
					break;
				case FlipCubeGame.BoxSides.right:
					current = InformedSearch.BoxSides.right;
					break;
				case FlipCubeGame.BoxSides.left:
					current = InformedSearch.BoxSides.left;
					break;
				case FlipCubeGame.BoxSides.back:
					current = InformedSearch.BoxSides.back;
					break;
				case FlipCubeGame.BoxSides.front:
					current = InformedSearch.BoxSides.front;
					break;
			}

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

			InformedSearch artificialIntelligenceSystem = new InformedSearch(flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, current, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, InformedSearch.BoxSides.bottom, fieldTemp, fieldSize);
			List<int[,]> listMoves = artificialIntelligenceSystem.GetMapMovesAlgorithm2();
			List<int[,,]> listMovesInformation = artificialIntelligenceSystem.GetMapMovesInformationAlgorithm2();

			AlgorithmMoveMap algorithmMoveMap = new AlgorithmMoveMap(listMoves, flipCubeGame.СurrentPanelX, flipCubeGame.СurrentPanelY, flipCubeGame.FinishPanelX, flipCubeGame.FinishPanelY, listMovesInformation);
			algorithmMoveMap.Show();
		}

		#endregion

		private void domainUpDownSideRedFace_SelectedItemChanged(object sender, EventArgs e)
		{
			ResetAll();
		}
	}
}