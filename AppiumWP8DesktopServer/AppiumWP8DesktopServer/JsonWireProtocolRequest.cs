using Newtonsoft.Json.Linq;
using System;

namespace AppiumWP8DesktopServer
{
    public class JsonWireProtocolRequest
    {
        public String Json { get; set; }

        private Lazy<dynamic> _lazyData;

        public dynamic Data { get { return _lazyData.Value; } }
        public JsonWireProtocolRequest()
        {
            _lazyData = new Lazy<dynamic>(() => { return JObject.Parse(Json); });
        }
    }
}
