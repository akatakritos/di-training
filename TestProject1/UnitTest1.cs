using Moq;

namespace TestProject1;

public class UnitTest1
{
    public class FakeNotificationService : IPushNotificationService
    {
        public string LastNotification { get; private set; }
        public void SendNotification(string notification)
        {
            LastNotification = notification;
        }

        public bool IsConnected()
        {
            throw new NotImplementedException();
        }
    }
    
    [Fact]
    public void TheTestClassSendsThisIsATestMessage_UsingHandwrittenMockService()
    {
        // arrange
        var fake = new FakeNotificationService();
        var tester = new SendTestPushNotification(fake);
        
        // act
        tester.Send();
        
        // assert
        Assert.Equal("this is a test", fake.LastNotification);

    }
    
    [Fact]
    public void TheTestClassSendsThisIsATestMessage_UsingMoqLibrary()
    {
        // arrange
        var fake = new Mock<IPushNotificationService>();
        var tester = new SendTestPushNotification(fake.Object);
        
        // act
        tester.Send();
        
        // assert
        fake.Verify(m => m.SendNotification("this is a test"), Times.Once);

    }
    
    // you can also configure Moq to return certain values
    [Fact]
    public void ItCanBeFaked()
    {
        // arrange
        var fake = new Mock<IPushNotificationService>();

        fake.Setup(m => m.IsConnected()).Returns(false);
        
        // act
        var isConnected = fake.Object.IsConnected();
        
        // assert
        Assert.True(isConnected);

    }
}
