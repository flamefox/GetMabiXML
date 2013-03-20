using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Configuration;
using System.Collections;


namespace GetMabiXml
{
    public class ServerControl: INotifyPropertyChanged
    {
        public ServerControl()
        {
            ServerID = ConfigurationManager.AppSettings["GameServer"];
            Page = "1";

            Count = ConfigurationManager.AppSettings["CountPrePage"];
            SearchWord = "";
            SearchType = "4";
            GameRegion = ConfigurationManager.AppSettings["GameRegion"];
            SortType = "";
            SortOption = "1";
        }

        ~ServerControl()
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfg.AppSettings.Settings["GameServer"].Value = ServerID;
            cfg.AppSettings.Settings["CountPrePage"].Value = Count;
            cfg.AppSettings.Settings["GameRegion"].Value = GameRegion;
            cfg.Save();
            
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private int m_ServerID;
        public string ServerID
        {
            get { return m_ServerID.ToString(); }
            set 
            {
                m_ServerID = Convert.ToInt32(value);

            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ServerID"));
            }
            }
        }
        private int m_Page;
        public string Page
        {
            get { return m_Page.ToString(); }
            set
            {
                if(Convert.ToInt32(value) > 0)
                {
                    m_Page = Convert.ToInt32(value);
                }
                
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Page"));
                }
            }
        }
        private int m_Count;
        public string Count
        {
            get { return m_Count.ToString(); }
            set 
            {

                m_Count = Convert.ToInt32(value);
#if TRIAL
                if (m_Count > 100)
                {
                    m_Count = 100;
                }
#endif

                if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Count"));
            }
            }
        }
        private string m_SearchWord;
        public string SearchWord
        {
            get { return m_SearchWord; }
            set {
                m_SearchWord = value;
                if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("SearchWord"));
            }
            }
        }

        private string m_SearchType;
        public string SearchType
        {
            get { return m_SearchType; }
            set
            {
                m_SearchType = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SearchType"));
                }
            }
        }

        private string m_GameRegion;
        public string GameRegion
        {
            get { return m_GameRegion; }
            set
            {
                m_GameRegion = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("GameRegion"));
                }
            }
        }

        private string m_SortType;
        public string SortType
        {
            get { return m_SortType; }
            set { m_SortType = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("SortType"));
            }
            }
        }

        private int m_SortOption;
        public string SortOption
        {
            get { return m_SortOption.ToString(); }
            set { m_SortOption = Convert.ToInt32(value);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("SortOption"));
            }
            }
        }
        }



        
        
    
}
