using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Classes
{
    public static class StorageManager
    {
        private const String ERROR_NOSAVEDATALOCATED = "No save data located";
        private const String ERROR_NOTINITIALIZED = @"Have you called ""StorageManager.Init()""?";

        private static Windows.Storage.ApplicationDataContainer localSettings;
        private static Windows.Storage.StorageFolder localFolder;

        public static void Init()
        {
            localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        }

        public static Object ReadSimpleSetting(string id)
        {
            if (localSettings != null && localFolder != null)
            {
                try
                {
                    return localSettings.Values[id];
                }
                catch (System.NullReferenceException)
                {
                    Debug.WriteLine(ERROR_NOSAVEDATALOCATED);
                };
            }
            else DebugWarnNotInitialised();

            return null;
        }

        public static void WriteSimpleSetting(string id, Object toStore)
        {
            if (localSettings != null && localFolder != null)
                localSettings.Values[id] = toStore;
            else DebugWarnNotInitialised();
        }

        private static void DebugWarnNotInitialised()
        {
            Debug.WriteLine(ERROR_NOTINITIALIZED);
        }
    }
}
