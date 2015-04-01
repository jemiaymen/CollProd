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
    /// Interaction logic for SSH.xaml
    /// </summary>
    public partial class SSH : UserControl
    {
        public SSH()
        {
            InitializeComponent();
            initConf();
        }

        public void initConf()
        {

            if (File.Exists("CollProdSSH.ini"))
            {
                StreamReader conf = new StreamReader("CollProdSSH.ini");

                if (conf != null)
                {
                    txtip.Text = conf.ReadLine();
                    txtuser.Text = conf.ReadLine();
                    pw.Password = conf.ReadLine();
                }
            }

        }
        public void Conf()
        {
            if (File.Exists("CollProdSSH.ini"))
            {
                File.Delete("CollProdSSH.ini");
            }

            StreamWriter conf = new StreamWriter("CollProdSSH.ini");
            conf.AutoFlush = true;
            conf.WriteLine(txtip.Text);
            conf.WriteLine(txtuser.Text);
            conf.WriteLine(pw.Password);
            conf.Close();
        }

        private void btnssh_Click(object sender, RoutedEventArgs e)
        {
            Conf();
        }
    }
}
