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

namespace CollProd.Pages.Config
{
    /// <summary>
    /// Interaction logic for SNMP.xaml
    /// </summary>
    public partial class SNMP : UserControl
    {
        public SNMP()
        {
            InitializeComponent();
            initConf();
        }


        public void initConf()
        {

            if (File.Exists("CollProdSNMP.ini"))
            {
                StreamReader conf = new StreamReader("CollProdSNMP.ini");

                if (conf != null)
                {
                    txtip.Text = conf.ReadLine();
                    txtcomm.Text = conf.ReadLine();
                }
            }
            
        }

        public void Conf()
        {
            if (File.Exists("CollProdSNMP.ini"))
            {
                File.Delete("CollProdSNMP.ini");
            }

            StreamWriter conf = new StreamWriter("CollProdSNMP.ini");
            conf.AutoFlush = true;
            conf.WriteLine(txtip.Text);
            conf.WriteLine(txtcomm.Text);
            conf.Close();
        }

        private void btnsnmp_Click(object sender, RoutedEventArgs e)
        {
            Conf();
        }
    }
}
