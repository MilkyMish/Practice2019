using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Entities
{
    public class Kolobok : Entity
    {
       public string[] Sprite = new string[7] { @"..\..\..\img\Green_tank.png", @"..\..\..\img\Green_tank1.png",
           @"..\..\..\img\Green_tank2.png", @"..\..\..\img\Green_tank3.png", @"..\..\..\img\Green_tank5.png",
           @"..\..\..\img\Green_tank6.png", @"..\..\..\img\Green_tank7.png" };
       public int Speed = 10;
       public int CurrentPic = 0;
       public void Move()
        {
            switch (Direction)
            {
                case RotateFlipType.RotateNoneFlipNone:
                    this.posY -= Speed;
                    break;
                case RotateFlipType.Rotate90FlipNone:
                    this.posX += Speed;
                    break;
                case RotateFlipType.Rotate180FlipNone:
                    this.posY += Speed;
                    break;
                case RotateFlipType.Rotate270FlipNone:
                    this.posX -= Speed;
                    break;
                default:
                    break;
            }
        }
        //public int[] SpriteSize = new int[2] { 75, 83 };
    }
}
