using System;
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
        private const int GAMEBOARD_VERTICAL_LOCATION = 4;

        private const int POSITIONPROMPT_VERTICAL_LOCATION = 12;
        private const int POSITIONPROMPT_HORIZONTAL_LOCATION = 3;

        private const int MESSAGEBOX_VERTICAL_LOCATION = 15;

        private const int TOP_LEFT_ROW = 3;
        private const int TOP_LEFT_COLUMN = 6;

        private Gameboard _gameboard;
        private ViewState _currentViewStat;

        
        
        #region PROPERTIES
        public ViewState CurrentViewState
        {
            get { return _currentViewStat; }
            set { _currentViewStat = value; }
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
            _currentViewStat = ViewState.Active;

            InitializeConsole();
        }

        /// <summary>
        /// configure the console window
        /// </summary>
        public void InitializeConsole()
        {
            ConsoleUtil.WindowWidth = ConsoleConfig.windowWidth;
            ConsoleUtil.WindowHeight = ConsoleConfig.windowHeight;

            Console.BackgroundColor = ConsoleConfig.bodyBackgroundColor;
            Console.ForegroundColor = ConsoleConfig.bodyForegroundColor;

            ConsoleUtil.WindowTitle = "The Tic-tac-toe Game";
        }
        public void DisplayGameboard()
        {
            for (int x = 0; x <= 5; x++)
            {
                if (x > 0)
                {
                    Console.WriteLine("|_____|_____|_____|_____|_____|_____|_____|");
                }
                else
                {
                    Console.WriteLine(" _________________________________________");
                }
                for (int y = 0; y <= 6; y++)
                {
                    _gameboard.Cells[x, y] = 0;
                    Console.Write("|  " + _gameboard.Cells[x, y] + "  ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("|_____|_____|_____|_____|_____|_____|_____|");
            Console.WriteLine("|  1     2     3     4     5     6     7  |");
        }
    }
}
        #endregion