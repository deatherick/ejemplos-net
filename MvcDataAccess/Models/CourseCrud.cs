﻿using System.Collections.Generic;
using System.Linq;

namespace MvcDataAccess.Models
{
   
    public class CourseCrud : ICourseCrud
    {

        public List<Course> SelectAll(SchoolEntities entities, int courseId = 0)
        {
            using (entities)
            {
                IQueryable<Course> courses;
                if (courseId != 0)
                {
                    courses = entities.Courses
                        .Where(c => c.CourseID == courseId)
                        .OrderBy(d => d.CourseID);
                }
                else
                {
                    courses = entities.Courses
                        .OrderBy(d => d.CourseID);
                }
                return courses.ToList();
            }
        }

        public List<Course> Select(SchoolEntities entities, int departmentId)
        {
            using (entities)
            {
                IQueryable<Course> courses = entities.Courses
                    .Where(c => c.DepartmentID == departmentId)
                    .OrderBy(d => d.CourseID);
                return courses.ToList();
            }
        }

        public bool Update(SchoolEntities entities, Course updatedCourse)
        {
            using (entities)
            {
                var course = (from c in entities.Courses
                              where c.CourseID == updatedCourse.CourseID
                              select c).FirstOrDefault();
                if (course == null) return false;
                course.Title = updatedCourse.Title;
                course.Credits = updatedCourse.Credits;
                course.DepartmentID = updatedCourse.DepartmentID;
                entities.SaveChanges();
                return true;
            }
        }
    }
}
