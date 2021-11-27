using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Media;
using System.Xml.Serialization;
using Brush = System.Windows.Media.Brush;
using Color = System.Windows.Media.Color;
using BrushConverter = System.Windows.Media.BrushConverter;

namespace ServiceNow
{
    public class RecentlyOpenedItems
    {
        private ObservableCollection<RecentlyOpenedItem> _recentlyOpenedItems = new ObservableCollection<RecentlyOpenedItem>();
        public RecentlyOpenedItems()
        {

        }

        public ObservableCollection<RecentlyOpenedItem> RecentItems
        {
            get { return _recentlyOpenedItems; }
            //set { _recentlyOpenedItems = value; }
        }
    }

    
    public class ServiceNowItem
    {
        bool _isIncident = false;
        bool _isPrivateTask = false;
        bool _isTask = false;
        bool _isProblem = false;
        bool _isRequest = false;
        bool _isRequestedItem = false;
        bool _isUserName = false;
        bool _isComputer = false;
        bool _isMonitor = false;
        bool _isKBArticle = false;
        bool _forceFreeTextSearch = false;
        string _searchText = "";
        string _item = "";
        string _url = "";


        public ServiceNowItem(string item)
        {
            _searchText = item;
            _item = item.ToUpper();
            RunAllChecks();
        }

        public string Item
        {
            get { return _item; }
        }

        public string Url
        {
            get { return _url; }
        }
        private string SearchText
        {
            get { return _searchText; }
        }

        public bool ForceFreeTextSearch
        {
            get { return _forceFreeTextSearch; }
            set { _forceFreeTextSearch = value; }
        }
        private void CheckIfIncident(string item)
        {
            if (item.Length > 3)
            {
                string threeLetterPrefix = item.Substring(0, 3);
                int threeLetterSuffix;
                bool isThreeLetterPrefixSuffixNumeric = Int32.TryParse(item.Substring(3, 1), out threeLetterSuffix);

                if (threeLetterPrefix.ToUpper() == "INC")
                {
                    if (isThreeLetterPrefixSuffixNumeric)
                    {
                        _isIncident = true;

                    }
                    else { _isIncident = false; }

                }
            }

        }
        private void CheckIfKBArticle(string item)
        {
            if (item.Length > 2)
            {
                string twoLetterPrefix = item.Substring(0, 2);
                int twoLetterSuffix;
                bool isThreeLetterPrefixSuffixNumeric = Int32.TryParse(item.Substring(2, 1), out twoLetterSuffix);

                if (twoLetterPrefix.ToUpper() == "KB")
                {
                    if (isThreeLetterPrefixSuffixNumeric)
                    {
                        _isKBArticle = true;

                    }
                    else { _isKBArticle = false; }

                }

            }


        }

        private void CheckIfRequest(string item)
        {
            if (item.Length > 3)
            {
                string threeLetterPrefix = item.Substring(0, 3);
                int threeLetterSuffix;
                bool isThreeLetterPrefixSuffixNumeric = Int32.TryParse(item.Substring(3, 1), out threeLetterSuffix);

                if (threeLetterPrefix.ToUpper() == "REQ")
                {
                    if (isThreeLetterPrefixSuffixNumeric)
                    {
                        _isRequest = true;

                    }
                    else { _isRequest = false; }
                }
            }

        }

        private void CheckIfRequestedItem(string item)
        {
            if (item.Length > 4)
            {
                string fourLetterPrefix = item.Substring(0, 4);
                int fourLetterSuffix;
                bool isFourLetterPrefixSuffixNumeric = Int32.TryParse(item.Substring(4, 1), out fourLetterSuffix);

                if (fourLetterPrefix.ToUpper() == "RITM")
                {
                    if (isFourLetterPrefixSuffixNumeric)
                    {
                        _isRequestedItem = true;

                    }
                    else { _isRequestedItem = false; }

                }
            }


        }

        private void CheckIfProblem(string item)
        {
            if (item.Length > 3)
            {
                string threeLetterPrefix = item.Substring(0, 3);
                int threeLetterSuffix;
                bool isThreeLetterPrefixSuffixNumeric = Int32.TryParse(item.Substring(3, 1), out threeLetterSuffix);

                if (threeLetterPrefix.ToUpper() == "PRB")
                {
                    if (isThreeLetterPrefixSuffixNumeric)
                    {
                        _isProblem = true;

                    }
                    else { _isProblem = false; }

                }
            }

        }
        private void CheckIfTask(string item)
        {
            if (item.Length > 4)
            {
                string fourLetterPrefix = item.Substring(0, 4);
                int fourLetterSuffix;
                bool isThreeLetterPrefixSuffixNumeric = Int32.TryParse(item.Substring(4, 1), out fourLetterSuffix);

                if (fourLetterPrefix.ToUpper() == "TASK")
                {
                    if (isThreeLetterPrefixSuffixNumeric)
                    {
                        _isTask = true;

                    }
                    else { _isTask = false; }

                }
            }

        }
        private void CheckIfPrivateTask(string item)
        {
            if (item.Length > 4)
            {
                string fourLetterPrefix = item.Substring(0, 4);
                int fourLetterSuffix;
                bool isFourLetterPrefixSuffixNumeric = Int32.TryParse(item.Substring(4, 1), out fourLetterSuffix);

                if (fourLetterPrefix.ToUpper() == "PTSK")
                {
                    if (isFourLetterPrefixSuffixNumeric)
                    {
                        _isPrivateTask = true;

                    }
                    else { _isPrivateTask = false; }

                }
            }


        }
        private void CheckIfMonitor(string item)
        {
            if (item.Length > 1)
            {
                string oneLetterPrefix = item.Substring(0, 1);
                int threeLetterSuffix;
                bool isoneLetterPrefixSuffixNumeric = Int32.TryParse(item.Substring(1, 1), out threeLetterSuffix);

                if (oneLetterPrefix.ToUpper() == "M")
                {
                    if (isoneLetterPrefixSuffixNumeric)
                    {
                        _isMonitor = true;

                    }
                    else { _isMonitor = false; }

                }
            }

        }

        private void CheckIfComputer(string item)
        {
            if (item.Length > 1)
            {

                string oneLetterPrefix = item.Substring(0, 1);
                int oneLetterSuffix;
                bool isoneLetterPrefixSuffixNumeric = Int32.TryParse(item.Substring(1, 1), out oneLetterSuffix);

                if (oneLetterPrefix.ToUpper() == "L" || oneLetterPrefix.ToUpper() == "D")
                {
                    if (isoneLetterPrefixSuffixNumeric)
                    {
                        _isComputer = true;

                    }
                    else { _isComputer = false; }

                }

            }

            if (item.Length > 6)
            {
                string laptopPrefix = item.Substring(0, 6);
                if (laptopPrefix.ToUpper() == "LAPTOP")
                {
                    _isComputer = true;
                }

            }
        }
        private void CheckIfUserName(string item)
        {
            int intfromString;
            bool isStringNumber = System.Int32.TryParse(item, out intfromString);
            if (isStringNumber)
            {
                _isUserName = true;
            }

        }

        private void RunAllChecks()
        {
            CheckIfIncident(SearchText);
            CheckIfComputer(SearchText);
            CheckIfKBArticle(SearchText);
            CheckIfMonitor(SearchText);
            CheckIfPrivateTask(SearchText);
            CheckIfUserName(SearchText);
            CheckIfPrivateTask(SearchText);
            CheckIfRequest(SearchText);
            CheckIfRequestedItem(SearchText);
            CheckIfProblem(SearchText);
            CheckIfTask(SearchText);

        }

        public void ExecuteProcess()
        {
            bool launched = false;

            if (ForceFreeTextSearch)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=textsearch.do?sysparm_search=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=textsearch.do?sysparm_search=" + SearchText);
                return;
            }


            if (_isIncident)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=incident.do?sysparm_query=number=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=incident.do?sysparm_query=number=" + SearchText);
                launched = true;
            }

            if (_isUserName)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=sys_user.do?sysparm_query=user_name=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=sys_user.do?sysparm_query=user_name=" + SearchText);
                launched = true;
            }

            if (_isComputer)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=cmdb_ci_computer.do?sysparm_query=name=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=cmdb_ci_computer.do?sysparm_query=name=" + SearchText);
                launched = true;
            }

            if (_isMonitor)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=cmdb_ci_peripheral.do?sysparm_query=name=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=cmdb_ci_peripheral.do?sysparm_query=name=" + SearchText);
                launched = true;
            }

            if (_isTask)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=sc_task.do?sysparm_query=number=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=sc_task.do?sysparm_query=number=" + SearchText);
                launched = true;
            }

            if (_isPrivateTask)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=vtb_task.do?sysparm_query=number=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=vtb_task.do?sysparm_query=number=" + SearchText);
                launched = true;
            }



            if (_isProblem)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=problem.do?sysparm_query=number=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=problem.do?sysparm_query=number=" + SearchText);
                launched = true;
            }

            if (_isRequest)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=sc_request.do?sysparm_query=number=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=sc_request.do?sysparm_query=number=" + SearchText);
                launched = true;
            }

            if (_isRequestedItem)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=sc_req_item.do?sysparm_query=number=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=sc_req_item.do?sysparm_query=number=" + SearchText);
                launched = true;
            }

            if (_isKBArticle)
            {
                _url = "https://uis.service-now.com/sp?id=kb_article&sys_id=" + SearchText;
                Process.Start("https://uis.service-now.com/sp?id=kb_article&sys_id=" + SearchText);
                launched = true;
            }

            if (launched == false)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=textsearch.do?sysparm_search=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=textsearch.do?sysparm_search=" + SearchText);
            }
        }

    }
    public class RecentlyOpenedItem
    {
        string _item = "";
        string _url = "";
        string _datetimeopened;

        public RecentlyOpenedItem(string item, string url, DateTime time)
        {
            //TODO Add DateTime property 
            _item = item;
            _url = url;
            _datetimeopened = time.ToString("dd.MM.yyyy HH:mm");
        }

        public RecentlyOpenedItem()
        {

        }

        public string Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public string DateTimeOpened
        {
            get { return _datetimeopened; }
            set { _datetimeopened = value; }
        }

    }
    public class Settings
    {
        double _opacity = 1.0;
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

        public Settings()
        {

            BrushConverter brushConv = new BrushConverter();
            

        }

       

        public bool MinimizeToTray
        {
            get { return _minimizetotray; }
            set { _minimizetotray = value;}
        }

        public string TitleBarBrushString
        {
            get { return _titlebarbrush; }
            set{ _titlebarbrush = value; }
        }

        public string MenuBrushString
        {
           get{ return _menubrush; }
           set{ _menubrush = value; }
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
        public double Opacity
        {

            get { return _opacity; }
            set { _opacity = value; }
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

            using (FileStream fs = new FileStream("snSettings.xml", FileMode.Open))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Settings));
                return (Settings)xml.Deserialize(fs);
            }

        }
        public void Save(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Settings));
                xml.Serialize(fs, this);
            }
        }

        public void Load(string filename)
        {

        }

    }

    public class ServiceNowTheme
    {

        Brush _titlebarbackground;
        Brush _menubackground;
        Brush _centerwindowbackground;

        private const string _menudefaulthexcolor = "#1C1C1F";
        private const string _titlebardefaulthexcolor = "#1C2C4D";
        private const string _mainwindowdefaulthexcolor = "#1E2330";

        public ServiceNowTheme()
        {

        }

        public string TitleBarDefaultHexColor
        {
            get{ return _titlebardefaulthexcolor; }
        }

        public string MenuDefaultHexColor
        {
            get{ return _menudefaulthexcolor; }
        }

        public string CenterWindowDefaultHexColor
        {
            get{ return _mainwindowdefaulthexcolor; }
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
            Color defaultTitleColor = new Color();
            defaultTitleColor.R = titleColor.R;
            defaultTitleColor.G = titleColor.G;
            defaultTitleColor.B = titleColor.B;
            defaultTitleColor.A = titleColor.A;
            Brush titleBrush = new SolidColorBrush(defaultTitleColor);
            titleBrush.Opacity = 1;
            _titlebarbackground = titleBrush;

            System.Drawing.Color menuColor = ConvertFromHexToRGB(MenuDefaultHexColor);
            Color defaultMenuColor = new Color();
            defaultMenuColor.R = menuColor.R;
            defaultMenuColor.G = menuColor.G;
            defaultMenuColor.B = menuColor.B;
            defaultMenuColor.A = menuColor.A;
            Brush menuBrush = new SolidColorBrush(defaultMenuColor);
            menuBrush.Opacity = 1;  
            _menubackground = menuBrush;

            System.Drawing.Color centerWindowColor = ConvertFromHexToRGB(CenterWindowDefaultHexColor);
            Color defaultcenterWindowColor = new Color();
            defaultcenterWindowColor.R = centerWindowColor.R;
            defaultcenterWindowColor.G = centerWindowColor.G;
            defaultcenterWindowColor.B = centerWindowColor.B;
            defaultcenterWindowColor.A = centerWindowColor.A;
            Brush centerWindowBrush = new SolidColorBrush(defaultcenterWindowColor);
            centerWindowBrush.Opacity = 1;
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

    }

   

}
