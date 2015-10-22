using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items
{
    public interface Item
    {

       void use(float dt);
       void use(float dt,GObject obj);
       void use(float dt,ICollection<GObject> obj);
       void drop();


    }

    public interface Weapon :Item
    {
       void onHit(GObject obj);
       void onKill(GObject obj);
    
    
    
    }


}
