using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace CStudy
{
    /// <summary>
    /// ModeSelect.xaml の相互作用ロジック
    /// </summary>
    public partial class ModeSelect : Page
    {
        //public void(string page)
        //{
        //    //var NextPage = new page();//ログイン登録画面を定義
        //    NavigationService.Navigate(new page());//ログイン登録画面に遷移
        //}

        public ModeSelect()////おなじまい
        {
            InitializeComponent();//おなじない
        }

        private void Button_Navi_Story_Click(object sender, RoutedEventArgs e)////ストーリーモードが選択されたら
        {
            NavigationService.Navigate(new Story());//ストーリー画面に遷移
        }

        private void Button_Navi_ProgramingTIPS_Click(object sender, RoutedEventArgs e)////ProgramingTIPSが選択されたら
        {
            NavigationService.Navigate(new ProgramingTIPS_TOC());//プログラミングTIPS画面に遷移
        }

        private void Button_Navi_LoginRegister_Click(object sender, RoutedEventArgs e)////ログイン登録画面が選択されたら
        {
            NavigationService.Navigate(new LoginRegister());//ログイン登録画面に遷移
        }

        private void Button_Navi_Exit_Click(object sender, RoutedEventArgs e)////Exitが押されたら
        {
            Application.Current.Shutdown();//アプリケーションを終了
        }
    }
}
