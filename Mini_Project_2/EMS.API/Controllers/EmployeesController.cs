using EMS.API.DTOs;
using EMS.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [ApiController]
    [Route("api/employees")]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeesController(EmployeeService service)
        {
            _service = service;
        }

        //  GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll(
            string? search,
            string? department,
            string? status,
            string sortBy = "name",
            string sortDir = "asc",
            int page = 1,
            int pageSize = 10)
        {
            var result = await _service.GetAllAsync(
                search, department, status, sortBy, sortDir, page, pageSize);

            return Ok(result);
        }

        //  GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var emp = await _service.GetByIdAsync(id);

            if (emp == null)
                return NotFound();

            return Ok(emp);
        }

        //  DASHBOARD
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var result = await _service.GetDashboardAsync();
            return Ok(result);
        }

        //  CREATE
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(EmployeeRequestDto dto)
        {
            var success = await _service.CreateAsync(dto);

            if (!success)
                return Conflict(new { message = "Email already exists" });

            return Ok(new { message = "Employee created" });
        }

        //  UPDATE
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, EmployeeRequestDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);

            if (!success)
                return NotFound(new { message = "Employee not found or email exists" });

            return Ok(new { message = "Employee updated" });
        }

        //  DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);

            if (!success)
                return NotFound(new { message = "Employee not found" });

            return Ok(new { message = "Employee deleted" });
        }
    }
}