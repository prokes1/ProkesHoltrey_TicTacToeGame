using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class Gameboard
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
        public void InitializeGameboard()
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
                    _cells[x, y] = 0;
                    Console.Write("|  " + _cells[x, y] + "  ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("|_____|_____|_____|_____|_____|_____|_____|");
        }

    }
}
