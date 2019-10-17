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
        //public static Tank tank = new Tank();
       
        
        private readonly DataBL data = new DataBL();
        //Graphics g;
        //Bitmap bitmap;
        private int MapWidth;
        private int MapHeight;

        DateTime lastTime;
        int gameTime;

       // Kolobok kolobok = new Kolobok();
        //List<Tank> tanks = new List<Tank>(); не работает
        //Tank[] tanks = new Tank[5];
        RotateFlipType kolobokDirection;
        


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



            Map.Paint += new PaintEventHandler(this.Map_Paint);
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
                tanks[i] = data.TankMove(tanks[i]);
                CheckEntityBounds(tanks[i]);
            }
            data.UpdateTanks(tanks);
            Kolobok kolobok = data.GetKolobok();
            Map.Refresh();
            CheckEntityBounds(kolobok);

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
                kolobokDirection = RotateFlipType.RotateNoneFlipNone;
            }
            if (e.KeyChar == 's')
            {
                kolobok.posY += kolobok.Speed;
                kolobokDirection = RotateFlipType.Rotate180FlipNone;
            }
            if (e.KeyChar == 'a')
            {
                kolobok.posX -= kolobok.Speed;
                kolobokDirection = RotateFlipType.Rotate270FlipNone;
            }
            if (e.KeyChar == 'd')
            {
                kolobok.posX += kolobok.Speed;
                kolobokDirection = RotateFlipType.Rotate90FlipNone;
            }
            data.UpdateKolobok(kolobok);

        }

        private void Map_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

            Kolobok kolobok = data.GetKolobok();
            Graphics g = e.Graphics;
            Bitmap sprite = new Bitmap(kolobok.Sprite);


            sprite.RotateFlip(kolobokDirection);
            g.DrawImage(sprite, kolobok.posX, kolobok.posY);

            List<Tank> tanks = (List<Tank>)data.GetTanks();

            foreach (Tank tank in tanks)
            {
                sprite = new Bitmap(tank.Sprite);
                sprite.RotateFlip(tank.TankDirection);
                g.DrawImage(sprite, tank.posX, tank.posY);
            }

        }

        
       

        private void CheckEntityBounds(Entity entity)
        {
            if (entity.posX < 0)
            {
                entity.posX = 0;
            }
            else if (entity.posX > MapWidth - entity.SpriteSize[0])
            {
                entity.posX = MapWidth - entity.SpriteSize[0];
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


    }
}
