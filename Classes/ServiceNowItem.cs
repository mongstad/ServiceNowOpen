using System.Diagnostics;
using System.Text.RegularExpressions;
namespace ServiceNow
{
    public class ServiceNowItem
    {
        bool _isIncident = false;
        bool _isPrivateTask = false;
        bool _isTask = false;
        bool _isProblem = false;
        bool _isRequest = false;
        bool _isRequestedItem = false;
        bool _isUserName = false;
        bool _isperson = false;
        bool _isComputer = false;
        bool _isperipheral = false;
        bool _isKBArticle = false;
        bool _forceFreeTextSearch = false;
        string _urlservicenowportal;
        string _searchText = "";
        string _regexconfigurationitems = "";
        string _regexusernames = "";
        string _regexperipherals = "";
        string _item = "";
        string _url = "";


        public ServiceNowItem(string item, string regex_ci, string regex_peripherals, string regex_usernames, string urlservicenowportal)
        {
            _searchText = item;
            _regexconfigurationitems = regex_ci;
            _regexusernames = regex_usernames;
            _regexperipherals = regex_peripherals;
            _urlservicenowportal = urlservicenowportal;
            _item = item.ToUpper();
            RunAllChecks();
        }

        public ServiceNowItem()
        {

        }

        public string Item
        {
            get { return _item; }
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

        public string RegExUserNames
        {
            get { return _regexusernames; }
            set { _regexusernames = value; }
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

            Regex regex = new Regex(@"^(INC)\d{7}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if(regex.IsMatch(item))
            {
                _isIncident = true;
            }
            else
            {
                _isIncident = false;
            }

        }
        private void CheckIfKBArticle(string item)
        {
            Regex regex = new Regex(@"^(KB)\d{7}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if(regex.IsMatch(item))
            {
                _isKBArticle = true;
            }
            else
            {
                _isKBArticle = false;
            }

        }


        private void CheckIfRequest(string item)
        {
            Regex regex = new Regex(@"^(REQ)\d{7}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if(regex.IsMatch(item))
            {
                _isRequest = true;
            }
            else
            {
                _isRequest = false;
            }

        }

        private void CheckIfRequestedItem(string item)
        {
            Regex regex = new Regex(@"^(RITM)\d{7}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if(regex.IsMatch(item))
            {
                _isRequestedItem = true;
            }
            else
            {
                _isRequestedItem = false;
            }

        }

        private void CheckIfProblem(string item)
        {
            Regex regex = new Regex(@"^(PRB)\d{7}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if(regex.IsMatch(item))
            {
                _isProblem = true;
            }
            else
            {
                _isProblem = false;
            }

        }
        private void CheckIfTask(string item)
        {
            Regex regex = new Regex(@"^(TASK)\d{7}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if(regex.IsMatch(item))
            {
                _isTask = true;
            }
            else
            {
                _isTask = false;
            }


        }
        private void CheckIfPrivateTask(string item)
        {
            Regex regex = new Regex(@"^(PTSK)\d{7}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if(regex.IsMatch(item))
            {
                _isPrivateTask = true;
            }
            else
            {
                _isPrivateTask = false;
            }
        }
        private void CheckIfMonitor(string item)
        {
            Regex regex = new Regex(_regexperipherals, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if(regex.IsMatch(item))
            {
                _isperipheral = true;
            }
            else
            {
                _isperipheral = false;
            }

        }

        private void CheckIfComputer(string item)
        {
            Regex regexOldNames = new Regex(_regexconfigurationitems, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if(regexOldNames.IsMatch(item))
            {
                _isComputer = true;
                return;
            }
            else
            {
                _isComputer = false;

            }



        }
        private void CheckIfUserName(string item)
        {

            Regex regex = new Regex(_regexusernames, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if(regex.IsMatch(item))
            {
                _isUserName = true;
            }
            else
            {
                _isUserName = false;
            }

        }

        private void CheckIfPerson(string item)
        {
            Regex regex = new Regex(@"(^[A-Za-z]{2})([A-Za-z]*) ([A-Za-z]{2})([A-Za-z]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if(regex.IsMatch(item))
            {
                _isperson = true;
            }
            else
            {
                _isperson = false;
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
            CheckIfPerson(SearchText);
            CheckIfPrivateTask(SearchText);
            CheckIfRequest(SearchText);
            CheckIfRequestedItem(SearchText);
            CheckIfProblem(SearchText);
            CheckIfTask(SearchText);

        }

        public void ExecuteProcess()
        {
            bool launched = false;

            if(ForceFreeTextSearch)
            {
                _url = _urlservicenowportal + "/nav_to.do?uri=textsearch.do?sysparm_search=" + SearchText;
                Process.Start(_urlservicenowportal + "/nav_to.do?uri=textsearch.do?sysparm_search=" + SearchText);
                return;
            }


            if(_isIncident)
            {
                _url = _urlservicenowportal + "/nav_to.do?uri=incident.do?sysparm_query=number=" + SearchText;
                Process.Start(_urlservicenowportal + "/nav_to.do?uri=incident.do?sysparm_query=number=" + SearchText);
                launched = true;
            }

            if(_isUserName)
            {
                _url = _urlservicenowportal + "/nav_to.do?uri=sys_user.do?sysparm_query=user_name=" + SearchText;
                Process.Start(_urlservicenowportal + "/nav_to.do?uri=sys_user.do?sysparm_query=user_name=" + SearchText);
                launched = true;
            }

            if(_isperson)
            {
                _url = _urlservicenowportal + "/nav_to.do?uri=sys_user.do?sysparm_query=name=" + SearchText;
                Process.Start(_urlservicenowportal + "/nav_to.do?uri=sys_user.do?sysparm_query=name=" + SearchText);
                launched = true;
            }



            if(_isComputer)
            {
                _url = _urlservicenowportal + "/nav_to.do?uri=cmdb_ci_computer.do?sysparm_query=name=" + SearchText;
                Process.Start(_urlservicenowportal + "/nav_to.do?uri=cmdb_ci_computer.do?sysparm_query=name=" + SearchText);
                launched = true;
            }

            if(_isperipheral)
            {
                _url = _urlservicenowportal + "/nav_to.do?uri=cmdb_ci_peripheral.do?sysparm_query=name=" + SearchText;
                Process.Start(_urlservicenowportal + "/nav_to.do?uri=cmdb_ci_peripheral.do?sysparm_query=name=" + SearchText);
                launched = true;
            }

            if(_isTask)
            {
                _url = _urlservicenowportal + "/nav_to.do?uri=sc_task.do?sysparm_query=number=" + SearchText;
                Process.Start(_urlservicenowportal + "/nav_to.do?uri=sc_task.do?sysparm_query=number=" + SearchText);
                launched = true;
            }

            if(_isPrivateTask)
            {
                _url = _urlservicenowportal + "/nav_to.do?uri=vtb_task.do?sysparm_query=number=" + SearchText;
                Process.Start(_urlservicenowportal + "/nav_to.do?uri=vtb_task.do?sysparm_query=number=" + SearchText);
                launched = true;
            }



            if(_isProblem)
            {
                _url = _urlservicenowportal + "/nav_to.do?uri=problem.do?sysparm_query=number=" + SearchText;
                Process.Start(_urlservicenowportal + "/nav_to.do?uri=problem.do?sysparm_query=number=" + SearchText);
                launched = true;
            }

            if(_isRequest)
            {
                _url = _urlservicenowportal + "/nav_to.do?uri=sc_request.do?sysparm_query=number=" + SearchText;
                Process.Start(_urlservicenowportal + "/nav_to.do?uri=sc_request.do?sysparm_query=number=" + SearchText);
                launched = true;
            }

            if(_isRequestedItem)
            {
                _url = _urlservicenowportal + "/nav_to.do?uri=sc_req_item.do?sysparm_query=number=" + SearchText;
                Process.Start(_urlservicenowportal + "/nav_to.do?uri=sc_req_item.do?sysparm_query=number=" + SearchText);
                launched = true;
            }

            if(_isKBArticle)
            {
                _url = _urlservicenowportal + "/sp?id=kb_article&sys_id=" + SearchText;
                Process.Start(_urlservicenowportal + "/sp?id=kb_article&sys_id=" + SearchText);
                launched = true;
            }

            if(launched == false)
            {
                _url = _urlservicenowportal + "/nav_to.do?uri=textsearch.do?sysparm_search=" + SearchText;
                Process.Start(_urlservicenowportal + "/nav_to.do?uri=textsearch.do?sysparm_search=" + SearchText);
            }
        }

    }
}