﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.Command;
using OcenUczelnie.Infrastructure.Services.Interfaces;

namespace OcenUczelnie.Api.Controllers {
    public class UniversityController : BaseApiController {
        private readonly IUniversityService _universityService;

        public UniversityController (IUniversityService universityService) {
            _universityService = universityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get () {
            var universities = await _universityService.BrowseAllAsync ();
            return Json (universities);
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetDetails (Guid id) {
            var courses = await _universityService.GetDetailsAsync (id);
            return Json (courses);
        }
        [Authorize(Roles = "admin")]
        [HttpPost ("add_university")]
        public async Task<IActionResult> AddUniversity ([FromBody] AddUniversity command) {
            await _universityService.AddAsync (command.Name, command.Shortcut, command.Place, command.Base64Image);
            return Ok ();
        }
        [Authorize(Roles = "admin")]
        [HttpPut ("update_courses")]
        public async Task<IActionResult> UpdateCourses ([FromBody] UpdateCourses command) {
            await _universityService.UpdateCoursesAsync (command.UniversityId, command.Courses);
            return Ok ();
        }
        [Authorize(Roles = "admin")]
        [HttpPut ("update_university")]
        public async Task<IActionResult> UpdateUniversity ([FromBody] UpdateUniversity command) {
            await _universityService.UpdateUniversityAsync (command.Id, command.Name, command.Shortcut, command.Place, command.Base64Image);
            return Ok ();
        }
        [Authorize(Roles = "admin")]
        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteUniversity (Guid id)  {
            await _universityService.DeleteUniversityAsync (id);
            return Ok ();
        }

    }
}