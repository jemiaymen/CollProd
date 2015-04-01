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
using FirstFloor.ModernUI.Windows.Controls;

namespace CollProd.Pages.View
{
    /// <summary>
    /// Interaction logic for General.xaml
    /// </summary>
    public partial class General : UserControl
    {
        public General()
        {
            InitializeComponent();
        }

        private void btngen_Click(object sender, RoutedEventArgs e)
        {
            var v = new ModernDialog
            {
                Title = "General",
                Content = "pewpew lazers rule. If you agree click ok"
            };
            v.Buttons = new Button[] { v.OkButton, v.CancelButton };
            v.ShowDialog();
        }

        private void btnflash_Click(object sender, RoutedEventArgs e)
        {
            var v = new ModernDialog
            {
                Title = "Flash",
                Content = "pewpew lazers rule. If you agree click ok"
            };
            v.Buttons = new Button[] { v.OkButton, v.CancelButton };
            v.ShowDialog();
        }
    }
}
