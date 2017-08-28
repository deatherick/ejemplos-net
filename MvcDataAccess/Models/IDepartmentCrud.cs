using System.Collections.Generic;

namespace MvcDataAccess.Models
{
    public interface IDepartmentCrud
    {
        List<Department> SelectAll(SchoolEntities entities, int departmentId = 0);
        List<Department> Select(SchoolEntities entities, int departmentId);
        bool Update(SchoolEntities entities, Department updatedDepartment);
    }
}
