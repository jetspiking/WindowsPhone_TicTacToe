using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TicTacToe.Classes
{
    public class ScalableTicTacToe
    {

        public int[,] board;
        public int winCount;
        public int playerTurnId = 0;
        public int playerWinId = -1;
        public int turnCounter = 0;

        public TicTacToePlayer[] ticTacToePlayers { get; set; }
        public TicTacToeBoardSize boardSize { get; set; }
        public String fatalGameError { get; set; }


        public ScalableTicTacToe(TicTacToeBoardSize boardSize, int playerCount, int winCount = 3)
        {
            if (winCount > boardSize.Width && winCount > boardSize.Height)
                fatalGameError = TicTacToeBoard.ERROR_WINCOUNT_LARGER_THAN_BOARDSIZE;

            if (((boardSize.Width * boardSize.Height) / playerCount) < 4)
                fatalGameError = TicTacToeBoard.ERROR_BOARDSIZE_TOO_SMALL_FOR_PLAYERCOUNT;

            if (playerCount > TicTacToeBoard.GAME_MAX_PLAYER_COUNT)
                fatalGameError = TicTacToeBoard.ERROR_MAX_PLAYER_COUNT_EXCEEDED;

            board = new int[boardSize.Width, boardSize.Height];

            for (int column = 0; column < boardSize.Width; column++)
                for (int row = 0; row < boardSize.Height; row++)
                    board[column, row] = TicTacToeBoard.BOARD_ID_EMPTY;

            ticTacToePlayers = new TicTacToePlayer[playerCount];

            for (int i = 0; i < playerCount; i++)
                ticTacToePlayers[i] = new TicTacToePlayer(TicTacToeBoard.DEFAULT_PLAYER_STRING + (i + 1).ToString(), i);


            this.boardSize = boardSize;
            this.winCount = winCount;
        }

        public void NewGame()
        {
            board = new int[boardSize.Width, boardSize.Height];

            for (int column = 0; column < boardSize.Width; column++)
                for (int row = 0; row < boardSize.Height; row++)
                    board[column, row] = TicTacToeBoard.BOARD_ID_EMPTY;

            ticTacToePlayers = new TicTacToePlayer[ticTacToePlayers.Length];

            for (int i = 0; i < ticTacToePlayers.Length; i++)
                ticTacToePlayers[i] = new TicTacToePlayer(TicTacToeBoard.DEFAULT_PLAYER_STRING + (i + 1).ToString(), i);

            fatalGameError = null;
            playerTurnId = 0;
            turnCounter = 0;
            playerWinId = -1;
        }

        public bool TakeTurn(int column, int row)
        {
            if (column >= this.boardSize.Width || column < 0 || row >= this.boardSize.Height || row < 0)
                return false;

            if (board[column, row] != TicTacToeBoard.BOARD_ID_EMPTY)
                return false;

            board[column, row] = playerTurnId;

            if (++playerTurnId==ticTacToePlayers.Length)
                playerTurnId = 0;

            turnCounter++;

            return true;
        }

        private int[] ClearArray()
        {
            int[] playerIdWinCounter = new int[ticTacToePlayers.Length];
            for (int i = 0; i < ticTacToePlayers.Length; i++)
                playerIdWinCounter[i] = 0;
            return playerIdWinCounter;
        }

        public int CheckGameState()
        {
            if (!String.IsNullOrEmpty(fatalGameError))
                return TicTacToeBoard.GAME_ID_FATAL_GAME_ERROR;

            int horizontalCheck = CheckHorizontal();
            if (horizontalCheck != TicTacToeBoard.GAME_ID_NO_WINNER) return horizontalCheck;

            int verticalCheck = CheckVertical();
            if (verticalCheck != TicTacToeBoard.GAME_ID_NO_WINNER) return verticalCheck;

            int diagonalToRightCheck = CheckDiagonalToRight();
            if (diagonalToRightCheck != TicTacToeBoard.GAME_ID_NO_WINNER) return diagonalToRightCheck;

            int diagonalToLeftCheck = CheckDiagonalToLeft();
            if (diagonalToLeftCheck != TicTacToeBoard.GAME_ID_NO_WINNER) return diagonalToLeftCheck;

            bool allFilled = true;
            for (int column = 0; column < boardSize.Width; column++)
                for (int row = 0; row < boardSize.Height; row++)
                {
                    if (board[column, row]==TicTacToeBoard.BOARD_ID_EMPTY)
                    {
                        allFilled = false;
                        break;
                    }
                }

            if (allFilled)
                return TicTacToeBoard.GAME_ID_TIE;

            return TicTacToeBoard.GAME_ID_IN_PROGRESS;
        }

        private int CheckHorizontal()
        {
            for (int row = 0; row < boardSize.Height; row++)
                for (int column = 0; column < boardSize.Width; column++)
                {
                    int streakCounter;
                    int playerIndex = board[column, row];

                    for (streakCounter = 0; streakCounter < winCount; streakCounter++)
                    {
                        if (playerIndex == TicTacToeBoard.BOARD_ID_EMPTY)
                            break;
                        if (column + streakCounter >= boardSize.Width)
                            break;
                        if (board[column + streakCounter, row] != playerIndex)
                            break;
                    }

                    if (streakCounter == winCount)
                    {
                        playerWinId = playerIndex;
                        return TicTacToeBoard.GAME_ID_WON;
                    }
                }
            return TicTacToeBoard.GAME_ID_NO_WINNER;
        }

        private int CheckVertical()
        {
            for (int column = 0; column < boardSize.Width; column++)
                for (int row = 0; row < boardSize.Height; row++)
                {
                    int streakCounter;
                    int playerIndex = board[column, row];

                    for (streakCounter = 0; streakCounter < winCount; streakCounter++)
                    {
                        if (playerIndex == TicTacToeBoard.BOARD_ID_EMPTY)
                            break;
                        if (row + streakCounter >= boardSize.Height)
                            break;
                        if (board[column, row + streakCounter] != playerIndex)
                            break;
                    }

                    if (streakCounter == winCount)
                    {
                        playerWinId = playerIndex;
                        return TicTacToeBoard.GAME_ID_WON;
                    }
                }
            return TicTacToeBoard.GAME_ID_NO_WINNER;
        }

        private int CheckDiagonalToRight()
        {
            for (int row = 0; row < boardSize.Height; row++)
                for (int column = 0; column < boardSize.Width; column++)
                {
                    int streakCounter;
                    int playerIndex = board[column, row];

                    for (streakCounter =0; streakCounter < winCount; streakCounter++)
                    {
                        if (playerIndex == TicTacToeBoard.BOARD_ID_EMPTY)
                            break;
                        if (row + streakCounter >= boardSize.Height)
                            break;
                        if (column + streakCounter >= boardSize.Width)
                            break;
                        if (board[column + streakCounter, row + streakCounter] != playerIndex)
                            break;
                    }

                    if (streakCounter == winCount)
                    {
                        playerWinId = playerIndex;
                        return TicTacToeBoard.GAME_ID_WON;
                    }
                }
            return TicTacToeBoard.GAME_ID_NO_WINNER;
        }

        private int CheckDiagonalToLeft()
        {
            for (int row = 0; row < boardSize.Height; row++)
                for (int column = 0; column < boardSize.Width; column++)
                {
                    int streakCounter;
                    int playerIndex = board[column, row];

                    for (streakCounter = 0; streakCounter < winCount; streakCounter++)
                    {
                        if (playerIndex == TicTacToeBoard.BOARD_ID_EMPTY)
                            break;
                        if (row + streakCounter >= boardSize.Height)
                            break;
                        if (column - streakCounter < 0)
                            break;
                        if (board[column - streakCounter, row + streakCounter] != playerIndex)
                            break;
                    }
                    if (streakCounter == winCount)
                    {
                        playerWinId = playerIndex;
                        return TicTacToeBoard.GAME_ID_WON;
                    }

                }
            return TicTacToeBoard.GAME_ID_NO_WINNER;
        }

        private void NextTurn()
        {
            if (playerTurnId != ticTacToePlayers.Length - 1)
                playerTurnId++;
            else playerTurnId = 0;
        }
    }

    public class TicTacToeBoard
    {
        public const int GAME_ID_WON = 0;
        public const int BOARD_ID_EMPTY = -1;
        public const int GAME_ID_IN_PROGRESS = -1;
        public const int GAME_ID_FATAL_GAME_ERROR = -2;
        public const int GAME_ID_NO_WINNER = -3;
        public const int GAME_ID_TIE = -4;
        public const int GAME_MAX_PLAYER_COUNT = 4;

        public const String DEFAULT_PLAYER_STRING = "Player ";
        public const String ERROR_WINCOUNT_LARGER_THAN_BOARDSIZE = "winCount is larger than the boardsize, winning would be impossible!";
        public const String ERROR_BOARDSIZE_TOO_SMALL_FOR_PLAYERCOUNT = "There are too many players for the entered boardsize.";
        public const String ERROR_NOT_ENOUGH_PLAYERS = "You can't play the game on your own dummy! (AI's are also counted as players!)";
        public const String ERROR_MAX_PLAYER_COUNT_EXCEEDED = "You exceeded the max player count.";
    }

    public struct TicTacToeBoardSize
    {
        public int Width;
        public int Height;

        public TicTacToeBoardSize(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
        }
    }

    public class TicTacToePlayer
    {
        private String name;
        private int characterId;

        public TicTacToePlayer(String name, int characterId)
        {
            this.name = name;
            this.characterId = characterId;
        }
    }
}
