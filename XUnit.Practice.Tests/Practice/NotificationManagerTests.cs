using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnit.Practice.Tests.Practice
{
    public class NotificationManagerTests
    {
        [Fact]
        public void Notify_CalledOnce_ValidInput()
        {
            //Arrange
            var emailServiceMock = new Mock<IEmailService>();

            emailServiceMock
                .Setup(s => s.SendEmail("1@mail.com", "Hi"))
                .Returns(true);

            var manager = new NotificationManager(emailServiceMock.Object);

            //Act 
            var result = manager.Notify("1@mail.com", "Hi");

            Assert.True(result);
            emailServiceMock.Verify(
                s => s.SendEmail("1@mail.com", "Hi"),
                Times.Once);


        }
    }
}
