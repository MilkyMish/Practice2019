using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Kolobok : Entity
    {
       public string[] Sprite = new string[7] { @"..\..\..\img\Green_tank.png", @"..\..\..\img\Green_tank1.png",
           @"..\..\..\img\Green_tank2.png", @"..\..\..\img\Green_tank3.png", @"..\..\..\img\Green_tank5.png",
           @"..\..\..\img\Green_tank6.png", @"..\..\..\img\Green_tank7.png" };
       public int Speed = 10;
       public int CurrentPic = 0;
       //public int[] SpriteSize = new int[2] { 75, 83 };
    }
}
