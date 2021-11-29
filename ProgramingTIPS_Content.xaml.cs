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
        public ProgramingTIPS_Content(string Which_Note_Open)////おなじない
        {
            InitializeComponent();//おなじない
            Method_PageOpen(Which_Note_Open);//メソッドPageOpenへ
        }

        private void Button_Navi_Other_Click(object sender, RoutedEventArgs e)////ほかのページを見ることを要求されたら
        {
            string Which_Note_Open = ((Button)sender).Name.ToString();//Which_Note_Openを押されたボタンの名前と定義
            switch (Which_Note_Open)///Which_Note_Openの値は？
            {
                case "Button_Navi_Back":///Backボタンだったら
                    Which_Note_Open = Button_Navi_Back.Content.ToString();//バックボタンのコンテンツをWhich_Note_Openに代入(1へ～16へってやつ)
                    break;//次へ
                default:///上記以外なら
                    Which_Note_Open = Button_Navi_Next.Content.ToString();//ネクストボタンのコンテンツをWhich_Note_Openに代入(同上)
                    break;//次へ
            }
            Which_Note_Open = "_" + Which_Note_Open.Substring(0, Which_Note_Open.Length - 1);//”へ”を排除し先頭に_をつける
            Method_PageOpen(Which_Note_Open);//メソッドPageOpenへ
        }

        private void Method_PageOpen(string Which_Note_Open)////メソッド"PageOpen"
        {
            string Path_Note = @"./data\note\" + Which_Note_Open;//ノートのパスを設定1
            string Path_Note_Title = Path_Note + "_Title" + ".CStudy";//ノートのタイトルパスを設定
            Path_Note += ".CStudy";//ノートのファイルパス設定2
            Label_ProgramingTIPS_Title.Content = ReadFile(Path_Note_Title);//タイトルラベルにメソッドReadFileの返り値を代入
            Label_ProgramingTIPS_Content.Content = ReadFile(Path_Note);//コンテンツラベルにメソッドReadFileの返り値を代入
            Which_Note_Open = Which_Note_Open.Substring(1, Which_Note_Open.Length - 1);//Which_Note_Openから"_"を排除
            int.TryParse(Which_Note_Open, out int Note_Num);//stringからintに変換
            Button_Navi_Back.Content = Note_Num - 1 + "へ";//ボタンBackのコンテンツの数字を-1して”へ”をつけ文字データに変換
            Button_Navi_Next.Content = Note_Num + 1 + "へ";//ボタンNextのコンテンツの数字を+1して”へ”をつけ文字データに変換
            Button_Navi_Back.Visibility = Visibility.Visible;//Backボタンを可視化
            Button_Navi_Next.Visibility = Visibility.Visible;//Nextボタンを可視化
            switch (Note_Num)///Note_Numが以下なら
            {
                case 1:///１なら
                    Button_Navi_Back.Visibility = Visibility.Hidden;//Backボタンを不可視化
                    break;//次へ
                case 16:///16なら
                    Button_Navi_Next.Visibility = Visibility.Hidden;//Nextボタンを不可視化
                    break;//次へ
            }

        }

        private string ReadFile(string Path_File)////メソッドReadFile
        {
            StreamReader Read = new StreamReader(Path_File, Encoding.GetEncoding("Shift_JIS"));///ShiftJISで読み込むことを定義
            string OutPut = Read.ReadToEnd();//ファイルの終わりまで読む
            Read.Close();///ファイルクローズ
            return OutPut;//ファイルの中身を返す
        }

        private void Button_Navi_TOC_Click(object sender, RoutedEventArgs e)////目次に戻るボタンが押されたら
        {
            NavigationService.Navigate(new ProgramingTIPS_TOC());//目次に戻る
        }
    }
}
