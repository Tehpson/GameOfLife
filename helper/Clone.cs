namespace GameOfLife.helper
{
    using System.Collections.Generic;

    public static class Clone
    {
        /// <summary>
        /// Clone the grid to another List
        /// </summary>
        /// <param name="ListToClone">List To Clone</param>
        /// <returns>a clone of the list that was inputed</returns>
        public static List<List<int>> CloneGrid(List<List<int>> ListToClone)
        {
            var tempList = new List<List<int>>();
            for (int y = 0; y < ListToClone.Count; y++)
            {
                var tempYList = new List<int>();
                for (int x = 0; x < ListToClone[y].Count; x++)
                {
                    tempYList.Add(ListToClone[y][x]);
                }
                tempList.Add(tempYList);
            }
            return tempList;
        }
    }
}