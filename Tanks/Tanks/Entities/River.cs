using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class River: Entity
    {
        public string Sprite = @"..\..\..\img\river.jpg";
        new public int[] SpriteSize = new int[2] { 75, 60 };
    }
}
