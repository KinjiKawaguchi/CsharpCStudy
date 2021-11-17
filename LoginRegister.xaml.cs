﻿using System;
using System.IO;
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
    /// LoginRegister.xaml の相互作用ロジック
    /// </summary>
    /// <summary>
    /// Page1.xaml の相互作用ロジック
    /// </summary>
    public partial class LoginRegister : Page
    {
        //public LoginRegister() InitalizeComponent();を試す
        public LoginRegister()
        {
            InitializeComponent();//おまじない
        }

        private void Botton_Select_Login_Click(object sender, RoutedEventArgs e)//ログインが選択されたら
        {
            Label_Register_UserID.Visibility = System.Windows.Visibility.Hidden;//登録関係アイテムを非表示-------------------------------------------------------↓
            TextBox_Register_UserID.Visibility = System.Windows.Visibility.Hidden;
            Label_Register_Password.Visibility = System.Windows.Visibility.Hidden;
            PasswordBox_Register_Password.Visibility = System.Windows.Visibility.Hidden;
            Botton_Register.Visibility = System.Windows.Visibility.Hidden;
            Label_Register_PasswordConfirm.Visibility = System.Windows.Visibility.Hidden;
            PasswordBox_Register_PasswordConfirm.Visibility = System.Windows.Visibility.Hidden;//---------------------------------------------------------------↑
            Label_Login_UserID.Visibility = System.Windows.Visibility.Visible;//ログイン関係のアイテムを表示-----------------------------------------------------↓
            TextBox_Login_UserID.Visibility = System.Windows.Visibility.Visible;
            Label_Login_Password.Visibility = System.Windows.Visibility.Visible;
            PasswordBox_Login_Password.Visibility = System.Windows.Visibility.Visible;
            Botton_Login.Visibility = System.Windows.Visibility.Visible;//--------------------------------------------------------------------------------------↑
        }

        private void Botton_Select_Register_Click(object sender, RoutedEventArgs e)//登録が選択されたら
        {
            Label_Login_UserID.Visibility = System.Windows.Visibility.Hidden;//ログインアイテムを非表示----------------------------------------------------------↓
            TextBox_Login_UserID.Visibility = System.Windows.Visibility.Hidden;
            Label_Login_Password.Visibility = System.Windows.Visibility.Hidden;
            PasswordBox_Login_Password.Visibility = System.Windows.Visibility.Hidden;
            Botton_Login.Visibility = System.Windows.Visibility.Hidden;//---------------------------------------------------------------------------------------↑
            Label_Register_UserID.Visibility = System.Windows.Visibility.Visible;//-----------------------------------------------------------------------------↓
            TextBox_Register_UserID.Visibility = System.Windows.Visibility.Visible;
            Label_Register_Password.Visibility = System.Windows.Visibility.Visible;
            PasswordBox_Register_Password.Visibility = System.Windows.Visibility.Visible;
            Label_Register_PasswordConfirm.Visibility= System.Windows.Visibility.Visible;
            PasswordBox_Register_PasswordConfirm.Visibility = System.Windows.Visibility.Visible;
            Botton_Register.Visibility = System.Windows.Visibility.Visible;//-----------------------------------------------------------------------------------↑
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)//Loginボタンが押されたら
        {
            string Login_UserID = TextBox_Login_UserID.Text;//入力されたIDを変数に代入
            string Login_Password = PasswordBox_Login_Password.Password;//入力されたパスワードを変数に代入
            string Path_Userdata = @"./data\user\" + Login_UserID;//UserIDからデータベースの保存パスを作成
            if (new DirectoryInfo(Path_Userdata).Exists)//ディレクトリデータ(ユーザデータ).存在するか　真：↑　偽：↓
            {
                Path_Userdata = Path_Userdata + @"\password.CStudy";//passwordファイルへのアクセスパスを作成
                StreamReader ReadPassword = new StreamReader(Path_Userdata, Encoding.GetEncoding("Shift_JIS"));//パスワードファイルパスと、入力形式をReadPasswordに定義
                string Check_Password = "";//Check_Passwordに空白を初期値として定義
                Check_Password = ReadPassword.ReadLine();//Check_Passwordに一行読み込んだ結果を代入
                //string Check_Password = sr.ReadLine();を試す
                if (Login_Password == Check_Password)//入力されたパスワードと保存されていたパスワードが一致したら　真：↑　偽：↓
                {
                    var Page_ModeSelect = new ModeSelect();//モード選択画面をPage_ModeSelectに代入
                    NavigationService.Navigate(Page_ModeSelect);//モード選択画面に遷移
                }
                else MessageBox.Show("不正なログイン情報です。");//ログインエラーを出力
            }
            else MessageBox.Show("不正なログイン情報です。");//ログインエラーを出力
        }


        private void RegisterButton_Click(object sender, RoutedEventArgs e)//Registerボタンが押されたら
        {
            string Register_UserID = TextBox_Register_UserID.Text;//入力されたIDを変数に代入
            string Register_Password = PasswordBox_Register_Password.Password.ToString();//入力されたパスワードを変数に代入
            string Register_PasswordConfirm = PasswordBox_Register_PasswordConfirm.Password.ToString();//入力された再度入力パスワードを変数に代入
            if (Register_Password == "") MessageBox.Show("Passwordが入力されていません。");//パスワード未入力エラーを出力
            else
            {

                if (Register_Password == Register_PasswordConfirm)//入力されたパスワードが一致していたら　真：↑　偽：↓」
                {
                    string Path_Userdata = "data\\user\\" + Register_UserID;//ユーザディレクトリ作成用ファイルパスを定義
                    //if(new DirectoryInfo(Path_Userdata).Exists) MessageBox.Show("すでに存在するUserIDです。");を試す
                    if (System.IO.File.Exists(Path_Userdata)) MessageBox.Show("既に存在するUserIDです。");//すでにユーザディレクトリが存在していたら　偽：↓
                    else
                    {
                        Directory.CreateDirectory(Path_Userdata);//UserIDと同値のディレクリを作成
                        Path_Userdata = Path_Userdata + ("\\password.CStudy");//パスワードファイルのパスを定義
                        var Page_ModeSelect = new ModeSelect();//モード選択画面を定義
                        NavigationService.Navigate(ModeSelect);//モード選択画面に遷移
                    }
                }
                else MessageBox.Show("パスワードが一致していません。");//パスワード不一致を表示
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)//Exitボタンが押されたら
        {
            Application.Current.Shutdown();//アプリケーションを終了する。
        }
    }
}
