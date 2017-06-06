using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Castle.Windsor;
using FastMember;
using MvcBusinessLogic;
using MvcBusinessLogic.Installers;
using MvcDataAccess.Models;

namespace WebExample.Backend
{
    public partial class Dashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Initialize the ObjectContext
            var container = new WindsorContainer();
            container.Install(
                new SchoolInstaller()
                );
            var courseObject = container.Resolve<CourseController>();
            var departmentObject = container.Resolve<DepartmentController>();



            // Define a query that returns all Department  
            // objects and course objects, ordered by name.
            var departments = departmentObject.SelectAll();
            try
            {
                // Bind the ComboBox control to the query, 
                // which is executed during data binding.
                // To prevent the query from being executed multiple times during binding, 
                // it is recommended to bind controls to the result of the Execute method. 
                if (IsPostBack)
                {
                    ddlDepartments.SelectedIndex = ddlDepartments.SelectedIndex;
                }
                if (!Page.IsPostBack)
                {
                    ddlDepartments.DataValueField = "DepartmentID";
                    ddlDepartments.DataTextField = "Name";
                    ddlDepartments.DataSource = departments;
                    ddlDepartments.DataBind();

                    var selectedValue = int.Parse(ddlDepartments.SelectedItem.Value);
                    //Get the object for the selected department.
                    var courses = courseObject.Select(selectedValue);
                    //Bind the grid view to the collection of Course objects
                    // that are related to the selected Department object.
                    //Persist the table in the Session object.
                    var table = new DataTable();
                    using (var reader = ObjectReader.Create(courses))
                    {
                        table.Load(reader);
                    }
                    Session["gridCoursesTable"] = table;

                    //Bind data to the GridView control.
                    BindData();
                }

                

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('" + ex.Message + "')", true);
            }
        }

        protected void ddlDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var container = new WindsorContainer();
                container.Install(
                    new SchoolInstaller()
                    );
                var courseObject = container.Resolve<CourseController>();
                ddlDepartments.SelectedIndex = ddlDepartments.SelectedIndex;
                var selectedValue = int.Parse(ddlDepartments.SelectedItem.Value);
                //Get the object for the selected department.
                var courses = courseObject.Select(selectedValue);
                //Bind the grid view to the collection of Course objects
                // that are related to the selected Department object.
                var table = new DataTable();
                using (var reader = ObjectReader.Create(courses))
                {
                    table.Load(reader);
                }
                Session["gridCoursesTable"] = table;

                //Bind data to the GridView control.
                BindData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('" + ex.Message + "')", true);
            }
        }

        protected void gridCourses_PreRender(object sender, EventArgs e)
        {
            if (gridCourses.Rows.Count <= 0) return;
            gridCourses.UseAccessibleHeader = true;
            gridCourses.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void gridCourses_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            var selectedIndex = gridCourses.SelectedIndex;
            Console.WriteLine(selectedIndex);
        }

        protected void gridCourses_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Set the edit index.
            gridCourses.EditIndex = e.NewEditIndex;
            //Bind data to the GridView control.
            BindData();
        }

        protected void gridCourses_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Retrieve the table from the session object.
            var dt = (DataTable)Session["gridCoursesTable"];
            var updateCourse = new Course();

            //Update the values.
            var row = gridCourses.Rows[e.RowIndex];
            updateCourse.Title = ((TextBox)row.Cells[0].Controls[0]).Text;
            updateCourse.CourseID = int.Parse(((TextBox)row.Cells[1].Controls[0]).Text);
            updateCourse.Credits = int.Parse(((TextBox)row.Cells[2].Controls[0]).Text);
            updateCourse.DepartmentID = int.Parse(((TextBox)row.Cells[3].Controls[0]).Text);
            var container = new WindsorContainer();
            container.Install(
                new SchoolInstaller()
                );
            var courseObject = container.Resolve<CourseController>();
            var result = courseObject.Update(updateCourse);
            if (result)
            {
                dt.Rows[row.DataItemIndex]["Title"] = updateCourse.Title;
                dt.Rows[row.DataItemIndex]["CourseID"] = updateCourse.CourseID;
                dt.Rows[row.DataItemIndex]["Credits"] = updateCourse.Credits;
                dt.Rows[row.DataItemIndex]["DepartmentID"] = updateCourse.DepartmentID;
            }
           
            //Reset the edit index.
            gridCourses.EditIndex = -1;

            //Bind data to the GridView control.
            BindData();
        }

        protected void gridCourses_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {   
            //Reset the edit index.
            gridCourses.EditIndex = -1;
            //Bind data to the GridView control.
            BindData();
        }
        protected void gridCourses_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridCourses.PageIndex = e.NewPageIndex;
            //Bind data to the GridView control.
            BindData();
        }
        private void BindData()
        {
            gridCourses.DataSource = Session["gridCoursesTable"];
            gridCourses.DataBind();
        }
    }
}