using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using StoneShard_Mono_RoomEditor.Content.Rooms;
using System;

namespace StoneShard_Mono_RoomEditor.Content
{
    public class Entity : GameContent
    {
        public Vector2 DrawOffset;

        public virtual Vector2 Position { get; set; }

        public virtual Vector2 TilePosition { get; set; }

        public virtual Rectangle TextureRectangle => Rectangle;

        public virtual Room CurrentRoom { get; set; }

        public float Rotation;

        public float Alpha = 1; 
        
        public string Mod = "";

        public string TexturePath = "";

        public bool UseAdditive = false;

        public bool IsHovering = false;
        
        internal int _width;

        internal int _height; 

        internal MouseState _currentMouse;

        internal MouseState _previousMouse;

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

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRect = new Rectangle(_currentMouse.Position - CurrentRoom.ViewPortPos.ToPoint(), new(1, 1));

            IsHovering = false;

            var viewPortRect = CurrentRoom.ViewPort.Bounds;

            var realpos = TextureRectangle.Location.ToVector2() * CurrentRoom.View.M11 + new Vector2(CurrentRoom.View.M41, CurrentRoom.View.M42);

            var realRect = new Rectangle(realpos.ToPoint(), (TextureRectangle.Size.ToVector2() * CurrentRoom.View.M11).ToPoint());

            if(realRect.Intersects(mouseRect))
                IsHovering = true;

            base.Update(gameTime);
        }
    }

    public class EntityData
    {
        public string Name;

        public string Mod;

        public string TexturePath;

        public string Type;

        public Vector2 Position;

        public Vector2 DrawOffset;
    }
}
