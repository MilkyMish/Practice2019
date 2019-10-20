using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Explosion:Entity
    {
        public string[] Sprites = new string[3] { @"..\..\..\img\exp1.png", @"..\..\..\img\exp2.png", @"..\..\..\img\exp2.png" };
        new public int[] SpriteSize = new int[2] { 40, 40 };
        public int curentpic = 0;
    }
}
