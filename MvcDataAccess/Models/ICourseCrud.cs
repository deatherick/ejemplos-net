﻿using System.Collections.Generic;

namespace MvcDataAccess.Models
{
    public interface ICourseCrud
    {
        List<Course> SelectAll(SchoolEntities entities, int courseId = 0);
        List<Course> Select(SchoolEntities entities, int departmentId);
        bool Update(SchoolEntities entities, Course updatedCourse);
    }
}
