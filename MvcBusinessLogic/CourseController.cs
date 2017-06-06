using System.Collections.Generic;
using MvcDataAccess.Models;

namespace MvcBusinessLogic
{
    public class CourseController
    {
        private readonly ICourseCrud _courseCrud;
        private readonly SchoolEntities _schoolEntities;

        public CourseController(ICourseCrud courseCrud, SchoolEntities schoolEntities)
        {
            _courseCrud = courseCrud;
            _schoolEntities = schoolEntities;
        }

        public List<Course> SelectAll(int courseId = 0)
        {
            return _courseCrud.SelectAll(_schoolEntities, courseId);
        }

        public List<Course> Select(int departmentId)
        {
            return _courseCrud.Select(_schoolEntities, departmentId);
        }
        public bool Update(Course updatedCourse)
        {
            return _courseCrud.Update(_schoolEntities, updatedCourse);
        }
    }
}
