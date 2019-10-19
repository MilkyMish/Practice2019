using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataLayer
{
    public class Data:IData
    {
        #region Config
        public int MapWidth = 530;
        public int MapHeight = 480;

        public int AppleCount = 5;
        public int TanksCount = 5;
        

        public int EntitySpeed = 1;

        public Configuration StartGame(Configuration configuration)
        {
           return configuration;
        }
        #endregion Config

        #region Resources
        private List<Tank> Tanks = new List<Tank>();
        private List<Apple> Apples = new List<Apple>();
        private List<Wall> Walls = new List<Wall>();
        private List<Bullet> Bullets = new List<Bullet>();

        #region Kolobok
        private Kolobok kolobok = new Kolobok();

        public Kolobok GetKolobok()
        {
            return kolobok;
        }
        public void UpdateKolobok(Kolobok _kolobok)
        {
            kolobok = _kolobok;
        }

        public void CreateKolobok(Kolobok _kolobok)
        {
            kolobok = _kolobok;
        }
        #endregion Kolobok

        #region Tanks
        public void AddTank(Tank tank)
        {
            Tanks.Add(tank);
        }

        public IEnumerable<Tank> GetTanks()
        {
            return Tanks;
        }

        public void UpdateTanks(List<Tank> tanks)
        {
            //Tanks.Clear();
            //Tanks = tanks;   
        }

        public void  RemoveTank(int id)
        {

        }
        #endregion Tanks

        #region Walls

        public  List<Wall> GenerateWalls()
        {
            Wall wall = new Wall();
            int WidthCount = (MapWidth / wall.SpriteSize[0]);
            //int HeightCount = (MapHeight / wall.SpriteSize[1]) - 4;
            //int RowCount = MapHeight / kolobok.SpriteSize[1];

            //for (int i = 0; i < RowCount; i++)
            //{
            //    for (int w = 0; w < WidthCount; w++)
            //    {
            //        Random random = new Random();
            //        int check = random.Next(1, 3);
            //        if (check==2)
            //        {
            //            Wall MapWall = new Wall(w * wall.SpriteSize[0], (i+2) * wall.SpriteSize[1]);
            //            Walls.Add(MapWall);                       
            //        }

            //    }
            //}
            int id = 0;
            for (int i = 0; i < WidthCount; i++)
            {
                if (i!=2 && i!=4 && i!=5)
                {
                    Wall MapWall = new Wall(i * wall.SpriteSize[0], 300);
                    MapWall.id = id++;
                    Walls.Add(MapWall);
                }
                else
                {
                    Wall MapWall = new Wall(i * wall.SpriteSize[0], 100);
                    MapWall.id = id++;
                    Walls.Add(MapWall);
                }
                      
            }
            return Walls;

        }
        public IEnumerable<Wall> GetWalls()
        {
            return Walls;
        }

        #endregion Walls

        #region Bullets
        public IEnumerable<Bullet> GetBullets()
        {
            return Bullets;
        }

        public void AddBullet(Bullet bullet)
        {
            Bullets.Add(bullet);
        }

        public void RemoveBullet(Bullet bullet)
        {
            Bullets.Remove(bullet);
        }

        public void UpdateBullets(List<Bullet> bullets)
        {
            Bullets = bullets;
        }

        #endregion Bullets

        #region Apples

        public IEnumerable<Apple> GetApples()
        {
            return Apples;
        }

        public void AddApple(Apple apple)
        {
            Apples.Add(apple);
        }

        public void RemoveApple(Apple apple)
        {
            Apples.Remove(apple);
        }

        public void UpdateApples(List<Apple> apples)
        {
            Apples = apples;
        }

        #endregion Apples


        public void Reset()
        {
            Tanks = new List<Tank>();
            Walls = new List<Wall>();
            Bullets = new List<Bullet>();
            Apples = new List<Apple>();
        Kolobok kolobok = new Kolobok();

        }
    }

        #endregion Resources
    
}
