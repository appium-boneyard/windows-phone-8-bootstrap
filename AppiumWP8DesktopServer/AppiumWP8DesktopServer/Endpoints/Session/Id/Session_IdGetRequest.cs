using NServiceKit.ServiceHost;

namespace AppiumWP8DesktopServer.Endpoints.Session.Id
{
    [Route("/wd/hub/session/{SessionId}")]
    public class Session_IdGetRequest : JsonWireProtocolRequest
    {
        public string SessionId { get; set; }

        public class Response : JsonWireProtocolResponse
        {
            public Model.Session Value { get; set; }
            public Response(Session_IdGetRequest request)
            {
                System.Console.WriteLine(request.Json);
                Value = Server.Model.Sessions.GetSessionById(request.SessionId);
            }
        }
    }
}
