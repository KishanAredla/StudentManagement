using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.DTOs;
using StudentApi.Exceptions;
using StudentApi.Mappings;
using StudentApi.Models;
using StudentApi.Services;

namespace StudentApi.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/students")]
    public class StudentsV2Controller : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsV2Controller(IStudentService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _service.GetByIdAsync(id);
            return Ok(new
            {
                id = student.Id,
                name = student.Name,
                email = student.Email,
                version = "v2"
            });
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> GetStudents(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] string? sortDir = "asc")
        {
            var result = await _service.GetPagedAsync(
                pageNumber, pageSize, search, sortBy, sortDir);

            return Ok(new
            {
                version = "v2",
                result
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> CreateStudent(CreateStudentDto dto)
        {
            var result = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetStudent),
                new { id = result.Id, version = "2.0" },
                result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> UpdateStudent(int id, UpdateStudentDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> PatchStudent(int id, StudentPatchDto dto)
        {
            await _service.PatchAsync(id, dto);
            return NoContent();
        }
    }


}
