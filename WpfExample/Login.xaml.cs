using System.Windows;

namespace WpfExample
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var window = new Menu {Owner = this};
            window.ShowDialog();
        }
    }
}
