using System.Windows;
using System.Windows.Input;

namespace BMCLV4.Windows
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            BmclCore.MainWindow = this;
            BmclCore.NIcon.MainWindow = this;
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            SysinfoLabel.Content = string.Format("{0} With {1} GB RAM",Config.GetSystemName(), Config.GetMemory()/1024);
            LabVersion.Content = BmclCore.BmclVersion;
        }
    }
}
