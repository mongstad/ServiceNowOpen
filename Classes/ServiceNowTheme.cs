using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using Brush = System.Windows.Media.Brush;
using BrushConverter = System.Windows.Media.BrushConverter;
using Color = System.Windows.Media.Color;
using Point = System.Drawing.Point;

namespace ServiceNow
{


    public class ServiceNowTheme
    {


        //Backgroundcolors
        string _titlebarbackgroundcolor = "";
        string _menubackgroundcolor = "";
        string _mainwindowbackgroundcolor = "";

        //Textcolors
        string _titlebartextcolor = "";
        string _mainwindowtextcolor = "";

        //Buttoncolors
        string _mainwindowbuttoncolor = "";
        string _menubuttoncolor = "";
        string _titlebarbuttoncolor = "";


        byte _r_menupanelbuttons = 255;
        byte _g_menupanelbuttons = 255;
        byte _b_menupanelbuttons = 255;

        byte _r_titlebarbuttons = 255;
        byte _g_titlebarbuttons = 255;
        byte _b_titlebarbuttons = 255;

        byte _r_mainwindowbuttons = 255;
        byte _g_mainwindowbuttons = 255;
        byte _b_mainwindowbuttons = 255;

        double _opacity = 0;

        private const string _menudefaultbackgroundcolor = "#1C1C1F";
        private const string _titlebardefaultbackgroundcolor = "#1C2C4D";
        private const string _mainwindowdefaultbackgroundcolor = "#1E2330";
        private const string _defaulttextcolor = "#ffffff";
        private const string _defaultbuttoncolor = "#ffffff";



        public ServiceNowTheme()
        {

        }


        public string MenuDefaultBackgroundColor
        {
            get { return _menudefaultbackgroundcolor; }

        }

        public string TitleBarDefaultBackgroundColor
        {
            get { return _titlebardefaultbackgroundcolor; }
        }

        public string MainWindowDefaultBackgroundColor
        {
            get { return _mainwindowdefaultbackgroundcolor; }
        }



        public string TitleBarTextColor
        {
            get { return _titlebartextcolor; }
            set { _titlebartextcolor = value; }
        }



        public string TitleBarBackgroundColor
        {
            get { return _titlebarbackgroundcolor; }
            set { _titlebarbackgroundcolor = value; }
        }



        public string TitleBarButtonColor
        {
            get { return _titlebarbuttoncolor; }
            set { _titlebarbuttoncolor = value; }
        }


        public string MainWindowTextColor
        {
            get { return _mainwindowtextcolor; }
            set { _mainwindowtextcolor = value; }
        }

        public string MainWindowBackgroundColor
        {
            get { return _mainwindowbackgroundcolor; }
            set { _mainwindowbackgroundcolor = value; }
        }

        public string MainWindowButtonColor
        {
            get { return _mainwindowbuttoncolor; }
            set { _mainwindowbuttoncolor = value; }
        }


        public string MenuButtonColor
        {
            get { return _menubuttoncolor; }
            set { _menubuttoncolor = value; }
        }

        public string MenuBackgroundColor
        {
            set { _menubackgroundcolor = value; }
            get { return _menubackgroundcolor; }
        }


        public string DefaultTextColor
        {
            get { return _defaulttextcolor; }
        }

        public string DefaultButtonColor
        {
            get { return _defaultbuttoncolor; }
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

            set
            {
                _r_menupanelbuttons = value[0];
                _g_menupanelbuttons = value[1];
                _b_menupanelbuttons = value[2];
            }

        }

        public byte[] MainWindowButtonsRGB
        {
            get
            {
                byte[] rgbArray = new byte[3];
                rgbArray[0] = _r_mainwindowbuttons;
                rgbArray[1] = _g_mainwindowbuttons;
                rgbArray[2] = _b_mainwindowbuttons;

                return rgbArray;
            }

            set
            {
                _r_mainwindowbuttons = value[0];
                _g_mainwindowbuttons = value[1];
                _b_mainwindowbuttons = value[2];
            }

        }

        public byte[] TitleBarButtonsRGB
        {
            get
            {
                byte[] rgbArray = new byte[3];
                rgbArray[0] = _r_titlebarbuttons;
                rgbArray[1] = _g_titlebarbuttons;
                rgbArray[2] = _b_titlebarbuttons;

                return rgbArray;
            }

            set
            {
                _r_titlebarbuttons = value[0];
                _g_titlebarbuttons = value[1];
                _b_titlebarbuttons = value[2];
            }

        }

        public double Opacity
        {
            get { return _opacity; }
            set { _opacity = value; }
        }



        public Brush ConvertHexColorToBrush(string brushstring)
        {
            BrushConverter brushConverter = new BrushConverter();
            Brush getBrush = (Brush)brushConverter.ConvertFromString(brushstring);
            return getBrush;
        }



        public Brush ConvertRGBToBrush(byte r, byte g, byte b)
        {

            Color newColor = System.Windows.Media.Color.FromRgb(r, g, b);
            Brush brush = new SolidColorBrush(newColor);
            return brush;

        }

        public Brush ConvertRGBToBrush(double r, double g, double b)
        {

            Color newColor = System.Windows.Media.Color.FromRgb((byte)r, (byte)g, (byte)b);
            Brush brush = new SolidColorBrush(newColor);
            return brush;

        }



        public Brush ConvertRGBToBrush(int r, int g, int b)
        {

            Color newColor = System.Windows.Media.Color.FromRgb((byte)r, (byte)g, (byte)b);
            Brush brush = new SolidColorBrush(newColor);
            return brush;

        }


        public string ConvertRGBToHexColor(double r, double g, double b)
        {
            Color newColor = System.Windows.Media.Color.FromRgb((byte)r, (byte)g, (byte)b);
            Brush brush = new SolidColorBrush(newColor);

            BrushConverter brushConverter = new BrushConverter();
            string hexColor = brushConverter.ConvertToString(brush);

            return hexColor;
        }


        public string ConvertRGBToHexColor(byte r, byte g, byte b)
        {
            Color newColor = System.Windows.Media.Color.FromRgb((byte)r, (byte)g, (byte)b);
            Brush brush = new SolidColorBrush(newColor);

            BrushConverter brushConverter = new BrushConverter();
            string hexColor = brushConverter.ConvertToString(brush);

            return hexColor;
        }

        public string ConvertRGBToHexColor(int r, int g, int b)
        {
            Color newColor = System.Windows.Media.Color.FromRgb((byte)r, (byte)g, (byte)b);
            Brush brush = new SolidColorBrush(newColor);

            BrushConverter brushConverter = new BrushConverter();
            string hexColor = brushConverter.ConvertToString(brush);

            return hexColor;
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

        public void ResetTextColorandBackgroundColorToDefault()
        {
            TitleBarBackgroundColor = TitleBarDefaultBackgroundColor;
            MenuBackgroundColor = MenuDefaultBackgroundColor;
            MainWindowBackgroundColor = MainWindowDefaultBackgroundColor;
            TitleBarTextColor = DefaultTextColor;
            MainWindowTextColor = DefaultTextColor;

        }

    }

}