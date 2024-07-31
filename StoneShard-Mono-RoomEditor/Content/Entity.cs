using Microsoft.Xna.Framework;
using StoneShard_Mono_RoomEditor.Content.Rooms;

namespace StoneShard_Mono_RoomEditor.Content
{
    public class Entity : GameContent
    {
        public Vector2 DrawOffset;

        public virtual Vector2 Position { get; set; }

        public virtual Vector2 TilePosition { get; set; }

        public virtual Room CurrentRoom { get; set; }

        public float Rotation;

        public float Alpha = 1;

        public bool UseAdditive = false; 
        
        internal int _width;

        internal int _height;

        public virtual int Width => _width;

        public virtual int Height => _height;

        public virtual string ID { get; }

        public virtual Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            }
        }

        public Entity()
        {

        }

        public Entity(Room room)
        {
            CurrentRoom = room;
        }

        public void SetPos(Vector2 pos) => TilePosition = pos;

        public void SetPos(int x, int y) => SetPos(new Vector2(x, y));
    }

    public class EntityData
    {
        public string Name;

        public Vector2 RelativePosition;
    }
}
