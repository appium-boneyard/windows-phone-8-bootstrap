using AppiumWP8DesktopServer.Model;
using NServiceKit.ServiceHost;
using System.Collections.Generic;

namespace AppiumWP8DesktopServer.Endpoints.Sessions
{
    [Route("/wd/hub/sessions", "GET")]
    public class SessionsGetRequest
    {
        public class Response : JsonWireProtocolResponse
        {
            public Model.Session[] Value { get; set; }
            public Response()
            {
                Value = Server.Model.Sessions.ToArray();
            }
        }
    }
}
