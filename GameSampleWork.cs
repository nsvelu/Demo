using System;
using System.Threading.Tasks;

namespace GameSampleWork
{
    public class LifeSimulation
    {
        private int iHeigth;
        private int iWidth;
        private bool[,] cells;

        public LifeSimulation(int iLifeHeigth, int iLifeWidth)
        {
            this.iHeigth = iLifeHeigth;
            this.iWidth = iLifeWidth;
            cells = new bool[iLifeWidth, iLifeWidth];
            GenerateRandField();
        }

        public void DrawGrow()
        {
            DrawGame();
            Grow();
        }

       
        private void Grow()
        {
            for (int iRow = 0; iRow < iHeigth; iRow++)
            {
                for (int iCol = 0; iCol < iWidth; iCol++)
                {
                    int numOfAliveNeighbors = GetNeighbors(iRow, iCol);

                    if (cells[iRow, iCol])
                    {
                        if (numOfAliveNeighbors < 2)
                        {
                            cells[iRow, iCol] = false;
                        }

                        if (numOfAliveNeighbors > 3)
                        {
                            cells[iRow, iCol] = false;
                        }
                    }
                    else
                    {
                        if (numOfAliveNeighbors == 3)
                        {
                            cells[iRow, iCol] = true;
                        }
                    }
                }
            }
        }

              
        private int GetNeighbors(int iX, int iY)
        {
            int iNumOfAlive = 0;

            for (int iRow = iX - 1; iRow < iX + 2; iRow++)
            {
                for (int iCol = iY - 1; iCol < iY + 2; iCol++)
                {
                    if (!((iRow < 0 || iCol < 0) || (iRow >= iHeigth || iCol >= iWidth)))
                    {
                        if (cells[iRow, iCol] == true) iNumOfAlive++;
                    }
                }
            }
            return iNumOfAlive;
        }

        
        private void DrawGame()
        {
            for (int iRow = 0; iRow < iHeigth; iRow++)
            {
                for (int iCol = 0; iCol < iWidth; iCol++)
                {
                    Console.Write(cells[iRow, iCol] ? "x" : " ");
                    if (iCol == iWidth - 1) Console.WriteLine("\r");
                }
            }
            Console.SetCursorPosition(0, Console.WindowTop);
        }

        
        private void GenerateRandField()
        {
            Random generator = new Random();
            int iNum;
            for (int iRow = 0; iRow < iHeigth; iRow++)
            {
                for (int iCol = 0; iCol < iWidth; iCol++)
                {
                    iNum = generator.Next(2);
                    cells[iRow, iCol] = ((iNum == 0) ? false : true);
                }
            }
        }
    }

    internal class Program
    {
        // Constants for the game rules.
        private const int iConHeigth = 25;
        private const int iConWidth = 25;
        private const uint iMaxRuns =50 ;

        private static void Main(string[] args)
        {
            int iRuns = 0;
            LifeSimulation objSim = new LifeSimulation(iConHeigth, iConWidth);

            while (iRuns++ < iMaxRuns)
            {
                objSim.DrawGrow();
               System.Threading.Thread.Sleep(100);
            }
        }
    }
}
