using NUnit.Framework;
using Moq;
using EMS.API.Services;
using EMS.API.Models;
using EMS.API.DTOs;
using System;
using System.Threading.Tasks;

namespace EMS.Tests.Services
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        private Mock<IEmployeeRepository> _repoMock;
        private EmployeeService _service;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IEmployeeRepository>();
            _service = new EmployeeService(_repoMock.Object);
        }

        [Test]
        public async Task GetByIdAsync_ValidId_ReturnsMappedDto()
        {
            var fakeEmployee = new Employee
            {
                Id = 1,
                FirstName = "Karthika",
                LastName = "Shanmugam",
                Email = "karthi@gmail.com",
                Phone = "9779543519",
                Department = "HR",
                Designation = "HR Executive",
                Salary = 80000,
                JoinDate = DateTime.UtcNow,
                Status = "Active",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _repoMock.Setup(r => r.GetByIdAsync(1))
                     .ReturnsAsync(fakeEmployee);

            var result = await _service.GetByIdAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.FirstName, Is.EqualTo("Karthika"));
            _repoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        [Test]
        public async Task GetByIdAsync_NonExistentId_ReturnsNull()
        {
            _repoMock.Setup(r => r.GetByIdAsync(9999))
                     .ReturnsAsync((Employee?)null);

            var result = await _service.GetByIdAsync(9999);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task CreateAsync_ValidEmployee_CallsAddAsync()
        {
            var dto = new EmployeeRequestDto
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test@test.com",
                Phone = "9876543210",
                Department = "Engineering",
                Designation = "Developer",
                Salary = 50000,
                JoinDate = DateTime.UtcNow,
                Status = "Active"
            };

            _repoMock.Setup(r => r.EmailExistsAsync(dto.Email, null))
                     .ReturnsAsync(false);

            var result = await _service.CreateAsync(dto);

            Assert.That(result, Is.True);
            _repoMock.Verify(r => r.AddAsync(It.IsAny<Employee>()), Times.Once);
        }

        [Test]
        public async Task CreateAsync_DuplicateEmail_ReturnsFalse()
        {
            var dto = new EmployeeRequestDto
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test@test.com",
                Phone = "9876543210",
                Department = "Engineering",
                Designation = "Developer",
                Salary = 50000,
                JoinDate = DateTime.UtcNow,
                Status = "Active"
            };

            _repoMock.Setup(r => r.EmailExistsAsync(dto.Email, null))
                     .ReturnsAsync(true);

            var result = await _service.CreateAsync(dto);

            Assert.That(result, Is.False);
            _repoMock.Verify(r => r.AddAsync(It.IsAny<Employee>()), Times.Never);
        }
    }
}