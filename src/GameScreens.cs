using static PongSharp.App.Program;

namespace PongSharp.App
{
    internal class GameScreens
    {
        internal const int LOGO_WAIT_TIME = 2;
        internal enum GameScreen
        {
            Logo = 0,
            Title,
            Gameplay,
            Ending
        }

        internal static void DrawScreenLogo(ref ulong dT) {
            DrawText("LOGO SCREEN", 20, 20, 40, LIGHTGRAY);
            DrawText($"WAIT for {LOGO_WAIT_TIME-dT/MAX_FPS} SECONDS...", 200, 100, 20, WHITE);
        }

        internal static void DrawScreenTitle(ulong dT) {
            DrawRectangle(0, 0, (int)DefaultScreenDimension.X, (int)DefaultScreenDimension.Y, GREEN);
            DrawText("TITLE SCREEN", 20, 20, 40, DARKGREEN);
            DrawText("PRESS ENTER or TAP to JUMP to GAMEPLAY SCREEN", 120, 220, 20, DARKGREEN);
        }

        internal static void DrawScreenGameplay(ulong dT) {
            DrawRectangle(0, 0, (int)DefaultScreenDimension.X, (int)DefaultScreenDimension.Y, PURPLE);
            DrawText("GAMEPLAY SCREEN", 20, 20, 40, MAROON);
            DrawText("PRESS ENTER or TAP to JUMP to TITLE SCREEN", 130, 220, 20, MAROON);
        }

        internal static void DrawScreenEnding(ulong dT) {
            DrawRectangle(0, 0, (int)DefaultScreenDimension.X, (int)DefaultScreenDimension.Y, BLUE);
            DrawRectangle(0, 100, (int)DefaultScreenDimension.X, 200, BLACK);
            DrawText("Are you sure you want to exit program? [Y/N]", 40, 180, 30, WHITE);

        }
        
        internal static void HandleScreenLogo(ref ulong dt) {
            dt++;    // Count frames
            if (dt > GameScreens.LOGO_WAIT_TIME*MAX_FPS) // <- wait for 2 seconds
            {
                _currentScreen = GameScreens.GameScreen.Title;
            }
        }

        internal static void HandleScreenTitle(ref ulong dt) {
            _previousScreen = GameScreens.GameScreen.Title;
            if (WindowShouldClose() || IsKeyPressed(KeyboardKey.KEY_ESCAPE)) _currentScreen = GameScreens.GameScreen.Ending;
            if (IsKeyPressed(KeyboardKey.KEY_ENTER)) _currentScreen = GameScreens.GameScreen.Gameplay;
        }

        internal static void HandleScreenGameplay(ref ulong dt) {
            _previousScreen = GameScreens.GameScreen.Gameplay;
            if (WindowShouldClose() || (IsKeyDown(KeyboardKey.KEY_LEFT_CONTROL) && (IsKeyPressed(KeyboardKey.KEY_Q)))) _currentScreen = GameScreens.GameScreen.Ending;
            if (IsKeyPressed(KeyboardKey.KEY_ESCAPE)) _currentScreen = GameScreens.GameScreen.Title;
        }

        internal static void HandleScreenEnding(ref ulong dt) {
            if (IsKeyPressed(KeyboardKey.KEY_Y)) shouldClose = true;
            else if (IsKeyPressed(KeyboardKey.KEY_N)) {
                shouldClose = false;
                _currentScreen = _previousScreen;                            
            }
        }
    }
}