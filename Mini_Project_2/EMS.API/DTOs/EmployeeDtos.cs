using System;
using System.Collections.Generic;

namespace EMS.API.DTOs
{
    //  CREATE / UPDATE EMPLOYEE
    public class EmployeeRequestDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    //  EMPLOYEE RESPONSE
    public class EmployeeResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    //  PAGINATION RESULT
    public class PagedResult<T>
    {
        public List<T> Data { get; set; } = new();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPrevPage { get; set; }
    }

    //  DASHBOARD - DEPARTMENT BREAKDOWN
    public class DepartmentBreakdownDto
    {
        public string Department { get; set; } = string.Empty;
        public int Count { get; set; }
        public double Percentage { get; set; }
    }

    //  DASHBOARD SUMMARY
    public class DashboardSummaryDto
    {
        public int TotalEmployees { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int TotalDepartments { get; set; }
        public List<DepartmentBreakdownDto> DepartmentBreakdown { get; set; } = new();
        public List<EmployeeResponseDto> RecentEmployees { get; set; } = new();
    }

    //  AUTH REQUEST (LOGIN / REGISTER)
    public class AuthRequestDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "Viewer"; // default
    }

    //  AUTH RESPONSE
    public class AuthResponseDto
    {
        public bool Success { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}