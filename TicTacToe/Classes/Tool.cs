using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TicTacToe.Classes
{
    public static class Tool
    {
        private const String systemAccentColor="SystemAccentColor";

        public static Size GetScreenSize()
        {
            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            return new Size(bounds.Width, bounds.Height);
        }

        public static Color GetSystemAccentColor(FrameworkElement frameworkElement)
        {
            return (Color)frameworkElement.Resources[systemAccentColor];
        }

        public static void OpenWebsite(String url)
        {
            var uri = new Uri(url);
            Windows.System.Launcher.LaunchUriAsync(uri);
        }

        public static void ComposeEmail(String recipient)
        {
            var emailMessage = new Windows.ApplicationModel.Email.EmailMessage();
            emailMessage.Body = String.Empty;

            var emailRecipient = new Windows.ApplicationModel.Email.EmailRecipient(recipient);
            emailMessage.To.Add(emailRecipient);
            emailMessage.Subject = "TicTacToe Ultimate feedback";

            Windows.ApplicationModel.Email.EmailManager.ShowComposeNewEmailAsync(emailMessage);
        }

       
    }
}
