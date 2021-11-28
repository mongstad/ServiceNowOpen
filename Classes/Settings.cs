using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Media;
using System.Xml.Serialization;
using Brush = System.Windows.Media.Brush;
using Color = System.Windows.Media.Color;
using BrushConverter = System.Windows.Media.BrushConverter;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Navigation;

namespace ServiceNow
{
    public class Settings
    {
        double _opacity = 1.0;
        bool _inverted = false;
        double _top = -1;
        double _left = -1;
        double _sliderPosition = 100;
        bool _topmost = false;
        bool _freetextsearch = false;
        bool _minimizetotray = false;
        RecentlyOpenedItems _recentlyopeneditems = new RecentlyOpenedItems();
        string _titlebarbrush = "";
        string _menubrush = "";
        string _centerwindowbrush = "";

        bool _titlebarimagesinverted = false;
        bool _titlebartextcolorinverted = false;
        bool _windowcontentimagesinverted = false;
        bool _windowscontenttextcolorinverted = false;
        bool _menupanelimagesinverted = false;

        public Settings()
        {

            // BrushConverter brushConv = new BrushConverter();


        }

        public bool TitleBarImagesInverted
        {
            get { return _titlebarimagesinverted; }
            set { _titlebarimagesinverted = value; }
        }

        public bool TitleBarTextColorInverted
        {
            get { return _titlebartextcolorinverted; }
            set { _titlebartextcolorinverted = value; }
        }

        public bool WindowContentImagesInverted
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

        public double Opacity
        {
            get { return _opacity; }
            set { _opacity = value; }
        }

        public bool Inverted
        {
            get { return _inverted; }
            set { _inverted = value; }
        }

        public bool MinimizeToTray
        {
            get { return _minimizetotray; }
            set { _minimizetotray = value; }
        }

        public string TitleBarBrushString
        {
            get { return _titlebarbrush; }
            set { _titlebarbrush = value; }
        }

        public string MenuBrushString
        {
            get { return _menubrush; }
            set { _menubrush = value; }
        }

        public string CenterWindowBrushString
        {
            get { return _centerwindowbrush; }
            set { _centerwindowbrush = value; }
        }
        public RecentlyOpenedItems RecentItems
        {
            get { return _recentlyopeneditems; }
            set { _recentlyopeneditems = value; }
        }
        public bool FreeTextSearch
        {
            get { return _freetextsearch; }
            set { _freetextsearch = value; }
        }
        public double SliderPosition
        {
            get { return _sliderPosition; }
            set { _sliderPosition = value; }
        }

        public bool TopMost
        {
            get { return _topmost; }
            set { _topmost = value; }

        }

        public double Top
        {
            get { return _top; }
            set { _top = value; }
        }

        public double Left
        {
            get { return _left; }
            set { _left = value; }
        }

        public void Save()
        {



            using(FileStream fs = new FileStream("snSettings.xml", FileMode.Create))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Settings));
                xml.Serialize(fs, this);
            }




        }

        public Settings Load()
        {

            using(FileStream fs = new FileStream("snSettings.xml", FileMode.Open))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Settings));
                return (Settings)xml.Deserialize(fs);
            }

        }
        public void Save(string filename)
        {
            using(FileStream fs = new FileStream(filename, FileMode.Create))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Settings));
                xml.Serialize(fs, this);
            }
        }

    }
}