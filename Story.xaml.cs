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
using System.Threading;

namespace CStudy
{
    /// <summary>
    /// Story.xaml の相互作用ロジック
    /// </summary>
    public partial class Story : Page
    {
        public Story()////ストーリーが選択されたら
        {
            InitializeComponent();////おまじない
            string SaveData = Method_CheckSave();//SaveDataにメソッドCheckSaveの返り値を代入
            int SaveData_Num = int.Parse(SaveData);//SaveDataをint型に変換しSaveData_Numに代入
            if (SaveData_Num == 4)///SaveData_Numが4なら
            {
                MessageBox.Show("体験版はここまでです。続きは製品版でお楽しみください。");//ここまでメッセージをメッセージボックスに表示
                Application.Current.Shutdown();//アプリケーションシャットダウン
            }
            else Method_MailOpen(SaveData_Num, "F");//SaveData_Numの最初のファイルを画面に表示
        }

        private void Button_Reply_Click(object sender, RoutedEventArgs e)////Button_Replyがクリックされたら
        {
            string SaveData = Method_CheckSave();//SaveDataを取得
            int SaveData_Num = int.Parse(SaveData);//SaveDataをint型に変換
            string Path_Answer = @"./data\story\answer\" + SaveData + @"\answer.CStudy";//期待される出力が保存されたPathを定義
            if (TextBox_Reply.Text == Method_ReadFile(Path_Answer, "All"))///返信が期待された値なら
            {
                string UserID = Method_CheckUserID();//現在のUserIDを取得
                string Path_Savedata = @"./data\user\" + UserID + @"\save.CStudy";//セーブデータファイルのファイルパスを取得
                File.AppendAllText(Path_Savedata, "\n" + SaveData_Num + 1);//セーブデータを1進める
                Method_MailOpen(SaveData_Num, "L");//
                Button_Reply.Visibility = Visibility.Hidden;
                Button_NextStory.Visibility = Visibility.Visible;
            }
            else
            {
                TextBlock_Mail.Text = "値が違うようだぞ。";
                Button_Reply.Visibility = Visibility.Hidden;
                Button_Retry.Visibility = Visibility.Visible;
            }
        }

        private void Button_Try_Click(object sender, RoutedEventArgs e)
        {
            Button_Reply.Visibility = Visibility.Visible;
            Button_NextStory.Visibility = Visibility.Hidden;
            Button_Retry.Visibility = Visibility.Hidden;
            string SaveData = Method_CheckSave();
            int SaveData_Num = int.Parse(SaveData);
            if(SaveData_Num == 4)
            {
                MessageBox.Show("体験版はここまでです。続きは製品版でお楽しみください。");
                Application.Current.Shutdown();
            }
            else Method_MailOpen(SaveData_Num, "F");
        }

        public string Method_CheckUserID()
        {
            string Path_NowUser = @"./data\NowUser.CStudy";
            string NowUser = Method_ReadFile(Path_NowUser, "All");
            return NowUser;
        }

        public string Method_CheckSave()
        {
            string NowUser = Method_CheckUserID();
            string Path_SaveData = @"./data\user\" + NowUser + @"\save.CStudy";
            string SaveData = Method_ReadFile(Path_SaveData, "Line");
            return SaveData;
        }

        public void Method_MailOpen(int SaveNum, string MailType)
        {
            string Path_MailData = @"./data\story\mail\" + SaveNum + @"\" + MailType + @".CStudy";
            Method_ReadFile(Path_MailData, "All");
            TextBlock_Mail.Text = Method_ReadFile(Path_MailData, "All");
        }

        public string Method_ReadFile(string Path_File, string How)
        {
            StreamReader Read = new StreamReader(Path_File, Encoding.GetEncoding("Shift_JIS"));
            string OutPut = "";
            if (How == "All")
            {
                OutPut = Read.ReadToEnd();//ファイルの終わりまで読む
            }
            else while (Read.EndOfStream == false) OutPut = Read.ReadLine();
            Read.Close();
            return OutPut;
        }
    }
}
