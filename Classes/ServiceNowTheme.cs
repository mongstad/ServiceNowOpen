using System;
using System.Windows.Media;
using Brush = System.Windows.Media.Brush;
using Color = System.Windows.Media.Color;
using BrushConverter = System.Windows.Media.BrushConverter;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Navigation;

namespace ServiceNow{
  
    
    public class ServiceNowTheme
    {

        Brush _titlebarbackground;
        Brush _menubackground;
        Brush _centerwindowbackground;
        byte _r_menupanelbuttons = 255;
        byte _g_menupanelbuttons = 255;
        byte _b_menupanelbuttons = 255;

        double _opacity = 0;
        //bool _invertedcolors = false;
        private const string _menudefaulthexcolor = "#1C1C1F";
        private const string _titlebardefaulthexcolor = "#1C2C4D";
        private const string _mainwindowdefaulthexcolor = "#1E2330";

        bool _titlebarimagesinverted = false;
        bool _titlebartextcolorinverted = false;
        bool _windowcontentimagesinverted = false;
        bool _windowscontenttextcolorinverted = false;
        bool _menupanelimagesinverted = false;


        public ServiceNowTheme()
        {

        }

        public byte[] MenuPanelButtonsRGB
        {
          get
            {
                byte[] rgbArray = new byte[3];
                rgbArray[0] = _r_menupanelbuttons;
                rgbArray[1] = _g_menupanelbuttons;
                rgbArray[2] = _b_menupanelbuttons;

                return rgbArray;
            }

            set{
                _r_menupanelbuttons = value[0];
                _g_menupanelbuttons = value[1];
                _b_menupanelbuttons = value[2];
            }

        }

        public double Opacity
        {
            get { return _opacity; }
            set { _opacity = value; }
        }

        public bool TitleBarButtonImagesInverted
        {
            get { return _titlebarimagesinverted; }
            set { _titlebarimagesinverted = value; }
        }

        public bool TitleBarTextColorInverted
        {
            get { return _titlebartextcolorinverted; }
            set { _titlebartextcolorinverted = value; }
        }

        public bool WindowContentButtonImagesInverted
        {
            get { return _windowcontentimagesinverted; }
            set { _windowcontentimagesinverted = value; }
        }

        public bool WindowContentTextColorInverted
        {
            get { return _windowscontenttextcolorinverted; }
            set { _windowscontenttextcolorinverted = value; }
        }

        public bool MenuPanelImagesInverted
        {
            get { return _menupanelimagesinverted; }
            set { _menupanelimagesinverted = value; }
        }



        public string TitleBarDefaultHexColor
        {
            get { return _titlebardefaulthexcolor; }
        }

        public string MenuDefaultHexColor
        {
            get { return _menudefaulthexcolor; }
        }

        public string CenterWindowDefaultHexColor
        {
            get { return _mainwindowdefaulthexcolor; }
        }


        public Brush ConvertStringToBrush(string brushstring)
        {
            BrushConverter brushConverter = new BrushConverter();
            Brush getBrush = (Brush)brushConverter.ConvertFromString(brushstring);
            return getBrush;
        }

        public Brush TitleBarBackground
        {
            get { return _titlebarbackground; }
            set { _titlebarbackground = value; }
        }

        public WriteableBitmap ConvertImageColor(byte red, byte green, byte blue, byte alpha, string imagepath)
        {
            Uri uri = new Uri(imagepath, UriKind.Relative);
            StreamResourceInfo resourceInfo = Application.GetResourceStream(uri);
            StreamResourceInfo x = resourceInfo;
            BitmapDecoder dec = BitmapDecoder.Create(resourceInfo.Stream, BitmapCreateOptions.None, BitmapCacheOption.Default);
            BitmapFrame image = dec.Frames[0];
            byte[] pixels = new byte[image.PixelWidth * image.PixelHeight * 4];
            image.CopyPixels(pixels, image.PixelWidth * 4, 0);

            var bmp = new WriteableBitmap(image.PixelWidth, image.PixelHeight, image.DpiX, image.DpiY, PixelFormats.Pbgra32, null);

            for(int i = 0; i < pixels.Length / 4; ++i)
            {
                byte b = pixels[i * 4];
                byte g = pixels[i * 4 + 1];
                byte r = pixels[i * 4 + 2];
                byte a = pixels[i * 4 + 3];


                if((r == 255 &&
                    g == 255 &&
                    b == 255 &&
                    a == 255) ||
                (a != 0 && a != 255 &&
                    r == g && g == b && r != 0))
                {

                    {

                        r = red;
                        g = green;
                        b = blue;
                        a = alpha;

                        pixels[i * 4] = b;
                        pixels[i * 4 + 1] = g;
                        pixels[i * 4 + 2] = r;
                        pixels[i * 4 + 3] = a;

                    }

                }

                bmp.WritePixels(new Int32Rect(0, 0, image.PixelWidth, image.PixelHeight), pixels, image.PixelWidth * 4, 0);

                // Source: https://stackoverflow.com/questions/20856424/wpf-modifying-image-colors-on-the-fly-c
            }

            return bmp;
        }

        public Brush MenuBackground
        {
            get { return _menubackground; }
            set { _menubackground = value; }
        }

        public Brush CenterWindowBackground
        {
            get { return _centerwindowbackground; }
            set { _centerwindowbackground = value; }
        }

        public void ResetToDefaultTheme()
        {
            System.Drawing.Color titleColor = ConvertFromHexToRGB(TitleBarDefaultHexColor);
            Color defaultTitleColor = new Color
            {
                R = titleColor.R,
                G = titleColor.G,
                B = titleColor.B,
                A = titleColor.A
            };


            Brush titleBrush = new SolidColorBrush(defaultTitleColor)
            {
                Opacity = 1,

            };

            _titlebarbackground = titleBrush;

            System.Drawing.Color menuColor = ConvertFromHexToRGB(MenuDefaultHexColor);
            Color defaultMenuColor = new Color
            {
                R = menuColor.R,
                G = menuColor.G,
                B = menuColor.B,
                A = menuColor.A
            };

            Brush menuBrush = new SolidColorBrush(defaultMenuColor)
            {
                Opacity = 1
            };
            _menubackground = menuBrush;

            System.Drawing.Color centerWindowColor = ConvertFromHexToRGB(CenterWindowDefaultHexColor);
            Color defaultcenterWindowColor = new Color
            {
                R = centerWindowColor.R,
                G = centerWindowColor.G,
                B = centerWindowColor.B,
                A = centerWindowColor.A
            };
            Brush centerWindowBrush = new SolidColorBrush(defaultcenterWindowColor)
            {
                Opacity = 1
            };
            _centerwindowbackground = centerWindowBrush;




        }

        public Brush ConvertRGBToBackgroundColor(byte r, byte g, byte b)
        {

            Color newColor = System.Windows.Media.Color.FromRgb(r, g, b);
            Brush brush = new SolidColorBrush(newColor);
            return brush;

        }

        public string ConvertFromRGBToHex(byte r, byte g, byte b)
        {
            Color myColor = Color.FromRgb(r, g, b);
            string hex = myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");
            return hex;
        }

        public System.Drawing.Color ConvertFromHexToRGB(string hexcolorvalue)
        {

            System.Drawing.Color _color = System.Drawing.ColorTranslator.FromHtml(hexcolorvalue);

            return _color;

        }

        public Brush WhiteBrush()
        {

            Color whiteColor = new Color
            {
                R = 255,
                G = 255,
                B = 255,
                A = 255

            };

            System.Windows.Media.Brush whiteBrush = new SolidColorBrush(whiteColor)
            {
                Opacity = 1,
                Color = whiteColor,


            };

            return whiteBrush;
        }

        public Brush BlackBrush()
        {

            Color blackColor = new Color
            {
                R = 0,
                G = 0,
                B = 0,
                A = 255

            };

            System.Windows.Media.Brush blackBrush = new SolidColorBrush(blackColor)
            {
                Opacity = 1,
                Color = blackColor,

            };

            return blackBrush;
        }

    }

}