using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class Player
    {
        private string _name;
        private int _score;
        private string _color;
        private Gameboard.CellValues _playerPiece;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }
        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public Gameboard.CellValues PlayerPiece
        {
            get { return _playerPiece; }
            set { _playerPiece = value; }
        }

    }
}
