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
    /// ProgramingTPS_Content.xaml の相互作用ロジック
    /// </summary>
    public partial class ProgramingTPS_Content : Page
    {
        public ProgramingTPS_Content()
        {
            InitializeComponent();
            string Path_Which_Note_Open = (@"./data\ProgramingTIPS.CStudy");
            StreamReader Which_Note_Open = new StreamReader(Path_Which_Note_Open, Encoding.GetEncoding("Shift_JIS"));//パスワードファイルパスと、入力形式をReadPasswordに定義
            string Note_Open = Which_Note_Open.ReadLine();//Cjeck_PassWordにパスワードを読み込んで代入。
            string Path_Note = (@"data\note") + Note_Open + (".CStudy");
            using (StreamReader File_Note = new StreamReader(Path_Note))
            {
                Label_ProgramingTIPS_Title.Content = File_Note.ReadLine();
                string line;
                //-Label_ProgramingTIPS_Content.Content = line.ReadAll;
                while ((line = File_Note.ReadLine()) != null) // 1行ずつ読み出し。
                {
                    Label_ProgramingTIPS_Content.Content = Label_ProgramingTIPS_Content.Content + line;
                }

            }

        }
    }
}
