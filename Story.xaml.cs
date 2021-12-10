using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Timers;

namespace CStudy
{
    /// <summary>
    /// Story.xaml の相互作用ロジック
    /// </summary>
    public partial class Story : Page
    {
        public static class Global
        {
            public static string UserID="";
            public static string SaveData="";
            public static int SaveData_Num=0;
            public static int Story_amount=0;
        }

        public Story()////ストーリーが選択されたら
        {
            InitializeComponent();////おまじない
            //グローバル変数の定義--------------------------------------------------------------------------------------------------------
            Global.UserID = Method_ReadFile(@"./data\NowUser.CStudy", "All");
            Global.SaveData = Method_ReadFile(@"./data\user\" + Global.UserID + @"\save.CStudy", "Line");
            Global.SaveData_Num = int.Parse(Global.SaveData);
            Global.Story_amount = Directory.GetFiles(@"./data\story\answer", "*", SearchOption.TopDirectoryOnly).Length;
            //----------------------------------------------------------------------------------------------------------------------------
            Play_Game();
            //if (Global.SaveData_Num == Global.Story_amount)///SaveData_Numが4なら
            //{
            //    MessageBox.Show("体験版はここまでです。続きは製品版でお楽しみください。");//ここまでメッセージをメッセージボックスに表示
            //   Application.Current.Shutdown();//アプリケーションシャットダウン
            //}
            //else
            //{
            //    Method_MailOpen(Global.SaveData_Num, "F");//SaveData_Numの最初のファイルを画面に表示
            //}
        }

        public void Play_Game()
        {
            switch (Global.SaveData_Num)
            {
                case 0:
                    Label_Boot.Visibility = Visibility.Visible;
                    string Path_File = (@"./data\story\boot.CStudy");
                    Timer timer = new Timer(500);
                    timer.Elapsed += (sender, e) =>
                    {
                        StreamReader Read = new StreamReader(Path_File, Encoding.GetEncoding("Shift_JIS"));
                        if(Read.EndOfStream == false)
                        {
                            Label_Boot.Content += Read.ReadLine();
                        }
                        else
                        {
                            timer.Stop();
                        }
                    };
                    Label_Boot.Visibility = Visibility.Hidden;
                    Button_Mail.Visibility = Visibility.Visible;
                    Button_Mail2.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void Button_Reply_Click(object sender, RoutedEventArgs e)////Button_Replyがクリックされたら
        {
            string Path_Answer = @"./data\story\answer\" + Global.SaveData + @"\answer.CStudy";//期待される出力が保存されたPathを定義
            if (TextBox_Reply.Text == Method_ReadFile(Path_Answer, "All"))///返信が期待された値なら
            {
                string Path_Savedata = @"./data\user\" + Global.UserID + @"\save.CStudy";//セーブデータファイルのファイルパスを取得
                File.AppendAllText(Path_Savedata, "\n" + Global.SaveData_Num + 1);//セーブデータを1進める
                Method_MailOpen(Global.SaveData_Num, "L");//クリアメッセージ（メール）を表示
                Button_Reply.Visibility = Visibility.Hidden;//返信ボタンを不可視にする
                Button_NextStory.Visibility = Visibility.Visible;//次のステージに進むボタンを可視化
            }
            else///期待される値と一致していなかったら
            {
                TextBlock_Mail.Text = "値が違うようだぞ。";//メールの欄に値が違うエラーを表示
                Button_Reply.Visibility = Visibility.Hidden;//リプライボタンを不可視
                Button_Retry.Visibility = Visibility.Visible;//リトライボタンを可視
            }
        }

        private void Button_Try_Click(object sender, RoutedEventArgs e)////リトライまたは次のステージボタンが押されたら
        {
            Button_Reply.Visibility = Visibility.Visible;//返信ボタンを可視化
            Button_NextStory.Visibility = Visibility.Hidden;//次のステージボタンを不可視
            Button_Retry.Visibility = Visibility.Hidden;//リトライボタンを不可視
            if (Global.SaveData_Num == 4)
            {
                MessageBox.Show("体験版はここまでです。続きは製品版でお楽しみください。");
                Application.Current.Shutdown();
            }
            else
            {
                Method_MailOpen(Global.SaveData_Num, "F");
            }
        }



        public void Method_MailOpen(int SaveData_Num, string MailType)
        {
            string Path_MailData = @"./data\story\mail\" + SaveData_Num + @"\" + MailType + @".CStudy";
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
            else
            {
                while (Read.EndOfStream == false)
                {
                    OutPut = Read.ReadLine();
                }
            }

            Read.Close();
            return OutPut;
        }

        private void Button_Open_Mail_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Mail.Visibility = Visibility.Visible;
        }
    }
}
