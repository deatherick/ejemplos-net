using System;

namespace DropDownListItemAttributess
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindDDL();
        }

        private void BindDDL()
        {
            var oLoad = new LoadControls();
            oLoad.BindDDL1(ddl1);
            oLoad.BindDDL2(ddl2);
        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            lbl1.Text = "This is on Postback";
        }
    }
}
