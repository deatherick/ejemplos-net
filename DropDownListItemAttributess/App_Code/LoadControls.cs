using System.Data;
using System.Web.UI.WebControls;

namespace DropDownListItemAttributess
{
    public class LoadControls : System.Web.UI.Page
    {
        /// <summary>
        /// this method binds the data to DropDownListAttributes DropDownList
        /// </summary>
        /// <param name="ddl1"></param>
        public void BindDDL1(DropDownListAttributes ddl1)
        {
            var ds = new DataSet();
            ds.ReadXml(Server.MapPath("Employee.xml"));

            ddl1.DataSource = ds.Tables[0];
            ddl1.DataTextField = "Name";
            ddl1.DataValueField = "Id";
            ddl1.DataBind();

            foreach (ListItem item in ddl1.Items)
                item.Attributes.Add("title", item.Text);

            foreach (ListItem item in ddl1.Items)
                item.Attributes.Add("ParamCode", item.Value);
        }

        /// <summary>
        /// this method binds the data to normal DropDownList
        /// </summary>
        /// <param name="ddl2"></param>
        public void BindDDL2(DropDownList ddl2)
        {
            var ds = new DataSet();
            ds.ReadXml(Server.MapPath("Employee.xml"));


            ddl2.DataSource = ds.Tables[0];
            ddl2.DataTextField = "Name";
            ddl2.DataValueField = "Id";
            ddl2.DataBind();

            foreach (ListItem item in ddl2.Items)
                item.Attributes.Add("title", item.Text);
        }
    }
}