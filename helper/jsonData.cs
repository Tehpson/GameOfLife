namespace GameOfLife.helper
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class JsonData
    {
        /// <summary>
        /// Get Data from a jasonfile
        /// </summary>
        /// <param name="file">name of the file</param>
        /// <returns>Grid From saved file</returns>
        internal static List<List<int>> GetJsonData(string file)
        {
            var path = Directory.GetCurrentDirectory();
            path = Path.Combine(path, "Pattern", file);
            var obj = File.ReadAllText(path);
            var jsondata = (List<List<int>>)JsonConvert.DeserializeObject(obj, typeof(List<List<int>>));
            return jsondata;
        }

        /// <summary>
        ///save Json data to file under the Pattern folder
        /// </summary>
        /// <param name="jsondata">Data to Save</param>
        internal static void SaveToJson(string jsondata)
        {
            Console.WriteLine("Patternd Saved");
            var path = Directory.GetCurrentDirectory();
            var now = DateTime.Now;
            path = Path.Combine(path, "Pattern");
            Directory.CreateDirectory(path);
            path = Path.Combine(path, $"Pattern{now.Day}{now.Hour}{now.Minute}{now.Second}{now.Millisecond}");
            File.WriteAllText(path + ".json", jsondata);
        }

        /// <summary>
        /// take a list and insert to the gird list
        /// </summary>
        /// <param name="ListTOAddToGrid">list to overire Grid with</param>
        /// <param name="Grid">Game Grid</param>
        public static void NestedListToGrid(List<List<int>> ListTOAddToGrid, List<List<int>> Grid)
        {
            if (ListTOAddToGrid.Count == Grid.Count && ListTOAddToGrid[0].Count == Grid[0].Count)
            {
                return;
            }
            var gridYCount = Grid.Count;
            var gridXCount = Grid[0].Count;
            Grid.Clear();
            for (int i = 0; i < gridYCount; i++)
            {
                var temp = new List<int>();
                for (int y = 0; y < gridXCount; y++)
                {
                    if (ListTOAddToGrid.Count <= i || ListTOAddToGrid[0].Count <= y)
                    {
                        temp.Add(0);
                    }
                    else
                    {
                        temp.Add(ListTOAddToGrid[i][y]);
                    }
                }
                Grid.Add(temp);
            }
        }
    }
}