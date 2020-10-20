using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Classes
{
    public class TicTacToeSetup
    {
        public static TicTacToeSetup ticTacToeSetup; 

        public int amountOfPlayers = 2;
        public bool useAI = false;
        public int width = 3;
        public int height = 3;
        public int winCount = 3;
    }
}
