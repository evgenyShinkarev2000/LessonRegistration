using LessonRegistration.Data.Interfaces;
using LessonRegistration.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LessonRegistration.Data.Services
{
    public class Institutes :
        IAddOne<Institute>,
        IRemoveOne<Institute>,
        IUpdateOne<Institute>,
        IGetAll<Institute>,
        IGetByPostgreId<Institute?>
    {
        private readonly AppDBContext appDBContext;

        public Institutes(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public Institute Add(Institute institute)
        {
            appDBContext.Institutes.Add(institute);
            appDBContext.SaveChanges();

            return institute;
        }

        public IEnumerable<Institute> FindByName(string name)
        {
            var found = this.appDBContext.Institutes.Where(i => i.Name == name);

            return IncludeAll(found).AsEnumerable();
        }

        public IEnumerable<Institute> GetAll()
        {
            return IncludeAll(appDBContext.Institutes)
                .AsEnumerable();
        }

        public Institute? GetById(int id)
        {
            var found = appDBContext.Institutes.Find(id);
            if (found == null)
            {
                return null;
            }

            appDBContext.Entry(found).Collection(i => i.Departments).Load();

            return found;
        }

        public Institute Remove(Institute institute)
        {
            var removed = GetById(institute.Id);
            if (removed == null)
            {
                throw new Exception($"Can't remove unexisting institute with id {institute.Id}");
            }
            appDBContext.Institutes.Remove(removed);
            appDBContext.SaveChanges();

            return institute;
        }

        public Institute Update(Institute institute)
        {
            var oldInstitute = appDBContext.Institutes.Find(institute.Id);
            if (oldInstitute == null)
            {
                throw new Exception($"Can't update unexisting institute with id {institute.Id}");
            }
            appDBContext.Entry(oldInstitute).State = EntityState.Detached;
            appDBContext.Update(institute);
            appDBContext.SaveChanges();

            return institute;
        }

        public static IQueryable<Institute> IncludeAll(IQueryable<Institute> institutes)
        {
            return institutes.Include(i => i.Departments);
        }
    }
}
