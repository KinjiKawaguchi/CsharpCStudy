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

        public void Button_Navi_ProgramingTIPS_Content(object sender , RoutedEventArgs e)
        {
            //string NoteStr = ((Button)sender).Name.ToString();
            //NoteStr = NoteStr.Substring(NoteStr.IndexOf("_"));
            //string Path_Note = @"./data\note\" + NoteStr;
            string Path_ProgramingNote = @"./data\note\" + ((Button)sender).Name.ToString();
            //-StreamReader ReadNote = new StreamREader(Path_ProgramingNote,Encoding.GetEncoding("Shift_JIS"));
            //-string Buf_Note=ReadNote.ReadLine();
            //-Label_ProgramingNote_Title.Text = ReadNote.ReadLine();
            //-Label_ProgramingNote_Content.Text = ReadNote.ReadAll();
        }

        private void Button_Navi_ModeSelect_Click(object sender, RoutedEventArgs e)
        {
            var Page_ModeSelect = new ModeSelect();
            NavigationService.Navigate(Page_ModeSelect);
        }


    }
}
