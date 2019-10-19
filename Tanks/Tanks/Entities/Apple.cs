using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Apple : Entity
    {
        public string Sprite = @"..\..\..\img\Apple.png";
        new public int[] SpriteSize = new int[2] { 40, 40 };

        public Apple(int posX, int posY)
            {
            this.posX = posX;
            this.posY = posY;
        }

    }
}
