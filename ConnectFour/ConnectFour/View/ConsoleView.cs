using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class ConsoleView
    {
        public enum ViewState
        {
            Active,
            PlayerTimedOut, // TODO Track player time on task
            PlayerUsedMaxAttempts
        }
        private const int GAMEBOARD_VERTICAL_LOCATION = 3;

        private const int POSITIONPROMPT_VERTICAL_LOCATION = 17;
        private const int POSITIONPROMPT_HORIZONTAL_LOCATION = 3;

        private const int MESSAGEBOX_VERTICAL_LOCATION = 19;

        private Gameboard _gameboard;
        private ViewState _currentViewState;

        private const int Window_Width = ConsoleConfig.windowWidth;
        private const int Window_Height = ConsoleConfig.windowHeight;

        private const int Display_Horizontal_Margin = ConsoleConfig.Display_Horizontal_Margin;
        private const int Display_Vertical_Margin = ConsoleConfig.Display__Vertical_Margin;


        #region PROPERTIES
        public ViewState CurrentViewState
        {
            get { return _currentViewState; }
            set { _currentViewState = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public ConsoleView(Gameboard gameboard)
        {
            _gameboard = gameboard;
            InitializeView();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Initialize the console view
        /// </summary>
        public void InitializeView()
        {
            _currentViewState = ViewState.Active;

            InitializeConsoleWindow();
        }

        /// <summary>
        /// configure the console window
        /// </summary>
        public void InitializeConsoleWindow()
        {
            ConsoleUtil.WindowWidth = ConsoleConfig.windowWidth;
            ConsoleUtil.WindowHeight = ConsoleConfig.windowHeight;

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;

            DisplayReset();
        }


        public void DisplayMessage(string message)
        {
            // CODE CITATION: John Velis, NMC Instructor

            //
            // calculate the message area location on the console window
            //
            const int Message_Box_Text_Length = Window_Width - (2 * Display_Horizontal_Margin);
            const int Message_Box_Horizontal_Margin = Display_Horizontal_Margin;

            //
            // create a list of strings to hold the wrapped text message
            //
            List<string> messageLines;

            //
            // call utility method to wrap text and loop through list of strings to display
            //
            messageLines = ConsoleUtil.Wrap(message, Message_Box_Text_Length, Message_Box_Horizontal_Margin);
            foreach (var messageLine in messageLines)
            {
                Console.WriteLine(messageLine);
            }
        }

        public void DisplayReset()
        {
            Console.SetWindowSize(Window_Width, Window_Height);

            Console.Clear();
            Console.CursorVisible = false;

            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(Window_Width));
            Console.WriteLine(ConsoleUtil.Center("Connect Four"));
            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(Window_Width));

            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
        }

        public void DisplayGameboard()
        {
            Console.SetCursorPosition(0, GAMEBOARD_VERTICAL_LOCATION);

            for (int y = 0; y <= 5; y++)
            {
                if (y > 0)
                {
                    Console.WriteLine("|_____|_____|_____|_____|_____|_____|_____|");
                }
                else
                {
                    Console.WriteLine(" _________________________________________");
                }
                for (int x = 0; x <= 6; x++)
                {
                    if (_gameboard.Cells[x, y] == Gameboard.CellValues.E.ToString())
                    {
                        Console.Write("|     ");
                    }
                    else
                        Console.Write("|  " + _gameboard.Cells[x, y] + "  ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("|_____|_____|_____|_____|_____|_____|_____|");
            Console.WriteLine("|  1     2     3     4     5     6     7  |");
        }

        public void DisplayIntroMenu()
        {
            bool usingMenu = true;

            while (usingMenu)
            {
                string leftTab = ConsoleUtil.FillStringWithSpaces(Display_Horizontal_Margin);

                DisplayReset();
                Console.CursorVisible = false;

                Console.ForegroundColor = ConsoleColor.Yellow;
                DisplayMessage("Main Menu");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine();

                DisplayMessage(leftTab + "1. Start Game");
                Console.WriteLine();
                DisplayMessage(leftTab + "2. View Game Rules");
                Console.WriteLine();
                DisplayMessage(leftTab + "3. Exit");
                Console.WriteLine();
                Console.WriteLine();

                ConsoleKeyInfo userResponse = Console.ReadKey(true);
                switch (userResponse.KeyChar)
                {
                    case '1':
                        DisplayGameArea();
                        usingMenu = false;
                        break;
                    case '2':
                        DisplayGameRules();
                        break;
                    case '3':
                        usingMenu = false;
                        DisplayExitPrompt();
                        break;
                    default:
                        DisplayMessage("It appears you have selected an incorrect choice.");
                        Console.WriteLine();
                        DisplayMessage("Press any key to continue or the ESC key to exit.");

                        userResponse = Console.ReadKey(true);
                        if (userResponse.Key == ConsoleKey.Escape)
                        {
                            usingMenu = false;
                        }
                        break;
                }
            }
        }

        public void DisplayGameRules()
        {
            DisplayReset();
            Console.CursorVisible = false;

            Console.ForegroundColor = ConsoleColor.Yellow;
            DisplayMessage("Game Rules");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine();

            DisplayMessage("The name of the game is Connect Four. One player must get their pieces lined up four in a row anywhere on the board to win.");
            Console.WriteLine();
            DisplayMessage("The possible winning combinations are: Vertical, Horizontal, and Diagonal.");
            Console.WriteLine();
            DisplayMessage("After one of the players has managed one of the winning combinations, the players will be shown player-specific statistics prompted with a Post-Game Menu.");

            DisplayContinuePrompt();
        }

        public void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            ConsoleUtil.DisplayMessage("Press any key to continue.");
            ConsoleKeyInfo response = Console.ReadKey();

            Console.WriteLine();

            Console.CursorVisible = true;
        }
        public void DisplayClosingScreen()
        {
            ConsoleUtil.HeaderText = "The Connect Four Game";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Thanks for playing! Come back soon, ya' hear?");

            DisplayContinuePrompt();
        }
        public void DisplayExitPrompt()
        {
            DisplayReset();

            Console.CursorVisible = false;

            Console.WriteLine();
            ConsoleUtil.DisplayMessage("Thank you for playing the game. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }

        public void DisplayPostGameMenu()
        {
            _gameboard.InitializeGameboard();

            bool usingMenu = true;

            while (usingMenu)
            {
                string leftTab = ConsoleUtil.FillStringWithSpaces(Display_Horizontal_Margin);

                DisplayReset();
                Console.CursorVisible = false;

                Console.ForegroundColor = ConsoleColor.Yellow;
                DisplayMessage("Post-Game Menu");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine();

                DisplayMessage(leftTab + "1. Start a New Game");
                Console.WriteLine();
                DisplayMessage(leftTab + "2. View Game Rules");
                Console.WriteLine();
                DisplayMessage(leftTab + "3. View Historic Player Stats");
                Console.WriteLine();
                DisplayMessage(leftTab + "4. Exit");
                Console.WriteLine();
                Console.WriteLine();

                ConsoleKeyInfo userResponse = Console.ReadKey(true);
                switch (userResponse.KeyChar)
                {
                    case '1':
                        DisplayGameArea();
                        usingMenu = false;
                        break;
                    case '2':
                        DisplayGameRules();
                        usingMenu = true;
                        break;
                    case '3':
                        DisplayHistoricStats();
                        usingMenu = true;
                        break;
                    case '4':
                        usingMenu = false;
                        DisplayExitPrompt();
                        break;
                    default:
                        DisplayMessage("It appears you have selected an incorrect choice.");
                        Console.WriteLine();
                        DisplayMessage("Press any key to continue or the ESC key to exit.");

                        userResponse = Console.ReadKey(true);
                        if (userResponse.Key == ConsoleKey.Escape)
                        {
                            usingMenu = false;
                        }
                        break;
                }
            }
        }

        public void DisplayHistoricStats()
        {
            DisplayReset();
            Console.CursorVisible = false;

            Console.ForegroundColor = ConsoleColor.Yellow;
            DisplayMessage("Historic Player Statistics");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine();

            string stats = File.ReadAllText(@"..\PlayerStats.txt");

            string leftTab = ConsoleUtil.FillStringWithSpaces(Display_Horizontal_Margin);

            DisplayMessage(stats);
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        public void DisplayTimedOutScreen()
        {
            ConsoleUtil.HeaderText = "Session Timed Out!";
            ConsoleUtil.DisplayReset();

            DisplayMessageBox("It appears your session has timed out.");

            DisplayContinuePrompt();
        }
        public void DisplayGameArea()
        {
            DisplayReset();

            DisplayGameboard();
            DisplayGameStatus();
        }
        public void DisplayGameStatus()
        {
            StringBuilder sb = new StringBuilder();

            switch (_gameboard.CurrentRoundState)
            {
                case Gameboard.GameboardState.NewRound:
                    //
                    // The new game status should not be an necessary option here
                    //
                    break;
                case Gameboard.GameboardState.PlayerXTurn:
                    DisplayMessageBox("It is currently Player X's turn.");
                    break;
                case Gameboard.GameboardState.PlayerOTurn:
                    DisplayMessageBox("It is currently Player O's turn.");
                    break;
                case Gameboard.GameboardState.PlayerXWin:
                    DisplayMessageBox("Player X Wins! Press any key to continue.");

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                case Gameboard.GameboardState.PlayerOWin:
                    DisplayMessageBox("Player O Wins! Press any key to continue.");

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                case Gameboard.GameboardState.CatsGame:
                    DisplayMessageBox("Cat's Game! Press any key to continue.");

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                default:
                    break;
            }
        }
        public void DisplayCurrentGameStatus(int roundsPlayed, int playerXWins, int playerOWins, int catsGames)
        {
            DisplayReset();
            ConsoleUtil.HeaderText = "Current Game Status";

            string[] PlayerInfo = { "Rounds Played: " + roundsPlayed.ToString() + " | ", "Player X Wins: " + playerXWins.ToString() + " | ", "Player O Wins: " + playerOWins.ToString() };
            File.WriteAllLines(@"..\PlayerStats.txt", PlayerInfo);

            double playerXPercentageWins = (double)playerXWins / roundsPlayed;
            double playerOPercentageWins = (double)playerOWins / roundsPlayed;
            double percentageOfCatsGames = (double)catsGames / roundsPlayed;

            ConsoleUtil.DisplayMessage("Rounds Played: " + roundsPlayed);
            ConsoleUtil.DisplayMessage("Player X Wins: " + playerXWins + " - " + String.Format("{0:P2}", playerXPercentageWins));
            ConsoleUtil.DisplayMessage("Player O Wins: " + playerOWins + " - " + String.Format("{0:P2}", playerOPercentageWins));
            ConsoleUtil.DisplayMessage("Cat's Games: " + catsGames + " - " + String.Format("{0:P2}", percentageOfCatsGames));
            DisplayContinuePrompt();

            DisplayPostGameMenu();
        }

        public void DisplayMessageBox(string message)
        {
            string leftMargin = new String(' ', ConsoleConfig.displayHorizontalMargin);
            string topBottom = new String('*', ConsoleConfig.windowWidth - 2 * ConsoleConfig.displayHorizontalMargin);

            StringBuilder sb = new StringBuilder();

            Console.SetCursorPosition(0, MESSAGEBOX_VERTICAL_LOCATION);
            Console.WriteLine(leftMargin + topBottom);

            Console.WriteLine(ConsoleUtil.Center("Game Status"));

            ConsoleUtil.DisplayMessage(message);

            Console.WriteLine(Environment.NewLine + leftMargin + topBottom);
        }
        public void DisplayMaxAttemptsReachedScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.HeaderText = "Maximum Attempts Reached!";
            ConsoleUtil.DisplayReset();

            sb.Append(" It appears that you are having difficulty entering your");
            sb.Append(" choice. Please refer to the instructions and play again.");

            DisplayMessageBox(sb.ToString());

            DisplayContinuePrompt();
        }
        private void DisplayPositionPrompt()
        {
            //
            // Clear line by overwriting with spaces
            //
            Console.SetCursorPosition(POSITIONPROMPT_HORIZONTAL_LOCATION, POSITIONPROMPT_VERTICAL_LOCATION);
            Console.Write(new String(' ', ConsoleConfig.windowWidth));
            //
            // Write new prompt
            //
            Console.SetCursorPosition(POSITIONPROMPT_HORIZONTAL_LOCATION, POSITIONPROMPT_VERTICAL_LOCATION);
            Console.Write("Enter column number: ");
        }
        public int GetPlayerPositionChoice()
        {
            //
            // Initialize gameboardPosition with -1 values
            //
            int gameboardPosition = -1;
            //
            // Get column number.
            //
            if (CurrentViewState != ViewState.PlayerUsedMaxAttempts)
            {
                gameboardPosition = PlayerColumnChoice();
            }

            return gameboardPosition;

        }
        private int PlayerColumnChoice()
        {
            int tempCoordinate = -1;
            int numOfPlayerAttempts = 1;
            int maxNumOfPlayerAttempts = 4;

            while ((numOfPlayerAttempts <= maxNumOfPlayerAttempts))
            {
                DisplayPositionPrompt();

                if (int.TryParse(Console.ReadLine(), out tempCoordinate))
                {

                    if (tempCoordinate >= 1 && tempCoordinate <= _gameboard.MaxColumnNum)
                    {
                        tempCoordinate -= 1;
                        if (_gameboard.ColumnValues[tempCoordinate] >= 6)
                        {
                            DisplayMessageBox("Column is full.");
                        }
                        else
                        {
                            _gameboard.ColumnValues[tempCoordinate]++;
                            return tempCoordinate;
                        }
                    }
                    else
                    {
                        DisplayMessageBox("Numbers are limited to 1 - 7....");
                    }
                }

                else
                {
                    DisplayMessageBox("Numbers are limited to 1 - 7....");
                }


                numOfPlayerAttempts++;
            }
            CurrentViewState = ViewState.PlayerUsedMaxAttempts;
            return tempCoordinate;
        }
        public void DisplayGamePositionChoiceNotAvailableScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.HeaderText = "Position Choice Unavailable";
            ConsoleUtil.DisplayReset();

            sb.Append(" It appears that you have chosen a position that is already");
            sb.Append(" taken. Please try again.");

            DisplayMessageBox(sb.ToString());

            DisplayContinuePrompt();
        }
    }
}
#endregion