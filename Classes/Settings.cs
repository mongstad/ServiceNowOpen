using System.IO;
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
        bool _hidefromtaskbar = false;
        ServiceNowTheme _servicenowtheme = new ServiceNowTheme();
        RecentlyOpenedItems _recentlyopeneditems = new RecentlyOpenedItems();
        string _urlservicenowportal = "";
        string _regexconfigurationitems = "";
        string _regexperipherals = "";
        string _regexusernames = "";

        public Settings()
        {


        }

        public string URLServiceNowPortal
        {
            get { return _urlservicenowportal; }
            set { _urlservicenowportal = value; }
        }

        public string RegExConfigurationItems
        {
            get { return _regexconfigurationitems; }
            set { _regexconfigurationitems = value; }
        }

        public string RegExPeripherals
        {
            get { return _regexperipherals; }
            set { _regexperipherals = value; }
        }

        public string RegExUsernames
        {
            get { return _regexusernames; }
            set { _regexusernames = value; }
        }

        public double Opacity
        {
            get { return _opacity; }
            set { _opacity = value; }
        }

        public bool HideFromTaskbar
        {
            get { return _hidefromtaskbar; }
            set { _hidefromtaskbar = value; }
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