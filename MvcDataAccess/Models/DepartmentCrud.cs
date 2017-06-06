using System.Collections.Generic;
using System.Linq;

namespace MvcDataAccess.Models
{
   
    public class DepartmentCrud : IDepartmentCrud
    {

        public List<Department> SelectAll(SchoolEntities entities, int departmentId = 0)
        {
            using (entities)
            {
                IQueryable<Department> departments;
                if (departmentId != 0)
                {
                    departments = entities.Departments
                        .Where(c => c.DepartmentID == departmentId)
                        .OrderBy(d => d.Name);
                }
                else
                {
                    departments = entities.Departments
                        .OrderBy(d => d.Name);
                }
                return departments.ToList();
            }
        }

        public List<Department> Select(SchoolEntities entities, int departmentId)
        {
            using (entities)
            {
                IQueryable<Department> departments = entities.Departments
                    .Where(c => c.DepartmentID == departmentId)
                    .OrderBy(d => d.DepartmentID);
                return departments.ToList();
            }
        }

        public bool Update(SchoolEntities entities, Department updatedDepartment)
        {
            using (entities)
            {
                var department = (from c in entities.Departments
                              where c.DepartmentID == updatedDepartment.DepartmentID
                              select c).FirstOrDefault();
                if (department == null) return false;
                department.Name = updatedDepartment.Name;
                department.Budget = updatedDepartment.Budget;
                department.DepartmentID = updatedDepartment.DepartmentID;
                entities.SaveChanges();
                return true;
            }
        }
    }
}
