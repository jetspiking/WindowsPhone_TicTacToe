using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace TicTacToe.Classes
{
    public static class Scaler
    {
        public static Size GetScaleSize(Size targetSize, Size designSize)
        {
           return new Size((float)targetSize.Width / designSize.Width, (float)targetSize.Height / designSize.Height);
        }

        public static Transform2DEffect ScaleImage(CanvasBitmap source, Size size)
        {
            Transform2DEffect scaledImage;
            scaledImage = new Transform2DEffect();
            scaledImage.Source = source;
            scaledImage.TransformMatrix = Matrix3x2.CreateScale(new Vector2((float)size.Width, (float)size.Height));
            return scaledImage;
        }
    }
}
