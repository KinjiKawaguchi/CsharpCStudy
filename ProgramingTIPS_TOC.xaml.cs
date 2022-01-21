using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CStudy
{
    /// <summary>
    /// ProgramingTIPS_TOC.xaml の相互作用ロジック
    /// </summary>
    public partial class ProgramingTIPS_TOC : Page
    {
        public ProgramingTIPS_TOC()///おなじない
        {
            InitializeComponent();//おなじない
        }

        public void Button_Navi_ProgramingTIPS_Content_Click(object sender, RoutedEventArgs e)///ノートが選択されたら
        {
            NavigationService.Navigate(new ProgramingTIPS_Content(((Button)sender).Name.ToString()));//ボタンの名前を引数に入れて表示画面へ遷移。変数を引き継ぐ
        }

        private void Button_Navi_ModeSelect_Click(object sender, RoutedEventArgs e)///モード選択画面が押されたら
        {
            NavigationService.Navigate(new ModeSelect());//モード選択画面へ遷移
        }
    }
}
