using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectFour;

namespace ConnectFour
{
    class GameControl
    {
        private bool _playingGame;
        private bool _playingRound;
        private int _roundNumber;

        private int _playerXNumberOfWins;
        private int _playerONumberOfWins;
        private int _numberOfCatsGames;

        private Gameboard _gameboard;
        private ConsoleView _gameView;
        private Player _playerX = new Player();
        private Player _playerO = new Player();
        public GameControl()
        {
            InitializeGame();
            PlayGame();
        }
        public void InitializeGame()
        {
            //
            // Initialize game variables
            //
            _playingGame = true;
            _playingRound = true;
            _roundNumber = 0;
            _playerONumberOfWins = 0;
            _playerXNumberOfWins = 0;
            _numberOfCatsGames = 0;
            _gameboard = new Gameboard();
            _gameView = new ConsoleView(_gameboard);
            _playerO.PlayerPiece = Gameboard.CellValues.O;
            _playerX.PlayerPiece = Gameboard.CellValues.X;
            //
            // Initialize game board status
            //
            _gameboard.InitializeGameboard();
        }
        public void PlayGame()
        {
            _gameView.DisplayIntroMenu();

            while (_playingGame)
            {
                //
                // Round loop
                //
                while (_playingRound)
                {
                    //
                    // Perform the task associated with the current game and round state
                    //
                    ManageGameStateTasks();

                    //
                    // Evaluate and update the current game board state
                    //
                    _gameboard.UpdateGameboardState();
                }

                //
                // Round Complete: Display the results
                //
                _gameView.DisplayCurrentGameStatus(_roundNumber, _playerXNumberOfWins, _playerONumberOfWins, _numberOfCatsGames);

                //
                // Confirm no major user errors
                //
                if (_gameView.CurrentViewState != ConsoleView.ViewState.PlayerUsedMaxAttempts ||
                    _gameView.CurrentViewState != ConsoleView.ViewState.PlayerTimedOut)
                {
                    //
                    // Prompt user to play another round
                    //
                    _gameboard.InitializeGameboard();
                    _gameView.InitializeView();
                    _playingRound = true;
                }
                //
                // Major user error recorded, end game
                //
                else
                {
                    _playingGame = false;
                }
            }

            _gameView.DisplayClosingScreen();
        }
        private void ManageGameStateTasks()
        {
            switch (_gameView.CurrentViewState)
            {
                case ConsoleView.ViewState.Active:
                    _gameView.DisplayGameArea();

                    switch (_gameboard.CurrentRoundState)
                    {
                        case Gameboard.GameboardState.NewRound:
                            _roundNumber++;
                            _gameboard.CurrentRoundState = Gameboard.GameboardState.PlayerXTurn;
                            break;

                        case Gameboard.GameboardState.PlayerXTurn:
                            ManagePlayerTurn();
                            break;

                        case Gameboard.GameboardState.PlayerOTurn:
                            ManagePlayerTurn();
                            break;

                        case Gameboard.GameboardState.PlayerXWin:
                            _playerXNumberOfWins++;
                            _playingRound = false;
                            break;

                        case Gameboard.GameboardState.PlayerOWin:
                            _playerONumberOfWins++;
                            _playingRound = false;
                            break;

                        case Gameboard.GameboardState.CatsGame:
                            _numberOfCatsGames++;
                            _playingRound = false;
                            break;

                        default:
                            break;
                    }
                    break;
                case ConsoleView.ViewState.PlayerTimedOut:
                    _gameView.DisplayTimedOutScreen();
                    _playingRound = false;
                    break;
                case ConsoleView.ViewState.PlayerUsedMaxAttempts:
                    _gameView.DisplayMaxAttemptsReachedScreen();
                    _playingRound = false;
                    _playingGame = false;
                    break;
                default:
                    break;
            }
        }

        private void ManagePlayerTurn()
        {
            int gameboardPosition = _gameView.GetPlayerPositionChoice();

            if (_gameView.CurrentViewState != ConsoleView.ViewState.PlayerUsedMaxAttempts)
            {
                //
                // player chose an open position on the game board, add it to the game board
                //
                if (_gameboard.GameboardPositionAvailable(gameboardPosition))
                {
                    if (_gameboard.CurrentRoundState == Gameboard.GameboardState.PlayerOTurn)
                    {
                        _gameboard.SetPlayerPiece(gameboardPosition, _playerO.PlayerPiece);
                    }
                    else if (_gameboard.CurrentRoundState == Gameboard.GameboardState.PlayerXTurn)
                    {
                        _gameboard.SetPlayerPiece(gameboardPosition, _playerX.PlayerPiece);
                    }
                }
                else
                {
                    _gameView.DisplayGamePositionChoiceNotAvailableScreen();
                }
            }
        }
    }
}
