namespace GameOfLife.View
{
    using Raylib;

    public static class DrawTableClass
    {
        /// <summary>
        /// MOST BE CALD AFTER Raylib.InitWindow()!!!!
        /// draws the game table
        /// </summary>
        /// <param name="BoxX">how many boxes there an in y</param>
        /// <param name="BoxY">how many boxes there are in x</param>
        /// <param name="boxWidth">width of the boxes</param>
        public static void DrawTable(int BoxX, int BoxY, int boxWidth)
        {
            if (Program.Logic.Grid.Count != 0 || Program.Logic.Grid[0].Count != 0)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.DARKPURPLE);
                for (int y = 0; y < BoxY; y++)
                {
                    for (int x = 0; x < BoxX; x++)
                    {
                        Raylib.DrawRectangle(x * boxWidth, y * boxWidth, boxWidth - 1, boxWidth - 1, Program.Logic.Grid[y][x] == 0 ? Color.DARKGRAY : Color.RED);
                    }
                }
                Raylib.EndDrawing();
            }
        }

        /// <summary>
        /// Draws the start screen woth two optins
        /// </summary>
        /// <param name="screenWidth">width of screen</param>
        /// <param name="screenHeight">high od screen</param>
        /// <param name="randomStartRec">the rectangle of the rendom start</param>
        /// <param name="customStartRec">the rectungle of teh custom start</param>
        public static void DrawStartScreen(int screenWidth, int screenHeight, Rectangle randomStartRec, Rectangle customStartRec)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);
            Raylib.DrawRectangleRounded(randomStartRec, 1f, 1, Color.DARKBLUE);
            Raylib.DrawRectangleRounded(customStartRec, 1f, 1, Color.DARKBLUE);
            Raylib.DrawText("Random Start", (screenWidth / 2) - 225, (screenHeight / 2) + 3, 30, Color.WHITE);
            Raylib.DrawText("Custom Start", (screenWidth / 2) + 25, (screenHeight / 2) + 3, 30, Color.WHITE);
            Raylib.EndDrawing();
        }
    }
}