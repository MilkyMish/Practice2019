using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataLayer;

namespace DataBusinessLayer
{
    public class DataBL
    {
        private readonly IData Game;
        private Configuration defaultConf = new Configuration();

        private List<Tank> Tanks;
        private List<Apple> Apples;



        public DataBL()
        {
            Configuration configuration = new Configuration();
            Game = new Data();
        }

        public Configuration Start()
        {
            //Tanks = new 
            return Game.StartGame(defaultConf);
        }

    }
}
