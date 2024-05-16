using CustomersTask.Infrastructure.Contracts;
using CustomersTask.Infrastructure.Models;
using Customers.Api.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CustomersTask.Test.Api
{
    public class CustomersControllerTests
    {
        private Mock<ICustomerRepository> _mockRepo;
        private CustomersController _controller;
        private Guid _testId;
        private Customer _testCustomer;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<ICustomerRepository>();
            _controller = new CustomersController(_mockRepo.Object);
            _testId = Guid.NewGuid();
            _testCustomer = new Customer { Id = _testId, Name = "Test Customer", Email= "Test@test" };
        }

        [Test]
        public async Task GetCustomers_ReturnsOkResult_WithListOfCustomers()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetCustomersAsync()).ReturnsAsync(new List<Customer>());

            //Act
            var result = await _controller.GetCustomers();

            //Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetCustomerAsync(_testId)).ReturnsAsync((Customer)null);

            //Act
            var result = await _controller.GetCustomer(_testId);

            //Assert
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task GetCustomer_ReturnsOkResult_WithCustomer()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetCustomerAsync(_testId)).ReturnsAsync(_testCustomer);

            //Act
            var result = await _controller.GetCustomer(_testId);

            //Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());


        }

        [Test]
        public async Task AddCustomer_ReturnsBadRequest_WhenCustomerIsNull()
        {
            //Arrange
            Customer nullCustomer = null;

            //Act
            var result = await _controller.AddCustomer(nullCustomer);

            //Assert
            Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task AddCustomer_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            //Arrange
            _controller.ModelState.AddModelError("error", "error message");

            //Act
            var result = await _controller.AddCustomer(_testCustomer);

            //Assert
            Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task AddCustomer_ReturnsCreatedAtAction_WhenCustomerIsValid()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.AddCustomerAsync(_testCustomer)).Returns(Task.CompletedTask);

            //Act
            var result = await _controller.AddCustomer(_testCustomer);

            //Assert
            Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            var createdAtResult = result.Result as CreatedAtActionResult;
            Assert.That(createdAtResult.ActionName, Is.EqualTo(nameof(_controller.GetCustomer)));
            Assert.That(createdAtResult.RouteValues["id"], Is.EqualTo(_testCustomer.Id));
            Assert.That(createdAtResult.Value, Is.EqualTo(_testCustomer));
        }



        [Test]
        public async Task UpdateCustomer_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            //Arrange
            _controller.ModelState.AddModelError("error", "error message");

            //Act
            var result = await _controller.UpdateCustomer(_testId, _testCustomer);

            //Assert
            Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task UpdateCustomer_ReturnsOkResult_WhenCustomerIsValid()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCustomerAsync(_testCustomer.Id)).ReturnsAsync(_testCustomer);
            _mockRepo.Setup(repo => repo.UpdateCustomerAsync(_testCustomer)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateCustomer(_testCustomer.Id, _testCustomer);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(_testCustomer));
        }

        [Test]
        public async Task UpdateCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetCustomerAsync(_testId)).ReturnsAsync((Customer)null);

            //Act
            var result = await _controller.UpdateCustomer(_testId, _testCustomer);

            //  Assert
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task DeleteCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetCustomerAsync(_testId)).ReturnsAsync((Customer)null);

            //Act
            var result = await _controller.DeleteCustomer(_testId);

            //Assert
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task DeleteCustomer_ReturnsOkResult_WhenCustomerExists()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCustomerAsync(_testCustomer.Id)).ReturnsAsync(_testCustomer);
            _mockRepo.Setup(repo => repo.DeleteCustomerAsync(_testCustomer.Id)).Returns(Task.CompletedTask);

            //Act
            var result = await _controller.DeleteCustomer(_testCustomer.Id);

            //Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(_testCustomer));
        }

    }
}

