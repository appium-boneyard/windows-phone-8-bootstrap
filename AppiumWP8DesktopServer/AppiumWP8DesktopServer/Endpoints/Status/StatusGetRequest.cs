using NServiceKit.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppiumWP8DesktopServer.Endpoints
{
    [Route("/wd/hub/status", "GET")]
    public class StatusGetRequest
    {
        public class Value
        {
            public BuildInfo Build { get; set; }
            public bool IsShuttingDown { get; set; }

            public Value()
            {
                Build = new BuildInfo();
            }
        }

        public class BuildInfo
        {
            public string Version { get; set; }
            public string Revision { get; set; }
        }

        public class Response : JsonWireProtocolResponse
        {
            public Value Value { get; set; }

            public Response()
            {
                Value.Build.Version = "1.0.0";
                Value.Build.Revision = "";
                Value.IsShuttingDown = false;
            }
        }
    }
}
