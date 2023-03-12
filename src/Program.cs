namespace PongSharp.App
{
    class Program
    {
	private const string APP_TITLE = "PongSharp";
        private const string APPMSG_START = $"{APP_TITLE} is starting up ...";
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

        /// <summary>
        /// Initialize the Logger and raylib window
        /// </summary>
        static void initApp()
        {
            // setup custom logger
            AppLog.SetCustomLogger($"<{APP_TITLE} Logger>");
            // output a start message to the logger
            TraceLog(TraceLogLevel.LOG_TRACE, APPMSG_START);
            // make window resizable
            SetConfigFlags(FLAG_WINDOW_RESIZABLE|FLAG_VSYNC_HINT);
            // set target fps for window (and delta-v calculations)
            SetTargetFPS(MAX_FPS);
            // make sure alt+f4 does not close the window, we handle this ourselves
            SetExitKey(KeyboardKey.KEY_NULL);
            // open the window with our default dimensions
            InitWindow((int)DefaultScreenDimension.X, (int)DefaultScreenDimension.Y, $"{APP_TITLE}");
            // make sure the window has a fixed minimum size
            SetWindowMinSize((int)MinScreenDimension.X, (int)MinScreenDimension.Y);
        }

        /// <summary>
        /// This selects a gamescreen based on _currentGameScreen
        /// </summary>
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

        /// <summary>
        /// This is a) the main entry point as well as the place where the main game loop runs
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            initApp();
            // exit has not been requested
            while (!shouldClose) {
                // check which screen we're on and run the correct "handler"
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
                ClearBackground(Color.BLACK);
                DrawGameScreen();
                EndDrawing();
            }
            CloseWindow();
            AppLog.ResetLog();
        }
    }
}
