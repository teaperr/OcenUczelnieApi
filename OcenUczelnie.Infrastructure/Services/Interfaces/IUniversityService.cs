﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OcenUczelnie.Infrastructure.DTO;

namespace OcenUczelnie.Infrastructure.Services.Interfaces
{
    public interface IUniversityService: IService
    {
        Task AddAsync(string name, string place);
        Task<UniversityDto> Get(Guid id);
        Task<IEnumerable<UniversityDto>> BrowseAllAsync();
        Task<IEnumerable<CourseDto>> BrowseCoursesAsync(Guid id);
    }
}