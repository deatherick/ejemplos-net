using System;
using System.Windows.Forms;

namespace ErickExample
{
    public partial class Version : Form
    {
        public Version()
        {
            InitializeComponent();
        }

        private void Version_Load(object sender, EventArgs e)
        {
            var ad = System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
            label1.Text = $@"Versión {ad.CurrentVersion}";
        }
    }
}
