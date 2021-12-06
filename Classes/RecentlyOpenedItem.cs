using System;

namespace ServiceNow
{
    public class RecentlyOpenedItem
    {
        string _item = "";
        string _url = "";
        string _datetimeopened;

        public RecentlyOpenedItem(string item, string url, DateTime time)
        {

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
}