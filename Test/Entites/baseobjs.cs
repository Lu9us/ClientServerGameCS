using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Entites
{

    [Serializable]
    public class Transform
    {
    public Transform()
        {
            xy = new float[2];
        }
    public Transform(float x, float y)
        {
            xy = new float[2];
          
            xy[0] = x;
            xy[1] = y;
        }
      public  float [] xy { get; set; }
      public   float rot { get; set; }
    
    }

    public abstract class GameObj
    {
        public int id { get; set; }
        public string name { get; set; }
        public Transform tran { get; set; }
        public string Sprite { get; set; }
    abstract public void onUpdate(float dt);
    abstract public void onSpawn();
    abstract public void onDeath();

    
    
    }


    [Serializable]
    public class ProxyObj
    {
      public  int id { get; set; }
      public Transform tran { get; set; }
      public string spritetag { get; set; }

    
    }

    public abstract class Item
    {
        long id { get; set; }
        String name { get; set; }

        abstract public void onUse(GameObj obj);
        abstract public void onUse(ICollection<GameObj> objList);
    }
}
