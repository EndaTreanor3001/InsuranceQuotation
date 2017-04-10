using System.Windows;
using WpfFrontEnd.ViewModels;

namespace WpfFrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainPageViewModel();            
        }
    }
}
