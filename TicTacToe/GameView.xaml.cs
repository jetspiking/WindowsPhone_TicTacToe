using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TicTacToe.Classes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace TicTacToe
{
    public sealed partial class GameView : Page, IDialogCallback
    {
        private ScalableTicTacToe ticTacToeGame;
        private TicTacToeDrawer ticTacToeDrawer;
        private TicTacToeController ticTacToeController;

        private bool shouldStop = false;

        public GameView()
        {
            this.InitializeComponent();
            ConfigureLayoutSizes();
            ConfigurePageColors();
            MainGrid.Background = new SolidColorBrush(AppDesign.useDarkTheme ? Colors.Black : Colors.White);
            this.RequestedTheme = AppDesign.useDarkTheme ? ElementTheme.Dark : ElementTheme.Light;

            Window.Current.SizeChanged += Current_SizeChanged;

            ticTacToeGame = new ScalableTicTacToe(new TicTacToeBoardSize(TicTacToeSetup.ticTacToeSetup.width, TicTacToeSetup.ticTacToeSetup.height), TicTacToeSetup.ticTacToeSetup.amountOfPlayers, TicTacToeSetup.ticTacToeSetup.winCount);
            ticTacToeController = new TicTacToeController(ticTacToeGame);
            ticTacToeDrawer = new TicTacToeDrawer(ticTacToeGame);

            CalculateCanvasSize();
        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            CalculateCanvasSize();
        }

        private void CalculateCanvasSize()
        {
            Size screenSize = Tool.GetScreenSize();

            if (screenSize.Width <= screenSize.Height)
            {
                MainCanvas.Width = screenSize.Width;
                MainCanvas.Height = MainCanvas.Width;
            }
            else
            {
                MainCanvas.Height = screenSize.Height / 1.5;
                MainCanvas.Width = MainCanvas.Height;
            }
        }

        private void ConfigureLayoutSizes()
        {
        }

        private void ConfigurePageColors()
        {
            SolidColorBrush accentColorBrush = new SolidColorBrush(Tool.GetSystemAccentColor(this));
            Button_Back.Background = accentColorBrush;
        }

        private void MainCanvas_CreateResources(CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {

        }

        private void MainCanvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (!shouldStop)
            {
                MainCanvas.Invalidate();
                Size senderSize = new Size(sender.ActualWidth, sender.ActualHeight);
                ticTacToeDrawer.Draw(sender, args, senderSize);
                ticTacToeDrawer.DrawMoves(sender, args, senderSize);
            }
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                shouldStop = true;
                this.Frame.GoBack();
            }
        }

        private void HandleInput(object sender, Size clickPosition)
        {
            bool accepted = ticTacToeController.handleInput(clickPosition, new Size(MainCanvas.ActualWidth, MainCanvas.ActualHeight));

            if (accepted)
            {
                int gameState = ticTacToeGame.CheckGameState();

                if (TicTacToeSetup.ticTacToeSetup.useAI)
                    if (ticTacToeGame.ticTacToePlayers.Length <= 2)
                        if (ticTacToeGame.playerTurnId == 1)
                        {
                            Random random = new Random();

                            List<Size> availablePositions = new List<Size>();

                            for (int column = 0; column < ticTacToeGame.boardSize.Width; column++)
                                for (int row = 0; row < ticTacToeGame.boardSize.Height; row++)
                                {
                                    int index = ticTacToeGame.board[column, row];
                                    if (index == TicTacToeBoard.BOARD_ID_EMPTY)
                                        availablePositions.Add(new Size(column, row));
                                }

                            if (availablePositions.Count != 0)
                            {
                                int randomIndex = random.Next(availablePositions.Count);
                                Size randomPosition = availablePositions[randomIndex];
                                ticTacToeGame.TakeTurn((int)randomPosition.Width, (int)randomPosition.Height);
                                gameState = ticTacToeGame.CheckGameState();
                                //PlayerTurn.Text = $"Turn: Player {ticTacToeGame.playerTurnId} Moves: {ticTacToeGame.turnCounter}";
                            }
                        }

                switch (gameState)
                {
                    case TicTacToeBoard.GAME_ID_WON:
                        {
                            CustomDialog customDialog = new CustomDialog(this, $"Player {this.ticTacToeGame.playerWinId} Won The Game!", String.Empty, "New Game", "To Menu");
                            customDialog.ShowDialog();
                            break;
                        }
                    case TicTacToeBoard.GAME_ID_FATAL_GAME_ERROR:
                        break;
                    case TicTacToeBoard.GAME_ID_TIE:
                        {
                            CustomDialog customDialog = new CustomDialog(this, $"It's A Tie!", String.Empty, "New Game", "To Menu");
                            customDialog.ShowDialog();
                            break;
                        }
                }
            }
        }

        public void NotifyFromDialog(ContentDialogResult dialogResult)
        {
            if (dialogResult == ContentDialogResult.Primary)
                ticTacToeGame.NewGame();
            else
            {
                if (this.Frame.CanGoBack)
                {
                    shouldStop = true;
                    this.Frame.GoBack();
                }
            }
        }

        private void MainCanvas_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HandleInput(sender, new Size(e.GetPosition(MainCanvas).X, e.GetPosition(MainCanvas).Y));
        }
    }
}
