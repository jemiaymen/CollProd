using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using SnmpSharpNet;

namespace CollProd.Pages.View
{
    /// <summary>
    /// Interaction logic for Ephone.xaml
    /// </summary>
    public partial class Ephone : UserControl
    {
        public string ip { get; set; }
        public string community { get; set; }
        string[] iods = { "1.3.6.1.4.1.9.9.439.1.1.47.1.4",
                              "1.3.6.1.4.1.9.9.439.1.1.43.1.4",
                              "1.3.6.1.4.1.9.9.439.1.1.47.1.6",
                              "1.3.6.1.4.1.9.9.439.1.1.47.1.7",
                              "1.3.6.1.4.1.9.9.439.1.1.60.1.3",
                              "1.3.6.1.4.1.9.9.439.1.2.6.1.2" };
        public Ephone()
        {
            InitializeComponent();
            if (IsConfigure())
            {
                dgphone.ItemsSource = SnmpEphone(ip, community, iods);
            }
        }

        private void btnphone_Click(object sender, RoutedEventArgs e)
        {
            if (IsConfigure())
            {
                dgphone.ItemsSource = SnmpEphone(ip, community, iods);
            }
        }

        public bool IsConfigure()
        {

            try
            {
                if (File.Exists("CollProdSNMP.ini"))
                {
                    StreamReader sr = new StreamReader("CollProdSNMP.ini");
                    ip = sr.ReadLine();
                    community = sr.ReadLine();
                    sr.Close();
                    return true;
                }
                else
                {
                    MessageBox.Show("Setting of SNMP protocol not Set ! \nGo To Setting=>Router=>SNMP !", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
            catch (Exception)
            { 
            }
            return false;
        }

        //oid[6] num,mac,name,des,droi,stat
        public List<phone> SnmpEphone(string host, string community, string[] oid)
        {
            //string[] iods = { "1.3.6.1.4.1.9.9.439.1.1.47.1.4",
            //                  "1.3.6.1.4.1.9.9.439.1.1.43.1.4",
            //                  "1.3.6.1.4.1.9.9.439.1.1.47.1.6",
            //                  "1.3.6.1.4.1.9.9.439.1.1.47.1.7",
            //                  "1.3.6.1.4.1.9.9.439.1.1.60.1.3",
            //                  "1.3.6.1.4.1.9.9.439.1.2.6.1.2" };
            List<phone> lst = new List<phone>();
            try
            {
                SimpleSnmp snmp = new SimpleSnmp(host, community);

                if (snmp.Valid)
                {
                    var nums = snmp.Walk(SnmpVersion.Ver2, oid[0]);
                    var macs = snmp.Walk(SnmpVersion.Ver2, oid[1]);
                    var names = snmp.Walk(SnmpVersion.Ver2, oid[2]);
                    var des = snmp.Walk(SnmpVersion.Ver2, oid[3]);
                    var drois = snmp.Walk(SnmpVersion.Ver2, oid[4]);
                    var stats = snmp.Walk(SnmpVersion.Ver2, oid[5]);

                    if (nums != null && macs != null && names != null && stats != null)
                    {
                        string[] num = new string[nums.Count];
                        string[] mac = new string[macs.Count];
                        string[] name = new string[names.Count];
                        string[] de = new string[des.Count];
                        string[] droi = new string[drois.Count];
                        string[] stat = new string[stats.Count];
                        int i = 0;
                        foreach (var item in nums)
                        {
                            num[i] = item.Value.ToString();
                            i++;
                        }
                        i = 0;
                        foreach (var item in macs)
                        {
                            mac[i] = item.Value.ToString();
                            i++;
                        }
                        i = 0;
                        foreach (var item in names)
                        {
                            name[i] = item.Value.ToString();
                            i++;
                        }
                        i = 0;
                        foreach (var item in des)
                        {
                            de[i] = item.Value.ToString();
                            i++;
                        }
                        i = 0;
                        foreach (var item in droi)
                        {
                            droi[i] = item;
                            i++;
                        }
                        i = 0;
                        //state 1 online 2 offine 3 offline with problem
                        foreach (var item in stats)
                        {
                            if (item.Value.ToString().Equals("1"))
                            {
                                stat[i] = "online";
                            }

                            if (item.Value.ToString().Equals("2"))
                            {
                                stat[i] = "offline";
                            }

                            if (item.Value.ToString().Equals("3"))
                            {
                                stat[i] = "broblem";
                            }
                            i++;
                        }

                        i = 0;
                        foreach (var n in num)
                        {

                            string macadress = "N/A";
                            string namephone = "N/A";
                            string description = "N/A";
                            string droitacces = "N/A";
                            string state = "Not Register";

                            try
                            {
                                macadress = mac[i];
                            }
                            catch (Exception)
                            {

                            }

                            try
                            {
                                namephone = name[i];
                            }
                            catch (Exception)
                            {

                            }


                            try
                            {
                                description = de[i];
                            }
                            catch (Exception)
                            {

                            }


                            try
                            {
                                droitacces = droi[i];
                            }
                            catch (Exception)
                            {

                            }

                            try
                            {
                                state = stat[i];
                            }
                            catch (Exception)
                            {

                            }
                            lst.Add(new phone(n, macadress, namephone, description, droitacces, state));
                            i++;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return lst;
        }
        #region phone helper class
        public class phone
        {
            public string num { get; set; }
            public string mac { get; set; }
            public string name { get; set; }
            public string desc { get; set; }
            public string droi { get; set; }
            public string state { get; set; }

            public phone()
            {

            }

            public phone(string num, string mac, string name, string desc, string droi, string state)
            {
                this.num = num;
                this.mac = mac;
                this.name = name;
                this.desc = desc;
                this.droi = droi;
                this.state = state;
            }
        }

        #endregion



    }
}
