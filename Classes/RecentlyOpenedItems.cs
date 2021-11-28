using System.Collections.ObjectModel;


namespace ServiceNow{
    public class RecentlyOpenedItems
    {
#pragma warning disable IDE0044 // Add readonly modifier
        private ObservableCollection<RecentlyOpenedItem> _recentlyOpenedItems = new ObservableCollection<RecentlyOpenedItem>();
#pragma warning restore IDE0044 // Add readonly modifier
        public RecentlyOpenedItems()
        {

        }

        public ObservableCollection<RecentlyOpenedItem> RecentItems
        {
            get { return _recentlyOpenedItems; }
            //set { _recentlyOpenedItems = value; }
        }
    }
}