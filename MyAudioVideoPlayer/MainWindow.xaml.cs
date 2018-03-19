using Microsoft.Win32;
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

namespace MyAudioVideoPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void IsPlaying (bool flag)
        {
            Play.IsEnabled = flag;
            Stop.IsEnabled = flag;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box 
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Videos"; // Default file name 
            dialog.DefaultExt = ".WMV"; // Default file extension 
            dialog.Filter = "WMV file (.wm)|*.wmv"; // Filter files by extension  

            // Show open file dialog box 
            Nullable<bool> result = dialog.ShowDialog();

            // Process open file dialog box results  
            if (result == true)
            {
                // Open document  
                MePlayer.Source = new Uri(dialog.FileName);
                Play.IsEnabled = true;
            }
            if (result == true)

            {

                // Open document

                string filename = dialog.FileName;

                Filepath.Text = filename;

            }

        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            IsPlaying(false);
            MePlayer.Stop();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            IsPlaying(true);
            MePlayer.Play();
        }


    }
}
