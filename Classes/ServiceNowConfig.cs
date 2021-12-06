using Microsoft.Win32;
using System;
using System.IO;
using System.Xml.Serialization;

namespace ServiceNow
{

    public class ServiceNowConfig
    {
        string _urlservicenowportal = "";
        string _regexpatternci = "";
        string _regexpatternperipherals = "";
        string _regexpatternusers = "";
        public ServiceNowConfig(string urlservicenowportal, string regexpatternci, string regexpatternperipherals, string regexpatternusers)
        {
            _urlservicenowportal = urlservicenowportal;
            _regexpatternci = regexpatternci;
            _regexpatternperipherals = regexpatternperipherals;
            _regexpatternusers = regexpatternusers;
        }

        public ServiceNowConfig()
        {

        }


        public string URLServiceNowPortal
        {
            get { return _urlservicenowportal; }
            set { _urlservicenowportal = value; }
        }

        public string RegExPatternCI
        {
            get { return _regexpatternci; }
            set { _regexpatternci = value; }
        }

        public string RegExPatternPeripherals
        {
            get { return _regexpatternperipherals; }
            set { _regexpatternperipherals = value; }
        }

        public string RegExPatternUsers
        {
            get { return _regexpatternusers; }
            set { _regexpatternusers = value; }
        }

        public void Save()
        {

            SaveFileDialog dlgSaveToFile = new SaveFileDialog
            {
                FileName = "config",
                DefaultExt = ".xml",
                Filter = "XML files|*.xml"
            };


            Nullable<bool> result = dlgSaveToFile.ShowDialog();

            if(result == true)
            {
                string fileName = dlgSaveToFile.FileName;
                using(FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(ServiceNowConfig));
                    xml.Serialize(fs, this);
                }

            }

        }

        public ServiceNowConfig Load()
        {

            OpenFileDialog dlgOpenFile = new OpenFileDialog
            {
                Filter = "XML files|*.xml",
                DefaultExt = ".xml",
                FileName = "config"
            };

            Nullable<bool> result = dlgOpenFile.ShowDialog();

            if(result == true)
            {
                string openFile = dlgOpenFile.FileName;

                using(FileStream fs = new FileStream(openFile, FileMode.Open))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(ServiceNowConfig));
                    return (ServiceNowConfig)xml.Deserialize(fs);
                }


            }

            return null;

        }

    }
}