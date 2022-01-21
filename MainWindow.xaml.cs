using System;
using System.Windows;
using Microsoft.SmallBasic.Library;


namespace CStudy
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    /// /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();//おまじない
            Uri uri = new Uri("/LoginRegister.xaml", UriKind.Relative);//最初に表示するXAMLファイルのパスをUriに定義
            frame.Source = uri;//最初に表示する
            this.WindowStyle = WindowStyle.None;//ボーダーレスに変更
            this.WindowState = WindowState.Maximized;//フルスクリーン
        }


    }
}
