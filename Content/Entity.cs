using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using StoneShard_Mono.Extensions;
using StoneShard_Mono.Content.Rooms;
using StoneShard_Mono.Content.Animations;
using StoneShard_Mono.Content;
using StoneShard_Mono.Managers;

namespace StoneShard_Mono
{
    [Serializable]
    public abstract class Entity : GameContent
    {
        public List<Vector2> CurrentPath;

        public virtual Vector2 Position { get; set; }

        public virtual Vector2 TilePosition { get; set; }

        public virtual Vector2 LastTilePos { get; set; }
        
        public virtual Vector2 TargetTilePos { get; set; }

        public Vector2 DrawOffset;

        public virtual Room CurrentRoom { get; set; }

        public float Rotation;

        public float Alpha = 1;

        internal bool _isHovering;

        public bool CanClick = true;

        public bool IsMove = false;

        public bool Clicked { get; internal set; }

        internal int _width;

        internal int _height;

        public int Direction;

        public virtual int Width => _width;

        public virtual int Height => _height;

        public virtual string ID { get; }

        public Animation CurrentAnimation = Animation.Empty;

        internal MouseState _currentMouse;

        internal MouseState _previousMouse;

        public virtual Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            }
        }

        public event EventHandler OnClick;

        public Entity()
        {

        }

        public Entity(Room room)
        {
            CurrentRoom = room;
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRect = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            if (CurrentAnimation != null && CurrentAnimation.MaxTime == 0) CurrentAnimation = Animation.Empty;
            CurrentAnimation?.Update(gameTime);

            _isHovering = false;

            if (mouseRect.Intersects(Rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed && CanClick)
                {
                    Clicked = true;
                    OnClick?.Invoke(this, new EventArgs());
                }
            }
        }

        public bool PlayAnimation(Animation animation)
        {
            if (CurrentAnimation.MaxTime != 0)
                return false;

            CurrentAnimation = animation;
            CurrentAnimation.Target = this;
            return true;
        }
    }
}
