using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entites
{
   public  class DataPacket
    {
       public List<ProxyObj> ObjList { get; set; }
        public DataPacket()
        {
            ObjList = new List<ProxyObj>();
        
        }

        public void addGameObj(GameObj obj)
        {
            ProxyObj proxy = new ProxyObj();
            proxy.id = obj.id;
            proxy.spritetag = obj.Sprite;
            proxy.tran = obj.tran;
            ObjList.Add(proxy);
        }

    }
}
