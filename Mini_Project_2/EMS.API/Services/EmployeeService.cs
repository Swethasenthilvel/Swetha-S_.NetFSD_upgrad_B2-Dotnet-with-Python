using EMS.API.DTOs;
using EMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        // GET ALL + SEARCH + FILTER + SORT + PAGINATION
        public async Task<PagedResult<EmployeeResponseDto>> GetAllAsync(
            string? search,
            string? department,
            string? status,
            string sortBy,
            string sortDir,
            int page,
            int pageSize)
        {
            var query = _repo.Query();

            // SEARCH
            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.ToLower();
                query = query.Where(e =>
                    (e.FirstName + " " + e.LastName).ToLower().Contains(term) ||
                    e.Email.ToLower().Contains(term));
            }

            // FILTER
            if (!string.IsNullOrWhiteSpace(department))
                query = query.Where(e => e.Department == department);

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(e => e.Status == status);

            // SORT
            if (sortBy == "id")
            {
                query = sortDir == "desc"
                    ? query.OrderByDescending(e => e.Id)
                    : query.OrderBy(e => e.Id);
            }
            else if (sortBy == "salary")
            {
                query = sortDir == "desc"
                    ? query.OrderByDescending(e => e.Salary)
                    : query.OrderBy(e => e.Salary);
            }
            else if (sortBy == "joinDate")
            {
                query = sortDir == "desc"
                    ? query.OrderByDescending(e => e.JoinDate)
                    : query.OrderBy(e => e.JoinDate);
            }
            else // name
            {
                query = sortDir == "desc"
                    ? query.OrderByDescending(e => e.LastName).ThenByDescending(e => e.FirstName)
                    : query.OrderBy(e => e.LastName).ThenBy(e => e.FirstName);
            }

            var totalCount = await query.CountAsync();

            var data = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new EmployeeResponseDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    Phone = e.Phone,
                    Department = e.Department,
                    Designation = e.Designation,
                    Salary = e.Salary,
                    JoinDate = e.JoinDate,
                    Status = e.Status
                })
                .ToListAsync();

            return new PagedResult<EmployeeResponseDto>
            {
                Data = data,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                HasNextPage = page < (int)Math.Ceiling(totalCount / (double)pageSize),
                HasPrevPage = page > 1
            };
        }

        // GET BY ID
        public async Task<EmployeeResponseDto?> GetByIdAsync(int id)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) return null;

            return new EmployeeResponseDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Phone = e.Phone,
                Department = e.Department,
                Designation = e.Designation,
                Salary = e.Salary,
                JoinDate = e.JoinDate,
                Status = e.Status
            };
        }

        // CREATE
        public async Task<bool> CreateAsync(EmployeeRequestDto dto)
        {
            if (await _repo.EmailExistsAsync(dto.Email))
                return false;

            var emp = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Department = dto.Department,
                Designation = dto.Designation,
                Salary = dto.Salary,
                JoinDate = dto.JoinDate,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(emp);
            return true;
        }

        // UPDATE
        public async Task<bool> UpdateAsync(int id, EmployeeRequestDto dto)
        {
            var emp = await _repo.GetByIdAsync(id);
            if (emp == null) return false;

            if (await _repo.EmailExistsAsync(dto.Email, id))
                return false;

            emp.FirstName = dto.FirstName;
            emp.LastName = dto.LastName;
            emp.Email = dto.Email;
            emp.Phone = dto.Phone;
            emp.Department = dto.Department;
            emp.Designation = dto.Designation;
            emp.Salary = dto.Salary;
            emp.JoinDate = dto.JoinDate;
            emp.Status = dto.Status;
            emp.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(emp);
            return true;
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var emp = await _repo.GetByIdAsync(id);
            if (emp == null) return false;

            await _repo.DeleteAsync(emp);
            return true;
        }

        // DASHBOARD
        public async Task<DashboardSummaryDto> GetDashboardAsync()
        {
            var query = _repo.Query();

            var totalEmployees = await query.CountAsync();
            var activeCount = await query.CountAsync(e => e.Status == "Active");
            var inactiveCount = await query.CountAsync(e => e.Status == "Inactive");

            var totalDepartments = await query
                .Select(e => e.Department)
                .Distinct()
                .CountAsync();

            var departmentBreakdown = await query
                .GroupBy(e => e.Department)
                .Select(g => new DepartmentBreakdownDto
                {
                    Department = g.Key,
                    Count = g.Count(),
                    Percentage = totalEmployees == 0 ? 0 :
                        Math.Round((g.Count() * 100.0) / totalEmployees, 2)
                })
                .OrderBy(x => x.Department)
                .ToListAsync();

            var recentEmployees = await query
                .OrderByDescending(e => e.CreatedAt)
                .ThenByDescending(e => e.Id)
                .Take(5)
                .Select(e => new EmployeeResponseDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    Phone = e.Phone,
                    Department = e.Department,
                    Designation = e.Designation,
                    Salary = e.Salary,
                    JoinDate = e.JoinDate,
                    Status = e.Status
                })
                .ToListAsync();

            return new DashboardSummaryDto
            {
                TotalEmployees = totalEmployees,
                ActiveCount = activeCount,
                InactiveCount = inactiveCount,
                TotalDepartments = totalDepartments,
                DepartmentBreakdown = departmentBreakdown,
                RecentEmployees = recentEmployees
            };
        }
    }
}