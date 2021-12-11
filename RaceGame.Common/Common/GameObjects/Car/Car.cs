namespace RaceGame.Api.Common.GameObjects.Car
{
    public class Car : MoveGameObject
    {
        // отслеживает количество попорядку пройденных чайтей уровня
        public int[] LevelsSequence;

        // колво правильно пройденных уровней
        public int RightLevelsSequence;
        public int Fuel { get; set; }
        public bool IsFailingTire { get; set; }
        public int Cartridges { get; set; }

        public bool IsCollizion { get; set; }
    }
}
