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
        LogForm logForm;
        DateTime lastTime;
        int gameTime;
        int AppleCounter;
        Label lbl_AppleCounter;
        List<Explosion> Explosions;
        int KolobokAnimation = 1;
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
        private void Reset()
        {
            Configuration startConf = data.Start();

            data.Reset();

            Map.BackColor = Color.Black;
            
            //data.AddTanks(startConf.TanksCount);
            //data.GenerateWalls();           
            for (int i = 0; i < startConf.AppleCount; i++)
            {
                data.AddApple();
            }

            Map.Paint += new PaintEventHandler(this.Map_Paint);
           
            this.Controls.Add(Map);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Configuration startConf = data.Start();
            MapWidth = startConf.MapWidth;
            MapHeight = startConf.MapHeight;
            Map.Size = new Size(MapWidth, MapHeight);
            Map.BackColor = Color.Black;
            Reset();
            Explosions = new List<Explosion>();
            lastTime = DateTime.Now;

            logForm = new LogForm(data)
            {
                StartPosition = FormStartPosition.Manual,
                Left = ClientSize.Width,
                Top = 0,
                
            }; 

            logForm.Show();

            Bitmap spriteTemp = new Bitmap(@"..\..\..\img\Apple.png");

            PictureBox applePic = new PictureBox();
            applePic.Location = new Point(10, 40);
            applePic.Size = new Size(40, 40);
            applePic.Image = new Bitmap(spriteTemp, new Size(40, 40));
            this.Controls.Add(applePic);

            lbl_AppleCounter = new Label();
            lbl_AppleCounter.Location = new Point(60,50);           
            lbl_AppleCounter.Text = AppleCounter.ToString();
            this.Controls.Add(lbl_AppleCounter);

          /*  Button btn_NewGame = new Button();
            btn_NewGame.Text = "new game";
            btn_NewGame.Click += btn_newGame_Click;
            btn_NewGame.Location = new Point(10, 100);
            this.Controls.Add(btn_NewGame);*/

            //this.KeyPress += MainForm_KeyPress;
            
            Refresh();

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
                if (tanks[i].fire)
                {
                    data.AddBullet(new Bullet(tanks[i], true));
                }
                CheckEntityBounds(tanks[i]);
            }
            data.UpdateTanks(tanks);

            Kolobok kolobok = data.GetKolobok();

            List<Bullet> bullets = (List<Bullet>)data.GetBullets();
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Move();
            }
            data.UpdateBullets(bullets);
            
            CheckEntityBounds(kolobok);

            bullets = CheckEntityBounds(bullets);
            data.UpdateBullets(bullets);

            List<Apple> apples = (List<Apple>)data.GetApples();
            CheckEntityBounds(apples);

            logForm.RefreshLog();
          
            Map.Refresh();
            
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            Game_Loop();
        }
       

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Kolobok kolobok = data.GetKolobok();
            if (e.KeyChar=='w')
            {
                for (int i = 0; i < kolobok.Speed; i++)
                {
                    kolobok.posY -= 1;
                }
                kolobok.Direction = RotateFlipType.RotateNoneFlipNone;
                kolobok = KolobokAnimationCheck(kolobok);


            }
            if (e.KeyChar == 's')
            {
                for (int i = 0; i < kolobok.Speed; i++)
                {
                    kolobok.posY += 1;
                }
                kolobok.Direction = RotateFlipType.Rotate180FlipNone;
                kolobok = KolobokAnimationCheck(kolobok);
            }
            if (e.KeyChar == 'a')
            {
                for (int i = 0; i < kolobok.Speed; i++)
                {
                    kolobok.posX -= 1;
                }
                
                kolobok.Direction = RotateFlipType.Rotate270FlipNone;
                kolobok = KolobokAnimationCheck(kolobok);
            }
            if (e.KeyChar == 'd')
            {
                for (int i = 0; i < kolobok.Speed; i++)
                {
                    kolobok.posX += 1;
                }
                kolobok.Direction = RotateFlipType.Rotate90FlipNone;
                kolobok = KolobokAnimationCheck(kolobok);
            }
            if (e.KeyChar == ' ')
            {
                data.AddBullet(new Bullet(kolobok, false));
                
            }
            data.UpdateKolobok(kolobok);

        }

        private Kolobok KolobokAnimationCheck(Kolobok kolobok)
        {
            if (KolobokAnimation==2)
            {
                KolobokAnimation = 0;
                if (kolobok.CurrentPic!=6)
                {
                    kolobok.CurrentPic++;
                    return kolobok;
                }
                else
                {
                    kolobok.CurrentPic = 0;
                    return kolobok;
                }
            }
            else
            {
                KolobokAnimation++;
                return kolobok;
            }
        }

        #region Drawing

        private void Map_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            List<Wall> walls = (List<Wall>)data.GetWalls();
            Bitmap sprite;
            if (walls.Count!=0)
            {
                sprite = new Bitmap(walls[0].Sprite);
                foreach (Wall wall in walls)
                {
                    g.DrawImage(sprite, wall.posX, wall.posY);
                }
            }
            

            Kolobok kolobok = data.GetKolobok();
            sprite = new Bitmap(kolobok.Sprite[kolobok.CurrentPic]);


            sprite.RotateFlip(kolobok.Direction);
            g.DrawImage(sprite, kolobok.posX, kolobok.posY);

            List<Tank> tanks = (List<Tank>)data.GetTanks();

            foreach (Tank tank in tanks)
            {
                sprite = new Bitmap(tank.Sprite);
                sprite.RotateFlip(tank.Direction);
                g.DrawImage(sprite, tank.posX, tank.posY);
            }

            List<River> rivers = (List<River>)data.GetRivers();
            foreach (River river in rivers)
            {
                Bitmap spriteTemp = new Bitmap(river.Sprite);
                sprite = new Bitmap(spriteTemp, new Size(river.SpriteSize[0], river.SpriteSize[1]));

                g.DrawImage(sprite, river.posX, river.posY);
            }

            List<Bullet> bullets = (List<Bullet>)data.GetBullets();
            foreach (Bullet bullet in bullets)
            {
                Bitmap spriteTemp = new Bitmap(bullet.Sprite);
                sprite = new Bitmap(spriteTemp, new Size(bullet.SpriteSize[0], bullet.SpriteSize[1]));
                sprite.RotateFlip(bullet.Direction);
                g.DrawImage(sprite, bullet.posX, bullet.posY);
            }
          
            List<Apple> apples = (List<Apple>)data.GetApples();         
            foreach (Apple apple in apples)
            {
                Bitmap spriteTemp = new Bitmap(apple.Sprite);
                sprite = new Bitmap(spriteTemp, new Size(apple.SpriteSize[0], apple.SpriteSize[1]));
                
                g.DrawImage(sprite, apple.posX, apple.posY);
            }

            List<Explosion> explosions = new List<Explosion>(Explosions);
            foreach (Explosion explosion in explosions)
            {

                    Bitmap spriteTemp = new Bitmap(explosion.Sprites[explosion.curentpic++]);
                    sprite = new Bitmap(spriteTemp, new Size(explosion.SpriteSize[0], explosion.SpriteSize[1]));
                    g.DrawImage(sprite, explosion.posX, explosion.posY);
                if (explosion.curentpic>=3)
                {
                    Explosions.Remove(explosion);
                }
               
            }


        }

        #endregion Drawing

        #region Collisions
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
        bool boxCollides(Wall wall, int x2, int y2, int[] spritesize2)
        {
            return !collides(wall.posX, wall.posY+15, wall.posX + wall.SpriteSize[0] - 5, wall.posY + wall.SpriteSize[1]-15,
                    x2, y2,
                    x2 + spritesize2[0] - 5, y2 + spritesize2[1]);
        }

        private void CheckEntityBounds(Entity entity)
        {
            List<Wall> walls = (List<Wall>)data.GetWalls();
            List<Tank> tanks = (List < Tank > )data.GetTanks();
            List<River> rivers = (List<River>)data.GetRivers();

            if (entity.posX < 0)
            {
                entity.posX = 0;
            }
            else if (entity.posX > MapWidth - entity.SpriteSize[0])
            {
                entity.posX = MapWidth - entity.SpriteSize[0];
            }
            foreach (Wall wall in walls)
            {
                if (boxCollides(wall, entity.posX, entity.posY, entity.SpriteSize))
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
            foreach (River river in rivers)
            {
                if (boxCollides(river.posX,river.posY,river.SpriteSize, entity.posX, entity.posY, entity.SpriteSize))
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
                    }
                    else
                    if (entity.Direction == RotateFlipType.Rotate90FlipNone)
                    {
                        entity.posX -= 10;
                    }
                }
            }
            foreach (Tank tank in tanks)
            {
                if ((tank.posX!=entity.posX || tank.posY!= entity.posY) && boxCollides(tank.posX, tank.posY, tank.SpriteSize, entity.posX, entity.posY, entity.SpriteSize))
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
                    }
                    else
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
            List<Wall> walls = (List<Wall>)data.GetWalls();
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
                        wall.Strength--;
                        bulletsTemp.Remove(bullet);
                        Explosions.Add(new Explosion() { posX = bullet.posX, posY = bullet.posY });
                        break;
                    }
                }

                if (bullet.enemyFire == false)
                {
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
                else
                {
                    Kolobok kolobok = data.GetKolobok();
                    if ((boxCollides(kolobok.posX, kolobok.posY, kolobok.SpriteSize, bullet)))
                    {

                        timer.Stop();
                    }
                }
            }
            data.UpdateTanks(tanksTemp);
            return bulletsTemp;
        }
        private void CheckEntityBounds(List<Apple> apples)
        {
            Kolobok kolobok = data.GetKolobok();
            List<Wall> walls = (List<Wall>)data.GetWalls();
            List<River> rivers = (List<River>)data.GetRivers();
            List<Apple> applesTemp = new List<Apple>(apples);
            foreach (Apple apple in applesTemp)
            {
                if (boxCollides(kolobok.posX, kolobok.posY, kolobok.SpriteSize, apple.posX, apple.posY, apple.SpriteSize))
                {
                    data.RemoveApple(apple);
                    lbl_AppleCounter.Text = (++AppleCounter).ToString();
                    data.AddApple();
                }
                else
                {
                    foreach (Wall wall in walls)
                    {
                        if (boxCollides(wall.posX, wall.posY, wall.SpriteSize, apple.posX, apple.posY, apple.SpriteSize))
                        {
                            apple.posY -= 45;

                        }
                    }
                    foreach (River river in rivers)
                    {
                        if (boxCollides(river.posX, river.posY, river.SpriteSize, apple.posX, apple.posY, apple.SpriteSize))
                        {
                            apple.posY -= 45;

                        }
                    }
                }
            }
           

            

        }

        #endregion Collisions
        private void btn_newGame_Click(object sender, EventArgs e)
        {

            //Map.Focus();
            //Focus();
            timer.Start();

        }

     
    }
}
