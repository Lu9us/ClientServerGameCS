using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;


namespace Input
{ [Serializable]
    public class KeyBoardW
    {
        public KeyboardState kb;
       

        public Keys[] getState()
        { 
         kb = Keyboard.GetState();
         Keys [] ka = kb.GetPressedKeys();
         return ka;
        }
       
    }
}
