using System;
using System.Windows;



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
            InitializeComponent();
            Uri uri = new Uri("/LoginRegister.xaml", UriKind.Relative);
            frame.Source = uri;
            this.WindowStyle = WindowStyle.None;

            // 最大化表示
            this.WindowState = WindowState.Maximized;
        }


    }
}
