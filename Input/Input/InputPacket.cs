using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
namespace Input
{
    [Serializable()]
  public  class InputPacket
    {
        public Keys[] keys;
        public int [] pos= new int[2];
        public ButtonState r;
        public ButtonState l;
    public    InputPacket()
        {
            MouseW mw = new MouseW();
            mw.getState();
            pos[0] = mw.ms.X;
            pos[1] = mw.ms.Y;
            l = mw.ms.LeftButton;
            r = mw.ms.RightButton;
            KeyBoardW kw = new KeyBoardW();
            kw.getState();
            keys = kw.kb.GetPressedKeys();
            
        }

    
    }
}
