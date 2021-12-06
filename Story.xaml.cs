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
            string Path_NowUser = @"./data\NowUser.CStudy";
            string NowUser = Method_ReadFile(Path_NowUser,0);
            string Path_SaveData = @"./data\user\" + NowUser + @"\save.CStudy";
            string SaveData = Method_ReadFile(Path_SaveData,1);
            int SaveData_Num;
            SaveData_Num = int.Parse(SaveData);
            for(int i = 0; i < SaveData_Num; i++)
            {
                
            }
            string Path_MailData = @"./data\story\mail\" + SaveData + "-1.CStudy";
            Method_ReadFile(Path_MailData,0);
        }

        public string Method_ReadFile(string Path_File,int How)
        {
            StreamReader Read= new StreamReader(Path_File,Encoding.GetEncoding("Shift_JIS"));
            string OutPut = "";
            if(How == 0)
            {
                OutPut = Read.ReadToEnd();//ファイルの終わりまで読む
                Read.Close();///ファイルクローズ
            }
            else
            {
                while(Read.EndOfStream == false) OutPut = Read.ReadLine();
            }
            return OutPut;
        }
    }
}
