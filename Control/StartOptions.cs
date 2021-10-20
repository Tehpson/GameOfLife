namespace GameOfLife.Control
{
    using Raylib;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal static class StartOptions
    {
        private static int BoxShift;
        private static bool ArrowDownIsdrawn;
        private static bool ArrowUppIsdrawn;
        private static List<Rectangle> localSaveRec = new List<Rectangle>();

        /// <summary>
        /// Draw and logic for the Local brows of saved data,
        /// </summary>
        /// <param name="screenWidth">Width of screen</param>
        /// <param name="screenHeight">High of screen</param>
        /// <param name="gamestate">gamestate to be able to change</param>
        /// <returns>new gamestate</returns>
        internal static Program.GameState BrowsLocalPatterns(int screenWidth, int screenHeight, Program.GameState gamestate)
        {
            var path = Directory.GetCurrentDirectory();
            path = Path.Combine(path, "Pattern");
            var directoryInfo = new DirectoryInfo(path);

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);
            var jsonFile = directoryInfo.GetFiles().Where(x => x.Extension == ".json").ToList();
            localSaveRec.Clear();
            for (int i = 0; i < jsonFile.Count; i++)
            {
                var rec = new Rectangle((screenWidth / 2) - 300, (40 * (i + 1)) + 20 + BoxShift, 600, 35);
                localSaveRec.Add(rec);
                if (rec.y + rec.height > screenHeight - 60)
                {
                    Raylib.DrawTriangle(new Vector2((screenWidth / 2) + 25, 675), new Vector2((screenWidth / 2) - 25, 675), new Vector2(screenWidth / 2, 700), Color.DARKBLUE);
                    ArrowDownIsdrawn = true;
                }
                else if (rec.y < 60)
                {
                    Raylib.DrawTriangle(new Vector2(screenWidth / 2, 20), new Vector2((screenWidth / 2) - 25, 45), new Vector2((screenWidth / 2) + 25, 45), Color.DARKBLUE);
                    ArrowUppIsdrawn = true;
                }
                else
                {
                    Raylib.DrawRectangleRec(rec, Color.DARKBLUE);
                    Raylib.DrawText(jsonFile[i].Name, (screenWidth / 2) - 290, (40 * (i + 1)) + 22 + BoxShift, 30, Color.WHITE);
                }
            }
            Raylib.EndDrawing();

            if (localSaveRec.Count(rec => rec.y < 60) == 0)
            {
                ArrowUppIsdrawn = false;
            }
            if (localSaveRec.Count(rec => rec.y + rec.height > screenHeight - 60) == 0)
            {
                ArrowDownIsdrawn = false;
            }

            if (ArrowDownIsdrawn)
            {
                if (Raylib.CheckCollisionPointTriangle(Raylib.GetMousePosition(), new Vector2((screenWidth / 2) + 25, 675), new Vector2((screenWidth / 2) - 25, 675), new Vector2(screenWidth / 2, 700)) && Raylib.IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    BoxShift -= 40;
                }
            }
            if (ArrowUppIsdrawn)
            {
                if (Raylib.CheckCollisionPointTriangle(Raylib.GetMousePosition(), new Vector2(screenWidth / 2, 20), new Vector2((screenWidth / 2) - 25, 45), new Vector2((screenWidth / 2) + 25, 45)) && Raylib.IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    BoxShift += 40;
                }
            }

            for (int y = 0; y < localSaveRec.Count; y++)
            {
                if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), localSaveRec[y]) && localSaveRec[y].y > 60 && localSaveRec[y].y + localSaveRec[y].height < screenHeight - 60 && Raylib.IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    path = Directory.GetCurrentDirectory();
                    path = Path.Combine(path, "Pattern");
                    directoryInfo = new DirectoryInfo(path);
                    var file = directoryInfo.GetFiles()[y].FullName;
                    Program.Logic.Grid = helper.JsonData.GetJsonData(file);
                    gamestate = Program.GameState.simulate;
                }
            }

            return gamestate;
        }

        /// <summary>
        /// Chooice start option
        /// </summary>
        /// <param name="boxX">number of boxes in X</param>
        /// <param name="boxY">nuimber of boxes in Y</param>
        /// <param name="gamestate">Gamestate</param>
        /// <param name="randomStartRec">The rectangle of the Random start button</param>
        /// <param name="customStartRec">The rectangle of the Custom start button</param>
        /// <returns></returns>
        public static Program.GameState SelectFirstScreen(int boxX, int boxY, Program.GameState gamestate, Rectangle randomStartRec, Rectangle customStartRec)
        {
            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), randomStartRec) && Raylib.IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON))
            {
                Program.Logic = new Logic(boxY, boxX, false);
                gamestate = Program.GameState.simulate;
            }

            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), customStartRec) && Raylib.IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON))
            {
                Program.Logic = new Logic(boxY, boxX, true);
                gamestate = Program.GameState.CustomStart;
            }

            return gamestate;
        }

        /// <summary>
        /// choose diffrent map
        /// </summary>
        /// <param name="screenWidth">width of screen</param>
        /// <param name="screenHeight">high of screen</param>
        /// <param name="gamestate">Games sate</param>
        /// <returns>new gamestate</returns>
        public static Program.GameState CustomSetup(int screenWidth, int screenHeight, Program.GameState gamestate)
        {
            var map1Texture = Raylib.LoadTexture(@"resources\Fire_Work.png");
            var map1 = new Rectangle((screenWidth / 3) - (map1Texture.width / 2), (screenHeight / 3) - (map1Texture.height / 2) - 60, map1Texture.width, map1Texture.height + 40);

            var map2Texture = Raylib.LoadTexture(@"resources\Gosper_glider_gun.png");
            var map2 = new Rectangle((screenWidth / 3 * 2) - (map2Texture.width / 2), (screenHeight / 3) - (map2Texture.height / 2) - 60, map2Texture.width, map2Texture.height + 40);

            var map3Texture = Raylib.LoadTexture(@"resources\Empty.png");
            var map3 = new Rectangle((screenWidth / 3) - (map3Texture.width / 2), (screenHeight / 3 * 2) - (map3Texture.height / 2) - 20, map3Texture.width, map3Texture.height + 40);

            var map4Texture = Raylib.LoadTexture(@"resources\Local_list.png");
            var map4 = new Rectangle((screenWidth / 3 * 2) - (map4Texture.width / 2), (screenHeight / 3 * 2) - (map4Texture.height / 2) - 20, map4Texture.width, map4Texture.height + 40);

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);
            Raylib.DrawRectangleRounded(map1, 0.2f, 1, Color.DARKBLUE);
            Raylib.DrawTexture(map1Texture, (screenWidth / 3) - (map1Texture.width / 2), (screenHeight / 3) - (map1Texture.height / 2) - 20, Color.WHITE);
            Raylib.DrawText("FireWork", (screenWidth / 3) - (map1Texture.width / 2) + 20, (screenHeight / 3) - (map1Texture.height / 2) - 55, 30, Color.WHITE);

            Raylib.DrawRectangleRounded(map2, 0.2f, 1, Color.DARKBLUE);
            Raylib.DrawTexture(map2Texture, (screenWidth / 3 * 2) - (map2Texture.width / 2), (screenHeight / 3) - (map3Texture.height / 2) - 20, Color.WHITE);
            Raylib.DrawText("Gosper glider gun", (screenWidth / 3 * 2) - (map3Texture.width / 2) + 20, (screenHeight / 3) - (map3Texture.height / 2) - 55, 30, Color.WHITE);

            Raylib.DrawRectangleRounded(map3, 0.2f, 1, Color.DARKBLUE);
            Raylib.DrawTexture(map3Texture, (screenWidth / 3) - (map3Texture.width / 0x2), (screenHeight / 3 * 2) + 20 - (map3Texture.height / 2), Color.WHITE);
            Raylib.DrawText("Free Placement", (screenWidth / 3) - (map3Texture.width / 2) + 20, (screenHeight / 3 * 2) + 20 - (map3Texture.height / 2) - 35, 30, Color.WHITE);

            Raylib.DrawRectangleRounded(map4, 0.2f, 1, Color.DARKBLUE);
            Raylib.DrawTexture(map4Texture, (screenWidth / 3 * 2) - (map4Texture.width / 2), (screenHeight / 3 * 2) + 20 - (map4Texture.height / 2), Color.WHITE);
            Raylib.DrawText("Gosper glider gun", (screenWidth / 3 * 2) - (map4Texture.width / 2) + 20, (screenHeight / 3 * 2) + 20 - (map4Texture.height / 2) - 35, 30, Color.WHITE);
            Raylib.EndDrawing();
            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), map1) && Raylib.IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON))
            {
                const string file = "Pattern23134619246.json";
                var jsondata = helper.JsonData.GetJsonData(file);
                Program.Logic.Grid = jsondata;
                gamestate = Program.GameState.simulate;
            }

            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), map2) && Raylib.IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON))
            {
                const string file = "Pattern19112426359.json";
                var jsondata = helper.JsonData.GetJsonData(file);
                Program.Logic.Grid = jsondata;
                gamestate = Program.GameState.simulate;
            }

            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), map3) && Raylib.IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON))
            {
                gamestate = Program.GameState.simulate;
            }

            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), map4) && Raylib.IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON))
            {
                gamestate = Program.GameState.LocalSaveFiles;
            }

            return gamestate;
        }
    }
}