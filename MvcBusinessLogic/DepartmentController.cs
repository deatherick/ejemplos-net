using System.Collections.Generic;
using MvcDataAccess.Models;

namespace MvcBusinessLogic
{
    public class DepartmentController
    {
        private readonly IDepartmentCrud _departmentCrud;
        private readonly SchoolEntities _schoolEntities;

        public DepartmentController(IDepartmentCrud departmentCrud, SchoolEntities schoolEntities)
        {
            _departmentCrud = departmentCrud;
            _schoolEntities = schoolEntities;
        }

        public List<Department> SelectAll(int departmentId = 0)
        {
            return _departmentCrud.SelectAll(_schoolEntities, departmentId);
        }

        public List<Department> Select(int departmentId)
        {
            return _departmentCrud.Select(_schoolEntities, departmentId);
        }

        public bool Update(SchoolEntities entities, Department updatedDepartment)
        {
            return _departmentCrud.Update(_schoolEntities, updatedDepartment);
        }
    }
}
