using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities
{
    public class Configuration
    {
        public int MapWidth;
        public int MapHeight;

        public int AppleCount;
        public int TanksCount;

        public int EntitySpeed;

        public Configuration()
        {
          MapWidth = 530;
          MapHeight = 480;

          AppleCount = 5;
          TanksCount = 5;

          EntitySpeed = 1;
        }
        public Configuration(int width, int height, int applecount, int tankscount, int speed)
        {
            MapWidth = width;
            MapHeight = height;

            AppleCount = applecount;
            TanksCount = tankscount;

            EntitySpeed = speed;
        }
    }
}
