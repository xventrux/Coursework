using Microsoft.EntityFrameworkCore;
using Site.Infrastructure.Repository;
using Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.Services.Repositories.СourseRepositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Course>> GetAllForGroupWithAll(Guid id)
        {
            List<Course> l = await GetAllFiltered(cr => cr.GroupId == id)
                .Include(cr => cr.Students)
                .Include(cr => cr.Teacher)
                .Include(cr => cr.Group).ToListAsync();

            foreach (var i in l)
            {
                i.Teacher.Courses = null;
                i.Group.Courses = null;

                foreach (var j in i.Students)
                {
                    j.Course = null;
                }

            }

            return l;
        }

        public async Task<List<Course>> GetAllForTeacherWithAll(string id)
        {
            List<Course> l = await GetAllFiltered(cr => cr.TeacherId == id)
                .Include(cr => cr.Students)
                .Include(cr => cr.Group).ToListAsync();

            foreach (var i in l)
            {
                i.Teacher.Courses = null;
                i.Group.Courses = null;

                foreach (var j in i.Students)
                {
                    j.Course = null;
                }
                
            }

            return l;
        }

        public async Task<List<Course>> GetAllWithAll()
        {
            List<Course> l = await GetAll().Include(cr => cr.Teacher).ToListAsync();

            foreach (var i in l)
            {
                i.Teacher.Courses = null;
                //i.Group.Courses = null;
            }

            return l;
        }

        public async Task<Course> GetByIdWithAll(Guid id)
        {
            Course c = await GetAllFiltered(cr => cr.Id == id)
                .Include(cr => cr.Teacher)
                .Include(cr => cr.Group)
                .Include(cr => cr.Materials)
                .Include(cr => cr.Students).SingleOrDefaultAsync();

            c.Teacher.Courses = null;
            c.Group.Courses = null;

            foreach(var i in c.Materials)
            {
                i.Course = null;
            }

            foreach (var i in c.Students)
            {
                i.Course = null;
            }

            return c;
        }
    }
}
