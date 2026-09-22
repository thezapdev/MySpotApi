using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace MySpotTests.Framework;

public class ServiceCollectionTests
{
     [Fact]
     public void test()
     {
          var serviceCollection = new ServiceCollection();
          serviceCollection.AddScoped<IMessenger,Messenger>();
          
          var serviceProvider = serviceCollection.BuildServiceProvider();
          using(var scope = serviceProvider.CreateScope())
          {
               var messenger = serviceProvider.GetRequiredService<IMessenger>();
               messenger.Send();
               var messenger2 = serviceProvider.GetRequiredService<IMessenger>();
               messenger2.Send();
          
               messenger.ShouldNotBeNull();
               messenger2.ShouldNotBeNull();
               messenger.ShouldBe(messenger2);
          }
          
          
          
     }
     private interface IMessenger
     {
          void Send();
     }
     private class Messenger : IMessenger
     {
          private readonly Guid _id = Guid.NewGuid();
          public void Send() => Console.WriteLine($"[{_id}] {nameof(Messenger)}: Sending message...");
     }
}