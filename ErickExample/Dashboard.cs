using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Windows.Forms;
using Castle.Windsor;
using MvcBusinessLogic;
using MvcBusinessLogic.Installers;
using MvcDataAccess.Models;

namespace ErickExample
{
    public class MiMenu
    {
        public string Nombre { get; set; }
        public List<MiMenu> SubMenus { get; set; }
    }
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            var menus = new List<MiMenu>
            {
                new MiMenu() {Nombre = "Primero"},
                new MiMenu() {Nombre = "Segundo", SubMenus = new List<MiMenu>() { new MiMenu() {Nombre = "Tercero"} } },
                new MiMenu() {Nombre = "Cuarto", SubMenus = new List<MiMenu>() { new MiMenu() {Nombre = "Quinto", SubMenus = new List<MiMenu>() { new MiMenu() {Nombre = "Sexto"} } } } }
            };
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

        //public void LlenarMenu(Itemmenu)

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
