using NUnit.Framework;
using Moq;
using EMS.API.Controllers;
using EMS.API.Services;
using EMS.API.DTOs;
using EMS.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EMS.Tests.Controllers
{
    [TestFixture]
    public class EmployeesControllerTests
    {
        private Mock<IEmployeeRepository> _repoMock;
        private EmployeeService _service;
        private EmployeesController _controller;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IEmployeeRepository>();
            _service = new EmployeeService(_repoMock.Object);
            _controller = new EmployeesController(_service);
        }

        [Test]
        public async Task GetById_ValidId_ReturnsOk()
        {
            var employee = new Employee
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                Email = "test@test.com",
                Phone = "9876543210",
                Department = "Engineering",
                Designation = "Developer",
                Salary = 50000,
                JoinDate = DateTime.UtcNow,
                Status = "Active",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(employee);

            var result = await _controller.GetById(1);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetById_InvalidId_ReturnsNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Employee?)null);

            var result = await _controller.GetById(999);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Create_Success_ReturnsOk()
        {
            _repoMock.Setup(r => r.EmailExistsAsync(It.IsAny<string>(), null))
                     .ReturnsAsync(false);

            var dto = new EmployeeRequestDto
            {
                FirstName = "Test",
                LastName = "User",
                Email = "new@test.com",
                Phone = "9876543210",
                Department = "Engineering",
                Designation = "Developer",
                Salary = 50000,
                JoinDate = DateTime.UtcNow,
                Status = "Active"
            };

            var result = await _controller.Create(dto);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            _repoMock.Verify(r => r.AddAsync(It.IsAny<Employee>()), Times.Once);
        }

        [Test]
        public async Task Create_Duplicate_ReturnsConflict()
        {
            _repoMock.Setup(r => r.EmailExistsAsync(It.IsAny<string>(), null))
                     .ReturnsAsync(true);

            var dto = new EmployeeRequestDto
            {
                FirstName = "Test",
                LastName = "User",
                Email = "dup@test.com",
                Phone = "9876543210",
                Department = "Engineering",
                Designation = "Developer",
                Salary = 50000,
                JoinDate = DateTime.UtcNow,
                Status = "Active"
            };

            var result = await _controller.Create(dto);

            Assert.That(result, Is.InstanceOf<ConflictObjectResult>());
            _repoMock.Verify(r => r.AddAsync(It.IsAny<Employee>()), Times.Never);
        }
    }
}