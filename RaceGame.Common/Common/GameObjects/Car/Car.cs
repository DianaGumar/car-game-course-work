using System.Collections.Generic;

namespace RaceGame.Api.Common.GameObjects.Car
{
    public class Car : MoveGameObject
    {
        // отслеживает количество попорядку пройденных чайтей уровня
        public virtual bool[] LevelsSequence { get; set; }

        // колво правильно пройденных уровней
        public virtual int RightLevelsSequence { get; set; }

        public virtual bool IsFailingTire { get; set; }

        public virtual float MaxFuel { get; set; }

        public virtual int Cartridges { get; set; }

        public virtual bool Tire { get; set; }

        public virtual int MaxCartridges { get; set; }

        public virtual bool IsCollizion { get; set; }

        public virtual string PrizeId { get; set; }
    }
}
