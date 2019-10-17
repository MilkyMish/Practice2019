using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Entities
{
    public class Tank:Entity
    {
        public int Speed = 10;
        public RotateFlipType TankDirection = RotateFlipType.RotateNoneFlipNone;
        public string Sprite = @"..\..\..\img\Blue_tank.png";

        public Tank (int[] position)
        {
            posX = position[0];
            posY = position[1];
        }

        public Tank()
        {
            
        }
    }
}
