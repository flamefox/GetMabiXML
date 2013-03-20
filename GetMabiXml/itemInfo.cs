using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
namespace GetMabiXml
{
    [XmlRoot("ItemDesc")]
    public class ItemInfo
    {
        private string m_Shop_Name;
        [XmlAttribute("Shop_Name")]
        public string Shop_Name
        {
            get { return m_Shop_Name; }
            set { m_Shop_Name = value; }
        }
        private string m_Item_ID;
        [XmlAttribute("Item_ID")]
        public string Item_ID
        {
            get { return m_Item_ID; }
            set { m_Item_ID = value; }
        }

        private string m_Area;
        [XmlAttribute("Area")]
        public string Area
        {
            get { return m_Area; }
            set { m_Area = value; }
        }
        private string m_Char_Name;
        [XmlAttribute("Char_Name")]
        public string Char_Name
        {
            get { return m_Char_Name; }
            set { m_Char_Name = value; }
        }
        private string m_Comment;

        [XmlAttribute("Comment")]
        public string Comment
        {
            get { return m_Comment; }
            set { m_Comment = value; }
        }
        private string m_Start_Time;
        [XmlAttribute("Start_Time")]
        public string Start_Time
        {
            get { return m_Start_Time; }
            set { m_Start_Time = value; }
        }
        private string m_Item_ClassId;
        [XmlAttribute("Item_ClassId")]
        public string Item_ClassId
        {
            get { return m_Item_ClassId; }
            set { m_Item_ClassId = value; }
        }
        private string m_Item_Name;
        [XmlAttribute("Item_Name")]
        public string Item_Name
        {
            get { return m_Item_Name; }
            set { m_Item_Name = value; }
        }
        private int m_Item_Price;
        [XmlAttribute("Item_Price")]
        public int Item_Price
        {
            get { return m_Item_Price; }
            set { m_Item_Price = value; }
        }
        private int m_Item_Color1;
        [XmlAttribute("Item_Color1")]
        public int Item_Color1
        {
            get { return m_Item_Color1; }
            set { m_Item_Color1 = value; }
        }
        private int m_Item_Color2;
        [XmlAttribute("Item_Color2")]
        public int Item_Color2
        {
            get { return m_Item_Color2; }
            set { m_Item_Color2 = value; }
        }
        private int m_Item_Color3;
        [XmlAttribute("Item_Color3")]
        public int Item_Color3
        {
            get { return m_Item_Color3; }
            set { m_Item_Color3 = value; }
        }
        private int m_Count;
        [XmlAttribute("Count")]
        public int Count
        {
            get { return m_Count; }
            set { m_Count = value; }
        }

        public ItemInfo()
        {
            m_Count = 0;
            m_Item_Color3 = 0;
            m_Item_Color2 = 0;
            m_Item_Color1 = 0;
            m_Item_Price = 0;
            m_Item_Name = "";
            m_Item_ClassId = "";
            m_Item_ID = "";
            m_Area = "";
            m_Char_Name = "";
            m_Comment = "";
            m_Start_Time = "";

        }
    }
}
