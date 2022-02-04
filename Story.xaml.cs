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
            public static string UserID = "";//Global変数定義・初期化----------------------------------------------------------------------↓
            public static string SaveData = "";
            public static int SaveData_Num = 0;
            public static int Story_amount = 0;//---------------------------------------------------------------------------------↑
        }

        public Story()////ストーリーが選択されたら
        {
            InitializeComponent();////おまじない
            Play_Game();//メソッドプレイゲームに移動
        }

        //private readonly Queue<string> lineQueue = new Queue<string>();
        public async void Play_Game()////メソッドPlayGame
        {
            Global.UserID = Method_ReadFile(@"./data\NowUser.CStudy", "All");//GlobalUserIDの値をNowUserファイルに格納されているUserIDに定義
            Global.SaveData = Method_ReadFile(@"./data\user\" + Global.UserID + @"\save.CStudy", "Line");//ログイン中のUserの保存データをSaveDataに代入
            Global.SaveData_Num = int.Parse(Global.SaveData);//Globa.SaveDataをint型に変換しGlobal.SaveData_Numに代入
            string Story_amount = Method_ReadFile(@"./data\story\Number_of_Story.CStudy", "Line");//現在あるストーリーの数を取得
            Global.Story_amount = int.Parse(Story_amount);//ストーリーの数をint型に変換
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
                    Button_Mail.Visibility = Visibility.Visible;//StoryのGUIを表示--------------------------------------------------------↓
                    Button_Mail2.Visibility = Visibility.Visible;
                    Image_Taskbar.Visibility = Visibility.Visible;
                    Button_WindowsMark.Visibility = Visibility.Visible;//----------------------------------------------------------------↑
                    break;
                default://初期起動以外なら
                    if (Global.SaveData_Num == Global.Story_amount + 1)//ストーリの数と現在のセーブデータが同じなら
                    {
                        MessageBox.Show("体験版はここまでです。続きは製品版でお楽しみください。");//ここで終了メッセージをメッセージボックスに表示
                        NavigationService.Navigate(new ModeSelect());//モード選択へ遷移
                    }
                    else//ストーリーが終了してないなら
                    {
                        
                        Image_Taskbar.Visibility = Visibility.Visible;//Storyの画面部品を表示---------------------------------------------------↓
                        Button_WindowsMark.Visibility = Visibility.Visible;
                        Label_MailTitle.Visibility = Visibility.Visible;
                        TextBlock_MailContent.Visibility = Visibility.Visible;
                        WB_Paiza.Visibility = Visibility.Visible;
                        Button_NextStory.Visibility = Visibility.Hidden;
                        Button_Navi_Reply.Visibility = Visibility.Visible;//-------------------------------------------------------------------↑
                        Method_MailOpen(Global.SaveData_Num, "F");//ストーリー開始メールを表示
                    }
                    break;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------
        private void Button_Open_Mail_Click(object sender, RoutedEventArgs e)////Mailアプリを起動するボタンが押されたら
        {
            Click();//クリック音を再生
            TextBlock_MailContent.Visibility = Visibility.Visible;//メールアプリの部品を表示---------------------------------------------↓
            Label_MailTitle.Visibility = Visibility.Visible;
            Button_Paiza_Download.Visibility = Visibility.Visible;//-------------------------------------------------------------------↑
            Method_MailOpen(Global.SaveData_Num, "F");//ストーリー開始メールを表示
        }

        private async void Button_Paiza_Download_Click(object sender, RoutedEventArgs e)//Paizaのダウンロードボタンが押されたら
        {
            Click();//クリックを再生
            ME_PaizaDownLoading.Visibility = Visibility.Visible;//MediaElementを可視化
            ME_PaizaDownLoading.LoadedBehavior = MediaState.Manual;//MEの状態をManualに変更
            ME_PaizaDownLoading.Source = new Uri(@"C:\CStudy\VIDEO\Loading.mp4");//ロードムービーのパスを定義
            ME_PaizaDownLoading.Play();//動画を再生
            await Task.Delay(4000);//タスクを4秒間停止
            ME_PaizaDownLoading.Visibility = Visibility.Hidden;//を不可視に
            Button_Paiza_Download.Visibility = Visibility.Hidden;//PaizaのDonwloadボタンを不可視に
            Button_Paiza.Visibility = Visibility.Visible;//Paizaのアプリアイコンを可視化
        }

        private void Button_Paiza_Click(object sender, RoutedEventArgs e)
        {
            Click();//クリック音を鳴らす
            Button_Mail.Visibility = Visibility.Hidden;//
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
            Label_MailTitle.Visibility = Visibility.Hidden;//返信用画面のGUI表示・受信ボックスGUI非表示--------------------------------------↓
            TextBlock_MailContent.Visibility = Visibility.Hidden;
            TextBox_Reply.Visibility = Visibility.Visible;
            Button_Reply.Visibility = Visibility.Visible;
            Button_Navi_Mail.Visibility = Visibility.Visible;//---------------------------------------------------------------------------↑
        }

        private void Button_Navi_Mail_Click(object sender, RoutedEventArgs e)
        {
            Label_MailTitle.Visibility = Visibility.Visible;//返信用画面GUIの非表示・受信ボックスGUI表示-------------------------------------↓
            TextBlock_MailContent.Visibility = Visibility.Visible;
            TextBox_Reply.Visibility = Visibility.Hidden;
            Button_Reply.Visibility = Visibility.Hidden;
            Button_Navi_Mail.Visibility = Visibility.Hidden;//----------------------------------------------------------------------------↑
        }

        private void Button_Reply_Click(object sender, RoutedEventArgs e)////Button_Replyがクリックされたら
        {
            Click();//クリック音を鳴らす
            string Path_Answer = @"./data\story\answer\" + Global.SaveData + @"\answer.CStudy";//期待される出力が保存されたPathを定義
            if (TextBox_Reply.Text == Method_ReadFile(Path_Answer, "All"))///返信が適切な値なら
            {
                string Path_Savedata = @"./data\user\" + Global.UserID + @"\save.CStudy";//セーブデータファイルのファイルパスを取得
                int NextNum = Global.SaveData_Num + 1;//NextNumに今遊んでいるセーブデータ番号に1足した数を代入
                System.IO.File.AppendAllText(Path_Savedata, "\n" + NextNum);//セーブファイルを更新
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
            Click();//Click音を鳴らす
            TextBox_Reply.Visibility = Visibility.Hidden;//返信用画面GUI非表示----------------------------------------------------↓
            Button_Navi_Mail.Visibility = Visibility.Hidden;
            Button_Reply.Visibility = Visibility.Hidden;
            Button_Retry.Visibility = Visibility.Hidden;//-----------------------------------------------------------------------↑
            Play_Game();//Play_Gameメソッドに移動
        }

        private void WinMark_Click(object sender, RoutedEventArgs e)////Windowsマークがクリックされたら
        {
            Click();//クリック音を鳴らす
            if (Button_Shutdown.Visibility == Visibility.Visible)///シャットダウンボタンが可視なら
            {
                WB_Paiza.Margin = new Thickness(0, 0, 968, 40);//Paizaの表示領域を広げる
                Button_Shutdown.Visibility = Visibility.Hidden;//シャットダウンボタンを隠す
                Button_Back.Visibility = Visibility.Hidden;//メニューバックボタンを隠す
            }
            else///シャットダウンボタンが不可視なら
            {
                WB_Paiza.Margin = new Thickness(0, 0, 968, 103);//Piazaの表示領域を狭める
                Button_Shutdown.Visibility = Visibility.Visible;//シャットダウンボタンを可視化
                Button_Back.Visibility = Visibility.Visible;//メニューバックボタンを可視化
            }
        }

        private void Button_Shutdown_Click(object sender, RoutedEventArgs e)////シャットダウンが押されたら
        {
            Application.Current.Shutdown();//アプリケーションシャットダウン
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)////バックボタンがおされたら　
        {
            NavigationService.Navigate(new ModeSelect());//ModeSeectへ画面遷移
        }

        public void Method_MailOpen(int SaveData_Num, string MailType)////メソッドMailOpen
        {
            Sound.Play(@"C:\CStudy\music\Sound_Notice.mp3");//メールの着信音を鳴らす
            string Path_MailData = @"./data\story\mail\" + SaveData_Num + @"\" + MailType + @".CStudy";//適切なメールのファイルパスを定義
            string Path_MailTitleData = @"./data\story\title\" + SaveData_Num + @".CStudy";//対応するメールのタイトルファイルパスを定義
            TextBlock_MailContent.Text = Method_ReadFile(Path_MailData, "All");//メールデータをTextBlockのコンテンツとして表示
            Label_MailTitle.Content = Method_ReadFile(Path_MailTitleData, "All");//メールタイトルデータをタイトルLabelに表示
        }

        public string Method_ReadFile(string Path_File, string How)//メソッドReadFile
        {
            StreamReader Read = new StreamReader(Path_File, Encoding.GetEncoding("Shift_JIS"));//エンコード方式をShift_JISと定義
            string OutPut = "";//出力用文字列変数を定義・初期化
            if (How == "All")///読み込み方式が”すべて”だったら("All)
            {
                OutPut = Read.ReadToEnd();//ファイルの終わりまで読む
            }
            else///読み込み方式が最後の一行だったら
            {
                while (Read.EndOfStream == false)///ファイルが終わるまで
                {
                    OutPut = Read.ReadLine();//ファイルの中身を一行読み取る
                }
            }
            Read.Close();//テキストファイルをクローズ
            return OutPut;//出力を返す
        }

        public void Method_PlaySound(string Path)////メソッドPlaySound
        {
            Sound.Stop(Path);//該当の音を再生状態にかかわらず停止
            Sound.Play(Path);//該当の音を再度再生
        }

        public void Click()///メソッドClick
        {
            Method_PlaySound("C:\\CStudy\\music\\sound_click.mp3");//クリック音を再生
        }
    }
}
