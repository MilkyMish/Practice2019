using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class LogView
    {
        public string Name;
        public int X;
        public int Y;
        

        public LogView(Tank tank)
        {
            Name = "Tank ";
            X = tank.posX;
            Y = tank.posY;
        }
        public LogView(Apple apple)
        {
            Name = "Apple ";
            X = apple.posX;
            Y = apple.posY;
        }
        public LogView(Kolobok kolobok)
        {
            Name = "Kolobok ";
            X = kolobok.posX;
            Y = kolobok.posY;
        }
    }
}
