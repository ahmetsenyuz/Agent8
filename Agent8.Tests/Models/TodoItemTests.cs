using System.ComponentModel.DataAnnotations;
using Agent8.Models;
using Xunit;

namespace Agent8.Tests.Models
{
    public class TodoItemTests
    {
        [Fact]
        public void TodoItem_TitleIsRequired()
        {
            // Arrange
            var todo = new TodoItem();

            // Act
            var validationContext = new ValidationContext(todo);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(todo, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("The Title field is required."));
        }

        [Fact]
        public void TodoItem_TitleMaxLength()
        {
            // Arrange
            var todo = new TodoItem { Title = new string('A', 201) }; // 201 characters

            // Act
            var validationContext = new ValidationContext(todo);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(todo, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("The field Title must be a string with a maximum length of 200."));
        }

        [Fact]
        public void TodoItem_DescriptionMaxLength()
        {
            // Arrange
            var todo = new TodoItem { Description = new string('B', 1001) }; // 1001 characters

            // Act
            var validationContext = new ValidationContext(todo);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(todo, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("The field Description must be a string with a maximum length of 1000."));
        }

        [Fact]
        public void TodoItem_ValidTodoItem_IsValid()
        {
            // Arrange
            var todo = new TodoItem 
            { 
                Title = "Valid Title",
                Description = "Valid Description"
            };

            // Act
            var validationContext = new ValidationContext(todo);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(todo, validationContext, validationResults, true);

            // Assert
            Assert.True(isValid);
        }
    }
}