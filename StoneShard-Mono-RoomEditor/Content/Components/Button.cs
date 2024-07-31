using StoneShard_Mono_RoomEditor.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using FontStashSharp;
using StoneShard_Mono_RoomEditor.Extensions;

namespace StoneShard_Mono_RoomEditor.Content.Components
{
    public class Button : Component
    {
        public Button(string texID, Vector2 relativePos = default, int scale = 2, EventHandler click = null, string text = "", Color? backgroundColor = null,
            EventHandler<(SpriteBatch spriteBatch, GameTime gameTime)> drawing = null, string hoverID = "", string pressID = "", string fontID = "", int fontSize = 20,
            bool textHorizontalMiddle = true, bool textVerticalMiddle = true, bool drawShadow = true)
        {
            _texture = Main.TextureManager[TexType.UI, texID, scale];
            RelativePosition = relativePos == default ? new(0, 0) : relativePos;
            CanClick = true;
            BackgroundColor = backgroundColor ?? Color.White;
            Text = text;
            _font = fontID == "" ? Main.FontManager["SSFont", fontSize] : Main.FontManager[fontID, fontSize];
            TextHorizontalMiddle = textHorizontalMiddle;
            TextVerticalMiddle = textVerticalMiddle;
            DrawShadow = drawShadow;

            Drawing += drawing ?? ((obj, args) =>
            {
                var destination = new Rectangle(Position.ToPoint() + (Size / 2).ToPoint(), Size.ToPoint());
                if (_isHovering)
                {
                    if (_currentMouse.LeftButton == ButtonState.Pressed)
                        args.spriteBatch.Draw(Press, destination, new(new(0, 0), Size.ToPoint()), Color.White * Alpha, Rotation, Size / 2, SpriteEffects.None, 0);
                    else
                        args.spriteBatch.Draw(Hover, destination, new(new(0, 0), Size.ToPoint()), Color.White * Alpha, Rotation, Size / 2, SpriteEffects.None, 0);
                }
                else
                    args.spriteBatch.Draw(_texture, destination, new(new(0, 0), Size.ToPoint()), Color.White * Alpha, Rotation, Size / 2, SpriteEffects.None, 0);
            });

            OnClick += click != null ? click : (obj, args) => { };

            Hover = hoverID == "" ? _texture : Main.TextureManager[TexType.UI, hoverID, scale];
            Press = pressID == "" ? _texture : Main.TextureManager[TexType.UI, pressID, scale];
        }

        public int Timer;

        public event EventHandler<(SpriteBatch spriteBatch, GameTime gameTime)> Drawing;

        public Texture2D Hover;

        public Texture2D Press;

        public bool TextHorizontalMiddle;
        public bool TextVerticalMiddle;
        public bool DrawShadow;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!_init || !Visible) return;

            if (BackgroundColor != default)
                spriteBatch.DrawRectangle(new((int)Position.X, (int)Position.Y, Width, Height), BackgroundColor * _alpha);

            Drawing?.Invoke(this, (spriteBatch, gameTime));

            if (!string.IsNullOrEmpty(Text))
            {
                var size = _font.MeasureString(Text);
                int x, y = 0;
                if (TextVerticalMiddle)
                    y = (int)(Position.Y + (Height - (int)_font.MeasureString(Text).Y) / 2) - 4;
                x = Rectangle.X;
                if (TextHorizontalMiddle)
                    x += Rectangle.Width / 2 - (int)size.X / 2;

                if (DrawShadow)
                    spriteBatch.DrawString(_font, Text, new(x + 2, y + 2), Color.Black * _alpha);

                spriteBatch.DrawString(_font, Text, new(x, y), BackgroundColor * _alpha);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
