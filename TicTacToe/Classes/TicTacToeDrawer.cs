using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TicTacToe.Classes
{
    public class TicTacToeDrawer
    {
        private ScalableTicTacToe ticTacToeBoard;
        private String[] playerCharacters = new String[TicTacToeBoard.GAME_MAX_PLAYER_COUNT];

        public TicTacToeDrawer(ScalableTicTacToe ticTacToeBoard)
        {
            this.ticTacToeBoard = ticTacToeBoard;
            playerCharacters[0] = "🎃";
            playerCharacters[1] = "💀";
            playerCharacters[2] = "🦇";
            playerCharacters[3] = "🕷";
        }

        public void DrawMoves(CanvasControl sender, CanvasDrawEventArgs args, Size canvasSize)
        {
            double squareWidth = (canvasSize.Width / ticTacToeBoard.boardSize.Width);
            double squareHeight = (canvasSize.Height / ticTacToeBoard.boardSize.Height);
            CanvasTextFormat format = new CanvasTextFormat { FontSize = AppDesign.APP_PLAYER_ID_FONTSIZE, WordWrapping = CanvasWordWrapping.NoWrap };
            
            AppDesign.APP_PLAYER_ID_FONTSIZE = (int)(squareWidth / 1.5);

            for (int column = 0; column < ticTacToeBoard.boardSize.Width; column++)
                for (int row = 0; row < ticTacToeBoard.boardSize.Height; row++)
                {
                    int index = ticTacToeBoard.board[column, row];
                    if (index < 0) continue;
                    string characterToPrint = AppDesign.showHolidayOverlay ? playerCharacters[index].ToString() : index.ToString();
                    CanvasTextLayout textLayout = new CanvasTextLayout(args.DrawingSession, characterToPrint, format, 0.0f, 0.0f);
                    Vector2 drawPosition = new Vector2((float)(squareWidth * column + squareWidth/2 - textLayout.LayoutBounds.Width/2), (float)squareHeight * row);
                    Rect layoutRectangle = new Rect(drawPosition.X + textLayout.DrawBounds.X, drawPosition.Y + textLayout.DrawBounds.Y, textLayout.DrawBounds.Width, textLayout.DrawBounds.Height);
                    args.DrawingSession.DrawRectangle(layoutRectangle, Colors.Black, 1.0f);
                    args.DrawingSession.DrawTextLayout(textLayout, drawPosition.X, drawPosition.Y, Tool.GetSystemAccentColor(sender));
                }
        }

        public void Draw(CanvasControl sender, CanvasDrawEventArgs args, Size canvasSize)
        {
            double squareWidth = (canvasSize.Width / ticTacToeBoard.boardSize.Width);
            double squareHeight = (canvasSize.Height / ticTacToeBoard.boardSize.Height);

            for (int column = 0; column < ticTacToeBoard.boardSize.Width; column++)
                for (int row = 0; row < ticTacToeBoard.boardSize.Height; row++)
                {
                    int borderOffset = 3;
                    float correctionOffset = .5f;

                    Rect rectangle = new Rect(new Point(squareWidth * column-correctionOffset, squareHeight * row-correctionOffset), new Point(squareWidth * column + squareWidth+correctionOffset, squareHeight * row + squareHeight+correctionOffset));
                    Rect fillRectangle = new Rect(new Point(squareWidth * column + borderOffset, squareHeight * row + borderOffset), new Point(squareWidth * column + squareWidth - borderOffset, squareHeight * row + squareHeight - borderOffset));

                    args.DrawingSession.FillRectangle(rectangle, Tool.GetSystemAccentColor(sender));
                    args.DrawingSession.FillRectangle(fillRectangle, AppDesign.useDarkTheme ? Colors.Black : Colors.White);

                }
        }
    }
}
