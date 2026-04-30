using EMS.API.Models;
using System.Linq;

namespace EMS.API.Services
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> Query();
        Task<Employee?> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
        Task<bool> EmailExistsAsync(string email, int? excludeId = null);
    }
}