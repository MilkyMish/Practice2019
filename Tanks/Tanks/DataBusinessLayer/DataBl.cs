using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataLayer;
using System.Drawing;
using System.Threading;

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

        public Tank TankMove(Tank tank)
        {
            Random random = new Random();
            Thread.Sleep(10);
            switch (random.Next(4))
            {
                case 0:
                    tank.posY -= tank.Speed;
                    tank.TankDirection = RotateFlipType.RotateNoneFlipNone;
                    return tank;
                case 1:
                    tank.posY += tank.Speed;
                    tank.TankDirection = RotateFlipType.Rotate180FlipNone;
                    return tank;
                case 2:
                    tank.posX -= tank.Speed;
                    tank.TankDirection = RotateFlipType.Rotate270FlipNone;
                    return tank;
                case 3:
                    tank.posX += tank.Speed;
                    tank.TankDirection = RotateFlipType.Rotate90FlipNone;
                    return tank;
                default:
                    return tank;

            }
        }

        public void UpdateTanks(List<Tank> tanks)
        {
            Game.UpdateTanks(tanks);
        }
    }
}
