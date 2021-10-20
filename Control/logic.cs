namespace GameOfLife.Control
{
    using System;
    using System.Collections.Generic;

    public class Logic
    {
        public List<List<int>> Grid { get; set; } = new List<List<int>>();

        /// <summary>
        /// Fills the grid if CustomGridOn == true: fills empty grid else fills renodm grid
        /// </summary>
        /// <param name="boxY">boxes in y</param>
        /// <param name="boxX">boxes in x</param>
        /// <param name="empty"> true = emptyboard false = rendom. Defould flase</param>
        /// <param name="Denisty">1:? ratio of alive Defualt: 8</param>
        public Logic(int boxY, int boxX, bool empty = false, int Denisty = 8)
        {
            boxY = boxY <= 0 ? 1 : boxY;
            boxX = boxX <= 0 ? 1 : boxX;
            Grid.Clear();

            var rnd = new Random();
            for (int y = 0; y < boxY; y++)
            {
                var tmplist = new List<int>();
                for (int x = 0; x < boxX; x++)
                {
                    tmplist.Add(empty ? 0 : rnd.Next(0, Denisty) == 0 ? 1 : 0);
                }
                Grid.Add(tmplist);
            }
        }

        /// <summary>
        /// turns box live at given position
        /// </summary>
        /// <param name="posY">pos Y</param>
        /// <param name="posX">pos X</param>
        public void CustomGrid(int posY, int posX)
        {
            if (posX < 0 || posY < 0 || posX > Grid[0].Count || posY > Grid.Count) return;
            Grid[posY][posX] = Grid[posY][posX] == 0 ? 1 : 0;
        }

        /// <summary>
        /// Update and go to next Generation
        /// </summary>
        public virtual void NextGeneration()
        {
            var tempGrid = helper.Clone.CloneGrid(Grid);
            for (int y = 0; y < Grid.Count; y++)
            {
                for (int x = 0; x < Grid[y].Count; x++)
                {
                    var aliveCount = CheackAliveAround(x, y);
                    if (Grid[y][x] == 0 && aliveCount == 3)
                    {
                        tempGrid[y][x] = 1;
                    }
                    else if (Grid[y][x] == 1)
                    {
                        if (aliveCount < 2 || aliveCount > 3) tempGrid[y][x] = 0;
                    }
                }
            }
            Grid = tempGrid;
        }

        /// <summary>
        /// Cehack the amount of living cells around given cell
        /// </summary>
        /// <param name="x">pos X</param>
        /// <param name="y">pos Y</param>
        /// <returns>number of celse around that is alive</returns>
        private int CheackAliveAround(int x, int y)
        {
            var aliveCount = 0;
            var rowUp = y - 1 < 0 ? Grid.Count - 1 : y - 1;
            var rowDown = y + 1 > Grid.Count - 1 ? 0 : y + 1;
            var sideLeft = x - 1 < 0 ? Grid[y].Count - 1 : x - 1;
            var sideRigth = x + 1 > Grid[y].Count - 1 ? 0 : x + 1;

            if (Grid[rowUp][sideLeft] == 1) aliveCount++;
            if (Grid[rowUp][x] == 1) aliveCount++;
            if (Grid[rowUp][sideRigth] == 1) aliveCount++;
            if (Grid[y][sideLeft] == 1) aliveCount++;
            if (Grid[y][sideRigth] == 1) aliveCount++;
            if (Grid[rowDown][sideLeft] == 1) aliveCount++;
            if (Grid[rowDown][x] == 1) aliveCount++;
            if (Grid[rowDown][sideRigth] == 1) aliveCount++;

            return aliveCount;
        }
    }
}