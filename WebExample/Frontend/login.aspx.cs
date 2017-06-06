using System;
using System.Globalization;
using System.Web.UI;
using Castle.Windsor;
using MvcBusinessLogic;
using MvcBusinessLogic.Installers;
using MvcDataAccess.Entities;

namespace WebExample.Frontend
{
    public partial class Login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ddlLanguages.Items.FindByValue(CultureInfo.CurrentCulture.Name) != null)
                {
                    ddlLanguages.Items.FindByValue(CultureInfo.CurrentCulture.Name).Selected = true;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var container = new WindsorContainer();
            var pIdentity = txtIdentity.Text;
            var pPassword = txtPassword.Text;
            container.Install(
                new LoginInstaller()
                );
            var loginObject = container.Resolve<LoginController>();

            var response = loginObject.Login(pIdentity, pPassword);
            if (response.Success)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('" + @"Bienvenido " + ((Person)response.Object).Name + "')", true);
                Server.Transfer("~/Backend/Dashboard.aspx", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('" + response.Message + "')", true);
            }

            container.Dispose();
        }
    }
}