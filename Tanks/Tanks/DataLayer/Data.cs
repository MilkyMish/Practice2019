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
        private List<Apple> Apples;

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
        #endregion Resources
    }
}
