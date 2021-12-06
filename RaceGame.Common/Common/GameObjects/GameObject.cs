using System;

namespace RaceGame.Api.Common.GameObjects
{
    public class GameObject
    {
        public string Id { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float SizeX { get; set; }
        public float SizeY { get; set; }
        public float Angle { get; set; }
        public GameObjectType Name { get; set; }

        public int SpriteId { get; set; }
        public float SpriteSizeX { get; set; }
        public float SpriteSizeY { get; set; }
    }
}
