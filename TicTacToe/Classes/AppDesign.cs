using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace TicTacToe.Classes
{
    public static class AppDesign
    {
        public const String APP_WALLPAPER_URI_PATH = "ms-appx:///Assets/Wallpapers/";
        public const String APP_AUDIO_URI_PATH = "ms-appx:///Assets/Audio/";
        public const String APP_MY_MAIL = "DustinHendriks2000@gmail.com";
        public const String APP_MY_LINKEDIN = "https://www.LinkedIn.com/in/DustinHendriks";
        public const String APP_MY_GITHUB = "https://www.github.com/jetspiking";

        public const String APP_STORAGE_GAME_AUDIO = "app_storage_game_audio";
        public const String APP_STORAGE_DARK_THEME = "app_storage_dark_theme";
        public const String APP_STORAGE_HOLIDAY_OVERLAY = "app_storage_holiday_overlay";

        public static int APP_PLAYER_ID_FONTSIZE = 50;

        public static bool enableGameAudio;
        public static bool useDarkTheme;
        public static bool showHolidayOverlay;

        public static void SetStatusBarColor(Color foregroundColor, Color backgroundColor)
        {
            if (ApiInformation.IsTypePresent(PropertyString.VIEWMANAGEMENT_STATUSBAR))
            {
                StatusBar statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    statusBar.BackgroundOpacity = 1.0;
                    if (foregroundColor != null)
                        statusBar.ForegroundColor = foregroundColor;
                    if (backgroundColor != null)
                        statusBar.BackgroundColor = backgroundColor;
                }
            }
        }
    }
}
