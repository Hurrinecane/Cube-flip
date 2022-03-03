﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using static Cube_flip.FlipCubeGame;

namespace Cube_flip
{
    partial class InformedSearch
    {
		private List<FieldCell> O1;
        private List<FieldCell> C1;
        private List<FieldCell> O2;
        private List<FieldCell> C2;
        private FieldCell startState;
		private FieldCell finalState;

        List<int[,,]> listMovesInformation;
        List<int[,]> fieldMapMoves;

        private int fieldSize;
		private int[,] field;

		private bool noExit = true;

		public InformedSearch(int startStatePanelX, int startStatePanelY, BoxSides startStateSide, int finalStatePanelX, int finalStatePanelY, BoxSides finalStateSide, int[,] field, int fieldSize)
		{
			startState = new FieldCell(startStatePanelX, startStatePanelY, startStateSide);
            startState.H = 0;
            startState.G = 0;
            finalState = new FieldCell(finalStatePanelX, finalStatePanelY, finalStateSide);
            finalState.H = 0;
            finalState.G = 0;

            this.fieldSize = fieldSize;

			this.field = new int[this.fieldSize, this.fieldSize];
            this.field = field;
        }

        public void FindingWayAlgorithm1()
        {
            noExit = true;

            O1 = new List<FieldCell>();
            C1 = new List<FieldCell>();

            O1.Add(startState);

            while (O1.Count > 0)
            {
                FieldCell temp = GetMinimumValue(O1);
                O1.Remove(temp);

                if (temp == finalState)
                {
                    finalState.from = temp.from;
                    C1.Add(temp);
                    noExit = false;
                    return;
                }

                foreach (FieldCell p in MovesAlgorithm1(temp))
                    if (!O1.Contains(p) && !C1.Contains(p))
                        O1.Add(p);
                    else if (O1.Contains(p))
                    {
                       int index = O1.IndexOf(p);

                        FieldCell similar = O1[index];

                        if (p.Value < similar.Value)
                        {
                            O1.Remove(similar);
                            O1.Add(p);
                        }
                    }
                    else if (C1.Contains(p))
                    {
                        int index = C1.IndexOf(p);

                        FieldCell similar = C1[index];

                        if (p.Value < similar.Value)
                        {
                            C1.Remove(similar);
                            O1.Add(p);
                        }
                    }

                C1.Add(temp);
            }

            MessageBox.Show("К выбранной цели нет пути!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void FindingWayAlgorithm2()
        {
            noExit = true;

            O2 = new List<FieldCell>();
            C2 = new List<FieldCell>();

            O2.Add(startState);

            while (O2.Count > 0)
            {
                FieldCell temp = GetMinimumValue(O2);
                O2.Remove(temp);

                if (temp == finalState)
                {
                    finalState.from = temp.from;
                    C2.Add(temp);
                    noExit = false;
                    return;
                }

                foreach (FieldCell p in MovesAlgorithm2(temp))
                    if (!O2.Contains(p) && !C2.Contains(p))
                        O2.Add(p);
                    else if (O2.Contains(p))
                    {
                        int index = O2.IndexOf(p);

                        FieldCell similar = O2[index];

                        if (p.Value < similar.Value)
                        {
                            O2.Remove(similar);
                            O2.Add(p);
                        }
                    }
                    else if (C2.Contains(p))
                    {
                        int index = C2.IndexOf(p);

                        FieldCell similar = C2[index];

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

        private List<FieldCell> MovesAlgorithm1(FieldCell currentPosition)
        {
            List<FieldCell> way = new List<FieldCell>();

            FieldCell temporaryWay;

            int x = currentPosition.GetX;
            int y = currentPosition.GetY;

            if (x != 16)
                if (field[x + 1, y] == 1 || field[x + 1, y] == 3)
                {
                    temporaryWay = new FieldCell(x + 1, y, ChangeCurrentSide(FlipDirection.down, currentPosition.GetSide));
                    temporaryWay.from = currentPosition;
                    int h = GetHeuristicValueAlgorithm1(temporaryWay.GetX, temporaryWay.GetY, finalState.GetX, finalState.GetY);
                    temporaryWay.H = h;
                    temporaryWay.G = currentPosition.G + 1;
                    way.Add(temporaryWay);
                }


            if (x != 0)
                if (field[x - 1, y] == 1 || field[x - 1, y] == 3)
                {
                    temporaryWay = new FieldCell(x - 1, y, ChangeCurrentSide(FlipDirection.up, currentPosition.GetSide));
                    temporaryWay.from = currentPosition;
                    int h = GetHeuristicValueAlgorithm1(temporaryWay.GetX, temporaryWay.GetY, finalState.GetX, finalState.GetY);
                    temporaryWay.H = h;
                    temporaryWay.G = currentPosition.G + 1;
                    way.Add(temporaryWay);
                }

            if (y != 16)
                if (field[x, y + 1] == 1 || field[x, y + 1] == 3)
                {
                    temporaryWay = new FieldCell(x, y + 1, ChangeCurrentSide(FlipDirection.right, currentPosition.GetSide));
                    temporaryWay.from = currentPosition;
                    int h = GetHeuristicValueAlgorithm1(temporaryWay.GetX, temporaryWay.GetY, finalState.GetX, finalState.GetY);
                    temporaryWay.H = h;
                    temporaryWay.G = currentPosition.G + 1;
                    way.Add(temporaryWay);
                }

            if (y != 0)
                if (field[x, y - 1] == 1 || field[x, y - 1] == 3)
                {
                    temporaryWay = new FieldCell(x, y - 1, ChangeCurrentSide(FlipDirection.left, currentPosition.GetSide));
                    temporaryWay.from = currentPosition;
                    int h = GetHeuristicValueAlgorithm1(temporaryWay.GetX, temporaryWay.GetY, finalState.GetX, finalState.GetY);
                    temporaryWay.H = h;
                    temporaryWay.G = currentPosition.G + 1;
                    way.Add(temporaryWay);
                }

            return way;
        }

        private List<FieldCell> MovesAlgorithm2(FieldCell currentPosition)
        {
            List<FieldCell> way = new List<FieldCell>();

            FieldCell temporaryWay;

            int x = currentPosition.GetX;
            int y = currentPosition.GetY;

            if (x != 16)
                if (field[x + 1, y] == 1 || field[x + 1, y] == 3)
                {
                    temporaryWay = new FieldCell(x + 1, y, ChangeCurrentSide(FlipDirection.down, currentPosition.GetSide));
                    temporaryWay.from = currentPosition;
                    int h = GetHeuristicValueAlgorithm2(temporaryWay.GetX, temporaryWay.GetY, finalState.GetX, finalState.GetY, finalState.GetSide, temporaryWay.GetSide);
                    temporaryWay.H = h;
                    temporaryWay.G = currentPosition.G + 1;
                    way.Add(temporaryWay);
                }


            if (x != 0)
                if (field[x - 1, y] == 1 || field[x - 1, y] == 3)
                {
                    temporaryWay = new FieldCell(x - 1, y, ChangeCurrentSide(FlipDirection.up, currentPosition.GetSide));
                    temporaryWay.from = currentPosition;
                    int h = GetHeuristicValueAlgorithm2(temporaryWay.GetX, temporaryWay.GetY, finalState.GetX, finalState.GetY, finalState.GetSide, temporaryWay.GetSide);
                    temporaryWay.H = h;
                    temporaryWay.G = currentPosition.G + 1;
                    way.Add(temporaryWay);
                }

            if (y != 16)
                if (field[x, y + 1] == 1 || field[x, y + 1] == 3)
                {
                    temporaryWay = new FieldCell(x, y + 1, ChangeCurrentSide(FlipDirection.right, currentPosition.GetSide));
                    temporaryWay.from = currentPosition;
                    int h = GetHeuristicValueAlgorithm2(temporaryWay.GetX, temporaryWay.GetY, finalState.GetX, finalState.GetY, finalState.GetSide, temporaryWay.GetSide);
                    temporaryWay.H = h;
                    temporaryWay.G = currentPosition.G + 1;
                    way.Add(temporaryWay);
                }

            if (y != 0)
                if (field[x, y - 1] == 1 || field[x, y - 1] == 3)
                {
					temporaryWay = new FieldCell(x, y - 1, ChangeCurrentSide(FlipDirection.left, currentPosition.GetSide))
					{
						from = currentPosition
					};
					int h = GetHeuristicValueAlgorithm2(temporaryWay.GetX, temporaryWay.GetY, finalState.GetX, finalState.GetY, finalState.GetSide, temporaryWay.GetSide);
                    temporaryWay.H = h;
                    temporaryWay.G = currentPosition.G + 1;
                    way.Add(temporaryWay);
                }

            return way;
        }

        private int GetHeuristicValueAlgorithm1(int startX, int startY, int finalX, int finalY)
        {
            return (Math.Abs(startX - finalX) + Math.Abs(startY - finalY));
        }

        private int GetHeuristicValueAlgorithm2(int startX, int startY, int finalX, int finalY, BoxSides finalStateSide, BoxSides currentStateSide)
        {
            int requiredTurns = 0;

            switch (finalStateSide)
            {
                case BoxSides.bottom:
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
                    break;
            }
                    
            return requiredTurns;
        }

        private FieldCell GetMinimumValue(List<FieldCell> O1)
        {
            FieldCell result = O1[0];

            for (int i = 0; i < O1.Count - 1; i++) 
                if (O1[i] < result) 
                    result = O1[i];

            return result;
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

        public string GetStatisticsAlgorithm1()
        {
            string statistics = "";

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            FindingWayAlgorithm1();
            stopWatch.Stop();
            statistics += "Время работы алгоритма: " + Convert.ToString(stopWatch.Elapsed) + Environment.NewLine;

            Queue<int> pathPanel = GetWayPanel();
            statistics += "Количество ходов: " + Convert.ToString((pathPanel.Count - 2) / 2) + Environment.NewLine;

            statistics += "Количество перебранных вариантов (C): " + Convert.ToString(C1.Count) + Environment.NewLine;

            statistics += "Количество путей на рассмотрение (O): " + Convert.ToString(O1.Count) + Environment.NewLine;

            return statistics;
        }

        public string GetStatisticsAlgorithm2()
        {
            string statistics = "";

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            FindingWayAlgorithm2();
            stopWatch.Stop();
            statistics += "Время работы алгоритма: " + Convert.ToString(stopWatch.Elapsed) + Environment.NewLine;

            Queue<int> pathPanel = GetWayPanel();
            statistics += "Количество ходов: " + Convert.ToString((pathPanel.Count - 2) / 2) + Environment.NewLine;

            statistics += "Количество перебранных вариантов (C): " + Convert.ToString(C2.Count) + Environment.NewLine;

            statistics += "Количество путей на рассмотрение (O): " + Convert.ToString(O2.Count) + Environment.NewLine;

            return statistics;
        }

        public void FindingMovesWayAlgorithm1()
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

            O1 = new List<FieldCell>();
            C1 = new List<FieldCell>();

            O1.Add(startState);

            while (O1.Count > 0)
            {
                FieldCell temp = GetMinimumValue(O1);
                O1.Remove(temp);

                if (temp == finalState)
                {
                    finalState.from = temp.from;
                    C1.Add(temp);
                    noExit = false;
                    return;
                }

                foreach (FieldCell p in MovesAlgorithm1(temp))
                    if (!O1.Contains(p) && !C1.Contains(p))
                    {
                        O1.Add(p);
                        fieldMapMovesСurrent[p.GetX, p.GetY]++;
                        fieldMapMovesСurrentInformation[p.GetX, p.GetY, 0] = p.G;
                        fieldMapMovesСurrentInformation[p.GetX, p.GetY, 1] = p.H;
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
                    else if (O1.Contains(p))
                    {
                        int index = O1.IndexOf(p);

                        FieldCell similar = O1[index];

                        if (p.Value < similar.Value)
                        {
                            O1.Remove(similar);
                            O1.Add(p);

                            fieldMapMovesСurrent[p.GetX, p.GetY]++;
                            fieldMapMovesСurrentInformation[p.GetX, p.GetY, 0] = p.G;
                            fieldMapMovesСurrentInformation[p.GetX, p.GetY, 1] = p.H;
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
                    else if (C1.Contains(p))
                    {
                        int index = C1.IndexOf(p);

                        FieldCell similar = C1[index];

                        if (p.Value < similar.Value)
                        {
                            C1.Remove(similar);
                            O1.Add(p);

                            fieldMapMovesСurrent[p.GetX, p.GetY]++;
                            fieldMapMovesСurrentInformation[p.GetX, p.GetY, 0] = p.G;
                            fieldMapMovesСurrentInformation[p.GetX, p.GetY, 1] = p.H;
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

                C1.Add(temp);
            }

            MessageBox.Show("К выбранной цели нет пути!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public List<int[,]> GetMapMovesAlgorithm1()
        {
            FindingMovesWayAlgorithm1();
            return fieldMapMoves;
        }

        public List<int[,,]> GetMapMovesInformationAlgorithm1()
        {
            FindingMovesWayAlgorithm1();
            return listMovesInformation;
        }

        public void FindingMovesWayAlgorithm2()
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

            O2 = new List<FieldCell>();
            C2 = new List<FieldCell>();

            O2.Add(startState);

            while (O2.Count > 0)
            {
                FieldCell temp = GetMinimumValue(O2);
                O2.Remove(temp);

                if (temp == finalState)
                {
                    finalState.from = temp.from;
                    C2.Add(temp);
                    noExit = false;
                    return;
                }

                foreach (FieldCell p in MovesAlgorithm2(temp))
                    if (!O2.Contains(p) && !C2.Contains(p))
                    {
                        O2.Add(p);
                        fieldMapMovesСurrent[p.GetX, p.GetY]++;
                        fieldMapMovesСurrentInformation[p.GetX, p.GetY, 0] = p.G;
                        fieldMapMovesСurrentInformation[p.GetX, p.GetY, 1] = p.H;
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

                        FieldCell similar = O2[index];

                        if (p.Value < similar.Value)
                        {
                            O2.Remove(similar);
                            O2.Add(p);

                            fieldMapMovesСurrent[p.GetX, p.GetY]++;
                            fieldMapMovesСurrentInformation[p.GetX, p.GetY, 0] = p.G;
                            fieldMapMovesСurrentInformation[p.GetX, p.GetY, 1] = p.H;
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

                        FieldCell similar = C2[index];

                        if (p.Value < similar.Value)
                        {
                            C2.Remove(similar);
                            O2.Add(p);

                            fieldMapMovesСurrent[p.GetX, p.GetY]++;
                            fieldMapMovesСurrentInformation[p.GetX, p.GetY, 0] = p.G;
                            fieldMapMovesСurrentInformation[p.GetX, p.GetY, 1] = p.H;
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

        public List<int[,]> GetMapMovesAlgorithm2()
        {
            FindingMovesWayAlgorithm2();
            return fieldMapMoves;
        }

        public List<int[,,]> GetMapMovesInformationAlgorithm2()
        {
            FindingMovesWayAlgorithm2();
            return listMovesInformation;
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