using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Items
{
   public interface GObject
    {
       void onTakeDamage(Item i);
       void onTakeDamage(); 
       void onUpdate(float dt);
       void onKill();
    }
   public class ProxyObj
   {
       void onDraw();
      public string sprite{get;set;}
      public Transform transfrom {get; set;}
   
   }

   public class Transform
   {
     public   Vector2 xy { get; set; }
     public float rot { get; set; }   
   }
}
