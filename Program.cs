namespace GameOfLife
{
    using Raylib;
    using System;

    internal static class Program
    {
        public enum GameState
        {
            start,
            CustomStart,
            LocalSaveFiles,
            simulate
        }

        public static Control.Logic Logic { get; set; }

        /// <summary>
        /// The glue
        /// </summary>
        private static void Main()
        {
            const int boxWidth = 15;
            const int screenWidth = 1275;
            const int screenHeight = 720;
            const int boxX = screenWidth / boxWidth;
            const int boxY = screenHeight / boxWidth;
            Raylib.InitWindow(screenWidth, screenHeight, "Game Of life");
            Raylib.SetTargetFPS(60);
            var gamestate = GameState.start;
            var nextupdate = DateTime.Now;
            DateTime spaceIsPressedTime = default;

            var randomStartRec = new Rectangle((screenWidth / 2) - 240, screenHeight / 2, 230, 35);
            var customStartRec = new Rectangle((screenWidth / 0x2) + 10, screenHeight / 2, 230, 35);

            while (!Raylib.WindowShouldClose())
            {
                if (gamestate == GameState.start)
                {
                    View.DrawTableClass.DrawStartScreen(screenWidth, screenHeight, randomStartRec, customStartRec);
                    gamestate = Control.StartOptions.SelectFirstScreen(boxX, boxY, gamestate, randomStartRec, customStartRec);
                }
                if (gamestate == GameState.CustomStart)
                {
                    gamestate = Control.StartOptions.CustomSetup(screenWidth, screenHeight, gamestate);
                }

                if (gamestate == GameState.LocalSaveFiles)
                {
                    gamestate = Control.StartOptions.BrowsLocalPatterns(screenWidth, screenHeight, gamestate);
                }

                if (gamestate == GameState.simulate)
                {
                    Control.SimulatButtonCheack.SimulateLogic(boxWidth, ref nextupdate, ref spaceIsPressedTime);
                    View.DrawTableClass.DrawTable(boxX, boxY, boxWidth);
                }
            }
            Raylib.CloseWindow();
        }
    }
}