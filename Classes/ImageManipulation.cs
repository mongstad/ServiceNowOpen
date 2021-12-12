using System;
using System.Windows;
using Point = System.Drawing.Point;
using System.Drawing;
using System.Drawing.Imaging;
using Image = System.Drawing.Image;
using System.Windows.Media.Imaging;
using Rectangle = System.Drawing.Rectangle;
using System.Threading;

namespace ServiceNowOpen
{
    public static class ImageManipulation
    {
        public static BitmapSource ChangeImageColor(int alpha, int red, int green, int blue ,string filename)
        {
            BitmapSource newImage = ConvertImageColor(alpha, red , green, blue, filename);
            return newImage;
        }

        private static BitmapSource ConvertImageColor(int alpha, int red, int green, int blue,string filename)
        {
           

            System.Drawing.Color newColor = System.Drawing.Color.FromArgb(alpha, red, green, blue);
            Image originalImage = Image.FromFile(filename);
            BitmapSource newImage = ToColorTone(originalImage, newColor);

            return newImage;

        }

        private static BitmapSource ToColorTone(Image image, System.Drawing.Color color)
        {

            //int brightness = color.A;
            //float scale = brightness;

            float r = color.R / 255f;
            float g = color.G / 255f;
            float b = color.B / 255f;



            ColorMatrix cm = new ColorMatrix(new float[][]
            {
                new float[] {r, 0, 0, 0, 0},
                new float[] {0, g, 0, 0, 0},
                new float[] {0, 0, b, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            });

            ImageAttributes ImAttribute = new ImageAttributes();
            ImAttribute.SetColorMatrix(cm);


            Point[] points =
            {
                new Point(0, 0),
                new Point(image.Width - 1, 0),
                new Point(0, image.Height - 1),
            };
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

            Bitmap myBitmap = new Bitmap(image.Width, image.Height);
            using(Graphics graphics = Graphics.FromImage(myBitmap))
            {
                graphics.DrawImage(image, points, rect, GraphicsUnit.Pixel, ImAttribute);
            }


            var bitmapData = myBitmap.LockBits(
            new System.Drawing.Rectangle(0, 0, myBitmap.Width, myBitmap.Height),
            System.Drawing.Imaging.ImageLockMode.ReadOnly, myBitmap.PixelFormat);

            var bitmapSource = BitmapSource.Create(
                bitmapData.Width, bitmapData.Height,
                myBitmap.HorizontalResolution, myBitmap.VerticalResolution,
                System.Windows.Media.PixelFormats.Bgra32, null,
                bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

            myBitmap.UnlockBits(bitmapData);

            return bitmapSource;

        }

    }
}
