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
            string Path_Note = @"./data\note\" + Which_Note_Oepen;//ノートのパスを設定1
            string Path_Note_Title = PathNote + "_Title" + ".CStudy";//ノートのタイトルパスを設定
            string Path_Note = Path_Note + ".CStudy";//ノートのファイルパス設定2
            StreamReader Read_Title = new StreamReader(Path_Note_Title, Encoding.GetEncoding("Shift_JIS"));
            string ProgramingTIPS_Title = Read_Title.ReadToEnd();
            Label_ProgramingTIPS_Title.Content = ProgramingTIPS_Title;
            Read_Title.Close();
            StreamReader Read_Content = new StreamReader(Path_Note, Encoding.GetEncoding("Shift_JIS"));
            string ProgramignTIPS_Content = Read_Content.ReadToEnd();
            Label_ProgramingTIPS_Content.Content = ProgramingTIPS_Content;
            Read_Content.Close();
        }
        
        private void Button_Navi_Back_Click(object sender, RoutedEventArgs e)
        {
            int Note_Num;
            Which_Note_Open = Which_Note_Open.Substring(1,Which_Note_Open.Length-1);
            int.TryParce(Which_Note_Open, out Note_Num);
            if(NoteNum==1)
            {
                Messagebox.Show("戻れるノートがありません。");
            }
            else
            {
                NoteNum--;
                Which_Note_Open = "_" + NoteNum;
                ProgramingTIPS_Content(Which_Note_Open);
            }
        }
        
        private void Button_Navi_Next_Click(object sender, RoutedEventArgs e)
        {
            int Note_Num;
            Which_Note_Open = Which_Note_Open.Substring(1,Which_Note_Open.Length-1);
            int.TryParce(Which_Note_Open, out Note_Num);
            if(NoteNum==16)
            {
                Messagebox.Show("次のノートがありません。");
            }
            else
            {
                NoteNum++;
                Which_Note_Open = "_" + NoteNum;
                ProgramingTIPS_Content(Which_Note_Open);
            }
        }

        private void Button_Navi_TOC_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProgramingTIPS_TOC());
        }
    }
}
