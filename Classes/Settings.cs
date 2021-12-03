﻿using System.IO;
using System.Xml.Serialization;

namespace ServiceNow
{
    public class Settings
    {
        double _opacity = 1.0;
        double _top = -1;
        double _left = -1;
        double _sliderPosition = 100;
        bool _topmost = false;
        bool _freetextsearch = false;
        bool _minimizetotray = false;
        ServiceNowTheme _servicenowtheme = new ServiceNowTheme();
        RecentlyOpenedItems _recentlyopeneditems = new RecentlyOpenedItems();
       
        public Settings()
        {

            
        }

        public double Opacity
        {
            get { return _opacity; }
            set { _opacity = value; }
        }

        public bool MinimizeToTray
        {
            get { return _minimizetotray; }
            set { _minimizetotray = value; }
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

        public ServiceNowTheme ServiceNowTheme
        {
            get { return _servicenowtheme; }
            set { _servicenowtheme = value; }
        }

        public RecentlyOpenedItems RecentItems
        {
            get { return _recentlyopeneditems; }
            set { _recentlyopeneditems = value; }
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