using RaceGame.Common.Common;

namespace RaceGame.Api.Common.GameObjects
{
    public class GameObject : Point
    {
        public virtual string Id { get; set; }
        public virtual float SizeX { get; set; }
        public virtual float SizeY { get; set; }
        public virtual float Angle { get; set; }
        public virtual GameObjectType Name { get; set; }
        public virtual int SpriteId { get; set; }
        public virtual float SpriteSizeX { get; set; }
        public virtual float SpriteSizeY { get; set; }
        public virtual bool IsDeactivate { get; set; }
    }
}
