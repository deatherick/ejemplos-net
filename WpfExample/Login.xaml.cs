using System.Windows;
using Castle.Windsor;
using MvcBusinessLogic;
using MvcBusinessLogic.Installers;
using MvcDataAccess.Entities;

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
            using (var container = new WindsorContainer())
            {
                var identity = TxtUsuario.Text;
                var password = TxtPassword.Password;

                container.Install(
                    new LoginInstaller()
                );

                var loginObject = container.Resolve<LoginController>();

                var response = loginObject.Login(identity, password);
                if (response.Success)
                {
                    if (MessageBox.Show(@"Bienvenido " + ((Person)response.Object).Name, @"Aceptar",MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                    {
                        var window = new Menu { Owner = this };
                        window.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show(response.Message);
                }

            }

        }
    }
}
