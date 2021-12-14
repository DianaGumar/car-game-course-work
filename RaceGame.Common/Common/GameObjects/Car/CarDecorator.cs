using System.Collections.Generic;

namespace RaceGame.Api.Common.GameObjects.Car
{
    public abstract class CarDecorator : Car
    {
        //декорируемая машина
        protected Car car;
        //выполнил ли декоратор свою работу
        public bool IsDecorate { get; protected set; }

        public CarDecorator(Car car) : base()
        {
            this.car = car;
            IsDecorate = true;
        }

        //переопределяем все публичные методы и свойства на декорируемую машину
        public override bool[] LevelsSequence { get => car.LevelsSequence; set => car.LevelsSequence = value; }
        public override int RightLevelsSequence { get => car.RightLevelsSequence; set => car.RightLevelsSequence = value; }
        public override bool IsFailingTire { get => car.IsFailingTire; set => car.IsFailingTire = value; }
        public override float MaxFuel { get => car.MaxFuel; set => car.MaxFuel = value; }
        public override int Cartridges { get => car.Cartridges; set => car.Cartridges = value; }
        public override int MaxCartridges { get => car.MaxCartridges; set => car.MaxCartridges = value; }
        public override bool IsCollizion { get => car.IsCollizion; set => car.IsCollizion = value; }
        public override string PrizeId { get => car.PrizeId; set => car.PrizeId = value; }
        public override bool Tire { get => car.Tire; set => car.Tire = value; }

        public override float Speed { get => car.Speed; set => car.Speed = value; }
        public override float MaxSpeed { get => car.MaxSpeed; set => car.MaxSpeed = value; }
        public override float Fuel { get => car.Fuel; set => car.Fuel = value; }
        public override float SpeedChange { get => car.SpeedChange; set => car.SpeedChange = value; }
        public override float OldPositionX { get => car.OldPositionX; set => car.OldPositionX = value; }
        public override float OldPositionY { get => car.OldPositionY; set => car.OldPositionY = value; }

        public override string Id { get => car.Id; set => car.Id = value; }
        public override float SizeX { get => car.SizeX; set => car.SizeX = value; }
        public override float SizeY { get => car.SizeY; set => car.SizeY = value; }
        public override float Angle { get => car.Angle; set => car.Angle = value; }
        public override GameObjectType Name { get => car.Name; set => car.Name = value; }
        public override int SpriteId { get => car.SpriteId; set => car.SpriteId = value; }
        public override float SpriteSizeX { get => car.SpriteSizeX; set => car.SpriteSizeX = value; }
        public override float SpriteSizeY { get => car.SpriteSizeY; set => car.SpriteSizeY = value; }
        public override bool IsDeactivate { get => car.IsDeactivate; set => car.IsDeactivate = value; }

        public override float PositionX { get => car.PositionX; set => car.PositionX = value; }
        public override float PositionY { get => car.PositionY; set => car.PositionY = value; }

        //вернуть машину
        public Car GetCar()
        {
            return car;
        }
    }
}
