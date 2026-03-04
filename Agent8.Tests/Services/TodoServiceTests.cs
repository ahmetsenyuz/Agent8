using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agent8.Models;
using Agent8.Services;
using Moq;
using Xunit;

namespace Agent8.Tests.Services
{
    public class TodoServiceTests
    {
        private readonly Mock<ITodoService> _mockTodoService;
        private readonly TodoService _todoService;

        public TodoServiceTests()
        {
            _mockTodoService = new Mock<ITodoService>();
            _todoService = new TodoService();
        }

        [Fact]
        public async Task GetAllTodosAsync_ReturnsAllTodos()
        {
            // Arrange
            var todos = new List<TodoItem>
            {
                new TodoItem { Id = 1, Title = "Test Todo 1" },
                new TodoItem { Id = 2, Title = "Test Todo 2" }
            };

            _mockTodoService.Setup(service => service.GetAllTodosAsync())
                .ReturnsAsync(todos);

            // Act
            var result = await _mockTodoService.Object.GetAllTodosAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetTodoByIdAsync_WithValidId_ReturnsTodo()
        {
            // Arrange
            var todo = new TodoItem { Id = 1, Title = "Test Todo" };
            
            _mockTodoService.Setup(service => service.GetTodoByIdAsync(1))
                .ReturnsAsync(todo);

            // Act
            var result = await _mockTodoService.Object.GetTodoByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Todo", result.Title);
        }

        [Fact]
        public async Task GetTodoByIdAsync_WithInvalidId_ReturnsNull()
        {
            // Arrange
            _mockTodoService.Setup(service => service.GetTodoByIdAsync(999))
                .ReturnsAsync((TodoItem)null);

            // Act
            var result = await _mockTodoService.Object.GetTodoByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateTodoAsync_WithValidTodo_CreatesTodo()
        {
            // Arrange
            var todo = new TodoItem { Title = "New Todo" };
            var createdTodo = new TodoItem { Id = 1, Title = "New Todo" };

            _mockTodoService.Setup(service => service.CreateTodoAsync(todo))
                .ReturnsAsync(createdTodo);

            // Act
            var result = await _mockTodoService.Object.CreateTodoAsync(todo);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task CreateTodoAsync_WithNullTodo_ThrowsArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => 
                _mockTodoService.Object.CreateTodoAsync(null));
        }

        [Fact]
        public async Task UpdateTodoAsync_WithValidTodo_UpdatesTodo()
        {
            // Arrange
            var todo = new TodoItem { Id = 1, Title = "Updated Todo" };
            var updatedTodo = new TodoItem { Id = 1, Title = "Updated Todo" };

            _mockTodoService.Setup(service => service.UpdateTodoAsync(todo))
                .ReturnsAsync(updatedTodo);

            // Act
            var result = await _mockTodoService.Object.UpdateTodoAsync(todo);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Todo", result.Title);
        }

        [Fact]
        public async Task UpdateTodoAsync_WithNullTodo_ThrowsArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => 
                _mockTodoService.Object.UpdateTodoAsync(null));
        }

        [Fact]
        public async Task UpdateTodoAsync_WithNonExistentTodo_ReturnsNull()
        {
            // Arrange
            var todo = new TodoItem { Id = 999, Title = "Non-existent Todo" };

            _mockTodoService.Setup(service => service.UpdateTodoAsync(todo))
                .ReturnsAsync((TodoItem)null);

            // Act
            var result = await _mockTodoService.Object.UpdateTodoAsync(todo);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteTodoAsync_WithValidId_DeletesTodo()
        {
            // Arrange
            _mockTodoService.Setup(service => service.DeleteTodoAsync(1))
                .ReturnsAsync(true);

            // Act
            var result = await _mockTodoService.Object.DeleteTodoAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteTodoAsync_WithInvalidId_ReturnsFalse()
        {
            // Arrange
            _mockTodoService.Setup(service => service.DeleteTodoAsync(999))
                .ReturnsAsync(false);

            // Act
            var result = await _mockTodoService.Object.DeleteTodoAsync(999);

            // Assert
            Assert.False(result);
        }
    }
}