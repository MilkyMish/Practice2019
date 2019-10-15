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
        public int MapWidth = 530;
        public int MapHeight = 480;

        public int AppleCount = 5;
        public int TanksCount = 5;

        public int EntitySpeed = 1;

        public Configuration StartGame(Configuration configuration)
        {
           return configuration;
        }
    }
}
