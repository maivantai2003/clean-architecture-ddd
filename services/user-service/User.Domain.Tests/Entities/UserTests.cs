using FluentAssertions;
using User.Domain.Entities;
using User.Domain.Enums;
using User.Domain.Users.Events;

namespace User.Domain.Tests.Entities
{
    public class UserTests
    {
        [Fact]
        public void CreateUser_Should_Set_Default_State()
        {
            // Act
            var userId = Guid.NewGuid();
            // Arrange
            var user = new User.Domain.Entities.User(
                userId,
                "Van Tai",
                "vantai08122003@gmail.com",
                "Van Tai"
            );
            //Assert
            user.UserId.Should().Be(userId);
            user.Username.Should().Be("Van Tai");
            user.FullName.Should().Be("Van Tai");
            user.Email.Should().Be("vantai08122003@gmail.com");
            user.Status.Should().Be(UserStatus.Active);
            user.DomainEvents.Should().BeEmpty();
        }
        [Fact]
        public void UpdateProfile_Should_Update_User_Properties()
        {
            // Arrange
            var user = Helpers.CreateUser();
            // Act
            user.UpdateEmail("newemail@gmai.com");
            // Assert
            user.Email.Should().Be("newemail@gmail.com");
            user.DomainEvents.Should().HaveCount(1);
            user.DomainEvents.First().Should().BeOfType<UserEmailUpdatedDomainEvent>();
            var domainEvent = (UserEmailUpdatedDomainEvent)user.DomainEvents.First();
            domainEvent.UserId.Should().Be(user.UserId);
            domainEvent.NewEmail.Should().Be("newemail@gmail.com");
        }
    }
}
