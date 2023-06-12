
// SendTestPushNotification depends on an IPushNotificationService, so we will provide
// the google one. Maybe someone will need the apple one later
var tester = new SendTestPushNotification(new GooglePushNotificationService());
tester.Send();


// ----

public interface IPushNotificationService
{
    void SendNotification(string notification);
    bool IsConnected();
}

public class GooglePushNotificationService : IPushNotificationService
{
    public void SendNotification(string notification)
    {
        // talk to push.google.com
        throw new NotImplementedException();
    }

    public bool IsConnected()
    {
        throw new NotImplementedException();
    }
}

class ApplePushNotificationService : IPushNotificationService
{
    public void SendNotification(string notification)
    {
        // talk to push.apple.com
        throw new NotImplementedException();
    }

    public bool IsConnected()
    {
        throw new NotImplementedException();
    }
}


public class SendTestPushNotification
{
    private readonly IPushNotificationService _pushNotificationService;

    public SendTestPushNotification(IPushNotificationService pushNotificationService)
    {
        _pushNotificationService = pushNotificationService;
    }

    public void Send()
    {
        _pushNotificationService.SendNotification("this is a test");
    }
    
}
