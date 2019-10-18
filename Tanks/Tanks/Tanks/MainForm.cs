using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities;
using DataBusinessLayer;

namespace Tanks
{
    public partial class MainForm : Form
    {

        private readonly DataBL data = new DataBL();
      
        private int MapWidth;
        private int MapHeight;

        DateTime lastTime;
        int gameTime;

        //List<Bullet> bullets = new List<Bullet>();
     
        


        public MainForm()
        {
            InitializeComponent();
        }

        private void Game_Loop()
        {
            DateTime now = DateTime.Now;
            
            var dt = (now - lastTime);

            Update(dt);
            //render();
            
            lastTime = now;                       
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Configuration startConf = data.Start();

            MapWidth = startConf.MapWidth;
            MapHeight = startConf.MapHeight;
            Map.Size = new Size(MapWidth, MapHeight);

            Map.BackColor = Color.Black;

            data.AddTanks(startConf.TanksCount);
            data.GenerateWalls();
            

            Map.Paint += new PaintEventHandler(this.Map_Paint);
            Map.KeyDown += Map_KeyDown;
            this.Controls.Add(Map);
            //tanks = (List<Tank>)data.GetTanks();
            //tanks = (Tank[])data.GetTanks();


            //START 

            lastTime = DateTime.Now;
            timer.Interval = 1000 / 60;
            timer.Start();

        }

        private void Update(TimeSpan dt)
        {
            gameTime = gameTime + dt.Milliseconds;

            List < Tank > tanks = (List<Tank>)data.GetTanks();
            for (int i = 0; i < tanks.Count; i++)
            {
                //Tank temp = data.TankMove(tanks[i]);
                //tanks[i] = temp;
                //tanks[i] = data.TankMove(tanks[i]);
                tanks[i].Move();
                CheckEntityBounds(tanks[i]);
            }
            data.UpdateTanks(tanks);
            Kolobok kolobok = data.GetKolobok();
            List<Bullet> bullets = data.GetBullets();
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Move();
            }
            data.UpdateBullets(bullets);
            
            CheckEntityBounds(kolobok);
            bullets = CheckEntityBounds(bullets);
            data.UpdateBullets(bullets);
            Map.Refresh();
        }

        //void handleInput(DateTime dt)
        //{

        //}

        private void timer_Tick(object sender, EventArgs e)
        {
            Game_Loop();
        }
       

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Kolobok kolobok = data.GetKolobok();
            if (e.KeyChar=='w')
            {
                kolobok.posY -= kolobok.Speed;
                kolobok.Direction = RotateFlipType.RotateNoneFlipNone;
            }
            if (e.KeyChar == 's')
            {
                kolobok.posY += kolobok.Speed;
                kolobok.Direction = RotateFlipType.Rotate180FlipNone;
            }
            if (e.KeyChar == 'a')
            {
                kolobok.posX -= kolobok.Speed;
                kolobok.Direction = RotateFlipType.Rotate270FlipNone;
            }
            if (e.KeyChar == 'd')
            {
                kolobok.posX += kolobok.Speed;
                kolobok.Direction = RotateFlipType.Rotate90FlipNone;
            }
            if (e.KeyChar == ' ')
            {
                data.AddBullet(new Bullet(kolobok));
                
            }
            data.UpdateKolobok(kolobok);

        }


        private void Map_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Map_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            List<Wall> walls = data.GetWalls();
            Bitmap sprite = new Bitmap(walls[0].Sprite);

            foreach (Wall wall in walls)
            {
                g.DrawImage(sprite, wall.posX, wall.posY);
            }

            Kolobok kolobok = data.GetKolobok();
            sprite = new Bitmap(kolobok.Sprite);


            sprite.RotateFlip(kolobok.Direction);
            g.DrawImage(sprite, kolobok.posX, kolobok.posY);

            List<Tank> tanks = (List<Tank>)data.GetTanks();

            foreach (Tank tank in tanks)
            {
                sprite = new Bitmap(tank.Sprite);
                sprite.RotateFlip(tank.Direction);
                g.DrawImage(sprite, tank.posX, tank.posY);
            }

            List<Bullet> bullets = data.GetBullets();
            foreach (Bullet bullet in bullets)
            {
                sprite = new Bitmap(bullet.Sprite);
                sprite.RotateFlip(bullet.Direction);
                g.DrawImage(sprite, bullet.posX, bullet.posY);
            }

        }

        bool collides(int x, int y, int r, int b, int x2, int y2, int r2, int b2)
        {
            return  r <= x2 || x > r2 || b <= y2 || y > b2;
           
        }

        bool boxCollides(int x, int y, int[] spritesize, int x2, int y2, int[] spritesize2)
        {
            return !collides(x, y, x + spritesize[0]-5, y + spritesize[1],
                    x2, y2,
                    x2 + spritesize2[0]-5, y2 + spritesize2[1]);
        }
        bool boxCollides(int x, int y, int[] spritesize, Bullet bullet)
        {
            int x2=bullet.posX;
            int y2=bullet.posY;
            int[] spritesize2 = bullet.SpriteSize;

            if (bullet.Direction == RotateFlipType.Rotate90FlipNone || bullet.Direction == RotateFlipType.Rotate270FlipNone)
            {
                return !collides(x, y, x + spritesize[0] - 5, y + spritesize[1],
                   x2, y2,
                   x2 + spritesize2[1] - 5, y2 + spritesize2[0]);
            }

            return !collides(x, y, x + spritesize[0] - 5, y + spritesize[1],
                    x2, y2,
                    x2 + spritesize2[0] - 5, y2 + spritesize2[1]);
        }


        private void CheckEntityBounds(Entity entity)
        {
            List<Wall> walls = data.GetWalls();

            if (entity.posX < 0)
            {
                entity.posX = 0;
            }
            else if (entity.posX > MapWidth - entity.SpriteSize[0])
            {
                entity.posX = MapWidth - entity.SpriteSize[0];
            }
            foreach (var wall in walls)
            {
                if (boxCollides(wall.posX, wall.posY, wall.SpriteSize, entity.posX, entity.posY, entity.SpriteSize))
                {
                   
                    if (entity.Direction == RotateFlipType.Rotate180FlipNone)
                    {
                        entity.posY -= 10;
                    }
                    else
                    if (entity.Direction == RotateFlipType.RotateNoneFlipNone)
                    {
                        entity.posY += 10;
                    }
                    else
                    if (entity.Direction == RotateFlipType.Rotate270FlipNone)
                    {
                        entity.posX += 10;
                    }else
                    if (entity.Direction == RotateFlipType.Rotate90FlipNone)
                    {
                        entity.posX -= 10;
                    }
                }


            }
           

            if (entity.posY < 0)
            {
                entity.posY = 0;
            }
            else if (entity.posY > MapHeight - entity.SpriteSize[1])
            {
                entity.posY = MapHeight - entity.SpriteSize[1];
            }
        }
        private List<Bullet> CheckEntityBounds(List<Bullet> bullets)
        {
            List<Wall> walls = data.GetWalls();
            List<Bullet> bulletsTemp = new List<Bullet>(bullets);
            List<Tank> tanks = (List<Tank>)data.GetTanks();
            List<Tank> tanksTemp = (List<Tank>)data.GetTanks();

            foreach (Bullet bullet in bullets)
            {
                if (bullet.posX < 0)
                {
                    bulletsTemp.Remove(bullet);
                    break;
                }
                else if (bullet.posX > MapWidth - bullet.SpriteSize[0])
                {
                    bulletsTemp.Remove(bullet);
                    break;
                }

                if (bullet.posY < 0)
                {
                    bulletsTemp.Remove(bullet);
                    break;
                }
                else 
                if ((bullet.posY > MapHeight - bullet.SpriteSize[1]) && 
                    !(bullet.Direction == RotateFlipType.Rotate270FlipNone || bullet.Direction == RotateFlipType.Rotate90FlipNone)) 
                {
                    bulletsTemp.Remove(bullet);
                    break;
                }

                foreach (Wall wall in walls)
                {
                    if (boxCollides(wall.posX, wall.posY, wall.SpriteSize, bullet))
                    {

                        bulletsTemp.Remove(bullet);
                        break;
                    }
                }

                foreach (Tank tank in tanks)
                {
                    if (boxCollides(tank.posX, tank.posY, tank.SpriteSize, bullet))
                    {
                        tanksTemp.Remove(tank);
                        bulletsTemp.Remove(bullet);
                        break;
                    }
                }


            }
            data.UpdateTanks(tanksTemp);
            return bulletsTemp;
        }



    }
}
