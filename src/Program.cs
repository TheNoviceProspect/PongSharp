using Raylib_cs;
using System.Numerics;

using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static Raylib_cs.ConfigFlags;


namespace PongSharp.App
{
    class Program
    {
        

        private const string APPMSG_START = "Pong# is starting up ...";
        internal const int MAX_FPS = 60;
        private const int MIN_WIDTH = 800;
        private const int MIN_HEIGHT = 600;
	    private const int DEFAULT_WIDTH = 1280;
        private const int DEFAULT_HEIGHT = 720;

        internal static bool shouldClose = false;



        internal static GameScreens.GameScreen _currentScreen = GameScreens.GameScreen.Logo;
        internal static GameScreens.GameScreen _previousScreen = GameScreens.GameScreen.Logo;
        internal static Vector2 DefaultScreenDimension = new Vector2(DEFAULT_WIDTH, DEFAULT_HEIGHT);
        private static Vector2 MinScreenDimension = new Vector2(MIN_WIDTH, MIN_HEIGHT);
        private static ulong _frameCounter = 0;

        static void initApp()
        {
            AppLog.SetCustomLogger("<Pong# Logger>");
            TraceLog(TraceLogLevel.LOG_TRACE, APPMSG_START);
            SetConfigFlags(FLAG_WINDOW_RESIZABLE|FLAG_VSYNC_HINT);
            SetTargetFPS(MAX_FPS);
            SetExitKey(KeyboardKey.KEY_NULL);
            InitWindow((int)DefaultScreenDimension.X, (int)DefaultScreenDimension.Y, "Pong#");
            SetWindowMinSize((int)MinScreenDimension.X, (int)MinScreenDimension.Y);
        }

        static void DrawGameScreen()
        {
            switch (_currentScreen)
            {
                case GameScreens.GameScreen.Logo:
                    {
                        GameScreens.DrawScreenLogo(ref _frameCounter);
                    }
                    break;
                case GameScreens.GameScreen.Title:
                    {
                        GameScreens.DrawScreenTitle(_frameCounter);
                    }
                    break;
                case GameScreens.GameScreen.Gameplay:
                    {
                        GameScreens.DrawScreenGameplay(_frameCounter);
                    }
                    break;
                case GameScreens.GameScreen.Ending:
                    {
                        GameScreens.DrawScreenEnding(_frameCounter);
                    }
                    break;
                default:
                    break;
            }
        }

        static void Main(string[] args)
        {
            initApp();
            while (!shouldClose) {
                switch (_currentScreen)
                {
                    case GameScreens.GameScreen.Logo:
                        {
                            GameScreens.HandleScreenLogo(ref _frameCounter);
                        }
                        break;
                    case GameScreens.GameScreen.Title:
                        {
                            GameScreens.HandleScreenTitle(ref _frameCounter);
                        }
                        break;
                    case GameScreens.GameScreen.Gameplay:
                        {
                            GameScreens.HandleScreenGameplay(ref _frameCounter);
                        }
                        break;
                    case GameScreens.GameScreen.Ending:
                        {
                            GameScreens.HandleScreenEnding(ref _frameCounter);
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
