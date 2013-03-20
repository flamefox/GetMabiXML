using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;
namespace GetMabiXml
{

    public class AdvertiseItems : INotifyPropertyChanged
    {
        public AdvertiseItems()
        {
            m_NowPage = "1";
            m_NextPage = "";
        }
        private string m_NowPage;
        private string m_NextPage;

        public event PropertyChangedEventHandler PropertyChanged;
        public string NowPage
        {
            get { return m_NowPage; }
            set
            {
                m_NowPage = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("NowPage"));
                }
            }
        }

        public string NextPage
        {
            get { return m_NextPage; }
            set
            {
                m_NextPage = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("NextPage"));
                }
            }
        }
    }
}
