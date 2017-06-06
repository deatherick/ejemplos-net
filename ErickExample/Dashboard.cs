using System;
using System.Windows.Forms;
using Castle.Windsor;
using MvcBusinessLogic;
using MvcBusinessLogic.Installers;
using MvcDataAccess.Models;

namespace ErickExample
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            //Initialize the ObjectContext
            var container = new WindsorContainer();
            container.Install(
                new SchoolInstaller()
                );
            var departmentObject = container.Resolve<DepartmentController>();


            // Define a query that returns all Department  
            // objects and course objects, ordered by name.
            var departments = departmentObject.SelectAll();
            cmbDepartments.DataSource = departments;
            cmbDepartments.ValueMember = "DepartmentID";
            cmbDepartments.DisplayMember = "Name";
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void deshacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Control Z");
        }

        private void cmbDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var container = new WindsorContainer();
                container.Install(
                    new SchoolInstaller()
                    );
                var courseObject = container.Resolve<CourseController>();
                var selectedValue = (Department) (cmbDepartments.SelectedItem);
                //Get the object for the selected department.
                var courses = courseObject.Select(selectedValue.DepartmentID);
                //Bind the grid view to the collection of Course objects
                // that are related to the selected Department object.
                gridCourses.DataSource = courses;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridCourses_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
