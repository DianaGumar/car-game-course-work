using RaceGame.Api.Common.GameObjects;
using RaceGame.Common.Common.Prize;

namespace RaceGame.Api.Common.Prize.Factory
{
    public class TirePrizeFactory : PrizeFactory
    {
        public override GameObject GetObject(float w, float h)
        {
            var tire = new Tire();
            tire.SizeX = w;
            tire.SizeY = h;

            return tire;
        }
    }
}
