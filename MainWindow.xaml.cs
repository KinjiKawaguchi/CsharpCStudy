using System;
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



namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStyle = WindowStyle.None;

            // 最大化表示
            this.WindowState = WindowState.Maximized;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)//Loginボタンが押されたら
        {
            string Login_UserID = TextBox_Login_UserID.Text;//入力されたIDを変数に代入
            string Login_Password = PasswordBox_Login_Password.Password.ToString();//入力されたパスワードを変数に代入
            string Path_Userdata = @"./data\user\" + Login_UserID;//UserIDからデータベースの保存パスを作成
            if (new DirectoryInfo(Path_Userdata).Exists)
            {
                Path_Userdata = Path_Userdata + @"\password.CStudy";//passwordファイルへのアクセスパスを作成
                StreamReader sr = new StreamReader(Path_Userdata, Encoding.GetEncoding("Shift_JIS"));
                string Check_Password="";
                while (sr.Peek() != -1)
                {
                    Check_Password= sr.ReadLine();
                }
                if (Login_Password == Check_Password)
                {
					MessageBox.Show("ログイン成功。");
                }
                else
                {
                    MessageBox.Show("パスワードが間違っています。");
                }
            }
            else
            {
                MessageBox.Show("存在しないUserIDです。");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)//Registerボタンが押されたら
        {
            string Register_UserID = TextBox_Register_UserID.Text;//入力されたIDを変数に代入
            string Register_Password = PasswordBox_Register_Password.Password.ToString();//入力されたパスワードを変数に代入
            string Register_PasswordConfirm = PasswordBox_Register_PasswordConfirm.Password.ToString();//入力された再度入力パスワードを変数に代入
            if (Register_Password == "") MessageBox.Show("Passwordが入力されていません。");//パスワード未入力エラーを出力
            else
            {

                if (Register_Password == Register_PasswordConfirm)//
                {
                    string Path_Userdata = "data\\user\\" + Register_UserID;
                    if (System.IO.File.Exists(Path_Userdata))
                    {
                        MessageBox.Show("既に存在するUserIDです。");
                    }
                    else
                    {
                        Directory.CreateDirectory(Path_Userdata);
                        Path_Userdata = Path_Userdata + ("\\password.CStudy");
                        File.AppendAllText(Path_Userdata, Register_Password + Environment.NewLine);
                        MessageBox.Show("登録成功。ログインしてください。");
                    }
                }
                else
                {
                    MessageBox.Show("不正な登録情報です。");
                }
            }


        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
