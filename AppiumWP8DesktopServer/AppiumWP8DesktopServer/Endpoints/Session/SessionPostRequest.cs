using AppiumWP8DesktopServer.Model;
using NServiceKit.ServiceHost;
using System.Collections.Generic;

namespace AppiumWP8DesktopServer.Endpoints.Session
{
    [Route("/wd/hub/session", "POST")]
    public class SessionPostRequest : JsonWireProtocolRequest
    {
        public class Response : JsonWireProtocolResponse
        {
            public Model.Session Session { get; set; }
            public Response(SessionPostRequest request)
            {
                System.Console.WriteLine(request.Json);
                Session = Server.Model.Sessions.CreateNewSession();
            }
        }
    }
}
