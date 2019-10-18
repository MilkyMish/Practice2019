using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class LogView
    {
        string Name;
        int X;
        int Y;
        

        public LogView(Tank tank)
        {
            Name = "Tank ";
            X = tank.posX;
            Y = tank.posY;
        }
        public LogView(Kolobok kolobok)
        {
            Name = "Kolobok ";
            X = kolobok.posX;
            Y = kolobok.posY;
        }
    }
}
