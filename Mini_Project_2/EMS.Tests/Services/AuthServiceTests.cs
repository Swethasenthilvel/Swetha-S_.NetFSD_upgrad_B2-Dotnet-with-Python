using NUnit.Framework;
using Moq;
using EMS.API.Services;
using EMS.API.DTOs;
using EMS.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace EMS.Tests.Services
{
    [TestFixture]
    public class AuthServiceTests
    {
        private AppDbContext _context;
        private AuthService _service;
        private Mock<IConfiguration> _configMock;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);

            _configMock = new Mock<IConfiguration>();
            _configMock.Setup(c => c["Jwt:Key"]).Returns("TestSecretKey_32Chars_ForNUnit!!");
            _configMock.Setup(c => c["Jwt:Issuer"]).Returns("EMS.API");
            _configMock.Setup(c => c["Jwt:Audience"]).Returns("EMS.Client");
            _configMock.Setup(c => c["Jwt:ExpiryHours"]).Returns("8");

            _service = new AuthService(_context, _configMock.Object);
        }

        [TearDown]
        public void Cleanup()
        {
            _context.Dispose();
        }

        [Test]
        public async Task RegisterAsync_NewUser_ReturnsTrue()
        {
            var dto = new AuthRequestDto
            {
                Username = "testuser",
                Password = "test123",
                Role = "Admin"
            };

            var result = await _service.RegisterAsync(dto);

            Assert.That(result.Success, Is.True);
        }

        [Test]
        public async Task RegisterAsync_DuplicateUser_ReturnsFalse()
        {
            var dto = new AuthRequestDto
            {
                Username = "testuser",
                Password = "test123",
                Role = "Admin"
            };

            await _service.RegisterAsync(dto);
            var result = await _service.RegisterAsync(dto);

            Assert.That(result.Success, Is.False);
        }

        [Test]
        public async Task LoginAsync_ValidCredentials_ReturnsToken()
        {
            var registerDto = new AuthRequestDto
            {
                Username = "admin",
                Password = "admin123",
                Role = "Admin"
            };

            await _service.RegisterAsync(registerDto);

            var loginDto = new AuthRequestDto
            {
                Username = "admin",
                Password = "admin123"
            };

            var result = await _service.LoginAsync(loginDto);

            Assert.That(result.Success, Is.True);
            Assert.That(result.Token, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public async Task LoginAsync_InvalidPassword_ReturnsFalse()
        {
            var registerDto = new AuthRequestDto
            {
                Username = "admin",
                Password = "admin123",
                Role = "Admin"
            };

            await _service.RegisterAsync(registerDto);

            var loginDto = new AuthRequestDto
            {
                Username = "admin",
                Password = "wrong"
            };

            var result = await _service.LoginAsync(loginDto);

            Assert.That(result.Success, Is.False);
        }
    }
}