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
        private int[,,] fieldInformation = new int[17, 17, 3];
        private int[,] field = new int[17, 17];
        Panel[,] panel = new Panel[17, 17];
        ToolTip[,] myToolTip = new ToolTip[17, 17];
        List<int[,,]> listMovesInformation;
        List<int[,]> listMoves;

        public AlgorithmMoveMap(List<int[,]> listMoves, int StartPanelX, int StartPanelY, int FinishPanelX, int FinishPanelY)
        {
            InitializeComponent();

            this.FinishPanelX = FinishPanelY;
            this.FinishPanelY = FinishPanelX;
            this.StartPanelX = StartPanelY;
            this.StartPanelY = StartPanelX;
            this.listMoves = listMoves;

            if (listMoves.Count > 0)
            {
                this.field = listMoves[listMoves.Count - 1];

                numericUpDownCurrentMove.Maximum = this.listMoves.Count - 1;
                trackBarCurrentMove.Maximum = this.listMoves.Count - 1;

                numericUpDownCurrentMove.Minimum = 1;
                trackBarCurrentMove.Minimum = 1;

                numericUpDownCurrentMove.Value = listMoves.Count - 1;
                trackBarCurrentMove.Value = listMoves.Count - 1;
            }
            else
            {
                numericUpDownCurrentMove.Maximum = 1;
                trackBarCurrentMove.Maximum = 1;

                numericUpDownCurrentMove.Minimum = 1;
                trackBarCurrentMove.Minimum = 1;

                numericUpDownCurrentMove.Value = 1;
                trackBarCurrentMove.Value = 1;
            }

            for (int i = 0; i < fieldSize; i++)
                for (int j = 0; j < fieldSize; j++)
                {
                    panel[i, j] = new Panel();
                    panel[i, j].Name = "Panel_" + Convert.ToString(j) + "_" + Convert.ToString(i);
                    panel[i, j].BorderStyle = BorderStyle.Fixed3D;

                    panel[i, j].Size = new Size(32, 32);
                    panel[i, j].Location = new Point(i * 32 + 2, j * 32 + 2);

                    panelMain.Controls.Add(panel[i, j]);
                }

            panel1.BackColor = Color.White;
            panel2.BackColor = Color.FromArgb(255, 156, 156);
            panel3.BackColor = Color.FromArgb(255, 85, 85);
            panel4.BackColor = Color.FromArgb(255, 26, 26);
            panel5.BackColor = Color.FromArgb(230, 0, 0);
            panel6.BackColor = Color.FromArgb(186, 0, 0);
            panel7.BackColor = Color.FromArgb(120, 0, 0);
            panel10.BackColor = Color.FromArgb(80, 0, 0);
            panel11.BackColor = Color.Black;

            panel[StartPanelY, StartPanelX].BackColor = Color.White;
            panel8.BackColor = Color.White;
            panel[StartPanelY, StartPanelX].Paint += new PaintEventHandler(this.StartDrawingPanel);
            panel8.Paint += new PaintEventHandler(this.StartDrawingPanel);
            panel[StartPanelY, StartPanelX].Invalidate();
            panel8.Invalidate();

            panel[FinishPanelY, FinishPanelX].BackColor = Color.White;
            panel9.BackColor = Color.White;
            panel[FinishPanelY, FinishPanelX].Paint += new PaintEventHandler(this.finishDrawingPanel);
            panel9.Paint += new PaintEventHandler(this.finishDrawingPanel);
            panel[FinishPanelY, FinishPanelX].Invalidate();
            panel9.Invalidate();

            panelMain.Invalidate();
        }

        public AlgorithmMoveMap(List<int[,]> listMoves, int StartPanelX, int StartPanelY, int FinishPanelX, int FinishPanelY, List<int[,,]> listMovesInformation)
        {
            InitializeComponent();

            this.FinishPanelX = FinishPanelY;
            this.FinishPanelY = FinishPanelX;
            this.StartPanelX = StartPanelY;
            this.StartPanelY = StartPanelX;
            this.listMoves = listMoves;
            this.listMovesInformation = listMovesInformation;

            if (this.listMoves.Count > 0 && this.listMovesInformation.Count > 0)
            {
                this.fieldInformation = listMovesInformation[listMovesInformation.Count - 1];
                this.field = listMoves[listMoves.Count - 1];

                numericUpDownCurrentMove.Maximum = this.listMoves.Count - 1;
                trackBarCurrentMove.Maximum = this.listMoves.Count - 1;

                numericUpDownCurrentMove.Minimum = 1;
                trackBarCurrentMove.Minimum = 1;

                numericUpDownCurrentMove.Value = listMoves.Count - 1;
                trackBarCurrentMove.Value = listMoves.Count - 1;
            }
            else
            {
                numericUpDownCurrentMove.Maximum = 1;
                trackBarCurrentMove.Maximum = 1;

                numericUpDownCurrentMove.Minimum = 1;
                trackBarCurrentMove.Minimum = 1;

                numericUpDownCurrentMove.Value = 1;
                trackBarCurrentMove.Value = 1;
            }

            if (listMovesInformation != null && listMovesInformation.Count > 0)
                for (int i = 0; i < fieldSize; i++)
                    for (int j = 0; j < fieldSize; j++)
                        myToolTip[i, j] = new ToolTip();


            for (int i = 0; i < fieldSize; i++)
                for (int j = 0; j < fieldSize; j++)
                {
                    panel[i, j] = new Panel();
                    panel[i, j].Name = "Panel_" + Convert.ToString(j) + "_" + Convert.ToString(i);
                    panel[i, j].BorderStyle = BorderStyle.Fixed3D;

                    panel[i, j].Size = new Size(32, 32);
                    panel[i, j].Location = new Point(i * 32 + 2, j * 32 + 2);

                    panelMain.Controls.Add(panel[i, j]);
                }

            panel1.BackColor = Color.White;
            panel2.BackColor = Color.FromArgb(255, 156, 156);
            panel3.BackColor = Color.FromArgb(255, 85, 85);
            panel4.BackColor = Color.FromArgb(255, 26, 26);
            panel5.BackColor = Color.FromArgb(230, 0, 0);
            panel6.BackColor = Color.FromArgb(186, 0, 0);
            panel7.BackColor = Color.FromArgb(120, 0, 0);
            panel10.BackColor = Color.FromArgb(80, 0, 0);
            panel11.BackColor = Color.Black;

            panel[StartPanelY, StartPanelX].BackColor = Color.White;
            panel8.BackColor = Color.White;
            panel[StartPanelY, StartPanelX].Paint += new PaintEventHandler(this.StartDrawingPanel);
            panel8.Paint += new PaintEventHandler(this.StartDrawingPanel);
            panel[StartPanelY, StartPanelX].Invalidate();
            panel8.Invalidate();

            panel[FinishPanelY, FinishPanelX].BackColor = Color.White;
            panel9.BackColor = Color.White;
            panel[FinishPanelY, FinishPanelX].Paint += new PaintEventHandler(this.finishDrawingPanel);
            panel9.Paint += new PaintEventHandler(this.finishDrawingPanel);
            panel[FinishPanelY, FinishPanelX].Invalidate();
            panel9.Invalidate();

            panelMain.Invalidate();
        }

        private void redrawPanel(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < fieldSize; i++)
                for (int j = 0; j < fieldSize; j++)
                {
                    if (field[j, i] == -1)
                        panel[i, j].BackColor = Color.Black;
                    else if (field[j, i] == 0)
                        panel[i, j].BackColor = Color.White;
                    else if (field[j, i] == 1)
                        panel[i, j].BackColor = Color.FromArgb(255, 156, 156);
                    else if (field[j, i] == 2)
                        panel[i, j].BackColor = Color.FromArgb(255, 85, 85);
                    else if (field[j, i] == 3)
                        panel[i, j].BackColor = Color.FromArgb(255, 26, 26);
                    else if (field[j, i] == 4)
                        panel[i, j].BackColor = Color.FromArgb(230, 0, 0);
                    else if (field[j, i] == 5)
                        panel[i, j].BackColor = Color.FromArgb(186, 0, 0);
                    else if (field[j, i] == 6)
                        panel[i, j].BackColor = Color.FromArgb(120, 0, 0);
                    else if (field[j, i] > 6)
                        panel[i, j].BackColor = Color.FromArgb(80, 0, 0);
                    else
                        panel[i, j].BackColor = Color.White;

                    if (listMovesInformation != null && listMovesInformation.Count > 0)
                    {
                        myToolTip[j, i].InitialDelay = 100;
                        myToolTip[j, i].UseAnimation = true;
                        myToolTip[j, i].ToolTipIcon = ToolTipIcon.Info;

                        int G = fieldInformation[j, i, 0];
                        int H = fieldInformation[j, i, 1];
                        int Value = fieldInformation[j, i, 2];

                        myToolTip[j, i].SetToolTip(panel[i, j], "Текущая стоимость пути (g): " + G.ToString() + Environment.NewLine + "Эвристическая оценка (h): " + H.ToString() + Environment.NewLine + "Ценность ячейки (f): " + Value.ToString() + Environment.NewLine);
                    }

                }

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

        private void numericUpDownCurrentMoveValueChanged(object sender, EventArgs e)
        {
            if (listMoves.Count <= 0)
                return;

            field = listMoves[Convert.ToInt32(numericUpDownCurrentMove.Value - 1)];

            if (listMovesInformation != null && listMovesInformation.Count > 0 && listMovesInformation.Count >= Convert.ToInt32(numericUpDownCurrentMove.Value - 1))
                fieldInformation = listMovesInformation[Convert.ToInt32(numericUpDownCurrentMove.Value - 1)];

            trackBarCurrentMove.Value = Convert.ToInt32(numericUpDownCurrentMove.Value);

            panelMain.Invalidate();
        }

        private void trackBarCurrentMoveScroll(object sender, EventArgs e)
        {
            if (listMoves.Count <= 0)
                return;

            field = listMoves[Convert.ToInt32(trackBarCurrentMove.Value - 1)];

            if (listMovesInformation != null && listMovesInformation.Count > 0 && listMovesInformation.Count >= Convert.ToInt32(trackBarCurrentMove.Value - 1))
                fieldInformation = listMovesInformation[Convert.ToInt32(trackBarCurrentMove.Value - 1)];

            numericUpDownCurrentMove.Value = trackBarCurrentMove.Value;

            panelMain.Invalidate();
        }

    }
}