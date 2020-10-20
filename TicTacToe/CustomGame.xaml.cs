using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TicTacToe.Classes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TicTacToe
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomGame : Page, IDialogCallback
    {
        public CustomGame()
        {
            this.InitializeComponent();
            ConfigureLayoutSizes();
            ConfigurePageColors();
            MainGrid.Background = new SolidColorBrush(AppDesign.useDarkTheme ? Colors.Black : Colors.White);
            this.RequestedTheme = AppDesign.useDarkTheme ? ElementTheme.Dark : ElementTheme.Light;
        }

        private void ConfigureLayoutSizes()
        {
        }

        private void ConfigurePageColors()
        {
            SolidColorBrush accentColorBrush = new SolidColorBrush(Tool.GetSystemAccentColor(this));
            Button_Back.Background = accentColorBrush;
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            TicTacToeSetup.ticTacToeSetup = new TicTacToeSetup();
            TicTacToeSetup.ticTacToeSetup.width = (int)WidthSlider.Value;
            TicTacToeSetup.ticTacToeSetup.height = (int)HeightSlider.Value;
            TicTacToeSetup.ticTacToeSetup.amountOfPlayers = (int)PlayerCount.Value;
            TicTacToeSetup.ticTacToeSetup.useAI = false;
            TicTacToeSetup.ticTacToeSetup.winCount = (int)ChainCount.Value;

            ScalableTicTacToe ticTacToeGame = new ScalableTicTacToe(new TicTacToeBoardSize(TicTacToeSetup.ticTacToeSetup.width, TicTacToeSetup.ticTacToeSetup.height), TicTacToeSetup.ticTacToeSetup.amountOfPlayers, TicTacToeSetup.ticTacToeSetup.winCount);

            if (ticTacToeGame.fatalGameError==null || ticTacToeGame.fatalGameError==String.Empty)
                this.Frame.Navigate(typeof(GameView), null);

            else
            {
                CustomDialog customDialog = new CustomDialog(this, "Oops!", ticTacToeGame.fatalGameError, "", "Close");
                customDialog.ShowDialog();
            }
        }

        public void NotifyFromDialog(ContentDialogResult dialogResult)
        {
            
        }
    }
}
