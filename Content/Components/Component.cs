using FontStashSharp;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using StoneShard_Mono.Extensions;
using StoneShard_Mono.Content.Animations;

namespace StoneShard_Mono.Content.Components
{
    public class Component : GameContent
    {
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!Visible)
                return;
            if (BackgroundColor != default)
                spriteBatch.DrawRectangle(new((int)Position.X, (int)Position.Y, Width, Height), BackgroundColor * _alpha);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = Rectangle.X + Rectangle.Width / 2 - _font.MeasureString(Text).X / 2;
                var y = Rectangle.Y + Rectangle.Height / 2 - _font.MeasureString(Text).Y / 2;

                spriteBatch.DrawString(_font, Text, new(x, y), BackgroundColor * _alpha);
            }
        }

        internal void DrawBorder(SpriteBatch spriteBatch, Color? borderColor = null, bool drawOri = false)
        {
            Color[] data = new Color[_texture.Width * _texture.Height];
            _texture.GetData(data);
            data = (from c in data select c.A == 0 ? c : Color.White).ToArray();
            Texture2D t = new Texture2D(spriteBatch.GraphicsDevice, _texture.Width, _texture.Height);
            t.SetData(data);

            Color borderC = borderColor == null ? Color.White : (Color)borderColor;

            spriteBatch.Draw(t, Position - new Vector2(0, 2), borderC);
            spriteBatch.Draw(t, Position - new Vector2(2, 0), borderC);
            spriteBatch.Draw(t, Position + new Vector2(0, 2), borderC);
            spriteBatch.Draw(t, Position + new Vector2(2, 0), borderC);
            if (!drawOri) spriteBatch.Draw(t, Position, Color.White);
            else spriteBatch.Draw(_texture, Position, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRect = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRect.Intersects(Rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed && CanClick)
                {
                    Clicked = true;
                    Click?.Invoke(this, new EventArgs());
                }
            }

            CurrentAnimation?.Update(gameTime);

            if (!_init) _init = true;

            if (HorizontalMiddle) RelativePosition.X = (Parent.Width - Width) / 2;
            if (VerticalMiddle) RelativePosition.Y = (Parent.Height - Height) / 2;

            DrawOffset = new(0, 0);
        }

        internal float _alpha = 1;

        internal MouseState _currentMouse;

        internal SpriteFontBase _font;

        internal bool _isHovering;

        internal MouseState _previousMouse;

        internal Texture2D _texture;

        internal bool _init = false;

        public event EventHandler Click;

        public float Scale;

        public Vector2 Size => new(Width, Height);

        public virtual int Height => _texture.Height;

        public virtual int Width => _texture.Width;

        public bool Clicked { get; internal set; }

        public Color BackgroundColor;

        public Vector2 Position = new(0, 0);

        public Vector2 RelativePosition = new(0, 0);

        public float Rotation;

        public Animation CurrentAnimation = Animation.Empty;

        public bool shouldCollect = false;

        public Vector2 DrawOffset = new(0, 0);

        public float DrawScale = 1f;

        public Container Parent;

        public bool CanClick = true;

        public string id = "";

        public bool HorizontalMiddle;
        public bool VerticalMiddle;

        public virtual bool Visible { get; set; } = true;

        public virtual Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            }
        }

        public virtual float Alpha
        {
            get => _alpha;
            set => _alpha = value;
        }

        public string Text { get; set; }
    }
}
