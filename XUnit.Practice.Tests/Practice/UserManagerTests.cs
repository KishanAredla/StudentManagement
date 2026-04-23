using Moq;

namespace XUnit.Practice.Tests.Practice
{
    public class UserManagerTests
    {
        [Fact]
        public void GetDisplayName_ReturnsFormattedUserName()
        {
            //Arrange 
            var userServiceMock = new Mock<IUserService>();

            userServiceMock
                .Setup(s => s.GetUserName(1))
                .Returns("Kishan");

            var manager = new UserManager(userServiceMock.Object);

            //Act
            var result = manager.GetDisplayName(1);

            //Assert
            Assert.Equal("User: Kishan", result);
            userServiceMock.Verify(
                s => s.GetUserName(1),
                Times.Once);
        }
    }
}
