using LessonRegistration.Data.Interfaces;
using LessonRegistration.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LessonRegistration.Data.Services
{
    public class Departments :
        IAddOne<Department>,
        IRemoveOne<Department>,
        IUpdateOne<Department>,
        IGetAll<Department>,
        IGetByPostgreId<Department?>
    {
        private readonly AppDBContext appDBContext;

        public Departments(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public Department Add(Department department)
        {
            var institute = this.appDBContext.Institutes.Find(department.Institute.Id);
            if (institute == null)
            {
                throw new Exception($"Can't create institute without existing department with id {department.Id}");
            }
            department.Institute = institute;

            appDBContext.Departments.Add(department);
            appDBContext.SaveChanges();

            return department;
        }

        public IEnumerable<Department> GetAll()
        {
            return IncludeAll(appDBContext.Departments).AsEnumerable();
        }

        public Department? GetById(int id)
        {
            var department = appDBContext.Departments.Find(id);
            if (department == null)
            {
                return null;
            }

            var departmentHandler = appDBContext.Entry(department);
            departmentHandler.Collection(d => d.Semesters).Load();
            departmentHandler.Reference(d => d.Institute).Load();

            return department;
        }

        public Department Remove(Department department)
        {
            appDBContext.Departments.Remove(department);
            appDBContext.SaveChanges();

            return department;
        }

        public Department Update(Department department)
        {
            var oldDepartment = appDBContext.Departments.Find(department.Id);
            if (oldDepartment == null)
            {
                throw new Exception($"Can't update unexisting department with id {department.Id}");
            }
            var institute = appDBContext.Institutes.Find(department.Institute.Id);
            if (institute == null)
            {
                throw new Exception($"Can't update department with id ${department.Id}. Institute with id ${department.Institute.Id} doesn't exist.");
            }

            department.Institute = institute;
            appDBContext.Entry(oldDepartment).State = EntityState.Detached;
            appDBContext.Departments.Update(department);
            appDBContext.SaveChanges();

            return department;
        }

        public IEnumerable<Department> FindByName(string name)
        {
            var found = appDBContext.Departments.Where(d => d.Name == name);

            return IncludeAll(found).AsEnumerable();
        }

        public static IQueryable<Department> IncludeAll(IQueryable<Department> departments)
        {
            return departments
                .Include(d => d.Institute)
                .Include(d => d.Semesters);
        }
    }
}
