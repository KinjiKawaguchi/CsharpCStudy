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
    /// Story.xaml の相互作用ロジック
    /// </summary>
    public partial class Story : Page
    {
        public Story()
        {
            InitializeComponent();
            string SaveData = Method_CheckSave();
            int SaveData_Num = int.Parse(SaveData);
            Method_MailOpen(SaveData_Num, "F");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string SaveData = Method_CheckSave();
            string Path_Answer = @"./data\story\answer\" + SaveData + @"\answer.CStudy";
            if (TextBox_Reply.Text == Method_ReadFile(Path_Answer, "All")) 
            {
                int SaveData_Num = int.Parse(SaveData);
                Method_MailOpen(SaveData_Num, "L");
            }
        }

        public string Method_CheckUserID()
        {
            string Path_NowUser = @"./data\NowUser.CStudy";
            string NowUser = Method_ReadFile(Path_NowUser, "All");
            return NowUser;
        }

        public string Method_CheckSave()
        {
            string Path_NowUser = @"./data\NowUser.CStudy";
            string NowUser = Method_ReadFile(Path_NowUser, "All");
            string Path_SaveData = @"./data\user\" + NowUser + @"\save.CStudy";
            string SaveData = Method_ReadFile(Path_SaveData, "Line");
            return SaveData;
        }

        public void Method_MailOpen(int SaveNum, string MailType)
        {
            string Path_MailData = @"./data\story\mail" + SaveNum + @"\" + MailType + @".CStudy";
            Method_ReadFile(Path_MailData, "All");
            TextBlock_Mail.Text = Method_ReadFile(Path_MailData, "All");
        }

        public string Method_ReadFile(string Path_File, string  How)
        {
            StreamReader Read = new StreamReader(Path_File, Encoding.GetEncoding("Shift_JIS"));
            string OutPut = "";
            if (How == "All")
            {
                OutPut = Read.ReadToEnd();//ファイルの終わりまで読む
                Read.Close();///ファイルクローズ
            }
            else while (Read.EndOfStream == false) OutPut = Read.ReadLine();
            return OutPut;
        }

    }
}
