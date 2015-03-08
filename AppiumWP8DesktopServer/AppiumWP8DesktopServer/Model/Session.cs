using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppiumWP8DesktopServer.Model
{
    public class Session
    {
        public string ID { get; set; }

        public Session()
        {
            ID = Guid.NewGuid().ToString();
        }
    }
}
