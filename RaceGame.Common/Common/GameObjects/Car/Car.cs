namespace RaceGame.Api.Common.GameObjects.Car
{
    public class Car : MoveGameObject
    {
        // отслеживает количество попорядку пройденных чайтей уровня
        public int[] LevelsSequence;

        // колво правильно пройденных уровней
        public int RightLevelsSequence;

        public bool IsFailingTire { get; set; }

        public float MaxFuel { get; set; }

        public int Cartridges { get; set; }

        public int MaxCartridges { get; set; }

        public bool IsCollizion { get; set; }

        public string PrizeId { get; set; }
    }
}
