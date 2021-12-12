using RaceGame.Api.Common.GameObjects;
using RaceGame.Common.Common.Prize;

namespace RaceGame.Api.Common.Prize.Factory
{
    public class CartridgePrizeFactory : PrizeFactory
    {
        public override GameObject GetObject(float w, float h)
        {
            var obj = new Cartridge();
            obj.SizeX = w;
            obj.SizeY = h;

            return obj;
        }
    }
}
