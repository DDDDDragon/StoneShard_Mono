using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneShard_Mono.Content.DataStructures;
using StoneShard_Mono.Extensions;

namespace StoneShard_Mono.Content.Components
{
    public class SizeContainer : Container
    {
        public SizeContainer(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public SizeContainer(Vector2 size)
        {
            _width = (int)size.X;
            _height = (int)size.Y;
        }

        public Color BorderColor = Color.Black;

        public UIVec4 BorderWidth = new();

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!_init || !Visible) return;
            if (BackgroundColor != default)
                spriteBatch.DrawRectangle(new((int)Position.X, (int)Position.Y, Width, Height), BackgroundColor * _alpha);

            spriteBatch.DrawRectangle(new((int)Position.X, (int)Position.Y, BorderWidth.X, _height), BorderColor);
            spriteBatch.DrawRectangle(new((int)Position.X, (int)Position.Y, _width, BorderWidth.Y), BorderColor);
            spriteBatch.DrawRectangle(new(_width - BorderWidth.Z + (int)Position.X, (int)Position.Y, BorderWidth.Z, Height), BorderColor);
            spriteBatch.DrawRectangle(new((int)Position.X, _height - BorderWidth.W + (int)Position.Y, _width, BorderWidth.W), BorderColor);

            foreach (var component in Children)
                component.Draw(spriteBatch, gameTime);
        }
    }
}
