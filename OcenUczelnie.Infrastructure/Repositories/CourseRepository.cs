﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OcenUczelnie.Core.Domain;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.EF;

namespace OcenUczelnie.Infrastructure.Repositories
{
    public class CourseRepository: ICourseRepository
    {
        private readonly OcenUczelnieContext _context;

        public CourseRepository(OcenUczelnieContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var course =await GetByIdAsync(id);
            if(course==null)
                return;
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task<Course> GetByIdAsync(Guid id)
        {
            var course = await _context.Courses.Include(c=>c.University).
                SingleOrDefaultAsync(c => c.Id == id);
            
            return course;
        }
        public async Task<Course> GetDetailsByIdAsync(Guid id)
        {
            var course = await _context.Courses.Include(c => c.University).Include(c => c.Reviews).
                SingleOrDefaultAsync(c => c.Id == id);

            return course;
        }

        public async Task<IEnumerable<Course>> BrowseUniversityCoursesAsync(Guid universityId)
        {
            var courses = await _context.Courses.Where(c => c.University.Id == universityId).ToListAsync();
            return courses;
        }
    }
}