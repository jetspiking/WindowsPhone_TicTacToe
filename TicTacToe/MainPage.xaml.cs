using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Threading.Tasks;
using TicTacToe.Classes;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace TicTacToe
{
    public sealed partial class MainPage : Page, IDialogCallback
    {
        private bool lastKnownOrientationWasPortrait = true;

        public MainPage()
        {
            this.InitializeComponent();
            AppDesign.SetStatusBarColor((Color)this.Resources[PropertyString.SYSTEM_ACCENT_COLOR], Colors.Black);
            Window.Current.SizeChanged += Current_SizeChanged;

            ConfigureLayoutSizes();
            ConfigurePageColors();
            InitializeStorageManager();

            MainGrid.Background = new SolidColorBrush(AppDesign.useDarkTheme ? Colors.Black : Colors.White);
            this.RequestedTheme = AppDesign.useDarkTheme ? ElementTheme.Dark : ElementTheme.Light;

            LoadHolidayBackground();
        }

        private void InitializeStorageManager()
        {
            StorageManager.Init();

            Object gameAudio = StorageManager.ReadSimpleSetting(AppDesign.APP_STORAGE_GAME_AUDIO);
            if (gameAudio != null)
                EnableGameAudio.IsChecked = (bool)gameAudio;
            else
            {
                EnableGameAudio.IsChecked = true;
                StorageManager.WriteSimpleSetting(AppDesign.APP_STORAGE_GAME_AUDIO, true);
            }
            AppDesign.enableGameAudio = EnableGameAudio.IsChecked;

            Object darkTheme = StorageManager.ReadSimpleSetting(AppDesign.APP_STORAGE_DARK_THEME);
            if (darkTheme != null)
                EnableDarkTheme.IsChecked = (bool)darkTheme;
            else
            {
                EnableDarkTheme.IsChecked = true;
                StorageManager.WriteSimpleSetting(AppDesign.APP_STORAGE_DARK_THEME, true);
            }
            AppDesign.useDarkTheme = EnableDarkTheme.IsChecked;

            Object holidayOverlay = StorageManager.ReadSimpleSetting(AppDesign.APP_STORAGE_HOLIDAY_OVERLAY);
            if (holidayOverlay != null)
                EnableHolidayOverlay.IsChecked = (bool)holidayOverlay;
            else
            {
                EnableHolidayOverlay.IsChecked = true;
                StorageManager.WriteSimpleSetting(AppDesign.APP_STORAGE_HOLIDAY_OVERLAY, true);
            }
            AppDesign.showHolidayOverlay = EnableHolidayOverlay.IsChecked;
        }

        private void LoadHolidayBackground()
        {
            if (AppDesign.showHolidayOverlay)
            {
                Size screenSize = Tool.GetScreenSize();
                String portraitOrLandscape = screenSize.Width <= screenSize.Height ? "portrait" : "landscape";
                OverlayGrid.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri($"{AppDesign.APP_WALLPAPER_URI_PATH}/special{portraitOrLandscape}.png")), Stretch = Stretch.UniformToFill };
            }
        }

        private void ConfigureLayoutSizes()
        {
            Size screenSize = Tool.GetScreenSize();

            int buttonWidth = (int)(screenSize.Width * 0.6);
            int buttonHeight = 150;

            Button_CreateGame.Width = buttonWidth;
            Button_CreateGame.Height = buttonHeight;
            Button_Settings.Width = buttonWidth;
            Button_Settings.Height = buttonHeight;
            Button_AboutMe.Width = buttonWidth;
            Button_AboutMe.Height = buttonHeight;
        }

        private void ConfigurePageColors()
        {
            SolidColorBrush accentColorBrush = new SolidColorBrush(Tool.GetSystemAccentColor(this));

            TextBlock_GameTitle.Foreground = accentColorBrush;
            Button_CreateGame.Background = accentColorBrush;
            Button_Settings.Background = accentColorBrush;
            Button_AboutMe.Background = accentColorBrush;
            Button_Close.Background = accentColorBrush;
        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            ConfigureLayoutSizes();

            Size screenSize = Tool.GetScreenSize();
            bool orientationIsPortrait = screenSize.Width <= screenSize.Height;
            if (lastKnownOrientationWasPortrait != orientationIsPortrait)
                LoadHolidayBackground();
            lastKnownOrientationWasPortrait = screenSize.Width <= screenSize.Height;
        }

        private void Button_CreateGame_Click(object sender, RoutedEventArgs e)
        {
            AudioPlayer.PlayClick();
            // ((Button)sender).Background = new SolidColorBrush(Tool.GetSystemAccentColor(this));
        }

        private void Button_Settings_Click(object sender, RoutedEventArgs e)
        {
            AudioPlayer.PlayClick();
            // ((Button)sender).Background = new SolidColorBrush(Tool.GetSystemAccentColor(this));
        }

        private void Button_AboutMe_Click(object sender, RoutedEventArgs e)
        {
            AudioPlayer.PlayClick();
            // ((Button)sender).Background = new SolidColorBrush(Tool.GetSystemAccentColor(this));
        }

        private void MenuFlyoutItem_Click_CustomGame(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CustomGame), null);
        }

        private void MenuFlyoutItem_Click_DefaultLocalPlayer(object sender, RoutedEventArgs e)
        {
            TicTacToeSetup.ticTacToeSetup = new TicTacToeSetup();
            TicTacToeSetup.ticTacToeSetup.width = 3;
            TicTacToeSetup.ticTacToeSetup.height = 3;
            TicTacToeSetup.ticTacToeSetup.amountOfPlayers = 2;
            TicTacToeSetup.ticTacToeSetup.useAI = false;
            TicTacToeSetup.ticTacToeSetup.winCount = 3;

            this.Frame.Navigate(typeof(GameView), null);
        }

        private void MenuFlyoutItem_Click_DefaultAIPlayer(object sender, RoutedEventArgs e)
        {
            TicTacToeSetup.ticTacToeSetup = new TicTacToeSetup();
            TicTacToeSetup.ticTacToeSetup.width = 3;
            TicTacToeSetup.ticTacToeSetup.height = 3;
            TicTacToeSetup.ticTacToeSetup.amountOfPlayers = 2;
            TicTacToeSetup.ticTacToeSetup.useAI = true;
            TicTacToeSetup.ticTacToeSetup.winCount = 3;

            this.Frame.Navigate(typeof(GameView), null);
        }

        private void ToggleMenuFlyoutItem_Click_ToggleGameAudio(object sender, RoutedEventArgs e)
        {
            bool useGameAudio = ((ToggleMenuFlyoutItem)sender).IsChecked;
            AppDesign.enableGameAudio = useGameAudio;
            StorageManager.WriteSimpleSetting(AppDesign.APP_STORAGE_GAME_AUDIO, useGameAudio);
        }

        private void ToggleMenuFlyoutItem_Click_ToggleDarkTheme(object sender, RoutedEventArgs e)
        {
            bool useDarkTheme = ((ToggleMenuFlyoutItem)sender).IsChecked;
            MainGrid.Background = new SolidColorBrush(useDarkTheme ? Colors.Black : Colors.White);
            AppDesign.useDarkTheme = useDarkTheme;
            this.RequestedTheme = AppDesign.useDarkTheme ? ElementTheme.Dark : ElementTheme.Light;
            StorageManager.WriteSimpleSetting(AppDesign.APP_STORAGE_DARK_THEME, useDarkTheme);
        }

        private void ToggleMenuFlyoutItem_Click_ToggleOverlay(object sender, RoutedEventArgs e)
        {
            bool useOverlay = ((ToggleMenuFlyoutItem)sender).IsChecked;
            AppDesign.showHolidayOverlay = useOverlay;
            if (useOverlay)
                LoadHolidayBackground();
            else OverlayGrid.Background = null;
            StorageManager.WriteSimpleSetting(AppDesign.APP_STORAGE_HOLIDAY_OVERLAY, useOverlay);
        }

        private void MenuFlyoutItem_Click_ContactMe(object sender, RoutedEventArgs e)
        {

            Tool.ComposeEmail(AppDesign.APP_MY_MAIL);
        }

        private void MenuFlyoutItem_Click_OpenLinkedIn(object sender, RoutedEventArgs e)
        {
            Tool.OpenWebsite(AppDesign.APP_MY_LINKEDIN);
        }

        private void MenuFlyoutItem_Click_ShowGitHub(object sender, RoutedEventArgs e)
        {
            Tool.OpenWebsite(AppDesign.APP_MY_GITHUB);
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            AudioPlayer.PlayClick();

            CustomDialog exitDialog = new CustomDialog(this, "Exit Application?", "Thank you for playing TicTacToe Ultimate!", "Ok", "Cancel");

            Task task = new Task(exitDialog.ShowDialog);
            task.RunSynchronously();
        }

        public void NotifyFromDialog(ContentDialogResult dialogResult)
        {
            if (dialogResult == ContentDialogResult.Primary)
                CoreApplication.Exit();
        }
    }
}
