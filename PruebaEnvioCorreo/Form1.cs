using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace PruebaEnvioCorreo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                textBox2.Text = string.Empty;
                if (string.IsNullOrEmpty(txtSubject.Text) || string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show(@"Llenar los campos");
                    return;
                }
                var email = new MailMessage
                {
                    From = new MailAddress("testcaex2017@outlook.com"),
                    Subject = txtSubject.Text,
                    Body = textBox1.Text,
                    IsBodyHtml = true,
                    Priority = MailPriority.Normal
                };

                email.To.Add(new MailAddress("erick.morales@caexlogistics.com"));

                var smtp = new SmtpClient
                {
                    Host = "smtp.live.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("testcaex2017@outlook.com", "textcaex2017")
                };

                smtp.Send(email);
                email.Dispose();
                textBox2.Text = @"Correo enviado";
            }
            catch (Exception exception)
            {
                textBox2.Text = exception.Message;
            }
        }
    }
}
