using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class Gameboard
    {
        private int _cellValue;
        private int[,] _cells = new int[6,7];

        public int CellValue
        {
            get { return _cellValue; }
            set { _cellValue = value; }
        }
        public int[,] Cells
        {
            get { return _cells; }
            set { _cells = value; }
        }
        
        public Gameboard()
        {

        }
        

    }
}
