using System.Windows;

namespace BelarusChess.Views
{
    /// <summary> Логика взаимодействия для EnterWindow.xaml </summary>
    public partial class EnterWindow : Window
    {
        HelpWindow helpWindow;

        public EnterWindow()
        {
            InitializeComponent();
        }

        private void ButtonGame_Click(object sender, RoutedEventArgs e)
        {
            new ChessWindow().Show();
            Close();
        }
        private void ButtonHelp_Click(object sender, RoutedEventArgs e)
        {
            if (helpWindow == null)
                helpWindow = new HelpWindow();
            helpWindow.Show();
        }
    }
}
