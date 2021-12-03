using System.Collections.ObjectModel;


namespace ServiceNow{
    public class RecentlyOpenedItems
    {

        private ObservableCollection<RecentlyOpenedItem> _recentitems = new ObservableCollection<RecentlyOpenedItem>();

        public RecentlyOpenedItems()
        {
           
        }

        public ObservableCollection<RecentlyOpenedItem> RecentItems
        {
            get { return _recentitems; }
            set { _recentitems = value; }
        }
    }
}