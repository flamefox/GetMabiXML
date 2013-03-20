using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Configuration;
using System.Collections;



namespace GetMabiXml
{
    public partial class Form1 : Form, INotifyPropertyChanged
    {
        Dictionary<string, string> dictServer = new Dictionary<string, string>();
        public Form1()
        {
            InitializeComponent();
            this.textBox1.Text = sc.Page;
            this.textBox2.Text = sc.Count;
            this.textBox3.Text = sc.SearchWord;
            this.textBox4.Text = sc.ServerID;
            this.textBox5.Text = NowPage;
            this.textBox6.Text = NextPage;
            this.textBox7.Text = sc.SearchType;
            this.textBox9.Text = sc.GameRegion;
            this.textBox10.Text = sc.SortType;
            this.textBox11.Text = sc.SortOption;

            this.textBox1.DataBindings.Add("Text", sc, "Page");
            this.textBox2.DataBindings.Add("Text", sc, "Count");
            this.textBox3.DataBindings.Add("Text", sc, "SearchWord");
            this.textBox4.DataBindings.Add("Text", sc, "ServerID");
            this.textBox7.DataBindings.Add("Text", sc, "SearchType");
            this.textBox9.DataBindings.Add("Text", sc, "GameRegion");
            this.textBox10.DataBindings.Add("Text", sc, "SortType");
            this.textBox11.DataBindings.Add("Text", sc, "SortOption");


            this.textBox5.DataBindings.Add("Text", ai, "NowPage", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBox6.DataBindings.Add("Text", ai, "NextPage", false, DataSourceUpdateMode.OnPropertyChanged);
            
            

            xmlDS = new DataSet();
            xmlDS.ReadXmlSchema("text1.xsd");

            DataColumnCollection dcc = xmlDS.Tables[1].Columns;
            //DataColumn dcArea = xmlDS.Tables[1].Columns["Area"];
            //DataColumn dcShop_Name = xmlDS.Tables[1].Columns["Shop_Name"];
            //DataColumn dcComment = xmlDS.Tables[1].Columns["Comment"];
            //DataColumn dcStart_Time = xmlDS.Tables[1].Columns["Start_Time"];
            //xmlDS.Tables[1].Columns.Remove("Area");
            //xmlDS.Tables[1].Columns.Remove("Shop_Name");
            //xmlDS.Tables[1].Columns.Remove("Comment");
            //xmlDS.Tables[1].Columns.Remove("Start_Time");

            //xmlDS.Tables[1].Columns.Add(dcArea);
            //xmlDS.Tables[1].Columns.Add(dcShop_Name);
            //xmlDS.Tables[1].Columns.Add(dcComment);
            //xmlDS.Tables[1].Columns.Add(dcStart_Time);

            object objColumnsConfig = ConfigurationManager.GetSection("showColumns");
            Hashtable cfgColumns = objColumnsConfig as Hashtable;
            foreach (string strKey in cfgColumns.Keys)
            {
                if (!bool.Parse((string)cfgColumns[strKey]))
                {
                    dcc.Remove(strKey);
                }
            }


            object objTest = ConfigurationManager.GetSection("serverList") ;
            Hashtable cfgServer = objTest as Hashtable;


            foreach (string strKey in cfgServer.Keys)
            {
                this.comboBox2.Items.Add(strKey);
                dictServer.Add(strKey, (string)cfgServer[strKey]);
            }
            
            //textBox1.DataBindings.Add("Text", )

        }
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_NowPage;
        public string NowPage
        {
            get { return m_NowPage; }
            set
            {
                m_NowPage = value;

                //if (PropertyChanged != null)
                //{
                //    PropertyChanged(this, new PropertyChangedEventArgs("NowPage"));
                //}
            }
        }
        private string m_NextPage;
        public string NextPage
        {
            get { return m_NextPage; }
            set
            {
                m_NextPage = value;

                //if (PropertyChanged != null)
                //{
                //    PropertyChanged(this, new PropertyChangedEventArgs("NextPage"));
                //}
            }
        }

        List<ItemInfo> lstData = new List<ItemInfo>();
        ServerControl sc = new ServerControl();
        AdvertiseItems ai = new AdvertiseItems();
        DataSet xmlDS;


        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private string GetMabiServerResponse(string nRegion, int nServer, int nPage, int nRow, int nSearchType, string SortType, int SortOption, string SearchWord)
        {
            WebClient wc = new WebClient();
            //wc.Headers.Add("Encoding", "GB2312");
            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 5.2; zh-CN; rv:1.9.1.4) Gecko/20091016 Firefox/3.5.4 (.NET CLR 3.5.30729)");
            //wc.Headers.Add("Host", "app01.luoqi.com.cn");
            wc.Headers.Add("Accept", @"text/*");
            //wc.Headers.Add("Cookie", @"PCID=12298594189600824174119; __utma=97240578.985951120052610400.1229859432.1262345662.1262419935.4; __utmz=97240578.1262345662.3.2.utmcsr=baidu|utmccn=(organic)|utmcmd=organic|utmctr=%B1%B4%B0%B2%C8%E3%E6%AB%20%C0%F1%CE%EF");
            wc.Headers.Add("Cache-Control", "no-cache");
            wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");


            //string strHome = @"http://app01.luoqi.com.cn/";
            //string strUrlBase = @"ShopAdvertise/ShopAdvertise.asp?Name_Server=mabicn16&CharacterId=4503599628833933&Page=1&Row=7&SearchType=4&SortType=&SortOption=1&SearchWord=";


            //string strPage = @"http://app"+nRegion+@".luoqi.com.cn/ShopAdvertise/ShopAdvertise.asp";

            //string strData = @"Name_Server=mabicn" + nServer;
            //strData += @"&Page=" + nPage;
            //strData += @"&Row=" + nRow;
            //strData += @"&SearchType=" + nSearchType;
            //strData += @"&SortType=" + SortType;
            //strData += @"&SortOption=" + SortOption;
            //strData += @"&SearchWord=" + SearchWord.Replace(" ", "%20");

            string strPage = string.Format(ConfigurationManager.AppSettings["AdvServer"], nRegion);

            string strData = string.Format(ConfigurationManager.AppSettings["AdvServerArg"], nServer, nPage, nRow, nSearchType, SortType, SortOption, SearchWord.Replace(" ", "%20"));

            //strData = @"Name_Server=mabicn16&CharacterId=&Page=1&Row=40&SearchType=4&SortType=&SortOption=1&SearchWord=" + SearchWord;
            byte[] bs = Encoding.GetEncoding("GB2312").GetBytes(strData);

            byte[] bsr = null;
            try
            {

                bsr = wc.UploadData(strPage, "POST", bs);

            }
            catch (System.Net.WebException ex)
            {

                Debug.WriteLine(ex.TargetSite);
                Debug.WriteLine(ex.Status.ToString());
                Debug.WriteLine(ex.Message);
            }


            string s = null;
            if (bsr == null)
            {
                s = "";
            }
            else
            {
                byte[] bGb2312 = Encoding.Convert(Encoding.GetEncoding("utf-16"), Encoding.Default, bsr);
                s = Encoding.Default.GetString(bGb2312);
                StringBuilder sb = new StringBuilder(s);
                sb.Remove(0, 1);
                s = sb.ToString();
            }


            return s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.SearchAD(1);
        }

        private void SearchAD(int nPage)
        {
            msglb.Text = "";
            if (int.Parse(sc.Page) != nPage)
            {
                sc.Page = nPage + "";
            }
            string sRes = this.GetMabiServerResponse(
                sc.GameRegion, 
                Convert.ToInt32(sc.ServerID), 
                Convert.ToInt32(nPage), 
                Convert.ToInt32(sc.Count), 
                Convert.ToInt32(sc.SearchType), 
                sc.SortType, 
                Convert.ToInt32(sc.SortOption), 
                sc.SearchWord
                );

            StringReader stream = null;
            XmlTextReader xmlreader = null;

            try
            {
                xmlDS.Clear();
                xmlDS.Tables[1].DefaultView.RowFilter = "";
                xmlDS.Tables[1].DefaultView.Sort = "";
                stream = new StringReader(sRes);
                xmlreader = new XmlTextReader(stream);

                xmlDS.ReadXml(xmlreader);

                //XmlSerializer ser = new XmlSerializer(typeof(ItemInfo));

                //XmlDocument doc = new XmlDocument();
                //doc.LoadXml(sRes);
                //string xmlHead = "<?xml version=\"1.0\" encoding=\"UTF-16\" ?>\r\n";

                //int nodeCount = doc.GetElementsByTagName("AdvertiseItems")[0].ChildNodes.Count;
                //var nodelist = doc.GetElementsByTagName("AdvertiseItems")[0].ChildNodes;
                //for (int i = 0; i < nodeCount; i++)
                //{
                //    var item = nodelist[i];
                //    string sTest = item.OuterXml;
                //    sTest = xmlHead + sTest;
                //    stream = new StringReader(sTest);
                //    xmlreader = new XmlTextReader(stream);
                //    ItemInfo infoTemp = ser.Deserialize(xmlreader) as ItemInfo;
                //    lstData.Add(infoTemp);
                //}

                ai.NowPage = xmlDS.Tables[0].Rows[0].ItemArray[1] as string;
                ai.NextPage = xmlDS.Tables[0].Rows[0].ItemArray[0] as string;


                if (int.Parse(ai.NextPage) == 0)
                {
                    this.button3.Enabled = false;
                }
                else
                {
                    this.button3.Enabled = true;
                }

                if (int.Parse(ai.NowPage) == 1)
                {
                    this.button4.Enabled = false;
                }
                else
                {
                    this.button4.Enabled = true;
                }


                this.dataGridView1.DataSource = xmlDS.Tables[1].DefaultView;


                this.label5.Text = xmlDS.Tables[1].DefaultView.Count + "";
            }
            catch (System.Exception ex)
            {
                msglb.Text = "没有找到:错误--" + ex.Message;
                this.button3.Enabled = false;
                this.button4.Enabled = false;
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
           


        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString().Length > 0)
            {
                DataColumn column = xmlDS.Tables[1].Columns[e.ColumnIndex];
                switch (column.ColumnName)
                {
                    //case 2:
                    //    {
                    //        //long iValue = Convert.ToInt64(e.Value.ToString());
                    //        //DateTime dtResult = new DateTime(iValue);
                    //        //e.Value = dtResult.ToString("yyyy-MM-dd HH:mm:ss");
                    //    }
                    //    break;
                    case "Item_Price":
                        {
                            int iValue = Convert.ToInt32(e.Value.ToString());
                            string sback = (iValue % 10000 == 0) ? "":(iValue % 10000) + "";
                            string sbegin = (iValue / 10000 == 0) ? "":(iValue / 10000) + "万";
                            e.Value = sbegin + sback;
                        }
                        break;
                    case "Item_Color1":
                    case "Item_Color2":
                    case "Item_Color3":
                        {
                            int iValue = Convert.ToInt32(e.Value.ToString());
                            Color color = Color.FromArgb(iValue);
                            
                            e.Value = color.A.ToString("X2") + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
                            Color bColor = Color.FromArgb(255, color.R, color.G, color.B);
                            e.CellStyle.BackColor = bColor;
                            e.CellStyle.ForeColor = Color.FromArgb(255 - bColor.R, 255 - bColor.G, 255 - bColor.B);
                            e.FormattingApplied = true; 
                        }
                        break;

                    default:
                        break;
                }

            }



        }

        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {          
            
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox8.Text.Length != 0)
            {
                switch (comboBox5.SelectedIndex)
                {
                    case 0:
                    case -1:
                        {
                            DataView dv = xmlDS.Tables[1].DefaultView;
                            int n = Convert.ToInt32(this.textBox8.Text, 16);
                            dv.RowFilter = "Item_Color1 =" + n;
                            break;
                        }

                    case 1:
                        {
                            DataView dv = xmlDS.Tables[1].DefaultView;
                            dv.RowFilter = "Item_ClassId  like '%" + DvRowFilter(this.textBox8.Text) + "%'";
                            break;
                        }

                    default:
                        break;
                }
            }
            else
            {
                DataView dv = xmlDS.Tables[1].DefaultView;
                dv.RowFilter = "";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comboBox1.SelectedIndex)
            {
                case 0:
                    sc.SearchType = "4";
                    break;
                case 1:
                    sc.SearchType = "1";
                    break;
                case 2:
                    sc.SearchType = "2";
                    break;
                default:
                    break;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{ENTER}"); 
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sc.Page = (int.Parse(sc.Page) + 1) + "";
            this.SearchAD(int.Parse(sc.Page));
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int nPage = int.Parse(sc.Page) - 1;
            if (nPage > 0)
            {
                sc.Page = nPage + "";
                this.SearchAD(int.Parse(sc.Page));
            }
            
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            msglb.Text = "本软件版权,玛丽 糟糕狐狸,请有爱的使用广告板,目前仅支持国服";
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strValue = dictServer[(string)this.comboBox2.SelectedItem];
            string[] strServerInfo = strValue.Split("|".ToCharArray());
            if (strServerInfo.Length == 2)
            {
                sc.GameRegion = strServerInfo[0];
                sc.ServerID = strServerInfo[1];
            } 
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comboBox3.SelectedIndex)
            {
                case 0:
                    sc.SortOption = "1";
                    sc.SortType = "";
                    this.comboBox4.SelectedIndex = -1;
                    break;
                case 1:
                    sc.SortOption = "1";
                    break;
                case 2:
                    sc.SortOption = "0";
                    break;
                default:
                    break;
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comboBox4.SelectedIndex)
            {
                case 0:
                    sc.SortType = "1";
                    break;
                case 1:
                    sc.SortType = "4";
                    break;
                case 2:
                    sc.SortType = "5";
                    break;
                default:
                    break;
            }
        }

        public static string DvRowFilter(string rowFilter)
        {
            //在DataView的RowFilter里面的特殊字符要用"[]"括起来，单引号要换成"''",他的表达式里面没有通配符的说法
            return rowFilter.Replace("[", "[[ ")
                .Replace("]", " ]]")
                .Replace("*", "[*]")
                .Replace("%", "[%]")
                .Replace("[[ ", "[[]")
                .Replace(" ]]", "[]]")
                .Replace("\'", "''");
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }



    }


}
