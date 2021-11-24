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
    /// ProgramingTPS_Content.xaml の相互作用ロジック
    /// </summary>
    public partial class ProgramingTIPS_Content : Page
    {
        public ProgramingTIPS_Content(string Which_Note_Open)
        {
            InitializeComponent();
            //-string Path_Note = @"./data\note\" + Which_Note_Open + ".CStudy"
            //-using (StreamReader File_Note = new StreamReader(Path_Note))
            //-{
            //-    Label_ProgramingTIPS_Title.Content = File_Note.ReadLine();
            //-    string line;
                //-Label_ProgramingTIPS_Content.Content = line.ReadAll;これも試してみる
            //-    while ((line = File_Note.ReadLine()) != null) // 1行ずつ読み出し。
            //-    {
            //-        Label_ProgramingTIPS_Content.Content = Label_ProgramingTIPS_Content.Content + line;
            //-    }
            //-}

        }

        private void Button_Navi_TOC_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProgramingTIPS_TOC());
        }
    }
}
