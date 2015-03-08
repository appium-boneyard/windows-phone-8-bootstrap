using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppiumWP8DesktopServer
{
    public class JsonWireProtocolResponse
    {
        public int status { get; set; }

        public JsonWireProtocolResponse()
        {
            status = 0;
        }
    }
}
