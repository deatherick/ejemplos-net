using Castle.Windsor;
using System;
using System.Windows.Forms;
using MvcBusinessLogic;
using MvcBusinessLogic.Classes;
using MvcBusinessLogic.Installers;
using MvcDataAccess.Entities;

namespace ErickExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var container = new WindsorContainer();
            var identity = textBox1.Text;
            var password = textBox2.Text;

            container.Install(
                //new DependenciesInstaller(),
                new LoginInstaller()
                );

            /*var dep1Thing = container.Resolve<IDependency1>();
            dep1Thing.SomeObject = "Esto ya fue instanciado y enviado como parametro";

            // CREATE THE MAIN OBJECT AND INVOKE ITS METHOD(S) AS DESIRED.
            var mainThing = container.Resolve<Main>();
            mainThing.DoSomething();

            Console.WriteLine(mainThing.Object1.SomeObject.ToString());
            */
            var loginObject = container.Resolve<LoginController>();

            var response = loginObject.Login(identity, password);
            if (response.Success)
            {
                if (MessageBox.Show(@"Bienvenido " + ((Person)response.Object).Name,@"Aceptar", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
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


            container.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var container = new WindsorContainer();

            container.Install(
                new UrlInstaller()
                );
            var urlObject = container.Resolve<HtmlTitleRetriever>();
            var title = urlObject.GetTitle(new Uri("https://www.google.com.gt"));
            MessageBox.Show(title);
            container.Release(urlObject);
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