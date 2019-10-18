using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Entities
{
    public class Bullet:Entity
    {
        public string Sprite = @"..\..\..\img\bullet.png";
        public int Speed = 13;
        new public int[] SpriteSize = new int[2] { 24, 63 };
        public bool enemyFire = false;



        public Bullet(Entity entity, bool enemyFire)
        {
            this.enemyFire = enemyFire;
            this.Direction = entity.Direction;
            switch (Direction)
            {
                case RotateFlipType.RotateNoneFlipNone:
                    this.posX = entity.posX + (entity.SpriteSize[0] / 2) - 10;
                    this.posY = entity.posY - SpriteSize[1];
                    break;
                case RotateFlipType.Rotate90FlipNone:
                    this.posX = entity.posX + entity.SpriteSize[0];
                    this.posY = entity.posY + SpriteSize[1]/2;
                    break;
                case RotateFlipType.Rotate180FlipNone:
                    this.posX = entity.posX + (entity.SpriteSize[0] / 2) -10;
                    this.posY = entity.posY + SpriteSize[1];
                    break;
                case RotateFlipType.Rotate270FlipNone:
                    this.posX = entity.posX - entity.SpriteSize[0];
                    this.posY = entity.posY + SpriteSize[1] / 2;
                    break;                
                default:
                    break;
            }
        }
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
    }
}
