
// SendTestPushNotification depends on an IPushNotificationService, so we will provide
// the google one. Maybe someone will need the apple one later

using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

// var tester = new SendTestPushNotification(new GooglePushNotificationService());
// tester.Send();


var services = new ServiceCollection();
services.AddPushNotifications();


var serviceProvider = services.BuildServiceProvider();

// beginning of http request
var httpRequestSCope = serviceProvider.CreateScope();
httpRequestSCope.ServiceProvider.GetRequiredService<SendTestPushNotification>();
httpRequestSCope.ServiceProvider.GetRequiredService<SendTestPushNotification>();

// end of http request
httpRequestSCope.Dispose();



// ----

public static class ServiceCollectionExtensions {
    public static void AddPushNotifications(this ServiceCollection services)
    {
        services.AddScoped<SendTestPushNotification>();
        services.AddScoped<IPushNotificationService, GooglePushNotificationService>();
    }
}

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
        Console.WriteLine("SEnding a push notification of \"{0}\" to google", notification);
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


public class SendTestPushNotification: IDisposable
{
    private readonly IPushNotificationService _pushNotificationService;

    public SendTestPushNotification(IPushNotificationService pushNotificationService)
    {
        Console.WriteLine("Creating SendTestPushNotification with a {0}", pushNotificationService.GetType().Name);
        _pushNotificationService = pushNotificationService;
    }

    public void Send()
    {
        _pushNotificationService.SendNotification("this is a test");
    }

    public void Dispose()
    {
        Console.WriteLine("cleaning up!");
    }
}
