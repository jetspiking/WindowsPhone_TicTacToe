﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace TicTacToe.Classes
{
    public interface IDialogCallback
    {
        void NotifyFromDialog(ContentDialogResult dialogResult);
    }
}
