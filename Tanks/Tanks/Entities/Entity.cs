using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Entities
{
   public class Entity
    {
        public int posX, posY;
        public  int[] SpriteSize = new int[2] { 75, 83 };
        public RotateFlipType Direction = RotateFlipType.RotateNoneFlipNone;
    }
}
