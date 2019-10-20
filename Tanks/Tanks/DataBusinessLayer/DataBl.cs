using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataLayer;
using System.Drawing;
using System.Threading;
using System.ComponentModel;

namespace DataBusinessLayer
{
    public class DataBL
    {
        private readonly IData Game;
        private Configuration defaultConf = new Configuration();



        public DataBL()
        {
            Configuration configuration = new Configuration();
            Game = new Data();
        }

        public Configuration Start()
        {
            defaultConf = new Configuration();
            return Game.StartGame(defaultConf);
        }
        #region Kolobok
        public void CreateKolobok(Kolobok kolobok)
        {
            Game.CreateKolobok(kolobok);
        }
        public Kolobok GetKolobok()
        {
            return Game.GetKolobok();
        }

        public void UpdateKolobok(Kolobok kolobok)
        {
            Game.UpdateKolobok(kolobok);
        }
        #endregion Kolobok

        #region Tanks
        public void AddTank(Tank tank)
        {
            Game.AddTank(tank);
        }

        private int[] tankSpawnPosition()
        {
            Entity Tanks = new Entity();
            Random random = new Random();
            int[] position = new int[2];
            defaultConf = new Configuration();
            position[0] = random.Next(10, defaultConf.MapWidth - Tanks.SpriteSize[0]);
            position[1] = random.Next(10, defaultConf.MapHeight - Tanks.SpriteSize[1]);
            return position;
        }

        public void AddTanks(int count)
        {
            for (int i = 0; i < count; i++) //TEST 1
            {
                int[] position = tankSpawnPosition();
                //int[] position = new int[2] {100,100};
                Tank tank = new Tank(position);
                Game.AddTank(tank);
                Thread.Sleep(100);
            }
        }

        public IEnumerable<Tank> GetTanks()
        {
            return Game.GetTanks();
        }
        /*
        public Tank TankMove(Tank tank)
        {
            Random random = new Random();
            Thread.Sleep(10);
            switch (random.Next(4))
            {
                case 0:
                    tank.posY -= tank.Speed;
                    tank.Direction = RotateFlipType.RotateNoneFlipNone;
                    return tank;
                case 1:
                    tank.posY += tank.Speed;
                    tank.Direction = RotateFlipType.Rotate180FlipNone;
                    return tank;
                case 2:
                    tank.posX -= tank.Speed;
                    tank.Direction = RotateFlipType.Rotate270FlipNone;
                    return tank;
                case 3:
                    tank.posX += tank.Speed;
                    tank.Direction = RotateFlipType.Rotate90FlipNone;
                    return tank;
                default:
                    return tank;

            }
        }*/

        public void UpdateTanks(List<Tank> tanks)
        {
            Game.UpdateTanks(tanks);
        }
        #endregion Tanks

        public void Reset()
        {
            Game.Reset();
        }

        public void GameOver()
        {
            // Thread.Sleep(1000000);
        }

        #region Walls
        public IEnumerable<Wall> GetWalls()
        {
            List<Wall> walls = (List<Wall>)Game.GetWalls();
            List<Wall> tempWalls = new List<Wall>(walls);
            foreach (var wall in tempWalls)
            {
                if (wall.Strength<=0)
                {
                    walls.Remove(wall);
                }
            }
            return walls;

        }
        public List<Wall> GenerateWalls()
        {
            return Game.GenerateWalls();
        }
        public IEnumerable<River> GetRivers()
        {
            return Game.GetRivers();
        }

        #endregion Walls

        #region Bullets
        public void AddBullet(Bullet bullet)
        {
            Game.AddBullet(bullet);
        }
        public void RemoveBullet(Bullet bullet)
        {
            Game.RemoveBullet(bullet);
        }
        public IEnumerable<Bullet> GetBullets()
        {
            return Game.GetBullets();
        }
        public void UpdateBullets(List<Bullet> bullets)
        {
            Game.UpdateBullets(bullets);
        }
        //public void BulletMove(Bullet bullet)
        //{

        //}
        #endregion Bullets

        #region Apples

        public Apple AppleSpawn()
        {          
            Random random = new Random();
            //List<Wall> walls = (List<Wall>)Game.GetWalls();
            Thread.Sleep(20);
            int posX = random.Next(defaultConf.MapWidth);
            int posY = random.Next(defaultConf.MapHeight);
            return new Apple(posX, posY);
        }

        public void AddApple()
        {
            Game.AddApple(AppleSpawn());
        }

        public void RemoveApple(Apple apple)
        {
            Game.RemoveApple(apple);
        }

        public IEnumerable<Apple> GetApples()
        {
            return Game.GetApples();
        }

        #endregion Apples

        public BindingList<LogView> UpdateLog()
        {
            BindingList<LogView> logList = new BindingList<LogView>();
            //BindingList<Entity> entities = new BindingList<Entity>();
            //BindingList<Bullet> bullets = (BindingList<Bullet>)Game.GetBullets();
          

            Kolobok kolobok = GetKolobok();
            LogView kolobokLog = new LogView(kolobok);
            logList.Add(kolobokLog);

            List<Tank> tanks = (List<Tank>)Game.GetTanks();
            foreach (Tank tank in tanks)
            {
                LogView tankView = new LogView(tank);
                logList.Add(tankView);
            }
            return logList;
        }

        

    }
}
