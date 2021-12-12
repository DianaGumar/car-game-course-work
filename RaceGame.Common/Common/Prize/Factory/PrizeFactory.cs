using RaceGame.Api.Common.GameObjects;

namespace RaceGame.Api.Common.Prize.Factory
{
    public abstract class PrizeFactory
    {
        // фабричный метод
        public abstract GameObject GetObject(float w, float h);
    }
}
