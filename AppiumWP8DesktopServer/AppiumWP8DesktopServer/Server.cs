using AppiumWP8DesktopServer.Endpoints;
using AppiumWP8DesktopServer.Endpoints.Session;
using AppiumWP8DesktopServer.Endpoints.Session.Id;
using AppiumWP8DesktopServer.Endpoints.Sessions;
using NServiceKit.ServiceHost;
using NServiceKit.ServiceInterface;
using NServiceKit.WebHost.Endpoints;
using System;

namespace AppiumWP8DesktopServer
{
    public class Server
    {
        public static Model.Model Model;

        public class AppiumDesktopService : Service
        {
            // GET /wd/hub/status
            public object Any(StatusGetRequest request)
            {
                return new StatusGetRequest.Response();
            }

            // GET /wd/hub/sessions
            public object Any(SessionsGetRequest request)
            {
                return new SessionsGetRequest.Response();
            }

            // POST /wd/hub/session
            public object Any(SessionPostRequest request)
            {
                return new SessionPostRequest.Response(request);
            }

            // POST /wd/hub/session/:sessionId
            public object Any(Session_IdGetRequest request)
            {
                return new Session_IdGetRequest.Response(request);
            }

        }

        public class AppHost : AppHostHttpListenerBase
        {
            public AppHost()
                : base("Appium for Windows Phone Desktop Server", typeof(AppiumDesktopService).Assembly) { }

            public override void Configure(Funq.Container container) { }
        }

        static void Main(string[] args)
        {
            Server.Model = new Model.Model();
            var listeningOn = args.Length == 0 ? "http://*:1337/" : args[0];
            var appHost = new AppHost();
            appHost.Init();
            appHost.Start(listeningOn);

            Console.WriteLine("Appium for Windows Phone Desktop Server Started at {0}, listening on {1}",
                DateTime.Now, listeningOn);

            Console.ReadLine();
        }
    }
}