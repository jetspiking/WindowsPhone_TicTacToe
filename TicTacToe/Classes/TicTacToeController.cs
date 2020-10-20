using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace TicTacToe.Classes
{
    public class TicTacToeController
    {
        private ScalableTicTacToe ticTacToeBoard;

        public TicTacToeController(ScalableTicTacToe ticTacToeBoard)
        {
            this.ticTacToeBoard = ticTacToeBoard;
        }

        public bool handleInput(Size position, Size canvasSize)
        {
            double squareWidth = (canvasSize.Width / ticTacToeBoard.boardSize.Width);    
            double squareHeight = (canvasSize.Height / ticTacToeBoard.boardSize.Height);

            int tileColumn = (int)(position.Width / squareWidth);
            int tileRow = (int)(position.Height / squareHeight);

            return ticTacToeBoard.TakeTurn(tileColumn, tileRow);
        }
    }
}
