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
            for (int row = 0; row < MAX_ROWS; row++)
            {
                for (int column = 0; column < MAX_COLUMNS; column++)
                {
                    _cells[column, row] = CellValues.E.ToString();
                }
            }
            _columnValue = new List<int>(new int[7]);
        }


        public bool GameboardPositionAvailable(int playerColumnChoice)
        {
            int cellLevel = _columnValue[playerColumnChoice];
            cellLevel = Math.Abs(cellLevel - 7);
            try
            {
                if (_cells[playerColumnChoice, cellLevel - 1] == CellValues.E.ToString())
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
            if (FourInARow(CellValues.X))
            {
                _currentRoundState = GameboardState.PlayerXWin;
            }
            //
            // A player O has won
            //
            else if (FourInARow(CellValues.O))
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
        private bool FourInARow(CellValues cellValue)
        {
            // Check rows for player win            
            string pieceToCheck = cellValue.ToString();
            for (int row = 0; row <= 6; row++)
            {
                if((_cells[row, 0] == pieceToCheck &&
                    _cells[row, 1] == pieceToCheck &&
                    _cells[row, 2] == pieceToCheck &&
                    _cells[row, 3] == pieceToCheck) ||
                   (_cells[row, 1] == pieceToCheck &&
                    _cells[row, 2] == pieceToCheck &&
                    _cells[row, 3] == pieceToCheck &&
                    _cells[row, 4] == pieceToCheck) ||
                   (_cells[row, 2] == pieceToCheck &&
                    _cells[row, 3] == pieceToCheck &&
                    _cells[row, 4] == pieceToCheck &&
                    _cells[row, 5] == pieceToCheck))
                {
                    return true;
                }
            }
            // Check columns for player win
            for (int column = 0; column <= 5; column++)
            {
                if((_cells[0, column] == pieceToCheck &&
                    _cells[1, column] == pieceToCheck &&
                    _cells[2, column] == pieceToCheck &&
                    _cells[3, column] == pieceToCheck) ||
                   (_cells[1, column] == pieceToCheck &&
                    _cells[2, column] == pieceToCheck &&
                    _cells[3, column] == pieceToCheck &&
                    _cells[4, column] == pieceToCheck) ||
                   (_cells[2, column] == pieceToCheck &&
                    _cells[3, column] == pieceToCheck &&
                    _cells[4, column] == pieceToCheck &&
                    _cells[5, column] == pieceToCheck) ||
                   (_cells[3, column] == pieceToCheck &&
                    _cells[4, column] == pieceToCheck &&
                    _cells[5, column] == pieceToCheck &&
                    _cells[6, column] == pieceToCheck)
                    )
                {
                    return true;
                }
            }
            
            for (int row = 0; row <= 3; row++)
            {
                for (int column = 0; column <= 2; column++)
                {
                    if (_cells[row, column] == pieceToCheck &&
                        _cells[row + 1, column + 1] == pieceToCheck &&
                        _cells[row + 2, column + 2] == pieceToCheck &&
                        _cells[row + 3, column + 3] == pieceToCheck)
                    {
                        return true;
                    }
                }
            }
            for (int row = 0; row <= 3; row++)
            {
                for (int column = 3; column <= 5; column++)
                {
                    if (_cells[row, column] == pieceToCheck &&
                        _cells[row + 1, column - 1] == pieceToCheck &&
                        _cells[row + 2, column - 2] == pieceToCheck &&
                        _cells[row + 3, column - 3] == pieceToCheck)
                    {
                        return true;
                    }
                }
            }



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
            cellLevel = Math.Abs(cellLevel - 7);
            _cells[playerColumnChoice, cellLevel - 1] = PlayerPiece.ToString();
            SetNextPlayer();
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
