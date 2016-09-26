using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class Control
    {
        public Control()
        {
            Gameboard gameboard = new Gameboard();
            gameboard.InitializeGameboard();
        }
    }
}
