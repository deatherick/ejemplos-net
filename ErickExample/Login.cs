using Castle.Windsor;
using System;
using System.Windows.Forms;
using MvcBusinessLogic;
using MvcBusinessLogic.Installers;
using MvcDataAccess.Entities;

namespace ErickExample
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var container = new WindsorContainer())
            {
                var identity = textBox1.Text;
                var password = textBox2.Text;

                container.Install(
                    new LoginInstaller()
                );

                var loginObject = container.Resolve<LoginController>();

                var response = loginObject.Login(identity, password);
                if (response.Success)
                {
                    if (MessageBox.Show(@"Bienvenido " + ((Person)response.Object).Name, @"Aceptar", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        Visible = false;
                        var dashboard = new Dashboard();
                        dashboard.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show(response.Message);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var container = new WindsorContainer())
            {
                container.Install(
                   new UrlInstaller()
                );
                var urlObject = container.Resolve<HtmlTitleRetriever>();
                var title = urlObject.GetTitle(new Uri("https://www.google.com.gt"));
                MessageBox.Show(title);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
        }
    }
}