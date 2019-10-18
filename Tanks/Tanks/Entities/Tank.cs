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
        public int Speed = 8;
        public string Sprite = @"..\..\..\img\Blue_tank.png";

        private Random random = new Random();
        private DirectionMove goTo = DirectionMove.UP;
        int moveCount = 0;
        public bool fire = false;
        //int LastX;
        //int LastY;

        public Tank (int[] position)
        {
            posX = position[0];
            posY = position[1];
            //LastX = posX;
            //LastY = posY;
        }

        public Tank()
        {
            
        }

        public void Move()
        {
            int check = random.Next(10);
            if (check > 3)
            {
                switch (goTo)
                {
                    case DirectionMove.UP:
                        this.posY -= this.Speed;
                        this.Direction = RotateFlipType.RotateNoneFlipNone;
                        break;
                    case DirectionMove.DOWN:
                        this.posY += this.Speed;
                        this.Direction = RotateFlipType.Rotate180FlipNone;
                        break;
                    case DirectionMove.LEFT:
                        this.posX -= this.Speed;
                        this.Direction = RotateFlipType.Rotate270FlipNone;
                        break;
                    case DirectionMove.RIGHT:
                        this.posX += this.Speed;
                        this.Direction = RotateFlipType.Rotate90FlipNone;
                        break;
                    default:
                        break;
                }
                moveCount++;
            }
            else
            {
                if (moveCount>10)
                {
                    moveCount = 0;
                    switch (check)
                    {
                        case 0:
                            goTo = DirectionMove.UP;
                            break;
                        case 1:
                            goTo = DirectionMove.DOWN;
                            break;
                        case 2:
                            goTo = DirectionMove.LEFT;
                            break;
                        case 3:
                            goTo = DirectionMove.RIGHT;
                            break;
                        default:
                            break;
                    }
                }
               
            }
        }
    }
}
