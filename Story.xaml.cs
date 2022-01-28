using Microsoft.SmallBasic.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CStudy
{
    /// <summary>
    /// Story.xaml の相互作用ロジック
    /// </summary>
    public partial class Story : Page
    {
        public static class Global////Global変数定義
        {
            public static string UserID = "";
            public static string SaveData = "";
            public static int SaveData_Num = 0;
            public static int Story_amount = 0;
        }

        public Story()////ストーリーが選択されたら
        {
            InitializeComponent();////おまじない
            Play_Game();//メソッドプレイゲームに移動
        }

        //private readonly Queue<string> lineQueue = new Queue<string>();
        public async void Play_Game()////メソッドPlayGame
        {
            //グローバル変数の定義--------------------------------------------------------------------------------------------------------
            Global.UserID = Method_ReadFile(@"./data\NowUser.CStudy", "All");
            Global.SaveData = Method_ReadFile(@"./data\user\" + Global.UserID + @"\save.CStudy", "Line");
            Global.SaveData_Num = int.Parse(Global.SaveData);
            string Story_amount = Method_ReadFile(@"./data\story\Number_of_Story.CStudy", "Line");
            Global.Story_amount = int.Parse(Story_amount);
            //----------------------------------------------------------------------------------------------------------------------------
            switch (Global.SaveData_Num)///Global.SaveData_Numの内容によって変更する
            {
                case 0:///初期起動なら
                    ME_Boot.Visibility = Visibility.Visible;//ME_Bootの可視状態を可視化
                    ME_Boot.LoadedBehavior = MediaState.Manual;//ME_BootのLoadedBehaviorをManualにする
                    ME_Boot.Source = new Uri(@"C:\CStudy\VIDEO\PCBoot.mp4");//MEのソースを定義
                    Method_PlaySound("C:\\CStudy\\music\\Sound_PCBoot.mp3");//メソッドPlaySoundでSound_PCBootを再生
                    ME_Boot.Play();//MEを再生
                    await Task.Delay(15000);//非同期処理で処理を1.5秒止める
                    Method_PlaySound("C:\\CStudy\\music\\Story.mp3");//メソッドPlaySoundでストーリーモードのBGMを再生
                    ME_Boot.Visibility = Visibility.Hidden;//ME_Bootの可視状態を不可視化
                    //Storyの画面部品を表示------------------------------------------------------------------------------------------------
                    Button_Mail.Visibility = Visibility.Visible;
                    Button_Mail2.Visibility = Visibility.Visible;
                    Image_Taskbar.Visibility = Visibility.Visible;
                    Button_WindowsMark.Visibility = Visibility.Visible;
                    //---------------------------------------------------------------------------------------------------------------------
                    break;//break
                default://初期起動以外なら
                    if (Global.SaveData_Num == Global.Story_amount + 1)//ストーリの数と現在のセーブデータが同じなら
                    {
                        MessageBox.Show("体験版はここまでです。続きは製品版でお楽しみください。");//ここで終了メッセージをメッセージボックスに表示
                        NavigationService.Navigate(new ModeSelect());//モード選択へ遷移
                    }
                    else//ストーリーが終了してないなら
                    {
                        //Storyの画面部品を表示------------------------------------------------------------------------------------------------
                        Image_Taskbar.Visibility = Visibility.Visible;
                        Button_WindowsMark.Visibility = Visibility.Visible;
                        Label_MailTitle.Visibility = Visibility.Visible;
                        TextBlock_MailContent.Visibility = Visibility.Visible;
                        WB_Paiza.Visibility = Visibility.Visible;
                        Button_NextStory.Visibility = Visibility.Hidden;
                        Button_Navi_Reply.Visibility = Visibility.Visible;
                        //---------------------------------------------------------------------------------------------------------------------
                        Method_MailOpen(Global.SaveData_Num, "F");//ストーリー開始メールを表示
                    }
                    break;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------
        private void Button_Open_Mail_Click(object sender, RoutedEventArgs e)////Mailアプリを起動するボタンが押されたら
        {
            Click();//クリック音を再生
            //メールアプリの部品を表示---------------------------------------------------------------------------------------------------------
            TextBlock_MailContent.Visibility = Visibility.Visible;
            Label_MailTitle.Visibility = Visibility.Visible;
            Button_Paiza_Download.Visibility = Visibility.Visible;
            //---------------------------------------------------------------------------------------------------------------------------------
            Method_MailOpen(Global.SaveData_Num, "F");//ストーリー開始メールを表示
        }

        private async void Button_Paiza_Download_Click(object sender, RoutedEventArgs e)//Paizaのダウンロードボタンが押されたら
        {
            Click();//クリックを再生
            ME_PaizaDownLoading.Visibility = Visibility.Visible;
            ME_PaizaDownLoading.LoadedBehavior = MediaState.Manual;
            ME_PaizaDownLoading.Source = new Uri(@"C:\CStudy\VIDEO\Loading.mp4");
            ME_PaizaDownLoading.Play();
            await Task.Delay(4000);
            ME_PaizaDownLoading.Visibility = Visibility.Hidden;
            Button_Paiza_Download.Visibility = Visibility.Hidden;
            Button_Paiza.Visibility = Visibility.Visible;
        }

        private void Button_Paiza_Click(object sender, RoutedEventArgs e)
        {
            Click();
            Button_Mail.Visibility = Visibility.Hidden;
            Button_Mail2.Visibility = Visibility.Hidden;
            Button_Paiza.Visibility = Visibility.Hidden;
            string Path_Savedata = @"./data\user\" + Global.UserID + @"\save.CStudy";//セーブデータファイルのファイルパスを取得
            int NextNum = Global.SaveData_Num + 1;
            System.IO.File.AppendAllText(Path_Savedata, "\n" + NextNum);//セーブデータを1進める
            Method_MailOpen(Global.SaveData_Num, "L");//クリアメッセージ（メール）を表示
            Button_NextStory.Visibility = Visibility.Visible;//次のステージに進むボタンを可視化
            WB_Paiza.Visibility = Visibility.Visible;
            //ここで返信する。
        }
        //-------------------------------------------------------------------------------------------------------------------------------

        private void Button_Navi_Reply_Click(object sender, RoutedEventArgs e)
        {
            Label_MailTitle.Visibility = Visibility.Hidden;
            TextBlock_MailContent.Visibility = Visibility.Hidden;
            TextBox_Reply.Visibility = Visibility.Visible;
            Button_Reply.Visibility = Visibility.Visible;
            Button_Navi_Mail.Visibility = Visibility.Visible;
        }

        private void Button_Navi_Mail_Click(object sender, RoutedEventArgs e)
        {
            Label_MailTitle.Visibility = Visibility.Visible;
            TextBlock_MailContent.Visibility = Visibility.Visible;
            TextBox_Reply.Visibility = Visibility.Hidden;
            Button_Reply.Visibility = Visibility.Hidden;
            Button_Navi_Mail.Visibility = Visibility.Hidden;
        }

        private void Button_Reply_Click(object sender, RoutedEventArgs e)////Button_Replyがクリックされたら
        {
            Click();
            string Path_Answer = @"./data\story\answer\" + Global.SaveData + @"\answer.CStudy";//期待される出力が保存されたPathを定義
            if (TextBox_Reply.Text == Method_ReadFile(Path_Answer, "All"))///返信が期待された値なら
            {
                string Path_Savedata = @"./data\user\" + Global.UserID + @"\save.CStudy";//セーブデータファイルのファイルパスを取得
                int NextNum = Global.SaveData_Num + 1;
                System.IO.File.AppendAllText(Path_Savedata, "\n" + NextNum);//セーブデータを1進める
                Method_MailOpen(Global.SaveData_Num, "L");//クリアメッセージ（メール）を表示
                Button_Reply.Visibility = Visibility.Hidden;//返信ボタンを不可視にする
                Button_NextStory.Visibility = Visibility.Visible;//次のステージに進むボタンを可視化
            }
            else///期待される値と一致していなかったら
            {
                /*Label_AddInfo.Content = "";*/
                TextBlock_MailContent.Text = "値が違うようだぞ。";//メールの欄に値が違うエラーを表示
                Button_Reply.Visibility = Visibility.Hidden;//リプライボタンを不可視
                Button_Retry.Visibility = Visibility.Visible;//リトライボタンを可視
            }
        }

        private void Button_Try_Click(object sender, RoutedEventArgs e)////リトライまたは次のステージボタンが押されたら
        {
            Click();
            TextBox_Reply.Visibility = Visibility.Hidden;
            Button_Navi_Mail.Visibility = Visibility.Hidden;
            Button_Reply.Visibility = Visibility.Hidden;
            Button_Retry.Visibility = Visibility.Hidden;
            Play_Game();
        }

        private void WinMark_Click(object sender, RoutedEventArgs e)
        {
            Click();
            if (Button_Shutdown.Visibility == Visibility.Visible)
            {
                WB_Paiza.Margin = new Thickness(0, 0, 968, 40);
                Button_Shutdown.Visibility = Visibility.Hidden;
                Button_Back.Visibility = Visibility.Hidden;
            }
            else
            {
                WB_Paiza.Margin = new Thickness(0, 0, 968, 103);
                Button_Shutdown.Visibility = Visibility.Visible;
                Button_Back.Visibility = Visibility.Visible;
            }
        }

        private void Button_Shutdown_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ModeSelect());
        }

        public void Method_MailOpen(int SaveData_Num, string MailType)
        {
            Sound.Play(@"C:\CStudy\music\Sound_Notice.mp3");
            string Path_MailData = @"./data\story\mail\" + SaveData_Num + @"\" + MailType + @".CStudy";
            string Path_MailTitleData = @"./data\story\title\" + SaveData_Num + @".CStudy";
            Method_ReadFile(Path_MailData, "All");
            TextBlock_MailContent.Text = Method_ReadFile(Path_MailData, "All");
            Label_MailTitle.Content = Method_ReadFile(Path_MailTitleData, "All");
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

        public void Method_PlaySound(string Path)
        {
            Sound.Stop(Path);
            Sound.Play(Path);
        }

        public void Click()
        {
            Method_PlaySound("C:\\CStudy\\music\\sound_click.mp3");
        }
    }
}