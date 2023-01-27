using Raylib_cs;
using System.Numerics;

using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static Raylib_cs.ConfigFlags;

namespace PongSharp.App
{
    class Program
    {
        enum GameScreen
        {
            Logo = 0,
            Title,
            Gameplay,
            Ending
        }

        private const string APPMSG_START = "Pong# is starting up ...";
        private const string APPMSG_WELCOME = "Welcome to RayTest in C#";
        private const int MAX_FPS = 60;
        private const int MIN_WIDTH = 800;
        private const int MIN_HEIGHT =600;
private const int DEFAULT_WIDTH = 1280;
        private const int DEFAULT_HEIGHT = 720;

        private const int LOGO_WAIT_TIME = 5;

        private static GameScreen _currentScreen = GameScreen.Logo;
        private static GameScreen _previousScreen = GameScreen.Logo;
        private static Vector2 DefaultScreenDimension = new Vector2(DEFAULT_WIDTH, DEFAULT_HEIGHT);
        private static Vector2 MinScreenDimension = new Vector2(MIN_WIDTH, MIN_HEIGHT);
        private static ulong _frameCounter = 0;

        static void initApp()
        {
            AppLog.SetCustomLogger("<Pong# Logger>");
            TraceLog(TraceLogLevel.LOG_TRACE, APPMSG_START);
            InitWindow((int)DefaultScreenDimension.X, (int)DefaultScreenDimension.Y, "Pong#");
            SetWindowMinSize((int)MinScreenDimension.X, (int)MinScreenDimension.Y);
            SetConfigFlags(FLAG_WINDOW_RESIZABLE|FLAG_VSYNC_HINT);
            SetTargetFPS(MAX_FPS);
            SetExitKey(KeyboardKey.KEY_NULL);
        }

        static void DrawGameScreen()
        {
            switch (_currentScreen)
            {
                case GameScreen.Logo:
                    {
                        // TODO: Draw LOGO screen here!
                        DrawText("LOGO SCREEN", 20, 20, 40, LIGHTGRAY);
                        DrawText($"WAIT for {LOGO_WAIT_TIME-_frameCounter/60} SECONDS...", 200, 100, 20, WHITE);
                    }
                    break;
                case GameScreen.Title:
                    {
                        // TODO: Draw TITLE screen here!
                        DrawRectangle(0, 0, (int)DefaultScreenDimension.X, (int)DefaultScreenDimension.Y, GREEN);
                        DrawText("TITLE SCREEN", 20, 20, 40, DARKGREEN);
                        DrawText("PRESS ENTER or TAP to JUMP to GAMEPLAY SCREEN", 120, 220, 20, DARKGREEN);
                    }
                    break;
                case GameScreen.Gameplay:
                    {
                        // TODO: Draw GAMEPLAY screen here!
                        DrawRectangle(0, 0, (int)DefaultScreenDimension.X, (int)DefaultScreenDimension.Y, PURPLE);
                        DrawText("GAMEPLAY SCREEN", 20, 20, 40, MAROON);
                        DrawText("PRESS ENTER or TAP to JUMP to TITLE SCREEN", 130, 220, 20, MAROON);
                    }
                    break;
                case GameScreen.Ending:
                    {
                        // TODO: Draw ENDING screen here!
                        DrawRectangle(0, 0, (int)DefaultScreenDimension.X, (int)DefaultScreenDimension.Y, BLUE);
                        //DrawText("ENDING SCREEN", 20, 20, 40, DARKBLUE);
                        //DrawText("PRESS ENTER or TAP to RETURN to TITLE SCREEN", 120, 220, 20, DARKBLUE);
                        DrawRectangle(0, 100, (int)DefaultScreenDimension.X, 200, BLACK);
                        DrawText("Are you sure you want to exit program? [Y/N]", 40, 180, 30, WHITE);
                    }
                    break;
                default:
                    break;
            }
        }

        static void Main(string[] args)
        {
            bool callExit = false;
            initApp();
            while (!callExit) {
                switch (_currentScreen)
                {
                    case GameScreen.Logo:
                        {
                            _frameCounter++;    // Count frames
                            if (_frameCounter > LOGO_WAIT_TIME*60) // <- wait for 2 seconds
                            {
                                _currentScreen = GameScreen.Title;
                            }
                        }
                        break;
                    case GameScreen.Title:
                        {
                            _previousScreen = GameScreen.Title;
                            if (WindowShouldClose() || IsKeyPressed(KeyboardKey.KEY_ESCAPE)) _currentScreen = GameScreen.Ending;
                            if (IsKeyPressed(KeyboardKey.KEY_ENTER)) _currentScreen = GameScreen.Gameplay;
                        }
                        break;
                    case GameScreen.Gameplay:
                        {
                            _previousScreen = GameScreen.Gameplay;
                            if (WindowShouldClose() || IsKeyPressed(KeyboardKey.KEY_ESCAPE)) _currentScreen = GameScreen.Ending;
                            if (IsKeyPressed(KeyboardKey.KEY_ENTER)) _currentScreen = GameScreen.Title;
                        }
                        break;
                    case GameScreen.Ending:
                        {
                            if (IsKeyPressed(KeyboardKey.KEY_Y)) callExit = true;
                            else if (IsKeyPressed(KeyboardKey.KEY_N)) {
                                callExit = false;
                                _currentScreen = _previousScreen;                            
                            }
                        }
                        break;
                    default:
                        break;
                }
                BeginDrawing();
                ClearBackground(BLACK);
                DrawGameScreen();
                EndDrawing();
            }
            CloseWindow();
            AppLog.ResetLog();
        }
    }
}
