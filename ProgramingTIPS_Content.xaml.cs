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
    public partial class ProgramingTIPS_Content : Page
    {
        public ProgramingTIPS_Content(string Which_Note_Open)
        {
            InitializeComponent();
            string Path_Note = @"./data\note\" + Which_Note_Open;//ノートのパスを設定1
            string Path_Note_Title = Path_Note + "_Title" + ".CStudy";//ノートのタイトルパスを設定
            Path_Note = Path_Note + ".CStudy";//ノートのファイルパス設定2
            StreamReader Read_Title = new StreamReader(Path_Note_Title, Encoding.GetEncoding("Shift_JIS"));
            string Input_ProgramingTIPS_Title = Read_Title.ReadToEnd();
            Label_ProgramingTIPS_Title.Content = Input_ProgramingTIPS_Title;
            Read_Title.Close();
            StreamReader Read_Content = new StreamReader(Path_Note, Encoding.GetEncoding("Shift_JIS"));
            string Input_ProgramingTIPS_Content = Read_Content.ReadToEnd();
            Label_ProgramingTIPS_Content.Content = Input_ProgramingTIPS_Content;
            Read_Content.Close();
            Which_Note_Open = Which_Note_Open.Substring(1, Which_Note_Open.Length - 1);
            int Note_Num;
            int.TryParse(Which_Note_Open, out Note_Num);
            switch(Note_Num)
            {
                case 1:
                    Button_Navi_Back.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case 16:
                    Button_Navi_Next.Visibility = System.Windows.Visibility.Hidden;
                    break;
            }
            Button_Navi_Back.Content = Note_Num - 1 + "へ";
            Button_Navi_Next.Content = Note_Num + 1 + "へ";
        }

        private void Button_Navi_Other_Click(object sender, RoutedEventArgs e)
        {
            string Which_Note_Open;
            string which_button_click =((Button)sender).Name.ToString();
            if(which_button_click == "Button_Navi_Back")
            {
                Which_Note_Open = Button_Navi_Back.Content.ToString();
            }
            else
            {
                Which_Note_Open = Button_Navi_Next.Content.ToString();
            }
            Which_Note_Open = Which_Note_Open.Substring(0, Which_Note_Open.Length - 1);
            Which_Note_Open = "_" + Which_Note_Open;
            string Path_Note = @"./data\note\" + Which_Note_Open;//ノートのパスを設定1
            string Path_Note_Title = Path_Note + "_Title" + ".CStudy";//ノートのタイトルパスを設定
            Path_Note = Path_Note + ".CStudy";//ノートのファイルパス設定2
            StreamReader Read_Title = new StreamReader(Path_Note_Title, Encoding.GetEncoding("Shift_JIS"));
            string Input_ProgramingTIPS_Title = Read_Title.ReadToEnd();
            Label_ProgramingTIPS_Title.Content = Input_ProgramingTIPS_Title;
            Read_Title.Close();
            StreamReader Read_Content = new StreamReader(Path_Note, Encoding.GetEncoding("Shift_JIS"));
            string Input_ProgramingTIPS_Content = Read_Content.ReadToEnd();
            Label_ProgramingTIPS_Content.Content = Input_ProgramingTIPS_Content;
            Read_Content.Close();
            Which_Note_Open = Which_Note_Open.Substring(1, Which_Note_Open.Length - 1);
            int Note_Num;
            int.TryParse(Which_Note_Open, out Note_Num);
            Button_Navi_Back.Visibility = System.Windows.Visibility.Visible;
            Button_Navi_Next.Visibility = System.Windows.Visibility.Visible;
            switch (Note_Num)
            {
                case 1:
                    Button_Navi_Back.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case 16:
                    Button_Navi_Next.Visibility = System.Windows.Visibility.Hidden;
                    break;
            }
            Button_Navi_Back.Content = Note_Num - 1 + "へ";
            Button_Navi_Next.Content = Note_Num + 1 + "へ";
        }

        private void Button_Navi_TOC_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProgramingTIPS_TOC());
        }
    }
}
