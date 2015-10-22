using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
namespace Input
{
    [Serializable()]
  public  class MouseW
    {
        public MouseState ms;
       
     public void getState()
        {
          ms=  Mouse.GetState();
    
        }
    }
}
