using System;
using System.Diagnostics;

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
            if(item.Length > 3)
            {
                string threeLetterPrefix = item.Substring(0, 3);
#pragma warning disable IDE0018 // Inline variable declaration
                int threeLetterSuffix;
#pragma warning restore IDE0018 // Inline variable declaration
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                bool isThreeLetterPrefixSuffixNumeric = Int32.TryParse(item.Substring(3, 1), out threeLetterSuffix);
#pragma warning restore IDE0059 // Unnecessary assignment of a value

                if(threeLetterPrefix.ToUpper() == "INC")
                {
                    if(isThreeLetterPrefixSuffixNumeric)
                    {
                        _isIncident = true;

                    }
                    else { _isIncident = false; }

                }
            }

        }
        private void CheckIfKBArticle(string item)
        {
            if(item.Length > 2)
            {
                string twoLetterPrefix = item.Substring(0, 2);
#pragma warning disable IDE0018 // Inline variable declaration
                int twoLetterSuffix;
#pragma warning restore IDE0018 // Inline variable declaration
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                bool isThreeLetterPrefixSuffixNumeric = Int32.TryParse(item.Substring(2, 1), out twoLetterSuffix);
#pragma warning restore IDE0059 // Unnecessary assignment of a value

                if(twoLetterPrefix.ToUpper() == "KB")
                {
                    if(isThreeLetterPrefixSuffixNumeric)
                    {
                        _isKBArticle = true;

                    }
                    else { _isKBArticle = false; }

                }

            }


        }

        private void CheckIfRequest(string item)
        {
            if(item.Length > 3)
            {
                string threeLetterPrefix = item.Substring(0, 3);
#pragma warning disable IDE0018 // Inline variable declaration
                int threeLetterSuffix;
#pragma warning restore IDE0018 // Inline variable declaration
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                bool isThreeLetterPrefixSuffixNumeric = Int32.TryParse(item.Substring(3, 1), out threeLetterSuffix);
#pragma warning restore IDE0059 // Unnecessary assignment of a value

                if(threeLetterPrefix.ToUpper() == "REQ")
                {
                    if(isThreeLetterPrefixSuffixNumeric)
                    {
                        _isRequest = true;

                    }
                    else { _isRequest = false; }
                }
            }

        }

        private void CheckIfRequestedItem(string item)
        {
            if(item.Length > 4)
            {
                string fourLetterPrefix = item.Substring(0, 4);
#pragma warning disable IDE0018 // Inline variable declaration
                int fourLetterSuffix;
#pragma warning restore IDE0018 // Inline variable declaration
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                bool isFourLetterPrefixSuffixNumeric = Int32.TryParse(item.Substring(4, 1), out fourLetterSuffix);
#pragma warning restore IDE0059 // Unnecessary assignment of a value

                if(fourLetterPrefix.ToUpper() == "RITM")
                {
                    if(isFourLetterPrefixSuffixNumeric)
                    {
                        _isRequestedItem = true;

                    }
                    else { _isRequestedItem = false; }

                }
            }


        }

        private void CheckIfProblem(string item)
        {
            if(item.Length > 3)
            {
                string threeLetterPrefix = item.Substring(0, 3);
#pragma warning disable IDE0018 // Inline variable declaration
                int threeLetterSuffix;
#pragma warning restore IDE0018 // Inline variable declaration
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                bool isThreeLetterPrefixSuffixNumeric = Int32.TryParse(item.Substring(3, 1), out threeLetterSuffix);
#pragma warning restore IDE0059 // Unnecessary assignment of a value

                if(threeLetterPrefix.ToUpper() == "PRB")
                {
                    if(isThreeLetterPrefixSuffixNumeric)
                    {
                        _isProblem = true;

                    }
                    else { _isProblem = false; }

                }
            }

        }
        private void CheckIfTask(string item)
        {
            if(item.Length > 4)
            {
                string fourLetterPrefix = item.Substring(0, 4);
#pragma warning disable IDE0018 // Inline variable declaration
                int fourLetterSuffix;
#pragma warning restore IDE0018 // Inline variable declaration
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                bool isThreeLetterPrefixSuffixNumeric = Int32.TryParse(item.Substring(4, 1), out fourLetterSuffix);
#pragma warning restore IDE0059 // Unnecessary assignment of a value

                if(fourLetterPrefix.ToUpper() == "TASK")
                {
                    if(isThreeLetterPrefixSuffixNumeric)
                    {
                        _isTask = true;

                    }
                    else { _isTask = false; }

                }
            }

        }
        private void CheckIfPrivateTask(string item)
        {
            if(item.Length > 4)
            {
                string fourLetterPrefix = item.Substring(0, 4);
#pragma warning disable IDE0018 // Inline variable declaration
                int fourLetterSuffix;
#pragma warning restore IDE0018 // Inline variable declaration
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                bool isFourLetterPrefixSuffixNumeric = Int32.TryParse(item.Substring(4, 1), out fourLetterSuffix);
#pragma warning restore IDE0059 // Unnecessary assignment of a value

                if(fourLetterPrefix.ToUpper() == "PTSK")
                {
                    if(isFourLetterPrefixSuffixNumeric)
                    {
                        _isPrivateTask = true;

                    }
                    else { _isPrivateTask = false; }

                }
            }


        }
        private void CheckIfMonitor(string item)
        {
            if(item.Length > 1)
            {
                string oneLetterPrefix = item.Substring(0, 1);
#pragma warning disable IDE0018 // Inline variable declaration
                int threeLetterSuffix;
#pragma warning restore IDE0018 // Inline variable declaration
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                bool isoneLetterPrefixSuffixNumeric = Int32.TryParse(item.Substring(1, 1), out threeLetterSuffix);
#pragma warning restore IDE0059 // Unnecessary assignment of a value

                if(oneLetterPrefix.ToUpper() == "M")
                {
                    if(isoneLetterPrefixSuffixNumeric)
                    {
                        _isMonitor = true;

                    }
                    else { _isMonitor = false; }

                }
            }

        }

        private void CheckIfComputer(string item)
        {
            if(item.Length > 1)
            {

                string oneLetterPrefix = item.Substring(0, 1);
#pragma warning disable IDE0018 // Inline variable declaration
                int oneLetterSuffix;
#pragma warning restore IDE0018 // Inline variable declaration
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                bool isoneLetterPrefixSuffixNumeric = Int32.TryParse(item.Substring(1, 1), out oneLetterSuffix);
#pragma warning restore IDE0059 // Unnecessary assignment of a value

                if(oneLetterPrefix.ToUpper() == "L" || oneLetterPrefix.ToUpper() == "D")
                {
                    if(isoneLetterPrefixSuffixNumeric)
                    {
                        _isComputer = true;

                    }
                    else { _isComputer = false; }

                }

            }

            if(item.Length > 6)
            {
                string laptopPrefix = item.Substring(0, 6);
                if(laptopPrefix.ToUpper() == "LAPTOP")
                {
                    _isComputer = true;
                }

            }
        }
        private void CheckIfUserName(string item)
        {
#pragma warning disable IDE0018 // Inline variable declaration
            int intfromString;
#pragma warning restore IDE0018 // Inline variable declaration
#pragma warning disable IDE0059 // Unnecessary assignment of a value
            bool isStringNumber = System.Int32.TryParse(item, out intfromString);
#pragma warning restore IDE0059 // Unnecessary assignment of a value
            if(isStringNumber)
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

            if(ForceFreeTextSearch)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=textsearch.do?sysparm_search=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=textsearch.do?sysparm_search=" + SearchText);
                return;
            }


            if(_isIncident)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=incident.do?sysparm_query=number=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=incident.do?sysparm_query=number=" + SearchText);
                launched = true;
            }

            if(_isUserName)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=sys_user.do?sysparm_query=user_name=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=sys_user.do?sysparm_query=user_name=" + SearchText);
                launched = true;
            }

            if(_isComputer)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=cmdb_ci_computer.do?sysparm_query=name=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=cmdb_ci_computer.do?sysparm_query=name=" + SearchText);
                launched = true;
            }

            if(_isMonitor)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=cmdb_ci_peripheral.do?sysparm_query=name=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=cmdb_ci_peripheral.do?sysparm_query=name=" + SearchText);
                launched = true;
            }

            if(_isTask)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=sc_task.do?sysparm_query=number=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=sc_task.do?sysparm_query=number=" + SearchText);
                launched = true;
            }

            if(_isPrivateTask)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=vtb_task.do?sysparm_query=number=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=vtb_task.do?sysparm_query=number=" + SearchText);
                launched = true;
            }



            if(_isProblem)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=problem.do?sysparm_query=number=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=problem.do?sysparm_query=number=" + SearchText);
                launched = true;
            }

            if(_isRequest)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=sc_request.do?sysparm_query=number=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=sc_request.do?sysparm_query=number=" + SearchText);
                launched = true;
            }

            if(_isRequestedItem)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=sc_req_item.do?sysparm_query=number=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=sc_req_item.do?sysparm_query=number=" + SearchText);
                launched = true;
            }

            if(_isKBArticle)
            {
                _url = "https://uis.service-now.com/sp?id=kb_article&sys_id=" + SearchText;
                Process.Start("https://uis.service-now.com/sp?id=kb_article&sys_id=" + SearchText);
                launched = true;
            }

            if(launched == false)
            {
                _url = "https://uis.service-now.com/nav_to.do?uri=textsearch.do?sysparm_search=" + SearchText;
                Process.Start("https://uis.service-now.com/nav_to.do?uri=textsearch.do?sysparm_search=" + SearchText);
            }
        }

    }
}