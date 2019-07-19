using System.Windows;

namespace BelarusChess
{
    /// <summary> Логика взаимодействия для EnterWindow.xaml </summary>
    public partial class EnterWindow : Window
    {
        HelpWindow helpWindow;

        public EnterWindow()
        {
            InitializeComponent();
        }

        // Events
        private void ButtonGame_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void ButtonHelp_Click(object sender, RoutedEventArgs e)
        {
            helpWindow = new HelpWindow();
            helpWindow.Show();
        }
    }
}
