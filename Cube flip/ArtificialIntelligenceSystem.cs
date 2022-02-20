using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Cube_flip
{
    class ArtificialIntelligenceSystem
    {
        private Queue<Field> OW;
        private Stack<Field> OD;
        private Queue<Field> CW;
        private Queue<Field> CD;
        private Field startState;
        private Field finalState;

        private int[,] field = new int[17, 17];
        private int[,] fieldMapMoves = new int[17, 17];

        private bool noExit = true;

        public enum desiredColorSide
        {
            left,
            bottom,
            top,
            right,
            straight,
            behind
        }

        public enum turningSide
        {
            left,
            bottom,
            top,
            right
        }

        public ArtificialIntelligenceSystem(int startStatePanelX, int startStatePanelY, desiredColorSide startStateColorSide, int finalStatePanelX, int finalStatePanelY, desiredColorSide finalStateColorSide, int[,] field)
        {
            startState = new Field(startStatePanelX, startStatePanelY, startStateColorSide);
            finalState = new Field(finalStatePanelX, finalStatePanelY, finalStateColorSide);

            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    this.field[i, j] = field[i, j];
                }
            }

            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    fieldMapMoves[i, j] = 0;
                }
            }
        }

        public void findingWayWidth()
        {
            noExit = true;

            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    fieldMapMoves[i, j] = 0;
                }
            }

            OW = new Queue<Field>();
            CW = new Queue<Field>();

            OW.Enqueue(startState);

            while (OW.Count > 0)
            {
                Field temp = OW.Dequeue();

                if (temp == finalState)
                {
                    finalState.from = temp.from;
                    CW.Enqueue(temp);
                    noExit = false;
                    return;
                }

                foreach (Field p in moves(temp))
                {
                    if (!CW.Contains(p) && !OW.Contains(p))
                    {
                        OW.Enqueue(p);
                        fieldMapMoves[p.CurrentPanelX, p.CurrentPanelY]++;
                    }
                }

                CW.Enqueue(temp);
            }

            MessageBox.Show("К выбранной цели нет пути!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void findingWayDepth()
        {
            noExit = true;

            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    fieldMapMoves[i, j] = 0;
                }
            }

            OD = new Stack<Field>();
            CD = new Queue<Field>();

            OD.Push(startState);

            while (OD.Count > 0)
            {
                Field temp = OD.Pop();

                if (temp == finalState)
                {
                    finalState.from = temp.from;
                    CD.Enqueue(temp);
                    noExit = false;
                    return;
                }

                foreach (Field p in moves(temp))
                {
                    if (!CD.Contains(p) && !OD.Contains(p))
                    {
                        OD.Push(p);
                        fieldMapMoves[p.CurrentPanelX, p.CurrentPanelY]++;
                    }
                }

                CD.Enqueue(temp);
            }

            MessageBox.Show("К выбранной цели нет пути!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private Queue<Field> moves(Field currentPosition)
        {
            Queue<Field> way = new Queue<Field>();

            Field temporaryWay1;
            Field temporaryWay2;
            Field temporaryWay3;
            Field temporaryWay4;

            int x = currentPosition.CurrentPanelX;
            int y = currentPosition.CurrentPanelY;

            if (x != 16)
            {
                if (field[x + 1, y] == 1 || field[x + 1, y] == 3)
                {
                    temporaryWay1 = new Field(x + 1, y, changeCurrentColor(turningSide.bottom, currentPosition.CurrentDesiredColorSide));
                    temporaryWay1.from = currentPosition;
                    way.Enqueue(temporaryWay1);
                }
            }

            if (x != 0)
            {
                if (field[x - 1, y] == 1 || field[x - 1, y] == 3)
                {
                    temporaryWay2 = new Field(x - 1, y, changeCurrentColor(turningSide.top, currentPosition.CurrentDesiredColorSide));
                    temporaryWay2.from = currentPosition;
                    way.Enqueue(temporaryWay2);
                }
            }

            if (y != 16)
            {
                if (field[x, y + 1] == 1 || field[x, y + 1] == 3)
                {
                    temporaryWay3 = new Field(x, y + 1, changeCurrentColor(turningSide.right, currentPosition.CurrentDesiredColorSide));
                    temporaryWay3.from = currentPosition;
                    way.Enqueue(temporaryWay3);
                }
            }

            if (y != 0)
            {
                if (field[x, y - 1] == 1 || field[x, y - 1] == 3)
                {
                    temporaryWay4 = new Field(x, y - 1, changeCurrentColor(turningSide.left, currentPosition.CurrentDesiredColorSide));
                    temporaryWay4.from = currentPosition;
                    way.Enqueue(temporaryWay4);
                }
            }

            return way;
        }

        public String pathOutput()
        {
            String receivedPath = "";

            if (noExit)
            {
                return receivedPath = "К выбранной цели нет пути!";
            }

            Stack<Field> way = new Stack<Field>();
            Field temp = finalState;

            while (temp != startState)
            {
                way.Push(temp);
                temp = temp.from;
            }
            way.Push(temp);

            int counter = 0;
            while (way.Count > 0)
            {
                temp = way.Pop();
                receivedPath += "Ход " + Convert.ToString(counter) + ". Позиция: " + Convert.ToString(temp.CurrentPanelX) + "," + Convert.ToString(temp.CurrentPanelY) + Environment.NewLine + "Красная сторона куба: " + Convert.ToString(temp.CurrentDesiredColorSide) + Environment.NewLine + Environment.NewLine;
                counter++;
            }

            return receivedPath;
        }

        public Queue<int> getWayPanel()
        {
            Stack<Field> way = new Stack<Field>();
            Queue<int> pathPanel = new Queue<int>();
            Field temp = finalState;

            if (noExit)
            {
               return pathPanel;
            }

            while (temp != startState)
            {
                way.Push(temp);
                temp = temp.from;
            }
            way.Push(temp);

            while (way.Count > 0)
            {
                temp = way.Pop();
                pathPanel.Enqueue(temp.CurrentPanelX);
                pathPanel.Enqueue(temp.CurrentPanelY);
            }

            return pathPanel;
        }

        public Queue<desiredColorSide> getWayColorSide()
        {
            Stack<Field> way = new Stack<Field>();
            Queue<desiredColorSide> pathPanel = new Queue<desiredColorSide>();
            Field temp = finalState;

            if (noExit)
            {
                return pathPanel;
            }

            while (temp != startState)
            {
                way.Push(temp);
                temp = temp.from;
            }
            way.Push(temp);

            while (way.Count > 0)
            {
                temp = way.Pop();
                pathPanel.Enqueue(temp.CurrentDesiredColorSide);
            }

            return pathPanel;
        }

        public String getStatisticsWidth()
        {
            String statistics = "";

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            findingWayWidth();
            stopWatch.Stop();
            statistics += "Время работы алгоритма: " + Convert.ToString(stopWatch.Elapsed) + Environment.NewLine;

            Queue<int> pathPanel = getWayPanel();
            statistics += "Количество ходов: " + Convert.ToString((pathPanel.Count - 2) / 2) + Environment.NewLine;

            statistics += "Количество перебранных вариантов: " + Convert.ToString(CW.Count) + Environment.NewLine;

            return statistics;
        }

        public String getStatisticsDepth()
        {
            String statistics = "";

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            findingWayDepth();
            stopWatch.Stop();
            statistics += "Время работы алгоритма: " + Convert.ToString(stopWatch.Elapsed) + Environment.NewLine;

            Queue<int> pathPanel = getWayPanel();
            statistics += "Количество ходов: " + Convert.ToString((pathPanel.Count - 2) / 2) + Environment.NewLine;

            statistics += "Количество перебранных вариантов: " + Convert.ToString(CD.Count) + Environment.NewLine;

            return statistics;
        }

        public int[,] getMapMovesWidth()
        {
            findingWayWidth();
            return fieldMapMoves;
        }

        public int[,] getMapMovesDepth()
        {
            findingWayDepth();
            return fieldMapMoves;
        }

        private class Field : IEquatable<Field>
        {
            private int currentPanelX;
            private int currentPanelY;
            private desiredColorSide currentDesiredColorSide;
            public Field from = null;

            public int CurrentPanelX
            {
                get
                {
                    return currentPanelX;
                }
            }

            public int CurrentPanelY
            {
                get
                {
                    return currentPanelY;
                }
            }

            public desiredColorSide CurrentDesiredColorSide
            {
                get
                {
                    return currentDesiredColorSide;
                }
            }

            public Field(int currentPanelX, int currentPanelY, desiredColorSide currentDesiredColorSide)
            {
                this.currentPanelX = currentPanelX;
                this.currentPanelY = currentPanelY;
                this.currentDesiredColorSide = currentDesiredColorSide;
            }

            public static bool operator ==(Field A, Field B)
            {
                if (A.currentPanelX == B.currentPanelX)
                {
                    if (A.currentPanelY == B.currentPanelY)
                    {
                        if (A.currentDesiredColorSide == B.currentDesiredColorSide)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            public static bool operator !=(Field A, Field B)
            {
                if (A.currentPanelX != B.currentPanelX)
                {
                    return true;
                }
                else if (A.currentPanelY != B.currentPanelY)
                {
                    return true;
                }
                else if (A.currentDesiredColorSide != B.currentDesiredColorSide)
                {
                    return true;
                }

                return false;
            }

            public bool Equals(Field other)
            {
                if (this.currentPanelX == other.currentPanelX && this.currentPanelY == other.currentPanelY && this.currentDesiredColorSide == other.currentDesiredColorSide) return true;
                else return false;
            }
        }

        private desiredColorSide changeCurrentColor(turningSide receivedTurningSide, desiredColorSide currentDesiredColorSide)
        {
            switch (receivedTurningSide)
            {
                case turningSide.left:
                    switch (currentDesiredColorSide)
                    {
                        case desiredColorSide.top:
                            return desiredColorSide.left;
                        case desiredColorSide.left:
                            return desiredColorSide.bottom;
                        case desiredColorSide.right:
                            return desiredColorSide.top;
                        case desiredColorSide.bottom:
                            return desiredColorSide.right;
                        case desiredColorSide.straight:
                            return desiredColorSide.straight;
                        case desiredColorSide.behind:
                            return desiredColorSide.behind;
                    }
                    break;
                case turningSide.right:
                    switch (currentDesiredColorSide)
                    {
                        case desiredColorSide.top:
                            return desiredColorSide.right;
                        case desiredColorSide.left:
                            return desiredColorSide.top;
                        case desiredColorSide.right:
                            return desiredColorSide.bottom;
                        case desiredColorSide.bottom:
                            return desiredColorSide.left;
                        case desiredColorSide.straight:
                            return desiredColorSide.straight;
                        case desiredColorSide.behind:
                            return desiredColorSide.behind;
                    }
                    break;
                case turningSide.top:
                    switch (currentDesiredColorSide)
                    {
                        case desiredColorSide.top:
                            return desiredColorSide.straight;
                        case desiredColorSide.left:
                            return desiredColorSide.left;
                        case desiredColorSide.right:
                            return desiredColorSide.right;
                        case desiredColorSide.bottom:
                            return desiredColorSide.behind;
                        case desiredColorSide.straight:
                            return desiredColorSide.bottom;
                        case desiredColorSide.behind:
                            return desiredColorSide.top;
                    }
                    break;
                case turningSide.bottom:
                    switch (currentDesiredColorSide)
                    {
                        case desiredColorSide.top:
                            return desiredColorSide.behind;
                        case desiredColorSide.left:
                            return desiredColorSide.left;
                        case desiredColorSide.right:
                            return desiredColorSide.right;
                        case desiredColorSide.bottom:
                            return desiredColorSide.straight;
                        case desiredColorSide.straight:
                            return desiredColorSide.top;
                        case desiredColorSide.behind:
                            return desiredColorSide.bottom;
                    }
                    break;
            }

            return desiredColorSide.top;
        }
    }
}