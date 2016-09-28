using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class Gameboard
    {
        public enum CellValues
        {
            X,
            O,
            E
        }

        public enum GameboardState
        {
            NewRound,
            PlayerXTurn,
            PlayerOTurn,
            PlayerXWin,
            PlayerOWin,
            CatsGame
        }

        private string[,] _cells = new string[7, 6];
        private const int MAX_COLUMNS = 7;
        private const int MAX_ROWS = 6;
        private List<int> _columnValue = new List<int>(new int[7]);
        private GameboardState _currentRoundState;

        public int MaxColumnNum
        {
            get { return MAX_COLUMNS; }
        }
        public int MaxRowNum
        {
            get { return MAX_ROWS; }
        }
        public List<int> ColumnValues
        {
            get { return _columnValue; }
            set { _columnValue = value; }
        }

        public string[,] Cells
        {
            get { return _cells; }
            set { _cells = value; }
        }


        public GameboardState CurrentRoundState
        {
            get { return _currentRoundState; }
            set { _currentRoundState = value; }
        }

        public Gameboard()
        {
            InitializeGameboard();
        }
        public void InitializeGameboard()
        {
            _currentRoundState = GameboardState.NewRound;

            //
            // Set all PlayerPiece array values to "None"
            //
            for (int row = 0; row < MAX_ROWS; row++)
            {
                for (int column = 0; column < MAX_COLUMNS; column++)
                {
                    _cells[column, row] = CellValues.E.ToString();
                }
            }
        }

        public bool GameboardPositionAvailable(int playerColumnChoice)
        {
            //
            // Confirm that the board position is empty
            // Note: gameboardPosition converted to array index by subtracting 1
            //
            int cellLevel;
            cellLevel = _columnValue[playerColumnChoice];
            switch (cellLevel)
            {
                case 1:
                    cellLevel = 6;
                    break;
                case 2:
                    cellLevel = 5;
                    break;
                case 3:
                    cellLevel = 4;
                    break;
                case 4:
                    cellLevel = 3;
                    break;
                case 5:
                    cellLevel = 2;
                    break;
                case 6:
                    cellLevel = 1;
                    break;
                default:
                    break;
            }
            try
            {
                if (_cells[playerColumnChoice, cellLevel-1] == CellValues.E.ToString())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return true;
            }
            
        }
        public void UpdateGameboardState()
        {
            if (FourInARow())
            {
                _currentRoundState = GameboardState.PlayerXWin;
            }
            //
            // A player O has won
            //
            else if (FourInARow())
            {
                _currentRoundState = GameboardState.PlayerOWin;
            }
            //
            // All positions filled
            //
            else if (IsCatsGame())
            {
                _currentRoundState = GameboardState.CatsGame;
            }
        }
        private bool FourInARow()
        {
            //
            // Check rows for player win
            //


            //
            // Check columns for player win
            //


            //
            // Check diagonals for player win
            //


            //
            // No Player Has Won
            //

            return false;
        }
        public bool IsCatsGame()
        {
            //
            // All positions on board are filled and no winner
            //
            for (int row = 0; row < MaxRowNum; row++)
            {
                for (int column = 0; column < MaxColumnNum; column++)
                {
                    if (_cells[column, row] == CellValues.E.ToString())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void SetPlayerPiece(int playerColumnChoice, Gameboard.CellValues PlayerPiece)
        {
            int cellLevel = _columnValue[playerColumnChoice];
            switch (cellLevel)
            {
                case 1:
                    cellLevel = 6;
                    break;
                case 2:
                    cellLevel = 5;
                    break;
                case 3:
                    cellLevel = 4;
                    break;
                case 4:
                    cellLevel = 3;
                    break;
                case 5:
                    cellLevel = 2;
                    break;
                case 6:
                    cellLevel = 1;
                    break;
                default:
                    break;
            }

            _cells[playerColumnChoice, cellLevel - 1] = PlayerPiece.ToString();
            SetNextPlayer();

            //_cells[playerColumnChoice, _columnValue[playerColumnChoice] - 1] = PlayerPiece.ToString();
            //SetNextPlayer();
        }
        private void SetNextPlayer()
        {

            if (_currentRoundState == GameboardState.PlayerXTurn)
            {
                _currentRoundState = GameboardState.PlayerOTurn;
            }
            else
            {
                _currentRoundState = GameboardState.PlayerXTurn;
            }
        }

    }
}
