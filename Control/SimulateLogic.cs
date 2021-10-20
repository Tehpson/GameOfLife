namespace GameOfLife.Control
{
    using Newtonsoft.Json;
    using Raylib;
    using System;

    internal static class SimulatButtonCheack
    {
        public static void SimulateLogic(int boxWidth, ref DateTime nextupdate, ref DateTime spaceIsPressedTime)
        {
            if (Raylib.IsKeyReleased(KeyboardKey.KEY_SPACE))
            {
                Program.Logic.NextGeneration();
                spaceIsPressedTime.AddDays(1);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                spaceIsPressedTime = DateTime.Now.AddMilliseconds(250);
            }
            if (DateTime.Now > nextupdate)
            {
                if (spaceIsPressedTime < DateTime.Now && Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
                {
                    Program.Logic.NextGeneration();
                }
                nextupdate = DateTime.Now.AddMilliseconds(100);
            }
            if (Raylib.IsKeyReleased(KeyboardKey.KEY_S))
            {
                var jsondata = JsonConvert.SerializeObject(Program.Logic.Grid);
                helper.JsonData.SaveToJson(jsondata);
            }
            if (Raylib.IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON))
            {
                var mousePosition = Raylib.GetMousePosition();
                var boxXClicked = mousePosition.x / boxWidth;
                var boxYClicked = mousePosition.y / boxWidth;
                Program.Logic.CustomGrid((int)boxYClicked, (int)boxXClicked);
            }
        }
    }
}