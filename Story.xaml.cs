﻿using Microsoft.SmallBasic.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;

namespace CStudy
{
    /// <summary>
    /// Story.xaml の相互作用ロジック
    /// </summary>
    public partial class Story : Page
    {
        public static class Global
        {
            public static string UserID = "";
            public static string SaveData = "";
            public static int SaveData_Num = 0;
            public static int Story_amount = 0;
        }

        public Story()////ストーリーが選択されたら
        {
            InitializeComponent();////おまじない


            /*MediaAudio.LoadedBehavior = MediaState.Stop;
            MediaAudio.Source = new Uri(@"bgm.wmv", UriKind.Relative);*/
            Play_Game();
        }

        private readonly DispatcherTimer timer1 = new DispatcherTimer();
        private readonly DispatcherTimer timer = new DispatcherTimer();
        private readonly Queue<string> lineQueue = new Queue<string>();
        public async void Play_Game()
        {
            /*MediaAudio.LoadedBehavior = MediaState.Manual;
            MediaAudio.Play();*/
            //グローバル変数の定義--------------------------------------------------------------------------------------------------------
            Global.UserID = Method_ReadFile(@"./data\NowUser.CStudy", "All");
            Global.SaveData = Method_ReadFile(@"./data\user\" + Global.UserID + @"\save.CStudy", "Line");
            Global.SaveData_Num = int.Parse(Global.SaveData);
            string Story_amount = Method_ReadFile(@"./data\story\Number_of_Story.CStudy", "Line");
            Global.Story_amount = int.Parse(Story_amount);
            //----------------------------------------------------------------------------------------------------------------------------
            switch (Global.SaveData_Num)
            {
                case 0:
                    ME_Boot.Visibility=Visibility.Visible;
                    ME_Boot.LoadedBehavior = MediaState.Manual;
                    ME_Boot.Source = new Uri(@"C:\Users\KAWAK\source\repos\KinjiKawaguchi\CsharpCStudy\VIDEO\PCBoot.mp4");
                    Sound.Play(@"C:\Users\KAWAK\source\repos\KinjiKawaguchi\CsharpCStudy\MUSIC\Sound_PCBoot.mp3");
                    ME_Boot.Play();
                    await Task.Delay(15000);
                    ME_Boot.Visibility = Visibility.Hidden;
                    Button_Mail.Visibility = Visibility.Visible;
                    Button_Mail2.Visibility = Visibility.Visible;
                    Image_Taskbar.Visibility = Visibility.Visible;
                    Button_WindowsMark.Visibility = Visibility.Visible;
                    /*
                    string Path_File = (@"./data\story\boot.CStudy");
                    lineQueue.Clear();//表示キュークリア
                    string[] file = System.IO.File.ReadAllLines(Path_File);//ファイル読み込み
                    foreach (string line in file)
                    {
                        lineQueue.Enqueue(line);// 表示データをキューに格納
                    }
                    // タイマー開始---------------------------------
                    timer1.Interval = new TimeSpan(0, 0, 0, 0, 50);
                    timer1.Tick += Timer1_Tick;
                    timer1.Start();
                    //----------------------------------------------
                    */
                    break;
                default:
                    if (Global.SaveData_Num == Global.Story_amount + 1)
                    {
                        MessageBox.Show("体験版はここまでです。続きは製品版でお楽しみください。");//ここまでメッセージをメッセージボックスに表示
                        NavigationService.Navigate(new ModeSelect());
                    }
                    else
                    {
                        Image_Taskbar.Visibility = Visibility.Visible;
                        Button_WindowsMark.Visibility = Visibility.Visible;
                        TextBlock_MailContent.Visibility = Visibility.Visible;
                        WB_Paiza.Visibility = Visibility.Visible;
                        TextBox_Reply.Visibility = Visibility.Visible;
                        Button_Reply.Visibility = Visibility.Visible;//返信ボタンを可視化
                        Button_NextStory.Visibility = Visibility.Hidden;//次のステージボタンを不可視
                        Button_Retry.Visibility = Visibility.Hidden;//リトライボタンを不可視
                        Method_MailOpen(Global.SaveData_Num, "F");
                    }
                    break;
            }
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (lineQueue.Count > 0)
            {
                // キューから取り出し1行表示
                Label_FirstMail.Content += "\n" + lineQueue.Dequeue();
            }
            else
            {
                // タイマー停止
                timer1.Stop();
                Label_FirstMail.Visibility = Visibility.Hidden;
                Button_Mail.Visibility = Visibility.Visible;
                Button_Mail2.Visibility = Visibility.Visible;
                Image_Taskbar.Visibility = Visibility.Visible;
                Button_WindowsMark.Visibility = Visibility.Visible;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------
        private void Button_Open_Mail_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_MailContent.Visibility = Visibility.Visible;
            //TextBox_Reply.Visibility = Visibility.Visible;
            //Button_Reply.Visibility = Visibility.Visible;
            Method_MailOpen(Global.SaveData_Num, "F");
            //メールにコーディン用のソフトをダウンロード・起動するように書く
            Button_Paiza_Download.Visibility = Visibility.Visible;
            //
        }

        private void Button_Paiza_Download_Click(object sender, RoutedEventArgs e)
        {
            Button_Paiza_Download.Visibility = Visibility.Hidden;
            Button_Paiza.Visibility = Visibility.Visible;
        }

        private void Button_Paiza_Click(object sender, RoutedEventArgs e)
        {
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
        private void Button_Reply_Click(object sender, RoutedEventArgs e)////Button_Replyがクリックされたら
        {
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
                TextBlock_MailContent.Text = "値が違うようだぞ。";//メールの欄に値が違うエラーを表示
                Button_Reply.Visibility = Visibility.Hidden;//リプライボタンを不可視
                Button_Retry.Visibility = Visibility.Visible;//リトライボタンを可視
            }
        }

        private void Button_Try_Click(object sender, RoutedEventArgs e)////リトライまたは次のステージボタンが押されたら
        {
            Play_Game();
        }

        private void WinMark_Click(object sender, RoutedEventArgs e)
        {
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

        //------------------------------------------------------------------------------------------------------------------------------
        /*
        public void Method_Timer(int d ,int h ,int m ,int s ,int ms,int times)
        {
            timer.Interval = new TimeSpan(d, h, m, s, ms);
            for(int i = 0; i < times; i++)
            {
                timer.Start();
                timer.Stop();
            }
        }*/
        //------------------------------------------------------------------------------------------------------------------------------

        public void Method_MailOpen(int SaveData_Num, string MailType)
        {
            string Path_MailData = @"./data\story\mail\" + SaveData_Num + @"\" + MailType + @".CStudy";
            Method_ReadFile(Path_MailData, "All");
            TextBlock_MailContent.Text = Method_ReadFile(Path_MailData, "All");
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

    }
}