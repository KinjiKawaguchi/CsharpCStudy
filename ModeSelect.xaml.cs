using System;
using System.IO;
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

namespace CStudy
{
    /// <summary>
    /// ModeSelect.xaml の相互作用ロジック
    /// </summary>
    public partial class ModeSelect : Page
    {
        public ModeSelect()
        {
            InitializeComponent();
        }

        private void Button_Navi_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
