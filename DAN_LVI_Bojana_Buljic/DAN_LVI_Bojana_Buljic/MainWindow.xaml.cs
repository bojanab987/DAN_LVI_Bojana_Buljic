using DAN_LVI_Bojana_Buljic.ViewModel;
using System.Windows;

namespace DAN_LVI_Bojana_Buljic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(this);
        }
    }
}
