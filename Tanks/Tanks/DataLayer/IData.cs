using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataLayer
{
    public interface IData
    {
        Configuration StartGame(Configuration configuration);


        void CreateKolobok(Kolobok _kolobok);
        Kolobok GetKolobok();
        void UpdateKolobok(Kolobok _kolobok);

        void AddTank(Tank tank);
        void UpdateTanks(List<Tank> tanks);
        IEnumerable<Tank> GetTanks();

        List<Wall> GenerateWalls();
        IEnumerable<Wall> GetWalls();

        IEnumerable<Bullet> GetBullets();
        void AddBullet(Bullet bullet);
        void RemoveBullet(Bullet bullet);
        void UpdateBullets(List<Bullet> bullets);

        IEnumerable<Apple> GetApples();
        void AddApple(Apple apple);
        void RemoveApple(Apple apple);
        void UpdateApples(List<Apple> apples);


        void Reset();
    }
}
