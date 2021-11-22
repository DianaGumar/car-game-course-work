using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;

namespace RaceGameLibrary
{
    //класс, отвечающий за всю игру
    public class GameEngine
    {
        private const int AMOUNTPRIZES = 10;

        private PrizeFactory prizeFactory;
        private int timeSpawn;
        private int timeDeltaSpawn;
        private int amountPrizes;
        private Random random;



        //хранит в себе все игровые объекты
        private List<GameObject> gameObjects;
        
        private Level Level { get => (Level)gameObjects[0]; }
        private Car Car1 { get => (Car)gameObjects[1]; }
        private Car Car2 { get => (Car)gameObjects[2]; }

        public GameEngine()
        {
            gameObjects = new List<GameObject>();
            timeSpawn = DateTime.Now.Second;
            timeDeltaSpawn = 10;
            amountPrizes = 0;
            random = new Random();
        }
        //метод выполняет добавление начальных объектов
        public void Start(Vector2 sizeScreen)
        {
            Vector2[] positions =
            {
                new Vector2(143,189),
                new Vector2(290, 47),
                new Vector2(536,189),
                new Vector2(772, 47),
                new Vector2(1134,47),

                new Vector2(848,626),
                new Vector2(586,466),
                new Vector2(297,626),
                new Vector2(143,319),
            };
            Vector2[] sizes =
            {
                new Vector2(276,130),
                new Vector2(376,142),
                new Vector2(375,130),
                new Vector2(362,142),
                new Vector2(162,701),

                new Vector2(286,122),
                new Vector2(392,160),
                new Vector2(430,122),
                new Vector2(154,429),
            };
            Sprite sprite = Sprite.LoadSprite("RACE.png");
            gameObjects.Add(new Level(Vector2.Zero, sizeScreen, sprite, 9, positions, sizes));
            sprite = Sprite.LoadSprite("car1.png");
            gameObjects.Add(new Car(new Vector2(150, 190), new Vector2(61, 24), sprite, new Key[] { Key.Up, Key.Down, Key.Right, Key.Left }));
            sprite = Sprite.LoadSprite("car2.png");
            gameObjects.Add(new Car(new Vector2(150, 215), new Vector2(61, 24), sprite, new Key[] { Key.W, Key.S, Key.D, Key.A }));
        }
        // обновляет все игровые объекты
        public void Update()
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.Update();
            }
            Car1.Controlling(Keyboard.GetState());
            Car2.Controlling(Keyboard.GetState());
            Collision.CheckLevelCollision(Level, Car1);
            Collision.CheckLevelCollision(Level, Car2);
            RespawnPrize();
        }

        public void Draw()
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.Draw();
            }
        }



        private void RespawnPrize()
        {
            if(timeSpawn + timeDeltaSpawn < DateTime.Now.Second)
            {
                while (amountPrizes < AMOUNTPRIZES)
                {
                    prizeFactory = GetPrizeFactory(random.Next(3));
                    gameObjects.Add(prizeFactory.GetPrize(new Vector2(random.Next(1420), random.Next(780))));
                    amountPrizes++;
                }
                timeSpawn = DateTime.Now.Second;
            }

        }

        private PrizeFactory GetPrizeFactory(int index)
        {
            switch(index)
            {
                case 0: return new BulletPrizeFactory(20, Sprite.LoadSprite("patron.png"));
                case 1: return new FuelPrizeFactory(20, Sprite.LoadSprite("health.png"));
                case 2: return new TirePrizeFactory(20, Sprite.LoadSprite("shina.png"));
                default: throw new Exception("NET TAKOY FABRIKI, NE TOT INDEX");
            }
        }
    }
}
