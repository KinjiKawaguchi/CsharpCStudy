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
