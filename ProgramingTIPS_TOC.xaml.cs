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

namespace CStudy
{
    /// <summary>
    /// ProgramingTIPS_TOC.xaml の相互作用ロジック
    /// </summary>
    public partial class ProgramingTIPS_TOC : Page
    {
        public ProgramingTIPS_TOC()
        {
            InitializeComponent();
        }

        public void Button_Navi_ProgramingTIPS_Content_Click(object sender , RoutedEventArgs e)
        {
            string Button_Name = ((Button)sender).Name.ToString();
            NavigationService.Navigate(new ProgramingTIPS_Content(Button_Name));
        }

        private void Button_Navi_ModeSelect_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ModeSelect());//モード選択画面へ遷移
        }


    }
}
