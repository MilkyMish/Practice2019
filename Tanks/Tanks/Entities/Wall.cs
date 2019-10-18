using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Wall:Entity
    {
        public string Sprite = @"..\..\..\img\Wall.png";
        new public int[] SpriteSize = new int[2] { 75, 46 };
        public int id; //для отладки

        public Wall()
        {
         
        }
        public Wall(int X,int Y)
        {
            posX = X;
            posY = Y;
        }
    }
}
