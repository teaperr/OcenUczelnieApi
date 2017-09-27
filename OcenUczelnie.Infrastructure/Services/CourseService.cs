﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OcenUczelnie.Core.Domain;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.DTO;
using OcenUczelnie.Infrastructure.Services.Interfaces;

namespace OcenUczelnie.Infrastructure.Services
{
    public class CourseService: ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        public async Task<CourseDto> Get(Guid id)
        {
            var course=await _courseRepository.GetByIdAsync(id);
            return _mapper.Map<Course, CourseDto>(course);
        }

        public async Task<IEnumerable<CourseDto>> BrowseUniversityCoursesAsync(Guid universityId)
        {
            var courses = await _courseRepository.BrowseUniversityCoursesAsync(universityId);
            return _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(courses);
        }
    }
}