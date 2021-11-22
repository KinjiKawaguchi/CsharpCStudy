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
            string Path_Which_Note_Open = (@"./data\ProgramingTIPS.CStudy");
            if(new DirectoryInfo(Path_Which_Note_Open).Exists) File.Delete(Path_Which_Note_Open);
            File.AppendAllText(Path_Which_Note_Open,((Button)sender).Name.ToString() + Environment.NewLine)
            NavigationService.Navigate(new ProgramingTPS_Content());
            //using (StreamReader File_Note = new StreamReader(@"./data\note\" + ((Button)sender).Name.ToString() + ".CStudy"))
            //{
            //    Label
            //}
            //    Console.WriteLine(Path_ProgramingNote);
            //-StreamReader ReadNote = new StreamREader(Path_ProgramingNote,Encoding.GetEncoding("Shift_JIS"));
            //-string Buf_Note=ReadNote.ReadLine();
            //-Label_ProgramingNote_Title.Text = ReadNote.ReadLine();
            //-Label_ProgramingNote_Content.Text = ReadNote.ReadAll();
        }

        private void Button_Navi_ModeSelect_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ModeSelect());//モード選択画面へ遷移
        }


    }
}
