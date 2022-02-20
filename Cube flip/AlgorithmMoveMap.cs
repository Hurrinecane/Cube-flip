using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cube_flip
{
    public partial class AlgorithmMoveMap : Form
    {
        private int FinishPanelX;
        private int FinishPanelY;
        private int StartPanelX;
        private int StartPanelY;
        private int fieldSize = 17;
        private int[,] field = new int[17, 17];
        Panel[,] panel = new Panel[17, 17];

        public AlgorithmMoveMap(int[,] field, int StartPanelX, int StartPanelY, int FinishPanelX, int FinishPanelY)
        {
            InitializeComponent();

            this.FinishPanelX = FinishPanelY;
            this.FinishPanelY = FinishPanelX;
            this.StartPanelX = StartPanelY;
            this.StartPanelY = StartPanelX;

            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    this.field[i, j] = field[i, j];
                }
            }

            panelMain.Controls.Clear();
            panelMain.Invalidate();
        }

        private void redrawPanel(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    panel[i, j] = new Panel();
                    panel[i, j].Name = "Panel_" + Convert.ToString(j) + "_" + Convert.ToString(i);
                    panel[i, j].BorderStyle = BorderStyle.Fixed3D;

                    if (field[j, i] == 0)
                    {
                        panel[i, j].BackColor = Color.White;
                    }
                    else if (field[j, i] == 1)
                    {
                        panel[i, j].BackColor = Color.FromArgb(140, 203, 94);
                    }
                    else if (field[j, i] == 2)
                    {
                        panel[i, j].BackColor = Color.FromArgb(119, 221, 231);
                    }
                    else if (field[j, i] == 3)
                    {
                        panel[i, j].BackColor = Color.FromArgb(42, 82, 190);
                    }
                    else if (field[j, i] == 4)
                    {
                        panel[i, j].BackColor = Color.FromArgb(255, 220, 51);
                    }
                    else if (field[j, i] == 5)
                    {
                        panel[i, j].BackColor = Color.FromArgb(255, 142, 14);
                    }
                    else if (field[j, i] == 6)
                    {
                        panel[i, j].BackColor = Color.FromArgb(255, 0, 0);
                    }
                    else 
                    {
                        panel[i, j].BackColor = Color.White;
                    }

                    panel[i, j].Size = new Size(32, 32);
                    panel[i, j].Location = new Point(i * 32 + 2, j * 32 + 2);

                    panelMain.Controls.Add(panel[i, j]);
                }
            }

            panel1.BackColor = Color.White;
            panel2.BackColor = Color.FromArgb(140, 203, 94);
            panel3.BackColor = Color.FromArgb(119, 221, 231);
            panel4.BackColor = Color.FromArgb(42, 82, 190);
            panel5.BackColor = Color.FromArgb(255, 220, 51);
            panel6.BackColor = Color.FromArgb(255, 142, 14);
            panel7.BackColor = Color.FromArgb(255, 0, 0);

            panel[StartPanelX, StartPanelY].BackColor = Color.White;
            panel8.BackColor = Color.White;
            panel[StartPanelX, StartPanelY].Paint += new PaintEventHandler(this.StartDrawingPanel);
            panel8.Paint += new PaintEventHandler(this.StartDrawingPanel);
            panel[StartPanelX, StartPanelY].Invalidate();
            panel8.Invalidate();

            panel[FinishPanelX, FinishPanelY].BackColor = Color.White;
            panel9.BackColor = Color.White;
            panel[FinishPanelX, FinishPanelY].Paint += new PaintEventHandler(this.finishDrawingPanel);
            panel9.Paint += new PaintEventHandler(this.finishDrawingPanel);
            panel[FinishPanelX, FinishPanelY].Invalidate();
            panel9.Invalidate();
        }

        private void StartDrawingPanel(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Green);
            pen.Width = 5;
            e.Graphics.FillRectangle(Brushes.Green, 2, 2, 24, 24);

            pen.Color = Color.Red;
            pen.Width = 2;
            e.Graphics.FillRectangle(Brushes.Red, 4, 4, 20, 20);
        }

        private void finishDrawingPanel(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Red);
            pen.Width = 3;
            e.Graphics.DrawEllipse(pen, 3, 3, 21, 21);
        }
    }
}