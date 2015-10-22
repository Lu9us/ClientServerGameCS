using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
namespace TestServer
{
    class ConClient
    {
       public  int id{get;set;}
       public NetConnection con { get; set; }
       public Input.InputPacket LastInput { get; set; }
    }
}
